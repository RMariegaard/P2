using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public interface IContentBasedFiltering
    {
        Dictionary<int, RecommendedArtist> RecommedArtists(Func<ITaggable, ITaggable, double> correlationMeasure, User newUser, Dictionary<int, RoskildeArtist> roskildeArtist, int k);
    }
}
