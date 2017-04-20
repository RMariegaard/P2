using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    class RArtist
    {
        public int Id;
        public string Name;

        public List<RTag> TagIds = new List<RTag>();

        public RArtist(string navn)
        {
            Name = navn;
        }


    }
    
    class RTag
    {
        public int Id;
        public int amount;
        public string Name;

        public RTag(string navn, int weight)
        {
            Name = navn;
            amount = weight;
        }
    }



}
