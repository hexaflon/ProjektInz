﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTest.Models.Db
{
    public partial class Odpowiedz
    {
        public Odpowiedz()
        {
            RozwiazanieDoPytan = new HashSet<RozwiazanieDoPytan>();
        }

        [Display(Name = "ID odpowiedzi")]
        public int IdOdpowiedz { get; set; }
        public int? IdPytanie { get; set; }
        [Display(Name = "Treść odpowiedzi")]
        [StringLength(100, ErrorMessage = "Maksymalna długość to 100 znaków.")]
        public string TrescOdpowiedzi { get; set; } = null!;
        [Display(Name = "Czy odpowiedź jest poprawna?")]
        public bool CzyPoprawny { get; set; }

        public virtual Pytanie? IdPytanieNavigation { get; set; }
        public virtual ICollection<RozwiazanieDoPytan> RozwiazanieDoPytan { get; set; }
    }
}
