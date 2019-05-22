using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gtf
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Properties.Resource1.agency.ToString());
            //string s = "1, 2,\"1\",\"JUNIKOWO - FRANOWO|FRANOWO - JUNIKOWO\",\"JUNIKOWO - Grunwaldzka - Reymonta - Hetma�ska - Zamenhofa - Jana Paw�a II - Trasa K�rnicka - Pia�nicka - Szwajcarska - FRANOWO^G - zjazd do zajezdni G�ogowska przez przystanek: Rondo Nowaka-Jeziora�skiego^N - kurs obs�ugiwany taborem niskopod�ogowym - z wyj�tkiem sytuacji awaryjnych^S - zjazd do zajezdni Forteczna (Staro��ka) przez przystanek: G�ogowska/Hetma�ska, Traugutta|FRANOWO - Pia�nicka - Trasa K�rnicka - Jana Paw�a II - Zamenhofa - Hetma�ska - Reymonta - Grunwaldzka - JUNIKOWO^N - kurs obs�ugiwany taborem niskopod�ogowym - z wyj�tkiem sytuacji awaryjnych\",0,D0006F,FFFFFF";

            //string A = "\"9_1009241 ^ M,F\",11:00:00,11:00:00,1639,0,\"ZAJEZDNIA / MADALI�SKIEGO\",0,1";

            //A = Routes.Usuwanie(A, 0, 8);
            //Console.WriteLine(A);

            Dane dane = new Dane();

            List<string> pobranem = new List<string>() { "Poznan" };

            Komunikacja kom = new Komunikacja(dane.pobrane_miasta);

            Kierunki kierunki = new Kierunki(kom);

            ListaPrzystankow lista = new ListaPrzystankow(kom, kierunki);

            GodzinyOdjazdu godziny = new GodzinyOdjazdu(lista, kom);

            godziny.WysiwetlanieGodzin();





            //kom.WyszukajStacje();
            //Console.WriteLine(s);
            //Route_Desc.Usuwanie(s);

            Console.ReadKey();
        }
    }
}
