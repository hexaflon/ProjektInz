using System;
using System.Collections.Generic;

namespace TestTest.Models.Db
{
    public partial class Test
    {
        public Test()
        {
            ListaPytan = new HashSet<ListaPytan>();
            Rozwiazanie = new HashSet<Rozwiazanie>();
        }

        public int IdTest { get; set; }
        public int? IdGrupy { get; set; }
        public int? IdNauczyciela { get; set; }
        public bool CzyWidoczny { get; set; }
        public DateTime? DataUtworzenia { get; set; }
        public DateTime? DataRozpoczecia { get; set; }
        public DateTime? DataZakonczenia { get; set; }
        public string? Tytul { get; set; }
        public string? Opis { get; set; }
        public int? CzasTrwania { get; set; }

        public virtual Grupy? IdGrupyNavigation { get; set; }
        public virtual Osoba? IdNauczycielaNavigation { get; set; }
        public virtual ICollection<ListaPytan> ListaPytan { get; set; }
        public virtual ICollection<Rozwiazanie> Rozwiazanie { get; set; }
    }
}
