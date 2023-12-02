using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace ProjektInzynierski.Pages.Group
{
    public class AddMembersModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public AddMembersModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet([FromQuery]int? id)
        {
            if (!id.HasValue) return RedirectToPage("./List");
            ViewData["idGrupy"] = id;

            var uczniowie = _context.Osoba
                .Where(o => o.Status==3)
                .Where(o => !_context.Uczestnicy.Any(u => u.IdUcznia == o.IdOsoba && u.IdGrupy == id)).ToList();

            wysUczniowie = _context.Osoba
                .Where(o => o.Status == 3)
                .Where(o => _context.Uczestnicy.Any(u => u.IdUcznia == o.IdOsoba && u.IdGrupy == id))
                .Include(o => o.Uczestnicy)
                .ToList();

            var uczniowieList = uczniowie.Select(osoba => new SelectListItem
            {
                Value = osoba.IdOsoba.ToString(),
                Text = $"{osoba.Nazwisko} {osoba.Imie} {osoba.Email}"
            }).ToList();
            ViewData["IdUcznia"] = new SelectList(uczniowieList, "Value","Text");
            return Page();
        }

        [BindProperty]
        public Uczestnicy Uczestnicy { get; set; } = default!;
        public List<Osoba> wysUczniowie { get; set; } = default!;


        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync([FromQuery]int? id)
        {
            if (!id.HasValue) return RedirectToPage("./List");

            if (!ModelState.IsValid || _context.Uczestnicy == null || Uczestnicy == null)
            {
                return Page();
            }

            int? grupyList = _context.Grupy.Where(gr => gr.IdGrupy == id).Select(gr => gr.IdGrupy).FirstOrDefault();
            if (grupyList == null) return NotFound();
            Uczestnicy.IdGrupy = id;

            var uczestnicyList = _context.Uczestnicy.ToList();
            if (uczestnicyList == null) Uczestnicy.IdUczestnicy = 0;
            else
            {
                Uczestnicy.IdUczestnicy = uczestnicyList.OrderByDescending(u => u.IdUczestnicy)
                                                        .Select(u => u.IdUczestnicy).FirstOrDefault() + 1;
            }

            
            //dodać dla użytkownika
            

            _context.Uczestnicy.Add(Uczestnicy);
            await _context.SaveChangesAsync();

            return RedirectToPage("", new {id=id});
        }


        

        public async Task<IActionResult> OnGetDelete([FromQuery]int? idUcznia, [FromQuery]int? idGrupy)
        {
            Console.WriteLine(idUcznia + " " + idGrupy + " usuwanie\n\n\n\n\n");
            if(idUcznia == null || idGrupy == null) return NotFound();
            var uczestnikDoUsunieca = await _context.Uczestnicy.FirstOrDefaultAsync(u => u.IdUcznia == idUcznia && u.IdGrupy == idGrupy);
            Console.WriteLine(uczestnikDoUsunieca.IdUczestnicy + " " + uczestnikDoUsunieca.IdUcznia + " " + uczestnikDoUsunieca.IdGrupy);
            if (uczestnikDoUsunieca != null)
            {
                _context.Uczestnicy.Remove(uczestnikDoUsunieca);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("", new { id = idGrupy });
        }
    }
}
