using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class RecommendedArtist : RoskildeArtist
    {
        public double FilteringRating { get; set; }
        public int Stars { get; set; }

        public RecommendedArtist(RoskildeArtist artist) : base(artist.TimeOfConcert, artist.Scene, artist)
        {
        }
    }
}