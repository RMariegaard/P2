using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{

    [Serializable]
    class RoskildeArtist : Artist
    {
        public DateTime TimeOfConcert { get; private set; }
        public string Scene { get; private set; }

        public RoskildeArtist(int ID, string name) : base(ID, name)
        {

        }

    }

}
