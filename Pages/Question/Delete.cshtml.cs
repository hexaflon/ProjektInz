﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace TestTest.Pages.Question
{
    public class DeleteModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public DeleteModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Pytanie Pytanie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pytania == null)
            {
                return NotFound();
            }

            var pytanie = await _context.Pytania.FirstOrDefaultAsync(m => m.Id == id);

            if (pytanie == null)
            {
                return NotFound();
            }
            else 
            {
                Pytanie = pytanie;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pytania == null)
            {
                return NotFound();
            }
            var pytanie = await _context.Pytania.FindAsync(id);

            if (pytanie != null)
            {
                Pytanie = pytanie;
                _context.Pytania.Remove(Pytanie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
