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
        public int amount;
        public double weight;
        public int Id;
        public Artist ThisArtist;

        public Userartist(int ID, int A, Artist thisartist)
        {
            amount = A;
            Id = ID;
            ThisArtist = thisartist;
        }
    }
}
