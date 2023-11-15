using System;
using System.Collections.Generic;

namespace TestTest.Models.Db;

public partial class Odpowiedzi
{
    public int Id { get; set; }

    public int IdPytania { get; set; }

    public string Odpowiedz { get; set; } = null!;

    public bool CzyPoprawna { get; set; }

    public virtual Pytanie IdPytaniaNavigation { get; set; } = null!;
}
