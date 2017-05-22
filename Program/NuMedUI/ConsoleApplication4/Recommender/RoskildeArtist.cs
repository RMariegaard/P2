using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{

    [Serializable]
    public class RoskildeArtist : Artist
    {
        // Two properties for saving information on where and when the artist plays:
        public DateTime TimeOfConcert { get; private set; }
        public string Scene { get; private set; }

        public RoskildeArtist(DateTime timeOfConcert, string scene, Artist artist) : base(artist.Id, artist.Name)
        {
            TimeOfConcert = timeOfConcert;
            Scene = scene;
            foreach (var tag in artist.Tags)
            {
                Tags.Add(tag.Key, new Tag(tag.Key));
            }
        }
    }
}
