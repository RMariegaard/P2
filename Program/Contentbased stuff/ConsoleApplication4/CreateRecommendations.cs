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
        private Dictionary<int, User> Users;
        Dictionary<int, Artist> Artists;
        Dictionary<int, RoskildeArtist> RoskildeArtists;

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
            Console.Clear();
        }

        public void Recommender()
        {
            var cosine = new Cosine();
            int id = 0;
            User newUser = new User(0);

            Console.WriteLine("ID: ");
            while (!int.TryParse(Console.ReadLine(), out id)) ;

            if (Users.ContainsKey(id))
                newUser = Users[id];

            StringBuilder streng = new StringBuilder();

            var recommendedArtists = CollaborativeFiltering.RecommendArtists(newUser, Users, RoskildeArtists);// ContentBasedFiltering.RecommedArtists(cosine.GetCosine, newUser, RoskildeArtists, 10); 
            streng.AppendLine("Collarborative");
            recommendedArtists.OrderByDescending(x => x.Value.CollaborativeFilteringRating).ToList().ForEach(x => streng.AppendLine(x.Value.thisArtist.Name + " - " + x.Value.CollaborativeFilteringRating));

            var recommendedArtists2 = ContentBasedFiltering.RecommedArtists(cosine.GetCosine, newUser, RoskildeArtists, 10);
            streng.AppendLine("\nContentBasedFiltering: ");
            recommendedArtists2.OrderByDescending(x => x.Value.ContentBasedFilteringRating).ToList().ForEach(x => streng.AppendLine(x.Value.thisArtist.Name + " - " + x.Value.ContentBasedFilteringRating));
        
            streng.AppendLine(" ---------");
            newUser.Artists.OrderByDescending(x => x.Value.Weight).ToList().ForEach(x => streng.AppendLine(x.Value.ThisArtist.Name + " - " + x.Value.Weight));

            Console.WriteLine(streng);
            Console.ReadKey();
            Console.Clear();
        }
    }
}
