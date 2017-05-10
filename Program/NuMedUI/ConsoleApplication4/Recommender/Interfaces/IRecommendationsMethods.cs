using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public interface IRecommendationsMethods
    {
        double GetCosine<T>(T element1, T element2)
        where T : ITaggable;
        double CalculateCorrelation(User user, User otherUser, Dictionary<int, Artist> allArtists);
        Dictionary<int, RecommendedArtist> RecommendArtistsCollab(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser,
        Dictionary<int, User> allUsers, Dictionary<int, RoskildeArtist> roskildeArtist, Dictionary<int, Artist> allArtists);
        Dictionary<int, RecommendedArtist> RecommedArtistsContent(Func<ITaggable, ITaggable, double> correlationMeasure, User newUser, Dictionary<int, RoskildeArtist> roskildeArtist, int k);

    }
}
