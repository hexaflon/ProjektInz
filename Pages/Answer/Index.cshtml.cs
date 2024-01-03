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

namespace TestTest.Pages.Answer
{
    [Authorize(Roles = "Nauczyciel,Admin")]
    public class IndexModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public IndexModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Odpowiedz> Odpowiedzi { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Odpowiedz != null)
            {
                Odpowiedzi = await _context.Odpowiedz
                .Include(o => o.IdPytanieNavigation).ToListAsync();
            }
        }
    }
}
