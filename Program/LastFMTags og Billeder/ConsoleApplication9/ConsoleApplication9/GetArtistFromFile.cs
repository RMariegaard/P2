using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication9
{

    class GetArtistFromFile
    {

        public static List<string> Names(string date)
        {
            List<string> list = new List<string>();

            string[] file = File.ReadAllLines(date, Encoding.Default);

            string[] scenes = { "APOLLO", "PAVILION", "AVALON", "ORANGE", "GLORIA", "ARENA", "RISING" };

            foreach(string linje in file.Skip(1))
            {
                if (linje == "DÃM-FUNK with MASTER BLAZTER") list.Add("DÃM-FUNK");
                else if (linje.Contains("MATTHEW DEAR")) list.Add("MATTHEW DEAR");
                else if (linje.Contains("MAGNETIC MAN")) list.Add("MAGNETIC MAN");
                else if (linje.Contains("THE EX")) list.Add("THE EX");
                else if (linje.Contains("CONGOTRONICS vs ROCKERS")) list.Add("CONGOTRONICS vs ROCKERS");
                else if (linje.Contains("ELEKTRO")) list.Add("ELEKTRO");
                else if (linje.Contains("PRINCE FATTY SOUNDSYSTEM")) list.Add("PRINCE FATTY SOUNDSYSTEM");
                else if (linje.Contains("TARRUS RILEY")) list.Add("TARRUS RILEY");
                else if (linje == "" || linje == "\n" || linje.Contains(" : ") || (linje.Contains("ANIBAL VELASQUEZ Y LOS LOCOS DEL SWING")))
                {
                    //DO nothing
                }
                else if (scenes.Any(linje.Contains))
                {
                    //Also do nothing
                }
                else
                {
                    list.Add(linje);
                }

            }
            return list;
        }

    }
}


