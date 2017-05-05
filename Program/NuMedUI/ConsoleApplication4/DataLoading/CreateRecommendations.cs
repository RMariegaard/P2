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
        public CreateRecommendations()
        {

        }
        public CreateRecommendations(Dictionary<int, User> users, Dictionary<int, Artist> artists, Dictionary<int, RoskildeArtist> roskildeArtists)
        {
            Users = users;
            Artists = artists;
            RoskildeArtists = roskildeArtists;
        }
        private Dictionary<int, User> Users;
        Dictionary<int, Artist> Artists;
        Dictionary<int, RoskildeArtist> RoskildeArtists;

        public bool CheckForUserId(int id)
        {
            return Users.ContainsKey(id);
        }

        public User GetUser(int id)
        {
            return Users[id];
        }

        public List<RoskildeArtist> GetRoskildeArtists()
        {
            List<RoskildeArtist> RoskildeNames = new List<RoskildeArtist>();
            RoskildeArtists.Values.ToList().ForEach(x => RoskildeNames.Add(x));
            return RoskildeNames;
        }

        public void LoadFiles()
        {
            string startupPath = Environment.CurrentDirectory;
            //Does it twice to go back two folders
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            Console.WriteLine("Reading File");
            Users = BinarySerialization.ReadFromBinaryFile<Dictionary<int, User>>(startupPath + @"\DataFiles\users.bin");
            Artists = BinarySerialization.ReadFromBinaryFile<Dictionary<int, Artist>>(startupPath + @"\DataFiles\artists.bin");
            RoskildeArtists = BinarySerialization.ReadFromBinaryFile<Dictionary<int, RoskildeArtist>>(startupPath + @"\DataFiles\Roskildeartists.bin");
            Console.WriteLine("Done Reading File");
        }

        Dictionary<int, RecommendedArtist> recommendedArtists;
        Dictionary<int, RecommendedArtist> recommendedArtists2;

        public Dictionary<int, RecommendedArtist> GetCollabRecommendedArtists()
        {
            return recommendedArtists;
        }
        public Dictionary<int, RecommendedArtist> GetcontentRecommendedArtists()
        {
            return recommendedArtists2;
        }

        public void Recommender(int id)
        {
            var cosine = new Cosine();
            var pearson = new PearsonCor();
            User newUser = new User(0);

            if (Users.ContainsKey(id))
            {
                newUser = Users[id];
            }
            else
            {
                //der skal være en popup
            }
                
            StringBuilder streng = new StringBuilder();

            recommendedArtists = CollaborativeFiltering.RecommendArtists(pearson.CalculateUser, newUser, Users, RoskildeArtists);// ContentBasedFiltering.RecommedArtists(cosine.GetCosine, newUser, RoskildeArtists, 10); 
            streng.AppendLine("Collarborative");
            recommendedArtists.OrderByDescending(x => x.Value.CollaborativeFilteringRating).ToList().ForEach(x => streng.AppendLine(x.Value.Name + " - " + x.Value.CollaborativeFilteringRating));

            recommendedArtists2 = ContentBasedFiltering.RecommedArtists(cosine.GetCosine, newUser, RoskildeArtists, 10);
            streng.AppendLine("\nContentBasedFiltering: ");
            recommendedArtists2.OrderByDescending(x => x.Value.ContentBasedFilteringRating).ToList().ForEach(x => streng.AppendLine(x.Value.Name + " - " + x.Value.ContentBasedFilteringRating));

            streng.AppendLine(" ---------");
            newUser.Artists.OrderByDescending(x => x.Value.Weight).ToList().ForEach(x => streng.AppendLine(x.Value.ThisArtist.Name + " - " + x.Value.Weight));

            Console.WriteLine(streng);
        }
    }
}
