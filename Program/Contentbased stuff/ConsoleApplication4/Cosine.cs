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

            //length, this is the vector definicion of length, so its acctually just the sum of all tag.weigths
            lengthUser = GetLengthUser(user);

            lengthArtist = GetLengthArtist(artist);

            //Result
            return CalculateTheCosine(dot, lengthUser, lengthArtist);
        }
        public double CalculateTheCosine(double dot, double length1, double length2)
        {
            if (length1 * length2 == 0.0)
                return 0.0;
            else
                return (dot) / (length1 * length2);
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

        //length, this is the vector definicion of length, so its acctually just the sum of all tag.weigths
        public double GetLengthUser(User user)
        {
            return user.Tags.Sum(x => x.Value.Weight);
        }
        //length, this is the vector definicion of length, so its acctually just the sum of all tag.weigths
        public double GetLengthArtist(Artist artist)
        {
            return artist.Tags.Sum(x => x.Value.Weight);
        }
    }
}
