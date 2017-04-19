using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class SimilarUser
    {
        public double similarity { get; set; }
        public int Id { get; private set; }
        
        public SimilarUser(int id)
        {
            Id = id;
        }
    }
}
