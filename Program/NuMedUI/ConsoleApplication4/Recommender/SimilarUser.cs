using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class SimilarUser
    {
        public double Similarity { get; set; }
        public int ID { get; private set; }
        public Dictionary<int, Userartist> Artists { get; set; }

        public SimilarUser(int id)
        {
            ID = id;
        }
    }
}
