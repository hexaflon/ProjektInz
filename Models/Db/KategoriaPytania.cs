using System;
using System.Collections.Generic;

namespace TestTest.Models.Db
{
    public partial class KategoriaPytania
    {
        public KategoriaPytania()
        {
            Pytanie = new HashSet<Pytanie>();
        }

        public int IdKategoriaPytania { get; set; }
        public string Nazwa { get; set; } = null!;
        public string Opis { get; set; } = null!;

        public virtual ICollection<Pytanie> Pytanie { get; set; }
    }
}
