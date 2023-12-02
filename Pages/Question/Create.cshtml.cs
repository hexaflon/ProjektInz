using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTest.Models.Db;

namespace TestTest.Pages.Question
{
    public class CreateModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public CreateModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            
        ViewData["IdKategoriaPytania"] = new SelectList(_context.KategoriaPytania, "IdKategoriaPytania", "Nazwa");
        ViewData["IdTypPytania"] = new SelectList(_context.TypPytania, "IdTypPytania", "Nazwa");
            
            return Page();
        }

        [BindProperty]
        public Pytanie Pytanie { get; set; } = default!;
        

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || Pytanie == null || _context.Pytanie == null)
            {
                return Page();
            }


            /*Dodać dla aktywnego użytkownika
             * 
             * 
             */
            int id;
            var query = _context.Pytanie.OrderByDescending(x => x.IdPytanie).FirstOrDefault();
            if (query == null) id = 0;
            else
            {
                id = query.IdPytanie + 1;
            }
            Pytanie.IdPytanie = id;
            //dodać dla obecnego użytkownika
            Pytanie.IdNauczyciela = 1;
            _context.Pytanie.Add(Pytanie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
