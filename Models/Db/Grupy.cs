using System;
using System.Collections.Generic;

namespace TestTest.Models.Db
{
    public partial class Grupy
    {
        public Grupy()
        {
            Test = new HashSet<Test>();
            Uczestnicy = new HashSet<Uczestnicy>();
        }

        public int IdGrupy { get; set; }
        public int? IdNauczyciela { get; set; }
        public string Nazwa { get; set; } = null!;

        public virtual Osoba? IdNauczycielaNavigation { get; set; }
        public virtual ICollection<Test> Test { get; set; }
        public virtual ICollection<Uczestnicy> Uczestnicy { get; set; }
    }
}
