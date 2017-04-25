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

        string[] file;
        string[] artistFile;
        string[] userTagFile;
        public Dictionary<int, User> UsersDic = new Dictionary<int, User>();
        public Dictionary<int, Artist> Artists = new Dictionary<int, Artist>();
        List<User> Users = new List<User>();
        Dictionary<int, RoskildeArtist> RoskildeArtists = new Dictionary<int, RoskildeArtist>();

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
            //Reading the different files
            try
            {
                file = File.ReadAllLines(startupPath + @"\DataFiles\user_artists.dat");
                artistFile = File.ReadAllLines(startupPath + @"\DataFiles\artists.dat");
                userTagFile = File.ReadAllLines(startupPath + @"\DataFiles\user_taggedartists.dat");
            }
            catch (Exception)
            {
                throw new Exception($"One or more files are missing at this location: {startupPath}");
            }
        }

        public void ReadArtist()
        {
            // "Artists" dic gets filled with all the artists from the dataset
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
                // Splits up file
                string[] data = line.Split('\t');

                // If the user ID dosn´t allready exsits. A new will be made:
                if (prev != int.Parse(data[0]))
                {
                    User _User = new User(int.Parse(data[0]));
                    Users.Add(_User);
                    index++;
                }

                // Creats a tempoary artist for later use:
                int tempId = int.Parse(data[1]);

                // The artist ID and play count, gets inserted in the user list. A pointer for the allready exsisting artist is created
                Users[index].Artists.Add(tempId, new Userartist(tempId, int.Parse(data[2]), Artists[tempId]));

                // The current ID is moved to Prev
                prev = int.Parse(data[0]);
            }
        }

        public void ReadTags()
        {
            // The tags gets transfered to the artists:
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

            // Read all tags from the dataset
            List<string> tagFile = File.ReadAllLines(startupPath + @"\DataFiles\tags.dat").ToList();

            // Reading the roskilde artists that did not have tags in the dataset
            string[] input = File.ReadAllLines(startupPath + @"\DataFiles\test.txt");

            int inputLength = input.Length;
            for (int i = 0; i < inputLength; i++)
            {
                // If a line starts with "0 ", then it is a new artist
                if (input[i].Contains("0\t"))
                {
                    if (!artistNameList.Contains(input[i].Substring(2)))
                    {
                        // An ID for the new artists can be made, based on the key.
                        int newId = Artists.Keys.Max() + 1;
                        Artists.Add(newId, new Artist(newId, input[i].Substring(2))); // Kunstneren tilføjes.
                        i++;
                        // All the upcomming lines from the file shoud contain a tag, until a new artists arrives with the start letters "0 "
                        while (!input[i].Contains("0\t") && input[i] != (""))
                        {
                            string[] tag = input[i].Split('\t');

                            // If the tag allready excits
                            if (tagFile.Any(x => x.Split('\t')[1].Equals(tag[0])))
                            {
                                //Console.WriteLine("hej");
                                string[] tagLine = tagFile.Single(x => x.Split('\t')[1].Equals(tag[0])).Split('\t');
                                int id = int.Parse(tagLine[0]);
                                // We make sure that there is an artists that contains this tag
                                if (dicTags.ContainsKey(id))
                                {
                                    // The tags are added to the relevent artist, and the number of tags:
                                    Artists[newId].Tags.Add(id, dicTags[id]);
                                    Artists[newId].Tags[id].Amount = int.Parse(tag[1]);
                                }
                            }
                            // If the tag is not in the dataset
                            else
                            {
                                // There is an ID for the new Tag
                                int newTagId = dicTags.Keys.Max() + 1;
                                string newTagName = input[i];
                                // Adding the new tag:
                                dicTags.Add(newTagId, new Tag(newTagId));
                                tagFile.Add(newTagId + "\t" + input[i]);
                                // The tag is added to the relevent artist, and the number of tags.
                                Artists[newId].Tags.Add(newTagId, dicTags[newTagId]);
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
            foreach (var artist in RoskildeArtists)
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
            //TEMP
            foreach (var userr in Users)
                UsersDic.Add(userr.Id, userr);

            BinarySerialization.WriteToBinaryFile<Dictionary<int, User>>(startupPath + @"\DataFiles\users.bin", UsersDic);
            BinarySerialization.WriteToBinaryFile<Dictionary<int, Artist>>(startupPath + @"\DataFiles\artists.bin", Artists);
            BinarySerialization.WriteToBinaryFile<Dictionary<int, RoskildeArtist>>(startupPath + @"\DataFiles\Roskildeartists.bin", RoskildeArtists);
        }

        public void ReadRoskildeSchedule(string date)
        {
            string[] roskildeFile = File.ReadAllLines(startupPath + @"\DataFiles\" + date + ".txt");
            string[] scenes = { "APOLLO", "PAVILION", "AVALON", "ORANGE", "GLORIA", "ARENA", "RISING" };
            string currentScene = default(string);
            DateTime currentTime = default(DateTime);

            foreach (string line in roskildeFile.Skip(1))
            {
                if (scenes.Any(line.Contains))
                {
                    currentScene = line;
                }
                else if (line.Contains(" : "))
                {
                    int day = int.Parse(date.Substring(date.Length - 2));
                    int month = day > 3 ? 6 : 7;
                    int year = 2011;
                    int hour = int.Parse(line.Substring(0, 2));
                    int minute = int.Parse(line.Substring(line.Length - 2));
                    int second = 0;
                    currentTime = new DateTime(year, month, day, hour, minute, second);
                }
                else if (line != "")
                {
                    foreach (var artist in Artists)
                    {
                        if (artist.Value.Name.ToUpper() == line)
                        {
                            RoskildeArtists.Add(artist.Key, new RoskildeArtist(currentTime, currentScene, artist.Value));
                            break;
                        }
                    }
                }
            }
        }
    }
}
