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
    public class CreateWithQuestionId : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public CreateWithQuestionId(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Odpowiedzi Odpowiedzi { get; set; } = default!;

        public IActionResult OnGet(string Id)
        {
            List<SelectListItem> IdPytania = new List<SelectListItem>();
            var IdPytan = _context.Pytania;
            foreach (var kat in IdPytan)
            {
                IdPytania.Add(new SelectListItem { Value = Convert.ToString(kat.Id), Text = kat.Tresc });
            }
            ViewData["IdPytania"] = IdPytania;
            Odpowiedzi.IdPytania = Convert.ToInt32(Id);
            int id = Convert.ToInt32(Id);
            if (_context.Pytania.FirstOrDefault(pyt => pyt.Id == id) == null) RedirectToPage("./Create");
            IdPytania.First(p => p.Value == Id).Selected = true;
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Odpowiedzi == null || Odpowiedzi == null)
            {
                return Page();
            }

            _context.Odpowiedzi.Add(Odpowiedzi);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
