using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    class Tag
    {
        public int Id;
        public int amount = 1;
        //public string Name;

        public Tag(int id)
        {
            Id = id;
        }
    }
}
