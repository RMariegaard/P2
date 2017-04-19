using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Recommender
{
    public class CreateRecommendations
    {
        public Dictionary<int, User> Users;
        Dictionary<int, Artist> Artists;
        List<RoskildeArtist> RoskildeArtists;

        public List<string> LoadFiles()
        {
            string startupPath = Environment.CurrentDirectory;
            //Does it twice to go back two folders
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            Console.WriteLine("Reading File");
            Users = BinarySerialization.ReadFromBinaryFile<Dictionary<int, User>>(startupPath + @"\DataFiles\users.bin");
            Artists = BinarySerialization.ReadFromBinaryFile<Dictionary<int, Artist>>(startupPath + @"\DataFiles\artists.bin");
            RoskildeArtists = BinarySerialization.ReadFromBinaryFile<List<RoskildeArtist>>(startupPath + @"\DataFiles\Roskildeartists.bin");
            
            Console.WriteLine("Done Reading File");
            List<string> RoskildeNames = new List<string>();
            RoskildeArtists.ForEach(x => RoskildeNames.Add(x.thisArtist.Name));

            Console.WriteLine("Roskilde artits:");
            foreach (string item in RoskildeNames)
            {
                Console.WriteLine(item);
            }

            return RoskildeNames;
        }

        Dictionary<int, RecommendedArtist> recommendedArtists;
        Dictionary<int, RecommendedArtist> recommendedArtists2;
        public List<string> Recommender(int id)
        {
            var cosine = new Cosine();
            User newUser = new User(0);
            List<string> streng = new List<string>();

            if (Users.ContainsKey(id))
                newUser = Users[id];
            

            //StringBuilder streng = new StringBuilder();

            recommendedArtists = CollaborativeFiltering.RecommendArtists(newUser, Users, RoskildeArtists);// ContentBasedFiltering.RecommedArtists(cosine.GetCosine, newUser, RoskildeArtists, 10); 
            streng.Add("Collarborative");
            recommendedArtists.OrderByDescending(x => x.Value.CollaborativeFilteringRating).ToList().ForEach(x => streng.Add(x.Value.thisArtist.Name/* + " - " + x.Value.CollaborativeFilteringRating*/));

            recommendedArtists2 = ContentBasedFiltering.RecommedArtists(cosine.GetCosine, newUser, RoskildeArtists, 10);
            streng.Add("\nContentBasedFiltering: ");
            recommendedArtists2.OrderByDescending(x => x.Value.ContentBasedFilteringRating).ToList().ForEach(x => streng.Add(x.Value.thisArtist.Name/* + " - " + x.Value.ContentBasedFilteringRating*/));
            
            //streng.Add(" ---------");
            //newUser.Artists.OrderByDescending(x => x.Value.Weight).ToList().ForEach(x => streng.Add(x.Value.ThisArtist.Name + " - " + x.Value.Weight));

            foreach (string item in streng)
            {
                Console.WriteLine(item);
            }
            return streng;
            //Console.ReadKey();
            //Console.Clear();
        }

        public void InformationOnArtist(string Name)
        {
            RecommendedArtist InfoArtist;
            ConvertIdName Convert = new ConvertIdName();

            foreach (var item in recommendedArtists2)
            {
                if (item.Value.thisArtist.Name == Name)
                {
                    InfoArtist = item.Value;
                    break;
                }
            }
        }
    }
}
