using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace ProjektInzynierski.Pages.Exam
{
    public class ListModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public ListModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Test> Test { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Test != null)
            {
                Test = await _context.Test
                .Include(t => t.IdGrupyNavigation)
                .Include(t => t.IdNauczycielaNavigation)
                .Include(t => t.ListaPytan)
                .ThenInclude(lp => lp.IdPytanieNavigation).ToListAsync();
            }
        }
    }
}
