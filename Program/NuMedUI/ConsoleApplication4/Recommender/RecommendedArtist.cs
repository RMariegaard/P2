using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class RecommendedArtist : RoskildeArtist
    {
        public double CollaborativeFilteringRating { get; set; }
        public double ContentBasedFilteringRating { get; set; }
        public int Stars { get; set; }
        public int userID;
        public RecommendedArtist(RoskildeArtist artist) : base(artist.TimeOfConcert, artist.Scene, artist)
        {
        }
    }
}