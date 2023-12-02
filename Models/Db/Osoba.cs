using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;


namespace TestTest.Models.Db
{
    public partial class Osoba
    {
        public Osoba()
        {
            Grupy = new HashSet<Grupy>();
            Pytanie = new HashSet<Pytanie>();
            Rozwiazanie = new HashSet<Rozwiazanie>();
            Test = new HashSet<Test>();
            Uczestnicy = new HashSet<Uczestnicy>();
        }

        public int IdOsoba { get; set; }
        public string Imie { get; set; } = null!;
        public string Nazwisko { get; set; } = null!;
        public string Haslo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? NrTelefonu { get; set; }
        public int Status { get; set; }

        public virtual Status StatusNavigation { get; set; } = null!;
        public virtual ICollection<Grupy> Grupy { get; set; }
        public virtual ICollection<Pytanie> Pytanie { get; set; }
        public virtual ICollection<Rozwiazanie> Rozwiazanie { get; set; }
        public virtual ICollection<Test> Test { get; set; }
        public virtual ICollection<Uczestnicy> Uczestnicy { get; set; }
    }
}
