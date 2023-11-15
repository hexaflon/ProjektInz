using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace TestTest.Pages.Question
{
    public class IndexModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public IndexModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Pytanie> Pytanie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Pytania != null)
            {
                Pytanie = await _context.Pytania
                .Include(p => p.IdKategoriiNavigation)
                .Include(p => p.IdTypPytaniaNavigation).ToListAsync();
            }
        }
    }
}
