using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class Cosine
    { 


        public double GetCosine(Artist artist1, Artist artist2)
        {
            double Dot = 0;
            double LengthArtist1 = 0;
            double LengthArtist2 = 0;
            //Dot
            
            foreach (var tag in artist1.Tags)
            {
                if (artist2.Tags.ContainsKey(tag.Key))
                {
                    Dot += artist2.Tags[tag.Key].Weight * tag.Value.Weight;
                }
            }

            //length
            foreach (var item in artist1.Tags)
            {
                LengthArtist1 += Math.Pow(item.Value.Weight, 2);
            }
            LengthArtist1 = Math.Sqrt(LengthArtist1);

            foreach (var item in artist2.Tags)
            {
                LengthArtist2 += Math.Pow(item.Value.Weight, 2);
            }
            LengthArtist2 = Math.Sqrt(LengthArtist2);

            //Result
            return (Dot) / (LengthArtist1 * LengthArtist2);
        }
    }
}
