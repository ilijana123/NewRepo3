using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PrisustvoesController : Controller
    {
        private readonly WebApplication1Context _context;

        public PrisustvoesController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: Prisustvoes
        public async Task<IActionResult> Index()
        {
            var webApplication1Context = _context.Prisustvo.Include(p => p.Casovi).Include(p => p.Student);
            return View(await webApplication1Context.ToListAsync());
        }

        // GET: Prisustvoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prisustvo = await _context.Prisustvo
                .Include(p => p.Casovi)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prisustvo == null)
            {
                return NotFound();
            }

            return View(prisustvo);
        }

        // GET: Prisustvoes/Create
        public IActionResult Create()
        {
            ViewData["CasoviId"] = new SelectList(_context.Casovi, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "ImePrezime");
            return View();
        }

        // POST: Prisustvoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CasoviId")] Prisustvo prisustvo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prisustvo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CasoviId"] = new SelectList(_context.Casovi, "Id", "Id", prisustvo.CasoviId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "ImePrezime", prisustvo.StudentId);
            return View(prisustvo);
        }

        // GET: Prisustvoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prisustvo = await _context.Prisustvo.FindAsync(id);
            if (prisustvo == null)
            {
                return NotFound();
            }
            ViewData["CasoviId"] = new SelectList(_context.Casovi, "Id", "Id", prisustvo.CasoviId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "ImePrezime", prisustvo.StudentId);
            return View(prisustvo);
        }

        // POST: Prisustvoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CasoviId")] Prisustvo prisustvo)
        {
            if (id != prisustvo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prisustvo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrisustvoExists(prisustvo.Id))
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
            ViewData["CasoviId"] = new SelectList(_context.Casovi, "Id", "Id", prisustvo.CasoviId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "ImePrezime", prisustvo.StudentId);
            return View(prisustvo);
        }

        // GET: Prisustvoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prisustvo = await _context.Prisustvo
                .Include(p => p.Casovi)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prisustvo == null)
            {
                return NotFound();
            }

            return View(prisustvo);
        }

        // POST: Prisustvoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prisustvo = await _context.Prisustvo.FindAsync(id);
            if (prisustvo != null)
            {
                _context.Prisustvo.Remove(prisustvo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrisustvoExists(int id)
        {
            return _context.Prisustvo.Any(e => e.Id == id);
        }
    }
}
