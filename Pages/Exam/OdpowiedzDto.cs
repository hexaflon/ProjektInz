using TestTest.Models.Db;

namespace ProjektInzynierski.Pages.Exam
{
    public class OdpowiedzDto:Odpowiedz
    {
        public bool selected;
        public OdpowiedzDto(Odpowiedz odp, bool selected)
        {
            this.IdOdpowiedz = odp.IdOdpowiedz;
            this.TrescOdpowiedzi = odp.TrescOdpowiedzi;
            this.CzyPoprawny = odp.CzyPoprawny;
            this.IdPytanie = odp.IdPytanie;
            this.selected = selected;

        }
    }
}
