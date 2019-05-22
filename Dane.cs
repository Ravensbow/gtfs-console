using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Net;
using System.IO.Compression;

namespace gtf
{
    class Dane
    {
        public WebClient wc = new WebClient();
        public System.Reflection.Assembly path;
        public string scieszkaGTFS;
        public List<string> pobrane_miasta= new List<string>();
        public List<string> dostepne_miasta = new List<string>() {"Poznan","Szczecin","Wroclaw","Bydgoszcz" };
        public DirectoryInfo GTFS;
        public Dictionary<string, DirectoryInfo> podfoldery = new Dictionary<string, DirectoryInfo>();
        public Dictionary<string, string> urlMiast = new Dictionary<string, string>() {
            { "Poznan", "https://www.ztm.poznan.pl/pl/dla-deweloperow/getGTFSFile" },
            { "Szczecin", "https://www.zditm.szczecin.pl/rozklady/GTFS/latest/google_gtfs.zip" },
            { "Wroclaw", "https://www.wroclaw.pl/open-data/87b09b32-f076-4475-8ec9-6020ed1f9ac0/OtwartyWroclaw_rozklad_jazdy_GTFS.zip" },
            { "Bydgoszcz","https://mkuran.pl/feed/bydgoszcz/bydgoszcz-latest.zip" } };


        public Dane()
        {
            path = Assembly.GetExecutingAssembly();
            //Console.WriteLine(path.Location);
            //Console.WriteLine(path.Location.Remove(path.Location.LastIndexOf("\\"),path.Location.Length-path.Location.LastIndexOf("\\")));
            scieszkaGTFS = path.Location.Remove(path.Location.LastIndexOf("\\"), path.Location.Length - path.Location.LastIndexOf("\\")) + "\\GTFS";
            //Console.WriteLine(scieszkaGTFS);

            if (!Directory.Exists("GTFS")) Directory.CreateDirectory("GTFS");

            pobrane_miasta = PodajMiasta();

            GTFS = new DirectoryInfo(scieszkaGTFS);

            foreach (string s in pobrane_miasta)
            { 
                podfoldery.Add(s, GTFS.CreateSubdirectory(s));
            }

            

            foreach (string s in pobrane_miasta)
            {
               

                //Sprawdzanie Aktualizacji:
                if (SprawdzanieAktualnosci(s)==true)
                {
                    wc.DownloadFile(new System.Uri(urlMiast[s]), GTFS.FullName + "\\" + s + "\\" + s + "GTFS.zip");

                    Console.WriteLine("Pobrano " + s);

                    ZipArchive archive = ZipFile.OpenRead(GTFS.FullName + "\\" + s + "\\" + s + "GTFS.zip");

                    archive.Entries[0].ExtractToFile(Path.Combine(GTFS.FullName + "\\" + s, archive.Entries[0].FullName), true);
                    Console.WriteLine("Wypakowano " + s);
                }
                else
                {
                    Console.WriteLine("Dane miasta " + s + " sa aktualne.");
                }
            }

            //foreach (string s in pobrane_miasta)
            //{
            //    ZipArchive archive = ZipFile.OpenRead(GTFS.FullName + "\\" + s + "\\" + s + "GTFS.zip");

            //    archive.Entries[0].ExtractToFile(Path.Combine(GTFS.FullName + "\\" + s, archive.Entries[0].FullName),true);
            //    Console.WriteLine("Wypakowano " + s);
            //}
        }

        public bool SprawdzanieAktualnosci(string s)
        {
            System.Net.WebRequest req = System.Net.HttpWebRequest.Create(urlMiast[s]);
            req.Method = "HEAD";
            System.Net.WebResponse resp = req.GetResponse();

            long ContentLength = 0;
            long internetsize = -1;

            if (long.TryParse(resp.Headers.Get("Content-Length"), out ContentLength))
            {
                internetsize = ContentLength;
            }

            long localsize = -1;

            if (File.Exists(GTFS.FullName + "\\" + s + "\\" + s + "GTFS.zip")) localsize = new FileInfo(GTFS.FullName + "\\" + s + "\\" + s + "GTFS.zip").Length;

            //Sprawdzanie Aktualizacji:
            if (internetsize != localsize || localsize == -1 || internetsize == -1) { return true; }
            else return false;

        }







        public List<string> PodajMiasta()
        {
            List<string> listawybranych = new List<string>();

            int i = 0;
            Console.WriteLine("Wpisz ciagiem numery interesujacych cie miast:");
            foreach(string s in dostepne_miasta)
            {
                Console.WriteLine(i++ + ". "+s);
            }
            string indeksywybranych = Console.ReadLine();

            foreach(char c in indeksywybranych)
            {
                
                listawybranych.Add(dostepne_miasta[(Convert.ToInt32(c) - 48)]);
            }

            foreach (string s in listawybranych) { Console.WriteLine(s); }

            return listawybranych;
        }
    }
}
