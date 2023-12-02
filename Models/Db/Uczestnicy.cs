using System;
using System.Collections.Generic;

namespace TestTest.Models.Db
{
    public partial class Uczestnicy
    {
        public int IdUczestnicy { get; set; }
        public int? IdGrupy { get; set; }
        public int? IdUcznia { get; set; }

        public virtual Grupy? IdGrupyNavigation { get; set; }
        public virtual Osoba? IdUczniaNavigation { get; set; }
    }
}
