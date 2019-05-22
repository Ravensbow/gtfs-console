using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gtf
{
    class Kierunki
    {
        public string wybrana_linia;
        public string wybrany_kierunek;
        public Routes linia = new Routes();
        public List<string> kierunkiList= new List<string>();

        public Kierunki(Komunikacja kom)
        {
            kom.WyswietlLinie();

            Console.Write("Podaj Linie: ");
            wybrana_linia = Console.ReadLine();

            foreach(Routes R in kom.routes)
            {
                if (R.route_id != null)
                {
                    if (R.route_id == wybrana_linia) linia = R;
                }
            }
            foreach(Trips T in kom.trips)
            {
                if(kierunkiList.Contains(T.trip_headsign)==false&&T.route_id == linia.route_id)
                {
                    kierunkiList.Add(T.trip_headsign);
                }
                
            }
            int i = 0;
            foreach (string s in kierunkiList) { Console.WriteLine(++i +". "+s); }
            //string liniakierunkow = linia.route_long_name;

            //while(liniakierunkow.Contains('"'))
            //{
            //    liniakierunkow=liniakierunkow.Remove(liniakierunkow.IndexOf('"'),1);
                
            //}
            //string[] tablica_kierunkow = liniakierunkow.Split('|');
            //int i = 0;
            //foreach (string s in tablica_kierunkow)
            //{
            //    Console.WriteLine(++i +". "+s);
            //}
            //int wybor = 1;
            int wybor=Convert.ToInt16(Console.ReadLine());
      
            
            //string[] nazwa_kierunku = tablica_kierunkow[wybor - 1].ToString().Split('-');
           
            //if (nazwa_kierunku.Length > 1)
            //{
            //    if (nazwa_kierunku[1][0]==' ') nazwa_kierunku[1] = nazwa_kierunku[1].Remove(nazwa_kierunku[1].IndexOf(" "), 1);
            //    if (nazwa_kierunku[1][0]==' ') nazwa_kierunku[1] = nazwa_kierunku[1].Remove(nazwa_kierunku.Length - 1, 1);
            //}
            wybrany_kierunek =kierunkiList[wybor-1];
        }

    }
}
