using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    abstract public class BaseArtist : ITaggable
    {
        public int Id { get; private set; }
        public string Name { get; protected set; }

        public Dictionary<int, Tag> Tags { get; private set; }

        public BaseArtist(int ID, string Name)
        {
            Tags = new Dictionary<int, Tag>();
            Id = ID;
            this.Name = Name;
        }

        public BaseArtist(int ID, Dictionary<int, Tag> tag)
        {
            Tags = tag;
            Id = ID;
        }
    }
}
