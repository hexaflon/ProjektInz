using System;
using System.Collections.Generic;

namespace TestTest.Models.Db
{
    public partial class Rozwiazanie
    {
        public Rozwiazanie()
        {
            RozwiazanieDoPytan = new HashSet<RozwiazanieDoPytan>();
        }

        public int IdRozwiazanie { get; set; }
        public int? IdUcznia { get; set; }
        public int? IdTest { get; set; }
        public double? LiczbaPunktow { get; set; }

        public virtual Test? IdTestNavigation { get; set; }
        public virtual Osoba? IdUczniaNavigation { get; set; }
        public virtual ICollection<RozwiazanieDoPytan> RozwiazanieDoPytan { get; set; }
    }
}
