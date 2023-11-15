using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTest.Models.Db;

namespace TestTest.Pages.Answer
{
    public class CreateModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public CreateModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }
        [BindProperty]
        public OdpowiedziDto Odpowiedzi { get; set; } = default!;
        
        public IActionResult OnGet()
        {
            List<SelectListItem> IdPytania = new List<SelectListItem>();
            var IdPytaniaList = _context.Pytania.ToList();
            //Console.WriteLine(IdPytaniaList)
            foreach(var kat in IdPytaniaList)
            {
                IdPytania.Add(new SelectListItem { Value = Convert.ToString(kat.Id), Text = kat.Tresc });
            }
            ViewData["IdPytania"] = IdPytania;
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Odpowiedzi == null || Odpowiedzi == null)
            {
                return Page();
            }
            if (_context.Odpowiedzi == null) Odpowiedzi.Id = 0;
            else
            {
                var OdpowiedziList = _context.Pytania.ToList();
                Odpowiedzi.Id = (from odp in OdpowiedziList
                              orderby Odpowiedzi.Id descending
                              select Odpowiedzi.Id).FirstOrDefault() + 1;
            }

            Odpowiedzi nowaOdpowiedz = new Odpowiedzi();

            nowaOdpowiedz.Id = Odpowiedzi.Id;
            nowaOdpowiedz.IdPytania = Convert.ToInt32(Odpowiedzi.IdPytania);
            nowaOdpowiedz.Odpowiedz = Odpowiedzi.Odpowiedz;
            nowaOdpowiedz.CzyPoprawna = Odpowiedzi.CzyPoprawna;

            _context.Odpowiedzi.Add(nowaOdpowiedz);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
