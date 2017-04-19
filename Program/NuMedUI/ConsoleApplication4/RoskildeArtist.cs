using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{

    [Serializable]
    public class RoskildeArtist
    {
        public int Id { get; private set; }
        public DateTime TimeOfConcert { get; private set; }
        public string Scene { get; private set; }
        public Artist thisArtist { get; private set; }

        public RoskildeArtist(int ID, DateTime timeOfConcert, string scene, Artist artist)
        {
            Id = ID;
            TimeOfConcert = timeOfConcert;
            Scene = scene;
            thisArtist = artist;
        }

    }

}
