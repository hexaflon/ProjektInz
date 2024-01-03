using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace TestTest.Pages.Question
{
    [Authorize(Roles = "Nauczyciel,Admin")]
    public class IndexModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public IndexModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Pytanie> Pytanie { get; set; } = default!;
        public IList<KategoriaPytania> Kategorie { get; set; } = default!;
        public IList<TypPytania> TypyPytan { get; set; } = default!;

        public async Task OnGetAsync(int? categoryId, int? typeId)
        {
            var query = _context.Pytanie
                .Include(p => p.IdKategoriaPytaniaNavigation)
                .Include(p => p.IdTypPytaniaNavigation)
                .Include(p => p.Odpowiedz)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.IdKategoriaPytania == categoryId);
            }

            if (typeId.HasValue)
            {
                query = query.Where(p => p.IdTypPytania == typeId);
            }

            Pytanie = await query.ToListAsync();
            Pytanie = Pytanie.OrderByDescending(p => p.Odpowiedz.FirstOrDefault()?.IdPytanie).ToList();

            Kategorie = await _context.KategoriaPytania.ToListAsync();
            TypyPytan = await _context.TypPytania.ToListAsync();
        }


        public IActionResult OnGetAddAnswer(int idPytanie)
        {
            if (idPytanie == null) return Page();

            return RedirectToPage("/Answer/Create", new { id = idPytanie });
        }
    }
}
