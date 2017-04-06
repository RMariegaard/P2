using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeRecommender
{
    [Serializable]
    class Userartist
    {
        //This class has no cunstructor because it will be loaded from the binary file
        public int Amount { get; private set; }
        public double Weight { get; set; }
        public int Id { get; private set; }
        public Artist ThisArtist { get; private set; }
        
    }
}
