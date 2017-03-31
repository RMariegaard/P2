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
        public int amount = 1; // Antal gange det er hørt/blevet tagget.
        public double weight; // Vægten af tagget sammenlignet med alle tags i listen.
        //public string Name;

        public Tag(int id)
        {
            Id = id;
        }
    }
}
