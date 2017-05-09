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
        //Getting the path for the location of the text files from data set
        string _startupPath = Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory));

        public Func<string, int> getArtistIdFromString = line => int.Parse(line.Split('\t')[1]);

        string[] _userFile;
        string[] _artistFile;
        string[] _userTagFile;
        private Dictionary<int, string> _dicArtistNames = new Dictionary<int, string>();
        public Dictionary<int, User> Users = new Dictionary<int, User>();
        public Dictionary<int, Artist> Artists = new Dictionary<int, Artist>();
        public Dictionary<int, RoskildeArtist> RoskildeArtists = new Dictionary<int, RoskildeArtist>();

        public void MakeBinaryFiles()
        {
            //Names for text files of roskilde scheduele
            List<string> listOfDates = new List<string>()
            { "Søndag 26", "Mandag 27", "Tirsdag 28", "Onsdag 29", "Torsdag 30", "Fredag 01", "Lørdag 02", "Søndag 03" };

            ReadFiles();
            MakeArtistDictornary();
            ReadTags();
            ReadArtist();
            ReadUsers();
            MakeRemainingRoskildeArtists();
            foreach (string date in listOfDates)
            {
               ReadRoskildeSchedule(date);
             }
             CalculateWeight();
            WriteToFile();
        }

        public void ReadFiles()
        {
            //Reading the different files as string arrays to the fields
            try
            {
                _userFile = File.ReadAllLines(_startupPath + @"\DataFiles\user_artists.dat");
                _artistFile = File.ReadAllLines(_startupPath + @"\DataFiles\artists.dat");
                _userTagFile = File.ReadAllLines(_startupPath + @"\DataFiles\user_taggedartists.dat");
            }
            catch (Exception)
            {
                throw new Exception($"One or more files are missing at this location: {_startupPath}");
            }
        }

        private void MakeArtistDictornary()
        {
            int artistID;
            string[] lineInArtistFile;
            string artistName;
            foreach(string line in _artistFile.Skip(1))
            {
                lineInArtistFile = line.Split('\t');
                artistID = int.Parse(lineInArtistFile[0]);
                artistName = lineInArtistFile[1];
                _dicArtistNames.Add(artistID, artistName);
            }
        }

        private void ReadArtist()
        {
            string[] lineInArtistFile;
            int artistID;
            string artistName;
            // "Artists" dictonary gets filled with all the artists from the dataset
            foreach (var line in _artistFile.Skip(1))
            {
                lineInArtistFile = line.Split('\t');
                artistID = int.Parse(lineInArtistFile[0]);
                artistName = lineInArtistFile[1];
                if (Artists.ContainsKey(artistID) == false)
                {
                    Artists.Add(artistID, new Artist(artistID, artistName));
                }
            }
        }

        private void ReadUsers()
        {
            int id = 2;
            int count = 0;
 
            foreach (var line in _userFile.Skip(1))
            {
                // Splits up file
                string[] data = line.Split('\t');
                id = int.Parse(data[0]);
                // If the user ID dosn´t allready exsits. A new will be made:
                if (!Users.ContainsKey(id))
                {
                    User user = new User(id);
                    Users.Add(id, user);
                }

                int ArtistId = int.Parse(data[1]);

                // The artist ID and play count, gets inserted in the user list. A pointer for the allready exsisting artist is created
                Users[id].Artists.Add(ArtistId, new Userartist(ArtistId, int.Parse(data[2]), Artists[ArtistId]));
            }
        }

        private void ReadTags()
        {
            
            // The tags gets transfered to the artists:
            int tagID;
            int artistID = 1;
            int newArtistID;
            Dictionary<int, Tag> dicArtistTags = new Dictionary<int, Tag>();

            string[] tagFile = _userTagFile.ToArray();

            var sortedTagFile = tagFile.Skip(1).Take(tagFile.Length).OrderBy(getArtistIdFromString).ToArray();

            foreach (string streng in sortedTagFile)
            {
                string[] data = streng.Split('\t');
                tagID = int.Parse(data[2]);
                newArtistID = int.Parse(data[1]);

                if(artistID != newArtistID)
                {
                    if (_dicArtistNames.ContainsKey(artistID) == true)
                    {
                        Artists.Add(artistID, new Artist(artistID, dicArtistTags, _dicArtistNames[artistID]));
                    }
                        artistID = newArtistID;
                        dicArtistTags = new Dictionary<int, Tag>();                  
                }

                if (dicArtistTags.ContainsKey(tagID))
                {
                    dicArtistTags[tagID].Amount++;
                }
                else
                {
                    dicArtistTags.Add(tagID, new Tag(tagID));
                }
                
            }

        }

        private void MakeRemainingRoskildeArtists()
        {

            //Creates dictonary of all tags
            var dicTags = new Dictionary<int, Tag>();
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

            //Get a list of string with the names of the artist
            List<string> artistNameList = Artists.Values.Select(x => x.Name).ToList();

            // Read all tags from the dataset
            List<string> tagFile = File.ReadAllLines(_startupPath + @"\DataFiles\tags.dat").ToList();

            // Reading the roskilde artists that did not have tags in the dataset
            // File is created using Last.FM api
            string[] inputFile = File.ReadAllLines(_startupPath + @"\DataFiles\test.txt");

            int fileLength = inputFile.Length;
            for (int i = 0; i < fileLength; i++)
            {
                // If a line starts with "0 ", then it is a new artist
                if (inputFile[i].Contains("0\t"))
                {
                    if (!artistNameList.Contains(inputFile[i].Substring(2)))
                    {
                        // An ID for the new artists can be made, based on the key.
                        int newId = Artists.Keys.Max() + 1;
                        string name = inputFile[i].Substring(2);
                        Artists.Add(newId, new Artist(newId, name)); 
                        i++;
                        // All the upcomming lines from the file shoud contain a tag, until a new artists arrives with the start letters "0 "
                        while (!inputFile[i].Contains("0\t") && inputFile[i] != (""))
                        {
                            string[] tag = inputFile[i].Split('\t');
                            string tagName = tag[0];
                            int tagAmount = int.Parse(tag[1]);
                            var exsistingTagNames = tagFile.Select(x => x.Split('\t')[1]);
                            // If the tag allready excits
                            if (exsistingTagNames.Contains(tagName))
                            {
                                //Gets a line from the tag database file where the tagname matches and then splits the line
                                string[] tagLine = tagFile.Single(x => x.Split('\t')[1] == tagName).Split('\t');
                                int id = int.Parse(tagLine[0]);
                                // We make sure that there is an artists that contains this tag
                                if (dicTags.ContainsKey(id))
                                {
                                    // The tags are added to the relevent artist, and the number of tags:
                                    Artists[newId].Tags.Add(id, dicTags[id]);
                                    Artists[newId].Tags[id].Amount = tagAmount;
                                }
                            }
                            // If the tag is not in the dataset
                            else
                            {
                                // There is an ID for the new Tag
                                int newTagId = dicTags.Keys.Max() + 1;
                                string newTagName = tagName;
                                // Adding the new tag:
                                dicTags.Add(newTagId, new Tag(newTagId));
                                tagFile.Add(newTagId + "\t" + newTagName);
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

        private void CalculateWeight()
        {
            foreach (var artist in RoskildeArtists.Values)
            {
                artist.CalcTagWeight();
            }

            foreach (var user in Users.Values)
            {
                user.UserTagHandling();
                user.CalculateArtistWeight();
            }
        }
        /*
        public void CalcCorrelationAverage(out double totalAvgContentRating, out double totalAvgCollabRating)
        {
            CreateRecommendations Recommender = new CreateRecommendations(Users, Artists, RoskildeArtists);

            Dictionary<int, RecommendedArtist> collab;
            Dictionary<int, RecommendedArtist> content;
            List<RecommendedArtist> array = new List<RecommendedArtist>();
            List<RecommendedArtist> array1 = new List<RecommendedArtist>();
            for (int i = 0; i < 2500; i++)
            {
                Recommender.Recommender(i);
                collab = Recommender.GetCollabRecommendedArtists();
                content = Recommender.GetcontentRecommendedArtists();
                if (collab != null && collab.Count() != 0)
                {
                    foreach (var element in collab)
                    {
                        element.Value.userID = i;
                        array.Add(element.Value);
                    }
                }
                if (content != null && content.Count() != 0)
                {
                    foreach (var element in content)
                    {
                        element.Value.userID = i;
                        array1.Add(element.Value);
                    }
                }
            }

            array1 = array1.OrderByDescending(x => x.ContentBasedFilteringRating).ToList();
            array = array.OrderByDescending(x => x.CollaborativeFilteringRating).ToList();

            totalAvgCollabRating = array.Sum(x => x.CollaborativeFilteringRating) / array.Count();
            totalAvgContentRating = array1.Sum(x => x.ContentBasedFilteringRating) / array1.Count();
        }*/

        private void WriteToFile()
        {

            BinarySerialization.WriteToBinaryFile<Dictionary<int, User>>(_startupPath + @"\DataFiles\users.bin", Users);
            BinarySerialization.WriteToBinaryFile<Dictionary<int, Artist>>(_startupPath + @"\DataFiles\artists.bin", Artists);
            BinarySerialization.WriteToBinaryFile<Dictionary<int, RoskildeArtist>>(_startupPath + @"\DataFiles\Roskildeartists.bin", RoskildeArtists);
        }

        private void ReadRoskildeSchedule(string date)
        {
            string[] roskildeFile = File.ReadAllLines(_startupPath + @"\DataFiles\" + date + ".txt");
            string[] scenes = { "APOLLO", "PAVILION", "AVALON", "ORANGE", "GLORIA", "ARENA", "RISING" };
            string currentScene = default(string);
            DateTime currentTime = default(DateTime);

            foreach (string line in roskildeFile.Skip(1))
            {
                //if the line is a scene
                if (scenes.Any(line.Contains))
                {
                    currentScene = line;
                }
                //if the line contains : then is it playing time
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
                //else if it is not empty then it is an artist
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
