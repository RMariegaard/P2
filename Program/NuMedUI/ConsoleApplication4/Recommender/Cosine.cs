using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class Cosine
    {
        public double GetCosine<T1, T2>(T1 user, T2 artist) 
            where T1 : ITag 
            where T2 : ITag
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

        public double CalcDotInCosine<T1, T2>(T1 user, T2 artist)
            where T1 : ITag
            where T2 : ITag
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
        public double GetDenumerator<T1, T2>(T1 user, T2 artist)
            where T1 : ITag
            where T2 : ITag
        {
            double temp = user.Tags.Sum(x => Math.Pow(x.Value.Weight, 2));


            double temp2 = artist.Tags.Sum(x => Math.Pow(x.Value.Weight, 2));
            return Math.Sqrt(temp) * Math.Sqrt(temp2);
        }
    }
}
