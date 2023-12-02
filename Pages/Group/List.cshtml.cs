﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace ProjektInzynierski.Pages.Group
{
    public class ListModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public ListModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Grupy> Grupy { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Grupy != null)
            {
                Grupy = await _context.Grupy
                .Include(g => g.IdNauczycielaNavigation).ToListAsync();
            }
        }
    }
}