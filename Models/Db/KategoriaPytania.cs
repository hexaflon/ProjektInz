using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTest.Models.Db
{
    public partial class KategoriaPytania
    {
        public KategoriaPytania()
        {
            Pytanie = new HashSet<Pytanie>();
        }

        [Display(Name = "ID kategorii pytania")]
        public int IdKategoriaPytania { get; set; }
        [Display(Name = "Kategoria pytania")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [StringLength(15, ErrorMessage = "Maksymalna długość to 15 znaków.")]
        public string Nazwa { get; set; } = null!;
        [Display(Name = "Opis kategorii")]
        [StringLength(100, ErrorMessage = "Maksymalna długość to 100 znaków.")]
        public string Opis { get; set; } = null!;

        public virtual ICollection<Pytanie> Pytanie { get; set; }
    }
}
