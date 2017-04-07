using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class ContentBasedFiltering
    {
        public static Dictionary<int,RecommendedArtist> RecommedArtists(Func<User, Artist, double> correlationMeasure, User newUser, Dictionary<int, Artist> allArtist, int k)
        {
            var recommendedArtist = new Dictionary<int, RecommendedArtist>();

            //Calculates the correlation
            foreach (var artist in allArtist.Values)
            {

                var tempArtist = new RecommendedArtist(artist);
                tempArtist.ContentBasedFilteringRating = correlationMeasure(newUser, artist);
                recommendedArtist.Add(tempArtist.thisArtist.Id, tempArtist);

            }
            //Burde omskrives meeeeeen virker for nu
            var final = recommendedArtist.OrderByDescending(x => x.Value.ContentBasedFilteringRating).Take(k)
                .ToDictionary((x => x.Key), (x=> x.Value));
           

            return final;
        }


    }
}
