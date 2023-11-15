using System;
using System.Collections.Generic;

namespace TestTest.Models.Db;

public partial class Kategoria
{
    public int Id { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<Pytanie> Pytanies { get; set; } = new List<Pytanie>();
}
