using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace TestTest.Pages.Answer
{
    public class EditModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public EditModel(TestTest.Models.Db.DatabaseContext context)
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

            var odpowiedzi =  await _context.Odpowiedzi.FirstOrDefaultAsync(m => m.Id == id);
            if (odpowiedzi == null)
            {
                return NotFound();
            }
            Odpowiedzi = odpowiedzi;
           ViewData["IdPytania"] = new SelectList(_context.Pytania, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Odpowiedzi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OdpowiedziExists(Odpowiedzi.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OdpowiedziExists(int id)
        {
          return (_context.Odpowiedzi?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
