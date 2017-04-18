using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class Cosine
    { 


        public double GetCosine(User user, Artist artist)
        {
            double Dot = 0;
            double LengthUser = 0;
            double LengthArtist = 0;
            //Dot
            
            foreach (var tag in user.Tags)
            {
                if (artist.Tags.ContainsKey(tag.Key))
                {
                    Dot += artist.Tags[tag.Key].Weight * tag.Value.Weight;
                }
            }

            //length
            foreach (var item in user.Tags)
            {
                LengthUser += Math.Pow(item.Value.Weight, 2);
            }
            LengthUser = Math.Sqrt(LengthUser);

            foreach (var item in artist.Tags)
            {
                LengthArtist += Math.Pow(item.Value.Weight, 2);
            }
            LengthArtist = Math.Sqrt(LengthArtist);

            //Result
            return (Dot) / (LengthUser * LengthArtist);
        }
    }
}
