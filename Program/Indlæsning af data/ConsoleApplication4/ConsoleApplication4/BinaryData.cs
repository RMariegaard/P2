using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Recommender
{
    public class BinaryData
    {
        string startupPath = Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory));

        string Username;
        string[] file;
        string[] artistFile;
        string[] userTagFile;
        public Dictionary<int, User> UsersDic = new Dictionary<int, User>();
        public Dictionary<int, Artist> Artists = new Dictionary<int, Artist>();
        List<User> Users = new List<User>();
        List<RoskildeArtist> RoskildeArtists = new List<RoskildeArtist>();

        public void DoItAll()
        {

            List<string> listOfDates = new List<string>()
            { "Søndag 26", "Mandag 27", "Tirsdag 28", "Onsdag 29", "Torsdag 30", "Fredag 01", "Lørdag 02", "Søndag 03" };

            ReadFile();
            ReadArtist();
            ReadUsers();
            ReadTags();
            MakeRemainingRoskildeArtists();
            foreach (string date in listOfDates)
            {
                ReadRoskildeSchedule(date);
            }
            CalculateWeight();
            WriteToFile();
        }

        public void ReadFile()
        {
            // Fil Indlæsning:
            Username = Environment.UserName;
            file = File.ReadAllLines(startupPath + @"\user_artists.dat");
            artistFile = File.ReadAllLines(startupPath + @"\artists.dat");
            userTagFile = File.ReadAllLines(startupPath + @"\user_taggedartists.dat");
        }
        
        public void ReadArtist()
        {
            // "Artists" dic udfyldes med alle kunstnere i datasættet:
            foreach (var String in artistFile.Skip(1))
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
            foreach (string streng in userTagFile.Skip(1))
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

        public void MakeRemainingRoskildeArtists()
        {
            // var dicArtists = Data.Artists;
            var dicTags = new Dictionary<int, Tag>();
            // dicArtists.Values.ToList().ForEach(x => x.Tags.ToList().ForEach(t => dicTags.Add(t.Key, t.Value)));

            foreach (var artist in Artists.Values)
            {
                foreach (var tag in artist.Tags)
                {
                    if (!dicTags.ContainsKey(tag.Key))
                    {
                        dicTags.Add(tag.Key, tag.Value);
                    }
                }
            }

            List<string> artistNameList = Artists.Values.Select(x => x.Name).ToList();
     
            // Indlæsning af alle tags fra datasættet:
            List<string> tagFile = File.ReadAllLines(startupPath + @"\tags.dat").ToList();

            //Indlæsning af de kunstnere der ikke havde tags fra datasættet, og som spiller til Roskilde:
            string[] input = File.ReadAllLines(startupPath + @"\test.txt");

            int inputLength = input.Length;
            for (int i = 0; i < inputLength; i++)
            {
                if (input[i].Contains("0\t")) // Hvis en linje starter med "0 " er det en ny kunstner:
                {
                    if (!artistNameList.Contains(input[i].Substring(2)))
                    {
                        int newId = Artists.Keys.Max() + 1; // Der findes et ID til den nye kunstner baseret på key:
                        Artists.Add(newId, new Artist(newId, input[i].Substring(2))); // Kunstneren tilføjes.
                        i++;
                        while (!input[i].Contains("0\t") && input[i] != ("")) // Alle efterfølgende linjer burde indeholde Tags indtil "0 ":
                        {
                            string[] tag = input[i].Split('\t');

                            // Hvis tagget allerede eksisterer:
                            if (tagFile.Any(x => x.Split('\t')[1].Equals(tag[0])))
                            {
                                Console.WriteLine("hej");
                                string[] tagLine = tagFile.Single(x => x.Split('\t')[1].Equals(tag[0])).Split('\t');
                                int id = int.Parse(tagLine[0]);
                                if (dicTags.ContainsKey(id)) //Det  kan være at ingen artist er tagget med tagget
                                {
                                    Artists[newId].Tags.Add(id, dicTags[id]); // Den relevante kunstner for tilføjet tagget og antallet af tags:
                                    Artists[newId].Tags[id].Amount = int.Parse(tag[1]);
                                }
                            }
                            // Hvis tagget ikke på forhånd eksisterer i datasættet:
                            else
                            {
                                int newTagId = dicTags.Keys.Max() + 1; // Der findes et ID til det nye tag:
                                string newTagName = input[i];
                                dicTags.Add(newTagId, new Tag(newTagId)); // Det tilføjes:
                                tagFile.Add(newTagId + "\t" + input[i]);
                                Artists[newId].Tags.Add(newTagId, dicTags[newTagId]); // Den relevante kunstner for tilføjet tagget og antallet af tags:
                                Artists[newId].Tags[newTagId].Amount = int.Parse(tag[1]);
                            }
                            i++;
                        }
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

            BinarySerialization.WriteToBinaryFile<Dictionary<int, User>>(startupPath + @"\users.bin", UsersDic);
            BinarySerialization.WriteToBinaryFile<Dictionary<int, Artist>>(startupPath + @"\artists.bin", Artists);
            BinarySerialization.WriteToBinaryFile<List<RoskildeArtist>>(startupPath + @"\Roskildeartists.bin", RoskildeArtists);
        }

        public void ReadRoskildeSchedule(string date)
        {
            string[] roskildeFile = File.ReadAllLines(startupPath + @"\" + date + ".txt");
            string[] scenes = { "APOLLO", "PAVILION", "AVALON", "ORANGE", "GLORIA", "ARENA", "RISING" };
            string currentScene = default(string);
            DateTime currentTime = default(DateTime);

            foreach (string line in roskildeFile.Skip(1))
            {
                if (scenes.Any(line.Contains))
                {
                    currentScene = line;
                }
                else if(line.Contains(" : "))
                {
                    int day = int.Parse(date.Substring(date.Length - 2));
                    int month = day > 3 ? 6 : 7;
                    int year = 2011;
                    int hour = int.Parse(line.Substring(0,2));
                    int minute = int.Parse(line.Substring(line.Length - 2));
                    int second = 0;
                    currentTime = new DateTime(year, month, day, hour, minute, second);
                }
                else if(line != "")
                {
                    foreach(var artist in Artists)
                    {
                        if(artist.Value.Name.ToUpper() == line)
                        {                                             
                            RoskildeArtists.Add(new RoskildeArtist(artist.Value.Id, currentTime, currentScene, artist.Value));
                            break;
                        }
                    }
                }
            }

            

        }

    }
}
