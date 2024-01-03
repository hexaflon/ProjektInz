using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;


namespace TestTest.Models.Db
{
    public partial class Osoba : IdentityUser
    {
        public int IdOsoba { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

    }
}
