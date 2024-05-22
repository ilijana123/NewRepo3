using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ViewModels;
namespace WebApplication1.Controllers
{
    public class CasovisController : Controller
    {
        private readonly WebApplication1Context _context;

        public CasovisController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: Casovis
        public async Task<IActionResult> Index()
        {
            var webApplication1Context = _context.Casovi.Include(c => c.Predmet).Include(s=>s.Studenti).ThenInclude(s=>s.Student);
            return View(await webApplication1Context.ToListAsync());
        }

        // GET: Casovis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casovi = await _context.Casovi.Include(c => c.Predmet).Include(s => s.Studenti).ThenInclude(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (casovi == null)
            {
                return NotFound();
            }

            return View(casovi);
        }

        // GET: Casovis/Create
        public IActionResult Create()
        {
            var students = _context.Student.AsEnumerable();
            students = students.OrderBy(s => s.ImePrezime);
            CEditViewModel viewModel = new CEditViewModel
            {
                Casovi = new Casovi(),
                StudentList = new MultiSelectList(students, "Id", "ImePrezime"),
                SelectedStudenti = new List<int>()
            };
            ViewData["PredmetId"] = new SelectList(_context.Set<Predmet>(), "Id", "ImePredmet");
            return View(viewModel);
        }

        // POST: Casovis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Casovi);
                await _context.SaveChangesAsync();
                if (viewModel.SelectedStudenti != null && viewModel.SelectedStudenti.Any())
                {
                    foreach (var studentId in viewModel.SelectedStudenti)
                    {
                        _context.Prisustvo.Add(new Prisustvo { CasoviId = viewModel.Casovi.Id, StudentId = studentId });
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PredmetId"] = new SelectList(_context.Set<Predmet>(), "Id", "ImePredmet", viewModel.Casovi.PredmetId);
            var students = _context.Student.AsEnumerable();
            students = students.OrderBy(s => s.ImePrezime);
            viewModel.StudentList = new MultiSelectList(students, "Id", "ImePrezime");
            return View(viewModel);
        }
        // GET: Casovis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casovi = await _context.Casovi
                .Include(s => s.Studenti)
                .ThenInclude(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (casovi == null)
            {
                return NotFound();
            }

            var students = _context.Student.AsEnumerable();
            students = students.OrderBy(s => s.ImePrezime);

            var viewModel = new CEditViewModel
            {
                Casovi = casovi,
                StudentList = new MultiSelectList(students, "Id", "ImePrezime"),
                SelectedStudenti = casovi.Studenti.Select(s => s.StudentId)
            };

            ViewData["PredmetId"] = new SelectList(_context.Set<Predmet>(), "Id", "ImePredmet", casovi.PredmetId);
            return View(viewModel);
        }

        // POST: Casovis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CEditViewModel viewmodel)
        {
            if (id != viewmodel.Casovi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Casovi);
                    await _context.SaveChangesAsync();
                    IEnumerable<int> newStudentList = viewmodel.SelectedStudenti;
                    IEnumerable<int> prevStudentList = _context.Prisustvo.Where(s => s.CasoviId == id).Select(s => s.StudentId);
                    IQueryable<Prisustvo> toBeRemoved = _context.Prisustvo.Where(s => s.CasoviId == id);
                    if (newStudentList != null)
                    {
                        toBeRemoved = toBeRemoved.Where(s => !newStudentList.Contains(s.StudentId));
                        foreach (int studentId in newStudentList)
                        {
                            if (!prevStudentList.Any(s => s == studentId))
                            {
                                _context.Prisustvo.Add(new Prisustvo { StudentId =studentId, CasoviId = id });
                            }
                        }
                    }
                    _context.Prisustvo.RemoveRange(toBeRemoved);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CasoviExists(viewmodel.Casovi.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PredmetId"] = new SelectList(_context.Set<Predmet>(), "Id", "ImePredmet", viewmodel.Casovi.PredmetId);
            return View(viewmodel);
        }

        // GET: Casovis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casovi = await _context.Casovi
                .Include(c => c.Predmet)
                .Include(c=>c.Studenti)
                .ThenInclude(c=>c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (casovi == null)
            {
                return NotFound();
            }

            return View(casovi);
        }

        // POST: Casovis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var casovi = await _context.Casovi.FindAsync(id);
            if (casovi != null)
            {
                _context.Casovi.Remove(casovi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CasoviExists(int id)
        {
            return _context.Casovi.Any(e => e.Id == id);
        }
    }
}
