using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    class Userartist
    {
        public int Weight;
        public int Id;
        public Artist ThisArtist;

        public Userartist(int ID, int W, Artist thisartist)
        {
            Weight = W;
            Id = ID;
            ThisArtist = thisartist;
        }
    }
}
