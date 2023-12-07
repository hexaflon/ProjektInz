﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTest.Models.Db;

namespace ProjektInzynierski.Pages.Exam
{
    public class ExamListModel : PageModel
    {
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public ExamListModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Test> Test { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Test != null)
            {
                

                var iducznia = 2;

                Test = _context.Test
                    .Where(gr => _context.Uczestnicy.Any(u => u.IdUcznia == iducznia))
                    .Where(t => t.DataRozpoczecia <= DateTime.Now && t.DataZakonczenia>=DateTime.Now).ToList();

            }
        }

        public async Task<IActionResult> OnPostAsync([FromQuery] int? idTest)
        {
            if (idTest == null) return NotFound();
            var test = _context.Test.FindAsync(idTest);
            if (test == null) return NotFound();

            Rozwiazanie rozwiazanie = new Rozwiazanie();
            rozwiazanie.IdUcznia = 2;//zmienic
            rozwiazanie.IdTest = idTest;
            var rozwiazanielist = _context.Rozwiazanie.ToList();
            if (rozwiazanielist == null) rozwiazanie.IdRozwiazanie = 0;
            else
            {
                rozwiazanie.IdRozwiazanie = rozwiazanielist
                    .OrderByDescending(r => r.IdRozwiazanie)
                    .Select(r => r.IdRozwiazanie).FirstOrDefault();
            }

            _context.Rozwiazanie.Add(rozwiazanie);
            await _context.SaveChangesAsync();

            
            return RedirectToPage("ExamPerform",new {idrozwiazania = rozwiazanie.IdRozwiazanie});
        
        }
    }
}