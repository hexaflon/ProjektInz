using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTest.Models.Db;

public partial class User
{
    [Display(Name = "ID użytkownika")]
    public int Id { get; set; }

    [Display(Name = "Nazwa użytkownika")]
    [Required(ErrorMessage = "To pole jest wymagane.")]
    [StringLength(20, ErrorMessage = "Maksymalna długość to 20 znaków.")]
    public string Username { get; set; } = null!;

    [Display(Name = "Hasło")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Hasło musi mieć co najmniej 8 znaków, jedną małą literę, jedną dużą literę i jedną cyfrę.")]
    public string Password { get; set; } = null!;

    [Display(Name = "Imię")]
    [Required(ErrorMessage = "To pole jest wymagane.")]
    [StringLength(20, ErrorMessage = "Maksymalna długość to 20 znaków.")]
    public string? Name { get; set; }
}
