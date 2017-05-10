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
        private List<double> _collabRating;
        private List<double> _contentRating;

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

            _users = BinarySerialization.ReadFromBinaryFile<Dictionary<int, User>>(startupPath + @"\DataFiles\users.bin");
            _artists = BinarySerialization.ReadFromBinaryFile<Dictionary<int, Artist>>(startupPath + @"\DataFiles\artists.bin");
            _roskildeArtists = BinarySerialization.ReadFromBinaryFile<Dictionary<int, RoskildeArtist>>(startupPath + @"\DataFiles\Roskildeartists.bin");
            _collabRating = BinarySerialization.ReadFromBinaryFile<List<double>>(startupPath + @"\DataFiles\CollabRatings.bin");
            _contentRating = BinarySerialization.ReadFromBinaryFile<List<double>>(startupPath + @"\DataFiles\contentRating.bin");
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
                

            RecommendedCollabArtists = _collaborativeFiltering.RecommendArtists(pearson.CalculateCorrelation, newUser, _users, _roskildeArtists, _artists);
            RecommendedContetArtists = _contentBasedFiltering.RecommedArtists(cosine.GetCosine, newUser, _roskildeArtists, 10);

            RecommendedCollabArtists = _giveRecomendationStars(RecommendedCollabArtists, _collabRating);
            RecommendedContetArtists = _giveRecomendationStars(RecommendedContetArtists, _contentRating);
        }

        private Dictionary<int, RecommendedArtist> _giveRecomendationStars(Dictionary<int, RecommendedArtist> recommendedArtists, List<double> overallRatings)
        {
            int count = overallRatings.Count();
            int ten = (int)(count * 0.1);
            int nine = (int)(count * 0.2);
            int eight = (int)(count * 0.3);
            int seven = (int)(count * 0.4);
            int six = (int)(count * 0.5);
            int five = (int)(count * 0.6);
            int four = (int)(count * 0.7);
            int three = (int)(count * 0.8);
            int two = (int)(count * 0.9);
            foreach (var element in recommendedArtists)
            {
                if (element.Value.FilteringRating > overallRatings[ten])
                    element.Value.Stars = 10;
                else if (element.Value.FilteringRating > overallRatings[nine])
                    element.Value.Stars = 9;
                else if (element.Value.FilteringRating > overallRatings[eight])
                    element.Value.Stars = 8;
                else if (element.Value.FilteringRating > overallRatings[seven])
                    element.Value.Stars = 7;
                else if (element.Value.FilteringRating > overallRatings[six])
                    element.Value.Stars = 6;
                else if (element.Value.FilteringRating > overallRatings[five])
                    element.Value.Stars = 5;
                else if (element.Value.FilteringRating > overallRatings[four])
                    element.Value.Stars = 4;
                else if (element.Value.FilteringRating > overallRatings[three])
                    element.Value.Stars = 3;
                else if (element.Value.FilteringRating > overallRatings[two])
                    element.Value.Stars = 2;
                else
                    element.Value.Stars = 1;
              }
            return recommendedArtists;


        }
    }
}
