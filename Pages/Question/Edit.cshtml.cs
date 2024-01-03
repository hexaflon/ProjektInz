using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace TestTest.Pages.Question
{
    [Authorize(Roles = "Nauczyciel,Admin")]
    public class EditModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public EditModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pytanie Pytanie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync([FromQuery]int? id)
        {
            if (id == null || _context.Pytanie == null)
            {
                return NotFound();
            }

            var pytanie =  await _context.Pytanie.FirstOrDefaultAsync(m => m.IdPytanie == id);
            if (pytanie == null)
            {
                return NotFound();
            }
            Pytanie = pytanie;
            ViewData["idNauczyciela"] = pytanie.IdNauczyciela;
            ViewData["IdKategoriaPytania"] = new SelectList(_context.KategoriaPytania, "IdKategoriaPytania", "Nazwa");
            ViewData["IdTypPytania"] = new SelectList(_context.TypPytania, "IdTypPytania", "Nazwa");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync([FromQuery] int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (id!=null) { 
                Pytanie.IdPytanie = (int)id;
            }
            
            _context.Attach(Pytanie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PytanieExists(Pytanie.IdPytanie))
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

        private bool PytanieExists(int id)
        {
          return (_context.Pytanie?.Any(e => e.IdPytanie == id)).GetValueOrDefault();
        }
    }
}
