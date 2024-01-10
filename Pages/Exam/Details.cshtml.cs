using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace ProjektInzynierski.Pages.Exam
{
    [Authorize(Roles = "Nauczyciel,Admin")]
    public class DetailsModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;
        private readonly UserManager<Osoba> _userManager;
        public DetailsModel(TestTest.Models.Db.DatabaseContext context, UserManager<Osoba> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Test Test { get; set; } = default!;
        public List<Wynik> Wyniki { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Test == null)
            {
                return NotFound();
            }

            var test = await _context.Test.Include(t=>t.ListaPytan).FirstOrDefaultAsync(m => m.IdTest == id);
            if (test == null)
            {
                return NotFound();
            }
            
            var rozwiazania = _context.Rozwiazanie.Where(r => r.IdTest == id).ToList();
           Wyniki = new List<Wynik>();
            foreach(var rozwiazanie in rozwiazania)
            {
                var user = _userManager.Users
                    .Where(u => u.IdOsoba == rozwiazanie.IdUcznia)
                    .Select(u => new {u.Name, u.Surname}).First();
                var wynik = new Wynik();
                wynik.imie = user.Name;
                wynik.nazwisko = user.Surname;
                wynik.punkty = (double)rozwiazanie.LiczbaPunktow;

                wynik.nowaWiadomosc(test.ListaPytan.Count());
                Console.WriteLine(wynik.nazwisko + " " + wynik.imie + " " + wynik.wiadomosc + " " + wynik.punkty);
                Wyniki.Add(wynik);

            }


            Test = test;

            return Page();
        }
    }
}



