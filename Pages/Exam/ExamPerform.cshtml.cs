using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public ExamPerformModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        public IList<RozwiazanieDoPytan> RozwiazanieDoPytan { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RozwiazanieDoPytan != null)
            {
                RozwiazanieDoPytan = await _context.RozwiazanieDoPytan
                .Include(r => r.IdOdpowiedzNavigation)
                .Include(r => r.IdRozwiazanieNavigation).ToListAsync();
            }
            RozwiazanieDoPytan x = new RozwiazanieDoPytan();
            
        }
    }
}
