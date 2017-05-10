using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public interface ICollaborativeFiltering
    {
        Dictionary<int, RecommendedArtist> RecommendArtists(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser,
            Dictionary<int, User> allUsers, Dictionary<int, RoskildeArtist> roskildeArtist, Dictionary<int, Artist> allArtists);
        List<SimilarUser> KNearestNeighbours(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser, Dictionary<int, User> users, Dictionary<int, Artist> allArtists);
        List<SimilarUser> KNearestNeighbours(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser, Dictionary<int, User> users, int k, Dictionary<int, Artist> allArtists);
    }
}
