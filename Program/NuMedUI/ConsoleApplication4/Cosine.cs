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
            double denumerator = 0;

            denumerator = GetDenumerator(user, artist);
            //Dot
            dot = CalcDotInCosine(user, artist);

            //length, this is the vector definicion of length, so its acctually just the sum of all tag.weigths


            //Result
            return CalculateTheCosine(dot, denumerator);
        }
        public double CalculateTheCosine(double dot, double denumerator)
        {
            if (denumerator == 0.0)
                return 0.0;
            else
                return (dot) / (denumerator);
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
        public double GetDenumerator(User user, Artist artist)
        {
            double temp = user.Tags.Sum(x => Math.Pow(x.Value.Weight, 2));


            double temp2 = artist.Tags.Sum(x => Math.Pow(x.Value.Weight, 2));
            return Math.Sqrt(temp) * Math.Sqrt(temp2);
        }
    }
}
