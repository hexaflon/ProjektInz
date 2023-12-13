using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTest.Models.Db
{
    public partial class Osoba
    {
        public Osoba()
        {
            Grupy = new HashSet<Grupy>();
            Pytanie = new HashSet<Pytanie>();
            Rozwiazanie = new HashSet<Rozwiazanie>();
            Test = new HashSet<Test>();
            Uczestnicy = new HashSet<Uczestnicy>();
        }

        [Display(Name = "ID osoby")]
        public int IdOsoba { get; set; }
        [Display(Name = "Imię")]
        [StringLength(45, ErrorMessage = "Maksymalna długość to 45 znaków.")]
        public string Imie { get; set; } = null!;
        [StringLength(45, ErrorMessage = "Maksymalna długość to 45 znaków.")]
        public string Nazwisko { get; set; } = null!;
        [Display(Name = "Hasło")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Hasło musi mieć co najmniej 8 znaków, jedną małą literę, jedną dużą literę i jedną cyfrę.")]
        [StringLength(45, ErrorMessage = "Maksymalna długość to 45 znaków.")]
        public string Haslo { get; set; } = null!;
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu e-mail.")]
        [StringLength(70, ErrorMessage = "Maksymalna długość to 70 znaków.")]
        public string Email { get; set; } = null!;
        [Display(Name = "Nr telefonu")]
        [Range(9, 11, ErrorMessage = "Nr telefonu musi mieć od 9 do 11 znaków (z nr kierunkowym lub bez).")]
        public string? NrTelefonu { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public int Status { get; set; }

        public virtual Status StatusNavigation { get; set; } = null!;
        public virtual ICollection<Grupy> Grupy { get; set; }
        public virtual ICollection<Pytanie> Pytanie { get; set; }
        public virtual ICollection<Rozwiazanie> Rozwiazanie { get; set; }
        public virtual ICollection<Test> Test { get; set; }
        public virtual ICollection<Uczestnicy> Uczestnicy { get; set; }
    }
}
