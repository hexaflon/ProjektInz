using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace TestTest.Pages.Answer
{
    public class DeleteModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public DeleteModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Odpowiedzi Odpowiedzi { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Odpowiedzi == null)
            {
                return NotFound();
            }

            var odpowiedzi = await _context.Odpowiedzi.FirstOrDefaultAsync(m => m.Id == id);

            if (odpowiedzi == null)
            {
                return NotFound();
            }
            else 
            {
                Odpowiedzi = odpowiedzi;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Odpowiedzi == null)
            {
                return NotFound();
            }
            var odpowiedzi = await _context.Odpowiedzi.FindAsync(id);

            if (odpowiedzi != null)
            {
                Odpowiedzi = odpowiedzi;
                _context.Odpowiedzi.Remove(Odpowiedzi);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
