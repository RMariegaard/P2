using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagstest
{
    class RecommendedArtist : RoskildeArtist
    {
        public double CFRating { get; } // Collaborativ Rating.
        public double CBFRating { get; } // Contentbased Rating.
        public List<int> RelatedArtists = new List<int>(); // List der indeholder ID´s på related artists så de kan fremvises til brugeren.

        public RecommendedArtist(DateTime spilletid, string scene, int[] TagID, int[] TagWeight, string Name) 
        : base(spilletid, scene, TagID, TagWeight, Name)
        {
            // CF og CBF rating skal komme i constructoren.(?)
        }
    }
}
