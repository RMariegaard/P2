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
            double dot = 0;
            double lengthUser = 0;
            double lengthArtist = 0;
            //Dot
            dot = CalcDotInCosine(user, artist);

            //length
            lengthUser = GetLengthUser(user);

            lengthArtist = GetLengthArtist(artist);

            //Result
            if (lengthUser * lengthArtist == 0.0)
                return 0;
            return (dot) / (lengthUser * lengthArtist);
        }


        public double CalcDotInCosine(User user, Artist artist)
        {
            double dot = 0.0;
            foreach (var element in user.Tags)
            {
                if (artist.Tags.ContainsKey(element.Key))
                {
                    dot += artist.Tags[element.Key].Weight * element.Value.Weight;
                }
            }
            return dot;
        }


        public double GetLengthUser(User user)
        {
            return user.Tags.Sum(x => x.Value.Weight);
        }
        public double GetLengthArtist(Artist artist)
        {
            return artist.Tags.Sum(x => x.Value.Weight);
        }
    }
}
