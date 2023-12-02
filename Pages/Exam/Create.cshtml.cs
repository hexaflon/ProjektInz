﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTest.Models.Db;

namespace ProjektInzynierski.Pages.Exam
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
        ViewData["IdGrupy"] = new SelectList(_context.Grupy, "IdGrupy", "Nazwa");
            return Page();
        }

        [BindProperty]
        public Test Test { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Test == null || Test == null)
            {
                return Page();
            }
            Test.DataUtworzenia = DateTime.Now;
            //dodać dla obecnego użytkownika
            Test.IdNauczyciela = 1;

            _context.Test.Add(Test);
            await _context.SaveChangesAsync();

            return RedirectToPage("./List");
        }
    }
}
