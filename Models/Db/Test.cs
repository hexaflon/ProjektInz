using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTest.Models.Db
{
    public partial class Test
    {
        public Test()
        {
            ListaPytan = new HashSet<ListaPytan>();
            Rozwiazanie = new HashSet<Rozwiazanie>();
        }

        [Display(Name = "ID testu")]
        public int IdTest { get; set; }
        [Display(Name = "ID grupy")]
        public int? IdGrupy { get; set; }
        [Display(Name = "ID nauczyciela")]
        public int? IdNauczyciela { get; set; }
        [Display(Name = "Czy test jest widoczny?")]
        public bool CzyWidoczny { get; set; }
        [Display(Name = "Data utworzenia")]
        public DateTime? DataUtworzenia { get; set; }
        [Display(Name = "Data rozpoczęcia")]
        public DateTime? DataRozpoczecia { get; set; }
        [Display(Name = "Data zakończenia")]
        public DateTime? DataZakonczenia { get; set; }
        [Display(Name = "Tytuł testu")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [StringLength(15, ErrorMessage = "Maksymalna długość to 15 znaków.")]
        public string? Tytul { get; set; }
        [Display(Name = "Opis testu")]
        [StringLength(200, ErrorMessage = "Maksymalna długość to 200 znaków.")]
        public string? Opis { get; set; }
        [Display(Name = "Czas trwania testu")]
        [Range(5, 120, ErrorMessage = "Czas trwania testu musi wynosić od 5 minut do 2 godzin.")]
        public int? CzasTrwania { get; set; }

        public virtual Grupy? IdGrupyNavigation { get; set; }
        public virtual Osoba? IdNauczycielaNavigation { get; set; }
        public virtual ICollection<ListaPytan> ListaPytan { get; set; }
        public virtual ICollection<Rozwiazanie> Rozwiazanie { get; set; }
    }
}
