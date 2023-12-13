using System;
using System.Collections.Generic;

namespace TestTest.Models.Db;

public partial class Pytanie
{

        public Pytanie()
        {
            ListaPytan = new HashSet<ListaPytan>();
            Odpowiedz = new HashSet<Odpowiedz>();
        }

        public int IdPytanie { get; set; }
        public int? IdNauczyciela { get; set; }
        public int? IdKategoriaPytania { get; set; }
        public int? IdTypPytania { get; set; }
        [Display(Name = "Treść pytania")]
        [StringLength(300, ErrorMessage = "Maksymalna długość to 300 znaków.")]
        public string? Tresc { get; set; }

        public virtual KategoriaPytania? IdKategoriaPytaniaNavigation { get; set; }
        public virtual Osoba? IdNauczycielaNavigation { get; set; }
        public virtual TypPytania? IdTypPytaniaNavigation { get; set; }
        public virtual ICollection<ListaPytan> ListaPytan { get; set; }
        public virtual ICollection<Odpowiedz> Odpowiedz { get; set; }
    
}
