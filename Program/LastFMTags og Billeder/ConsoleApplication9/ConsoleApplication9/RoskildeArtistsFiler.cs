using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    class RoskildeArtistsFiler
    {
        public static string s26 = (@"C:\Users\Casper\Documents\GitHub\P2\Tidsplan roskilde\Søndag 26.txt");
        public static string m27 = (@"C:\Users\Casper\Documents\GitHub\P2\Tidsplan roskilde\Mandag 27.txt");
        public static string t28 = (@"C:\Users\Casper\Documents\GitHub\P2\Tidsplan roskilde\Tirsdag 28.txt");
        public static string o29 = (@"C:\Users\Casper\Documents\GitHub\P2\Tidsplan roskilde\Onsdag 29.txt");
        public static string t30 = (@"C:\Users\Casper\Documents\GitHub\P2\Tidsplan roskilde\Tordsag 30.txt");
        public static string f01 = (@"C:\Users\Casper\Documents\GitHub\P2\Tidsplan roskilde\Fredag 01.txt");
        public static string l02 = (@"C:\Users\Casper\Documents\GitHub\P2\Tidsplan roskilde\Lørdag 02.txt");
        public static string s03 = (@"C:\Users\Casper\Documents\GitHub\P2\Tidsplan roskilde\Søndag 03.txt");

        public static List<string> allFiles =  new List<string>(){ s26, m27, t28, o29, t30, f01, l02, s03 };
    }
}
