using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class BinaryData
    {
        string Username;
        string[] file;
        string[] artist_file;
        string[] tag_fil;
        public Dictionary<int, User> UsersDic = new Dictionary<int, User>();
        public Dictionary<int, Artist> Artists = new Dictionary<int, Artist>();
        List<User> Users = new List<User>();

        public void DoItAll()
        {
            ReadFile();
            ReadArtist();
            ReadUsers();
            ReadTags();
            CalculateWeight();
            WriteToFile();
        }

        public void ReadFile()
        {
            // Fil Indlæsning:
            Username = Environment.UserName;
            file = System.IO.File.ReadAllLines(@"C:\Users\" + Username + @"\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\user_artists.dat");
            artist_file = System.IO.File.ReadAllLines(@"C:\Users\" + Username + @"\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\artists.dat");
            tag_fil = System.IO.File.ReadAllLines(@"C:\Users\" + Username + @"\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\user_taggedartists.dat");
        }
        
        public void ReadArtist()
        {
            // "Artists" dic udfyldes med alle kunstnere i datasættet:
            foreach (var String in artist_file.Skip(1))
            {
                string[] data = String.Split('\t');
                Artists.Add(int.Parse(data[0]), new Artist(int.Parse(data[0]), data[1]));
            }
        }

        public void ReadUsers()
        {
            int prev = 0;
            int index = -1;
            var file1 = file.Skip(1);

            foreach (var line in file1)
            {
                // Filen opsplittes:
                string[] data = line.Split('\t');

                // Hvis ikke User ID allerede eksisteres laves der en ny User:
                if (prev != int.Parse(data[0]))
                {
                    User _User = new User(int.Parse(data[0]));
                    Users.Add(_User);
                    index++;
                }

                // Der laves en tempartist til senere:
                int tempId = int.Parse(data[1]);

                // Artist ID og antal afspilninger bliver indsat i Users Artist liste. Samtidig bliver der oprettet en "pointer" til den instans af artist der allerede eksisterer:
                Users[index].Artists.Add(tempId, new Userartist(tempId, int.Parse(data[2]), Artists[tempId]));

                // Det nuværende ID sættes over I Prev:
                prev = int.Parse(data[0]);
            }
        }

        public void ReadTags()
        {
            // Tags bliver overført til respektive kunstnere:
            int TempTagID;
            foreach (string streng in tag_fil.Skip(1))
            {
                string[] data = streng.Split('\t');
                TempTagID = int.Parse(data[2]);
                foreach (var artist in Artists)
                {
                    if (int.Parse(data[1]) == artist.Value.Id)
                    {
                        if (artist.Value.Tags.ContainsKey(TempTagID))
                            artist.Value.Tags[TempTagID].Amount++;
                        else
                            artist.Value.Tags.Add(TempTagID, new Tag(TempTagID));
                        
                        break;
                    }
                }
            }
        }

        public void CalculateWeight()
        {
            foreach (var artist in Artists)
            {
                artist.Value.CalcTagWeight();
            }

            foreach (User user in Users)
            {
                user.TagCalc();
                user.CalculateArtistWeight();
            }
        }
        
        public void WriteToFile()
        {
            //Midlertidigt
            foreach (var userr in Users)
                UsersDic.Add(userr.Id, userr);

            BinarySerialization.WriteToBinaryFile<Dictionary<int, User>>(@"C: \Users\" + Username + @"\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\users.bin", UsersDic);
            BinarySerialization.WriteToBinaryFile<Dictionary<int, Artist>>(@"C: \Users\" + Username + @"\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\artists.bin", Artists);
        }
    }
}
