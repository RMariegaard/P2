using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    public class Userartist
    {
        public int Amount { get; private set; }
        public double Weight { get; set; }
        public int ID { get; private set; }
        // Instead of making a new instance of the same artist, this class simply has a reference to the artist:
        public Artist ThisArtist { get; private set; }

        public Userartist(int id, int amount, Artist thisArtist)
        {
            Amount = amount;
            ID = id;
            ThisArtist = thisArtist;
        }
    }
}
