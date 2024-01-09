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
    [Authorize(Roles = "Uczen,Admin")]
    public class ExamInfoModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;
        private readonly UserManager<Osoba> _userManager;

        public ExamInfoModel(TestTest.Models.Db.DatabaseContext context, UserManager<Osoba> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Rozwiazanie Rozwiazanie { get; set; } = default!;
        public Test Test { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync([FromQuery]int id)
        {
            if (id == null || _context.Test == null)
            {
                return NotFound();
            }

            var rozwiazanie = await _context.Rozwiazanie.FirstOrDefaultAsync(m => m.IdRozwiazanie == id);
            if (rozwiazanie == null)
            {
                return NotFound();
            }
            else 
            {
                var idOsoba = _userManager.GetUserAsync(User).Result.IdOsoba;
                if (idOsoba == rozwiazanie.IdUcznia || User.IsInRole("Admin"))
                {
                    Test = await _context.Test
                    .Include(t => t.ListaPytan)
                    .FirstOrDefaultAsync(t => t.IdTest == rozwiazanie.IdTest);
                    if (Test == null) return NotFound();
                    Rozwiazanie = rozwiazanie;
                }
                else return Forbid();

                
            }
            return Page();
        }
    }
}
