using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class ContentBasedFiltering : IContentBasedFiltering
    {
        public Dictionary<int, RecommendedArtist> RecommedArtists(Func<ITaggable, ITaggable, double> correlationMeasure, User newUser, Dictionary<int, RoskildeArtist> roskildeArtist, int k)
        {
            var recommendedArtist = new Dictionary<int, RecommendedArtist>();

            //Calculates the correlation for the artists and adds them to the dictionary above
            foreach (RoskildeArtist artist in roskildeArtist.Values)
            {
                var tempArtist = new RecommendedArtist(artist);
                tempArtist.ContentBasedFilteringRating = correlationMeasure(newUser, artist);
                recommendedArtist.Add(tempArtist.Id, tempArtist);

            }
            //Takes the k best artists and returns them as a dictornary
            var final = recommendedArtist
                .OrderByDescending(x => x.Value.ContentBasedFilteringRating)
                .Take(k)
                .ToDictionary((x => x.Key), (x => x.Value));

            return final;
        }
    }
}
