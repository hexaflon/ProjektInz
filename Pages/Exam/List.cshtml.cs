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

namespace ProjektInzynierski.Pages.Exam
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

        public IList<Test> Test { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Test != null)
            {
                var iduser = _userManager.GetUserAsync(User).Result.IdOsoba;
                Test = await _context.Test.Where(t => t.IdNauczyciela == iduser)
                .Include(t => t.IdGrupyNavigation)
                .Include(t => t.ListaPytan)
                .ThenInclude(lp => lp.IdPytanieNavigation).ToListAsync();
            }
        }
    }
}
