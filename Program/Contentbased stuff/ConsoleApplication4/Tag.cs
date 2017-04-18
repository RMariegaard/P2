using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    public class Tag
    {
        //This public class has no cunstructor because it will be loaded from the binary file
        public int Id { get; private set; }
        public int Amount { get; private set; }
        public double Weight { get; private set; }
    }
}
