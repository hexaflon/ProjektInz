using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTest.Models.Db;

namespace ProjektInzynierski.Pages.Group
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
        
            //ViewData["IdNauczyciela"] = new SelectList(_context.Osoba.Where(o => o.StatusNavigation.Nazwa.ToLower().Contains("uczen")), "IdOsoba", "imie" + " " + "nazwisko" + "email");
            return Page();
        }

        [BindProperty]
        public Grupy Grupy { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Grupy == null || Grupy == null)
            {
                return Page();
            }

            var grupyList = _context.Grupy.ToList();

            if (grupyList == null) Grupy.IdGrupy = 0;
            else
            {
                Grupy.IdGrupy = grupyList.OrderByDescending(gr => gr.IdGrupy).Select(gr => gr.IdGrupy).FirstOrDefault()+1;
            }
            //zmienić na obecnego użytkownika
            Grupy.IdNauczyciela = 1;
            _context.Grupy.Add(Grupy);
            await _context.SaveChangesAsync();

            return RedirectToPage("./AddMembers", new {id = Grupy.IdGrupy});
        }
    }
}
