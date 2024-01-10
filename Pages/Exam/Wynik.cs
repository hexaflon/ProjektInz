namespace ProjektInzynierski.Pages.Exam
{
    public class Wynik
    {


        public string wiadomosc { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public double punkty { get; set; }
        public Wynik()
        {

        }
        public Wynik(string wiadomosc, string imie, string nazwisko, double punkty)
        {
            this.wiadomosc = wiadomosc;
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.punkty = punkty;
        }

        public void nowaWiadomosc(int maxPkt)
        {
            this.wiadomosc = $"{punkty.ToString("0.##")}/{maxPkt} pkt.";
        }
    }
}
