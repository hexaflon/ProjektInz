namespace TestTest.Pages.Answer
{
    public class OdpowiedziDto
    {
        public int Id { get; set; }

        public string IdPytania { get; set; } = null!;

        public string Odpowiedz { get; set; } = null!;

        public bool CzyPoprawna { get; set; }

    }
}
