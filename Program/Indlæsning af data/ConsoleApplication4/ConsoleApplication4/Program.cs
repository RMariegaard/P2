using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Recommender
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryData Data = new BinaryData();

            Data.DoItAll();

            var dicArtists = Data.Artists;
            var dicTags = new Dictionary<int, Tag>();
            dicArtists.Values.ToList().ForEach(x => x.Tags.ToList().ForEach(t => dicTags.Add(t.Key, t.Value)));

            List<string> artistNameList = dicArtists.Values.Select(x => x.Name).ToList();
            

            List<string> tagFile = System.IO.File.ReadAllLines(@"C:\Users\" + "Casper" + @"\Documents\GitHub\P2\Program\hetrec2011-lastfm-2k\tags.dat").ToList();

            string[] input = File.ReadAllLines(@"C:\Users\Casper\Documents\GitHub\P2\Tidsplan roskilde\test.txt");

            int inputLength = input.Length;
            for(int i = 0; i < inputLength; i++)
            {
                if(input[i].Contains("0 "))
                {
                    if (!artistNameList.Contains(input[0]))
                    {
                        int newId = Data.Artists.Last().Key + 1;
                        Data.Artists.Add(newId, new Artist(newId, input[i].Substring(2)));
                        i++;
                        while (!input[i].Contains("0 "))
                        {
                            if (tagFile.Contains(input[i]))
                            {
                                string[] tagLine = tagFile.Single(x => x.Contains(input[i])).Split('\t');
                                int id = int.Parse(tagLine[0]);
                                Data.Artists[newId].Tags.Add(id, dicTags[id]);
                            }
                            else
                            {
                                int newTagId = dicTags.Last().Key + 1;
                                string newTagName = input[i];
                                dicTags.Add(newTagId, new Tag(newId));
                                tagFile.Add(newTagId + "\t" + input[i]);
                                Data.Artists[newId].Tags.Add(newTagId, dicTags[newTagId]);

                            }
                            i++;
                        }
                    }

                }
                
            }



            Console.WriteLine("done");
            Console.ReadKey();    
        }
    }
}
