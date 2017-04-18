using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class RecommendedArtist
    {
       public double CollaborativeFilteringRating { get; set; }
       public double ContentBasedFilteringRating { get; set; }
       public Artist thisArtist { get; private set; }

        public RecommendedArtist(Artist artist)
        {
            thisArtist = artist;
        }
    }
}
