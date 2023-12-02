using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
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
            if (_context.Pytanie != null)
            {
                Pytanie = await _context.Pytanie
                .Include(p => p.IdKategoriaPytaniaNavigation)
                .Include(p => p.IdNauczycielaNavigation)
                .Include(p => p.IdTypPytaniaNavigation)
                .Include(p => p.Odpowiedz)
                .ToListAsync();

                Pytanie = Pytanie.OrderByDescending(p => p.Odpowiedz.FirstOrDefault()?.IdPytanie).ToList();
            }
        }
        public IActionResult OnGetAddAnswer(int idPytanie)
        {
            if (idPytanie == null) return Page();

            return RedirectToPage("/Answer/Create",new{id=idPytanie });
        }
    }
}
