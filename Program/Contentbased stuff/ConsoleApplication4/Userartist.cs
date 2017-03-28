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
        public int Id;
        public int Weight;

        public Userartist(int ID, int W)
        {
            this.Id = ID;
            Weight = W;
        }

    }
}
