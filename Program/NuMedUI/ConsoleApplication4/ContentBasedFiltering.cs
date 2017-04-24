using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class ContentBasedFiltering
    {
        public static Dictionary<int, RecommendedArtist> RecommedArtists(Func<User, Artist, double> correlationMeasure, User newUser, Dictionary<int, RoskildeArtist> roskildeArtist, int k)
        {
            var recommendedArtist = new Dictionary<int, RecommendedArtist>();

            //Calculates the correlation
            foreach (RoskildeArtist artist in roskildeArtist.Values)
            {
                var tempArtist = new RecommendedArtist(artist);
                tempArtist.ContentBasedFilteringRating = correlationMeasure(newUser, artist);
                recommendedArtist.Add(tempArtist.Id, tempArtist);

            }
            //SHOULD BE REWRITTEN... BUUUT WORKS FOR NOW
            var final = recommendedArtist.OrderByDescending(x => x.Value.ContentBasedFilteringRating).Take(k)
                .ToDictionary((x => x.Key), (x => x.Value));

            return final;
        }
    }
}
