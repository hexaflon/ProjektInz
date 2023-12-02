using System;
using System.Collections.Generic;

namespace TestTest.Models.Db
{
    public partial class Odpowiedz
    {
        public Odpowiedz()
        {
            RozwiazanieDoPytan = new HashSet<RozwiazanieDoPytan>();
        }

        public int IdOdpowiedz { get; set; }
        public int? IdPytanie { get; set; }
        public string TrescOdpowiedzi { get; set; } = null!;
        public bool CzyPoprawny { get; set; }

        public virtual Pytanie? IdPytanieNavigation { get; set; }
        public virtual ICollection<RozwiazanieDoPytan> RozwiazanieDoPytan { get; set; }
    }
}
