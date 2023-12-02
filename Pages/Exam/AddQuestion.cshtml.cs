using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace ProjektInzynierski.Pages.Exam
{
    public class AddQuestionModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public AddQuestionModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet([FromQuery] int? id)
        {
            if (id.HasValue)
            {
                var testList = _context.Test.Include(t => t.ListaPytan).ThenInclude(lp => lp.IdPytanieNavigation).ToList();
                wysTest = (from test in testList
                           where test.IdTest == id
                           select test).FirstOrDefault();
                if (wysTest == null) return NotFound();
                ViewData["IdTest"] = id;
            }
            else
            {
                return RedirectToPage("/List");
            }

            ViewData["IdPytanie"] = new SelectList(_context.Pytanie, "IdPytanie", "Tresc");
            return Page();
        }

        [BindProperty]
        public ListaPytan ListaPytan { get; set; } = default!;
        
        public Test wysTest { get; set; }  

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync([FromQuery] int? id)
        {
            if (!id.HasValue) return NotFound();
            if (!ModelState.IsValid || _context.ListaPytan == null || ListaPytan == null)
            {
                return Page();
            }
            if (_context.ListaPytan == null) ListaPytan.IdListaPytan = 0;
            else
            {
                var lListaPytan = _context.ListaPytan.ToList();
                ListaPytan.IdListaPytan = (from lp in lListaPytan
                                         orderby lp.IdListaPytan descending
                                         select lp.IdListaPytan).FirstOrDefault() + 1;

            }
            ListaPytan.IdTest = id;
            _context.ListaPytan.Add(ListaPytan);
            await _context.SaveChangesAsync();

            return RedirectToPage("",new { id = id });
        }
    }
}
