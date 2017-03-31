using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class Cosine
    {
        double dot;
        double LengthArtist1;
        double LengthArtist2;

        public double GetCosine(Artist testArtist1, Artist testArtist2)
        {
            //Dot
            for (int i = 0; i < testArtist1.TagIds.Count(); i++)
            {
                if (testArtist2.TagIds.Exists(p => p.Id == testArtist1.TagIds[i].Id))
                {
                    dot += 4.2 * 4.2;/*testArtist2.TagIds.Find(p => p.Id == testArtist.TagIds[i].Id).Weight * testArtist.TagIds[i].Weight;*/
                }
            }
            //length
            foreach (Tag item in testArtist1.TagIds)
            {
                LengthArtist1 += Math.Pow(4.2/*item.Weight*/, 2);
            }
            LengthArtist1 = Math.Sqrt(LengthArtist1);

            foreach (Tag item in testArtist2.TagIds)
            {
                LengthArtist2 += Math.Pow(4.2/*item.Weight*/, 2);
            }
            LengthArtist2 = Math.Sqrt(LengthArtist2);

            //Result
            return (dot) / (LengthArtist1 * LengthArtist2);
        }
    }
}
