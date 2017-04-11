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
        public int Id { get; private set; }
        public int Amount { get; set; } // Antal gange det er hørt/blevet tagget.
        public double Weight { get; set; } // Vægten af tagget sammenlignet med alle tags i listen.
        public string Name;

        public Tag(int id)
        {
            Id = id;
            Amount = 1;
        }
    }
}
