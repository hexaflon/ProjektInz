using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTest.Models.Db
{
    public partial class TypPytania
    {
        public TypPytania()
        {
            Pytanie = new HashSet<Pytanie>();
        }

        [Display(Name = "ID typu pytania")]
        public int IdTypPytania { get; set; }
        [Display(Name = "Typ pytania")]
        [StringLength(20, ErrorMessage = "Maksymalna długość to 20 znaków.")]
        public string Nazwa { get; set; } = null!;
        [Display(Name = "Opis pytania")]
        [StringLength(100, ErrorMessage = "Maksymalna długość to 100 znaków.")]
        public string Opis { get; set; } = null!;

        public virtual ICollection<Pytanie> Pytanie { get; set; }
    }
}
