using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTest.Models.Db;

namespace TestTest.Pages.Question
{
    [Authorize(Roles = "Nauczyciel,Admin")]
    public class CreateModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;
        private readonly UserManager<Osoba> _userManager;
        public CreateModel(TestTest.Models.Db.DatabaseContext context, UserManager<Osoba> userManager)
        {
            _context = context;
            _userManager = userManager;
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


            int id;
            var query = _context.Pytanie.OrderByDescending(x => x.IdPytanie).FirstOrDefault();
            if (query == null) id = 0;
            else
            {
                id = query.IdPytanie + 1;
            }
            Pytanie.IdPytanie = id;
            //dodać dla obecnego użytkownika

            Pytanie.IdNauczyciela = _userManager.GetUserAsync(User).Result.IdOsoba;
            _context.Pytanie.Add(Pytanie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
