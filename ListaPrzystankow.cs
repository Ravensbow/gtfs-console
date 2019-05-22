using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gtf
{
    class ListaPrzystankow
    {
        public Trips trip;
        public Routes route;
        public string kierunek = String.Empty;
        public List<Stop_times> stop= new List<Stop_times>();
        public List<Stops> przystanki= new List<Stops>();
        public ListaPrzystankow(Komunikacja kom,Kierunki kier)
        {
            route = kier.linia;

            
            kierunek = kier.wybrany_kierunek;
         
            foreach(Trips T in kom.trips)
            {
                if (T.trip_headsign != null)
                {
                   
                    if (T.trip_headsign.Replace("/", " ").ToUpper() == kierunek.Replace("/", " ").ToUpper()&&T.route_id==route.route_id)
                    {
                        
                        trip = T;
                        break;
                    }
                }
            }

            foreach(Stop_times S in kom.stop_Times)
            {
                if (S.trip_id != null)
                {
                    if (S.trip_id.Replace("\"", "") == trip.trip_id.Replace("\"", ""))
                    {
                        stop.Add(S);
                    }
                }
            }

            stop.Sort();

            foreach(Stop_times S in stop)
            {
                foreach (Stops T in kom.stops)
                {
                    if (T.stop_id ==S.stop_id)
                    {
                        przystanki.Add(T);
                    }
                }
            }
           
            
            
        }
        public void WyswietlanieListyPrzystankow()
        {
            int i = 0;
            foreach (Stops S in przystanki)
            {
                Console.WriteLine(++i +". "+S.stop_name);


            }
        }
    }
}
