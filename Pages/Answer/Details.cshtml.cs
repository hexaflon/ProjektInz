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
    public class DetailsModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public DetailsModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

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
    }
}
