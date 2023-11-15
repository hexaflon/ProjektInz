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
            List<SelectListItem> kategorieList = new List<SelectListItem>();
            var kategorie = _context.Kategorie;
            foreach(var kat in kategorie)
            {
                kategorieList.Add(new SelectListItem { Value = Convert.ToString(kat.Id), Text = kat.Nazwa });
            }
            ViewData["Kategorie"] = kategorieList;

            List<SelectListItem> typPytaniaList = new List<SelectListItem>();
            var typPytan = _context.TypPytan;
            foreach (var typ in typPytan)
            {
                typPytaniaList.Add(new SelectListItem { Value = Convert.ToString(typ.Id), Text = typ.Nazwa });
            }
            ViewData["TypPytania"] = typPytaniaList;

            return Page();
        }

        [BindProperty]
        public PytanieDto Pytanie { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Pytania == null || Pytanie == null)
            {
                return Page();
            }
            if (_context.Pytania == null) Pytanie.Id = 0;
            else
            {
                var pytania = _context.Pytania.ToList();
                Pytanie.Id = (from pytanie in pytania
                           orderby pytanie.Id descending
                           select pytanie.Id).FirstOrDefault() + 1;
            }

            Pytanie nowePytanie = new Pytanie();
            nowePytanie.Id = Pytanie.Id;
            nowePytanie.IdTypPytania = Convert.ToInt32(Pytanie.IdTypPytania);
            nowePytanie.IdKategorii = Convert.ToInt32(Pytanie.IdKategorii);
            nowePytanie.Tresc = Pytanie.Tresc;


            _context.Pytania.Add(nowePytanie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
