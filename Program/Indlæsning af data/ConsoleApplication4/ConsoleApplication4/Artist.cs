using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    class Artist
    {
        public int Id;
        public string Name;
        public double total_tag_amount;

        public List<Tag> TagIds = new List<Tag>();

        public void CalcTagWeight()
        {
            foreach (Tag tag in TagIds)
            {
                total_tag_amount += tag.amount;
            }
            foreach (Tag tag in TagIds)
            {
                tag.weight = (100 / total_tag_amount) * tag.amount;
            }
        }

        public Artist(int ID, string navn)
        {
            Id = ID;
            Name = navn;
        }


    }
}
