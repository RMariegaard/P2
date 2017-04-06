using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    class Artist : BaseArtist
    {
        private double _total_tag_amount;

        public Artist(int ID, string navn) : base(ID, navn)
        {
        }



        public void CalcTagWeight()
        {
            List<Tag> list = Tags.Values.ToList();
            list.Sort((a, b) => -a.Amount.CompareTo(b.Amount));

            foreach (var tag in list)
            {
                _total_tag_amount += tag.Amount;
            }
            foreach (var tag in list)
            {
                tag.Weight = (100 / _total_tag_amount) * tag.Amount;
            }
        }



    }
}
