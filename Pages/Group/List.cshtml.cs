using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace ProjektInzynierski.Pages.Group
{
    [Authorize(Roles = "Nauczyciel,Admin")]
    public class ListModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;
        private readonly UserManager<Osoba> _userManager;
        public ListModel(TestTest.Models.Db.DatabaseContext context, UserManager<Osoba> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Grupy> Grupy { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Grupy != null)
            {
                var userId = _userManager.GetUserAsync(User).Result.IdOsoba;
                Grupy = await _context.Grupy
                    .Where(g=>g.IdNauczyciela==userId)
                    .ToListAsync();
                

            }
        }
    }
}
