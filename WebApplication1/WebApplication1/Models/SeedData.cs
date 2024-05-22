using Microsoft.EntityFrameworkCore;
using System.IO;
using WebApplication1.Models;
using WebApplication1.Data;
namespace WebApplication1.Models
{
    public class SeedData
    {
                       public static void Initialize(IServiceProvider serviceProvider)
                       {
                           using (var context = new WebApplication1Context(
                            serviceProvider.GetRequiredService<
                            DbContextOptions<WebApplication1Context>>()))
                           {
                // Look for any movies.
                               if (context.Predmet.Any() || context.Casovi.Any() || context.Student.Any() )
                               {
                                   return; // DB has been seeded
                               }
                               context.Predmet.AddRange(
                                new Predmet{ /*Id = 1, */ImePredmet = "Rob", Programa = "Reiner"},
                                new Predmet { /*Id = 2, */ImePredmet = "Ivan", Programa = "Reitman"},
                                new Predmet { /*Id = 3, */ImePredmet = "Howard", Programa = "Hawks" }
                            );
                                context.SaveChanges();
                                context.Student.AddRange(
                                new Student { /*Id = 1, */Indeks = "Billy", ImePrezime = "Crystal"},
                                new Student { /*Id = 2, */Indeks = "Meg", ImePrezime = "Ryan" },
                                new Student { /*Id = 3, */Indeks = "Carrie", ImePrezime = "Fisher"},
                                new Student { /*Id = 4, */Indeks = "Bill", ImePrezime = "Murray" },
                                new Student { /*Id = 5, */Indeks = "Dan", ImePrezime = "Aykroyd" },
                                new Student { /*Id = 6, */Indeks = "Sigourney", ImePrezime = "Weaver"},
                                new Student { /*Id = 7, */Indeks = "John", ImePrezime = "Wayne" },
                                new Student { /*Id = 8, */Indeks = "Dean", ImePrezime = "Martin" }
                            );
                               context.SaveChanges();
                               context.Casovi.AddRange(
                                new Casovi
                                {
                                    //Id = 1,
                                    Naslov = "When Harry Met Sally",
                                    Datum = DateTime.Parse("1989-2-12"),
                                    BrojCasovi = 5,
                                    TipCasovi = 3,
                                    Opis = "Romantic Comedy",
                                    PredmetId = context.Predmet.Single(d => d.ImePredmet == "Rob" ).Id
                                },
                                new Casovi
                                 {
                                    //Id = 2,
                                     Naslov = "GhostbustersPredmet",
                                     Datum = DateTime.Parse("1984-3-13"),
                                     BrojCasovi = 4,
                                     TipCasovi = 1,
                                     Opis = "Comedy",
                                     PredmetId = context.Predmet.Single(d => d.ImePredmet == "Ivan").Id
                                 },
                                 new Casovi
                                 {
                                     //Id = 3,
                                     Naslov = "Ghostbusters 2",
                                     Datum = DateTime.Parse("1986-2-23"),
                                     BrojCasovi = 3,
                                     TipCasovi = 3,
                                     Opis = "Comedy",
                                     PredmetId = context.Predmet.Single(d => d.ImePredmet == "Ivan").Id
                                    },
                                 new Casovi
                                 {
                                     //Id = 4,
                                     Naslov = "Rio Bravo",
                                     Datum = DateTime.Parse("1959-4-15"),
                                     BrojCasovi=3,
                                     TipCasovi=2,
                                     Opis = "Western",
                                     PredmetId = context.Predmet.Single(d => d.ImePredmet == "Howard").Id
                                 }
                                 );
                                context.SaveChanges();
                                context.Prisustvo.AddRange(
                                new Prisustvo { StudentId = 1, CasoviId = 1 },
                                new Prisustvo { StudentId = 2, CasoviId = 1 },
                                new Prisustvo { StudentId = 3, CasoviId = 1 },
                                new Prisustvo { StudentId = 4, CasoviId = 2 },
                                new Prisustvo { StudentId = 5, CasoviId = 2 },
                                new Prisustvo { StudentId = 6, CasoviId = 2 },
                                new Prisustvo { StudentId = 4, CasoviId = 3 },
                                new Prisustvo { StudentId = 5, CasoviId = 3 },
                                new Prisustvo { StudentId = 6, CasoviId = 3 },
                                new Prisustvo { StudentId = 7, CasoviId = 4 },
                                new Prisustvo { StudentId = 8, CasoviId = 4 }
                                );
                                context.SaveChanges();
                                 }
                                 }
    }
}
