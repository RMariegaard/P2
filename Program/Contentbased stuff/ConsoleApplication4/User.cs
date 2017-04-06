using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeRecommender
{
    [Serializable]
    class User
    {
        public int Id { get; private set; }
        public Dictionary<int, Userartist> Artists { get; private set; }
        public Dictionary<int, Tag> Tags { get; private set; }
        private double _total_tag_amount;
        private double _totalt_listen_amount;

        public double TotalTagAmount { get { return _total_tag_amount; } }
        public double TotalListenAmount { get { return _totalt_listen_amount; } }

        public User(int id)
        {
            Artists = new Dictionary<int, Userartist>();
            Tags = new Dictionary<int, Tag>();
            Id = id;
        }
    }
}
