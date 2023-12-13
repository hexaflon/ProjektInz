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
    public class DeleteModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public DeleteModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Test Test { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Test == null)
            {
                return NotFound();
            }

            var test = await _context.Test.FirstOrDefaultAsync(m => m.IdTest == id);

            if (test == null)
            {
                return NotFound();
            }
            else 
            {
                Test = test;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Test == null)
            {
                return NotFound();
            }
            var test = await _context.Test.FindAsync(id);

            if (test != null)
            {
                Test = test;
                _context.Test.Remove(Test);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./List");
        }
    }
}
