using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class Artist
    {
        public int Id;
        public string Name;

        public List<Tag> TagIds = new List<Tag>();

        public Artist(int ID, string navn)
        {
            Id = ID;
            Name = navn;
        }


    }
}
