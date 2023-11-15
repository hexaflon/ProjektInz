using System;
using System.Collections.Generic;

namespace TestTest.Models.Db;

public partial class Pytanie
{
    public int Id { get; set; }

    public int IdKategorii { get; set; }

    public int IdTypPytania { get; set; }

    public string Tresc { get; set; } = null!;

    public virtual Kategoria IdKategoriiNavigation { get; set; } = null!;

    public virtual TypPytanium IdTypPytaniaNavigation { get; set; } = null!;

    public virtual ICollection<Odpowiedzi> Odpowiedzis { get; set; } = new List<Odpowiedzi>();
}
