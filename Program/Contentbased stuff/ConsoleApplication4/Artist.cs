using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    public class Artist : BaseArtist
    {
        //This public class has no cunstructor because it will be loaded from the binary file
        private double _totalTagAmount;
        public double TotalTagAmount { get { return _totalTagAmount; } }
    }



}
