using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace ProjektInzynierski.Pages.Exam
{
    public class ExamDetailedInfoModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;
        private static int idWielokrotngo;
        private readonly UserManager<Osoba> _userManager;
        public ExamDetailedInfoModel(TestTest.Models.Db.DatabaseContext context, UserManager<Osoba> userManager)
        {
            _context = context;
            idWielokrotngo = _context.TypPytania
                .Where(tp => tp.Nazwa == "Wielokrotnego wyboru")
                .Select(tp => tp.IdTypPytania).FirstOrDefault();
            _userManager = userManager;
        }

        public List<Pytanie> pytaniaSprawdzianu { get; set; } = default!;
        public List<Pytanie> GetQuestions(int examId)
        {
            var questions = _context.ListaPytan
                .Include(lp => lp.IdPytanieNavigation)
                .ThenInclude(p => p.Odpowiedz)
                .Where(lp => lp.IdTest == examId)
                .Select(lp => lp.IdPytanieNavigation).ToList();

            return questions;
        }
        public List<Odpowiedz> wybraneOdp { get; set; } = default!;
        public string newStyleInput;
        public string newStyleLabel;
        public async Task OnGetAsync(int id)
        {
            if (_context.Rozwiazanie != null)
            {
                wybraneOdp = _context.RozwiazanieDoPytan
                    .Where(rdp => rdp.IdRozwiazanie==id)
                    .Select(rdp => rdp.IdOdpowiedzNavigation)
                    .ToList();
                pytaniaSprawdzianu = GetQuestions((int)_context.Rozwiazanie
                    .Where(r => r.IdRozwiazanie==id)
                    .Select(r => r.IdTest).First());
                ViewData["idWielokrotnego"] = idWielokrotngo;
            }
        }
    }
}
