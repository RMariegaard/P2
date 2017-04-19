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
        public double TotalTagAmount { get; private set; }

        public Artist(int ID, Dictionary<int, Tag> tag) : base(ID, tag) { }

        public void CalcTagWeight()
        {
            List<Tag> list = Tags.Values.ToList();
            list.Sort((a, b) => -a.Amount.CompareTo(b.Amount));

            foreach (var tag in list)
            {
                TotalTagAmount += tag.Amount;
            }
            foreach (var tag in list)
            {
                tag.Weight = (100 / TotalTagAmount) * tag.Amount;
            }
        }
    }
}
