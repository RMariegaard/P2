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
        public int Id { get; private set; }
        // The number of times the tag has been used
        public int Amount { get; set; }
        // The weight of the tag compared to all other tags in the list
        public double Weight { get; set; }
        public string Name;

        public Tag(int id)
        {
            Id = id;
            Amount = 1;
        }
        public Tag(double weight)
        {
            Weight = weight;
        }

    }
}
