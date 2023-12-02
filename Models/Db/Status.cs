using System;
using System.Collections.Generic;

namespace TestTest.Models.Db
{
    public partial class Status
    {
        public Status()
        {
            Osoba = new HashSet<Osoba>();
        }

        public int IdStatus { get; set; }
        public string Nazwa { get; set; } = null!;
        public string Opis { get; set; } = null!;

        public virtual ICollection<Osoba> Osoba { get; set; }
    }
}
