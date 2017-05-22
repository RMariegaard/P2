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

        // Two Construkters:
        public Artist(int ID, string name) : base(ID, name) { }
        // This Construkter is used when making the binary files.
        public Artist(int ID, Dictionary<int, Tag> tag, string name) : base(ID, tag)
        {
            this.Name = name;
            CalcTagWeight();
        }

        public void CalcTagWeight()
        {
            foreach (var tag in Tags)
            {
                TotalTagAmount += tag.Value.Amount;
            }
            foreach (var tag in Tags)
            {
                tag.Value.Weight = (100 / TotalTagAmount) * tag.Value.Amount;
            }
        }
    }
}
