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
        private Dictionary<int, User> _users;
        private Dictionary<int, Artist> _artists;
        private Dictionary<int, RoskildeArtist> _roskildeArtists;
        private ICollaborativeFiltering _collaborativeFiltering;
        private IContentBasedFiltering _contentBasedFiltering;

        public Dictionary<int, RecommendedArtist> RecommendedCollabArtists { get; private set; }
        public Dictionary<int, RecommendedArtist> RecommendedContetArtists { get; private set; }

        public CreateRecommendations(IContentBasedFiltering contentBasedFiltering, ICollaborativeFiltering collaborativeFiltering)
        {
            _collaborativeFiltering = collaborativeFiltering;
            _contentBasedFiltering = contentBasedFiltering;
        }

        public CreateRecommendations(IContentBasedFiltering contentBasedFiltering, ICollaborativeFiltering collaborativeFiltering, Dictionary<int, User> users, Dictionary<int, Artist> artists, Dictionary<int, RoskildeArtist> roskildeArtists) :this(contentBasedFiltering, collaborativeFiltering)
        {
            _users = users;
            _artists = artists;
            _roskildeArtists = roskildeArtists;
        }

        public bool CheckForUserId(int id)
        {
            return _users.ContainsKey(id);
        }

        public User GetUser(int id)
        {
            return _users[id];
        }

        public List<RoskildeArtist> GetRoskildeArtists()
        {
            List<RoskildeArtist> RoskildeNames = new List<RoskildeArtist>();
            _roskildeArtists.Values.ToList().ForEach(x => RoskildeNames.Add(x));
            return RoskildeNames;
        }

        public void LoadFiles()
        {
            string startupPath = Environment.CurrentDirectory;
            //Does it twice to go back two folders
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            Console.WriteLine("Reading File");
            _users = BinarySerialization.ReadFromBinaryFile<Dictionary<int, User>>(startupPath + @"\DataFiles\users.bin");
            _artists = BinarySerialization.ReadFromBinaryFile<Dictionary<int, Artist>>(startupPath + @"\DataFiles\artists.bin");
            _roskildeArtists = BinarySerialization.ReadFromBinaryFile<Dictionary<int, RoskildeArtist>>(startupPath + @"\DataFiles\Roskildeartists.bin");
            Console.WriteLine("Done Reading File");
        }

        public void GenerateRecommendations(int id)
        {
            var cosine = new Cosine();
            var pearson = new PearsonCor();
            User newUser = new User(0);

            if (_users.ContainsKey(id))
            {
                newUser = _users[id];
            }
                
            StringBuilder streng = new StringBuilder();

            RecommendedCollabArtists = _collaborativeFiltering.RecommendArtists(pearson.CalculateCorrelation, newUser, _users, _roskildeArtists, _artists);
            streng.AppendLine("Collarborative");
            RecommendedCollabArtists.OrderByDescending(x => x.Value.CollaborativeFilteringRating).ToList().ForEach(x => streng.AppendLine(x.Value.Name + " - " + x.Value.CollaborativeFilteringRating));

            RecommendedContetArtists = _contentBasedFiltering.RecommedArtists(cosine.GetCosine, newUser, _roskildeArtists, 10);
            streng.AppendLine("\nContentBasedFiltering: ");
            RecommendedContetArtists.OrderByDescending(x => x.Value.ContentBasedFilteringRating).ToList().ForEach(x => streng.AppendLine(x.Value.Name + " - " + x.Value.ContentBasedFilteringRating));

            streng.AppendLine(" ---------");
            newUser.Artists.OrderByDescending(x => x.Value.Weight).ToList().ForEach(x => streng.AppendLine(x.Value.ThisArtist.Name + " - " + x.Value.Weight));

            Console.WriteLine(streng);
        }
    }
}
