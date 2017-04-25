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
        public int Id { get; private set; }
        public Artist ThisArtist { get; private set; }

        public Userartist(int ID, int A, Artist thisartist)
        {
            Amount = A;
            Id = ID;
            ThisArtist = thisartist;
        }
        //Used to test
        public Userartist(double weight)
        {
            Weight = weight;
        }
    }
}
