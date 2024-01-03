using System;
using System.Collections.Generic;
using System.Data;
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
    [Authorize(Roles = "Uczen,Admin")]
    public class ExamPerformModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;
        private readonly UserManager<Osoba> _userManager;
        private static int idWielokrotngo;

        public ExamPerformModel(TestTest.Models.Db.DatabaseContext context, UserManager<Osoba> userManager)
        {
            _context = context;
            _userManager = userManager;
            idWielokrotngo =  _context.TypPytania
                .Where(tp => tp.Nazwa == "Wielokrotnego wyboru")
                .Select(tp => tp.IdTypPytania).FirstOrDefault();
        }

        public List<Pytanie> pytaniaSprawdzianu { get;set; } = default!;

        public List<Pytanie> GetQuestions(int examId)
        {
            var questions = _context.ListaPytan
                .Include(lp => lp.IdPytanieNavigation)
                .ThenInclude(p => p.Odpowiedz)
                .Where(lp => lp.IdTest == examId)
                .Select(lp => lp.IdPytanieNavigation).ToList();

            return questions;
        }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            pytaniaSprawdzianu = GetQuestions(id);
            if (pytaniaSprawdzianu == null) return NotFound();
            
            ViewData["idWielokrotnego"] = idWielokrotngo;

            return Page();
        }
        //, Dictionary<int, int[]> selectedAnswers
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var selectedAnswers = Request.Form;
            int ilePoprawnych = 0, zaznaczonePoprawne=0;
            double punkty = 0;
            Console.WriteLine(selectedAnswers);
            foreach (var question in selectedAnswers)
            {
                var temp = question.Key;
                int key=0;
                Console.WriteLine($"Converting '{temp}' to integer...");
                try
                {
                    key = Convert.ToInt32(temp);
                    Console.WriteLine($"Successfully converted '{temp}' to {key}.");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Failed to convert '{temp}' to integer: {ex.Message}");
                }

                var pytanie = _context.Pytanie.Include(p => p.Odpowiedz)
                    .Where(p => p.IdPytanie == key).FirstOrDefault();
                bool czyWielokrotnego = pytanie.IdTypPytania == idWielokrotngo;
                if (czyWielokrotnego)
                {
                    ilePoprawnych = pytanie.Odpowiedz
                        .Where(o => o.CzyPoprawny == true)
                        .Count();
                }

                foreach (var answer in question.Value)
                {
                    temp = answer;
                    int val = 0;
                    Console.WriteLine($"Converting '{temp}' to integer...");
                    try
                    {
                        val = Convert.ToInt32(temp);
                        Console.WriteLine($"Successfully converted '{temp}' to {val}.");
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Failed to convert '{temp}' to integer: {ex.Message}");
                    }
                    var isCorrect = pytanie.Odpowiedz.Any(o => o.IdOdpowiedz == val && o.CzyPoprawny);
                    if (czyWielokrotnego)
                    {
                        if (isCorrect)
                        {
                            zaznaczonePoprawne++;
                        }
                        else
                        {
                            zaznaczonePoprawne--;
                        }
                    }
                    else
                    {
                        if (isCorrect)
                        {
                            punkty++;
                        }
                    }
                }
                if (czyWielokrotnego)
                {
                    double punkt = (double)zaznaczonePoprawne / (double)ilePoprawnych;
                    if (zaznaczonePoprawne > 0) {
                        
                        punkty += punkt; }
                    Console.WriteLine($"Po {punkty} {zaznaczonePoprawne} {ilePoprawnych}, {punkt}");
                    zaznaczonePoprawne = 0;
                    ilePoprawnych = 0;
                }
            }
            Rozwiazanie rozwiazanieSprawdzianu = new Rozwiazanie();

            if (_context.Rozwiazanie == null) rozwiazanieSprawdzianu.IdRozwiazanie = 1;
            else
            {
                rozwiazanieSprawdzianu.IdRozwiazanie =
                    _context.Rozwiazanie.OrderByDescending(r => r.IdRozwiazanie)
                    .Select(r => r.IdRozwiazanie).FirstOrDefault()+1;
            }

            rozwiazanieSprawdzianu.IdTest = id;
            rozwiazanieSprawdzianu.IdUcznia = _userManager.GetUserAsync(User).Result.IdOsoba;
            rozwiazanieSprawdzianu.LiczbaPunktow = punkty;
            _context.Rozwiazanie.Add(rozwiazanieSprawdzianu);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
