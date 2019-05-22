using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gtf
{
    class GodzinyOdjazdu
    {
        public Routes Linia = new Routes();
        public List<Trips> Tripy = new List<Trips>();
        public Stops Przystanek = new Stops();
        public List<string> ostateczneGodziny = new List<string>();

        public GodzinyOdjazdu(ListaPrzystankow listaP, Komunikacja kom)
        {
            Linia = listaP.route;
            string kierunek = listaP.kierunek;

            Console.WriteLine("Dzien:");
            string dzien = Console.ReadLine();
           
            foreach (Trips T in kom.trips)
            {
                if (T.route_id == Linia.route_id && T.trip_headsign.Replace("/", " ").ToUpper() == kierunek.Replace("/", " ").ToUpper())
                {
                   
                    Console.WriteLine("Znalazlo Kierunek");
                    if (dzien == "poniedzialek")
                    {
                        foreach (Calendar C in kom.calendar)
                        {
                            if (C.service_id.Replace("\"", "") == T.service_id.Replace("\"", ""))
                            {
                                if (C.monday == "1") Tripy.Add(T);
                            }
                        }
                    }
                    else if (dzien == "wtorek")
                    {
                        foreach (Calendar C in kom.calendar)
                        {
                        
                            if (C.service_id.Replace("\"", "") == T.service_id.Replace("\"", ""))
                            {
                                if (C.tuesday == "1")
                                {
                                    Tripy.Add(T);
                                    Console.WriteLine("Znalazlo Tripa w dany dzien");
                                }
                            }

                        }
                    }
                    else if (dzien == "sroda")
                    {
                        foreach (Calendar C in kom.calendar)
                        {
                            if (C.service_id.Replace("\"", "") == T.service_id.Replace("\"", ""))
                            {
                                if (C.wednesday == "1") Tripy.Add(T);
                            }
                        }
                    }
                    else if (dzien == "czwartek")
                    {
                        foreach (Calendar C in kom.calendar)
                        {
                            if (C.service_id.Replace("\"", "") == T.service_id.Replace("\"", ""))
                            {
                                if (C.thursday == "1") Tripy.Add(T);
                            }
                        }
                    }
                    else if (dzien == "piatek")
                    {
                        foreach (Calendar C in kom.calendar)
                        {
                            if (C.service_id.Replace("\"", "") == T.service_id.Replace("\"", ""))
                            {
                                if (C.friday == "1") Tripy.Add(T);
                            }
                        }
                    }
                    else if (dzien == "sobota")
                    {
                        foreach (Calendar C in kom.calendar)
                        {
                            if (C.service_id.Replace("\"", "") == T.service_id.Replace("\"", ""))
                            {
                                if (C.saturday == "1") Tripy.Add(T);
                            }
                        }
                    }
                    else if (dzien == "niedziela")
                    {
                        foreach (Calendar C in kom.calendar)
                        {
                            if (C.service_id.Replace("\"", "") == T.service_id.Replace("\"", ""))
                            {
                                if (C.sunday == "1") Tripy.Add(T);
                            }
                        }
                    }
                }

            }

            listaP.WyswietlanieListyPrzystankow();

            Console.Write("Podaj przystanek: ");

            int wybor = Convert.ToInt32(Console.ReadLine());

            Przystanek = listaP.przystanki[wybor - 1];





            //Console.Clear();
            Console.WriteLine("Dostepne godziny odjazdu przystanku "+Przystanek.stop_name+" w strone "+kierunek+":");
       
            foreach (Stop_times Times in kom.stop_Times)
            {
                foreach (Trips T in Tripy)
                {
                    
                    if (Times.stop_id == Przystanek.stop_id && Times.trip_id.Replace("\"","") == T.trip_id.Replace("\"", ""))
                    {
                        ostateczneGodziny.Add(Times.departure_time);
                    }
                }
            }
            
            
            ostateczneGodziny.Sort();
           

        }
        public void WysiwetlanieGodzin()
        {
           
            int i = Convert.ToInt32(ostateczneGodziny[0].Substring(0, 2));
           
            Console.Write(i + ":00  |   ");
            foreach (string s in ostateczneGodziny)
            {
                
                if (i==Convert.ToInt32(s.Substring(0,2)))
                {
                    Console.Write(s + "   ");
                    
                }
                else
                {
                    Console.WriteLine();
                    i = Convert.ToInt32(s.Substring(0, 2));
                    
                    if(i<10)Console.Write(i + ":00  |   ");
                    else Console.Write(i + ":00 |   ");
                    Console.Write(s + "   ");
                }
                
            }
        }

    }
}
