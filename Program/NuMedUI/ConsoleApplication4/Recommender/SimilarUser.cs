using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class SimilarUser
    {
        // Properti that represents the similarity between 2 users.
        public double Similarity { get; set; }
        // An ID that is equal to the ID of the users this instanse is being made of.
        public int ID { get; private set; }
        // A Dictionary that resembles the Artists of the user this instanse is made of.
        public Dictionary<int, Userartist> Artists { get; set; }

        public SimilarUser(int id)
        {
            ID = id;
        }
    }
}
