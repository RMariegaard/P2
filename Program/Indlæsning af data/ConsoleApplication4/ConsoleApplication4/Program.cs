using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fil Indlæsning:
            string[] fil = System.IO.File.ReadAllLines(@"C:\Users\Lasse\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\user_artists.dat");
            string[] kunst_fil = System.IO.File.ReadAllLines(@"C:\Users\Lasse\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\artists.dat");
            string[] tag_fil = System.IO.File.ReadAllLines(@"C:\Users\Lasse\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\user_taggedartists.dat");

            // Initialisering af User list og Kunstner Arrayet:
            List<User> Users = new List<User>();
            Artist[] kunstere = new Artist[17632];

            // "kunstnere" array udfyldes med alle kunstnere i datasættet:
            int i = 0;
            foreach (var streng in kunst_fil.Skip(1))
            {
                string[] data = streng.Split('\t');
                Artist ny = new Artist(int.Parse(data[0]), data[1]);

                ny.Id = int.Parse(data[0]);
                ny.Name = data[1];
                kunstere.SetValue(ny, i);
                i++;
            }

            // Users listen bliver udfyldt:
            // 0 = bruger ID, 1 = Artist Id, 2 = weight
            int prev = 0;
            int index = -1;
            var fil1 = fil.Skip(1);

            foreach (var linje in fil1)
            {
                // Filen opsplittes:
                string[] data = linje.Split('\t');  
               
                // Hvis ikke User ID allerede eksisteres laves der en ny User:
                if (prev != int.Parse(data[0]))
                {
                    User _User = new User();
                    _User.Id = int.Parse(data[0]);
                    Users.Add(_User);
                    index++;
                }

                // Artist ID og antal afspilninger bliver indsat i Users Artist liste:
                Users[index].Artists.Add(new Userartist(int.Parse(data[1]), int.Parse(data[2])));

                // Det nuværende ID sættes over I Prev:
                prev = int.Parse(data[0]);

            }





            Console.Read();
        }
    }
}
