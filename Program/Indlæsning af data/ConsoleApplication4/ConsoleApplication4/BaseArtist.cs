using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    abstract public class BaseArtist
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Dictionary<int, Tag> Tags { get; private set; }

        public BaseArtist(int ID, string navn)
        {
            Tags = new Dictionary<int, Tag>();
            Id = ID;
            Name = navn;
        }

    }
}
