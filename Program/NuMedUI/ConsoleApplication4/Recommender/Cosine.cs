using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class Cosine : ICosine
    {
        public double GetCosine<T>(T element1, T element2) 
            where T : ITaggable 
        {
            double numerator = CalculateProductOfLengths(element1, element2);
            double denominator = CalculateDotProductInCosine(element1, element2);
            double value;
            //Can't devide by zero
            //The denominator is zero when atleast one of the vectors has length 0
            //that means that one of the elemnts has no tag and 
            //therfore the correlation should be zero
            if (denominator == 0)
            {
                value = 0;
            }
            else
            {
                value = numerator / denominator;
            }
            return value;
        }

        public double CalculateDotProductInCosine<T>(T element1, T element2)
            where T : ITaggable
        { 
            double dot = 0.0;
            foreach (var tag in element1.Tags)
            {
                if (element2.Tags.ContainsKey(tag.Key))
                {
                    dot += element2.Tags[tag.Key].Weight * tag.Value.Weight;
                }
            }
            return dot;
        }

        //Calculates the length of each vector and multiplies them
        //The length is calculated by the coordinates squares summed and then taking the square root
        public double CalculateProductOfLengths<T>(T element1, T element2)
            where T : ITaggable
        {
            double temp = element1.Tags.Sum(x => Math.Pow(x.Value.Weight, 2));
            double temp2 = element2.Tags.Sum(x => Math.Pow(x.Value.Weight, 2));
            return Math.Sqrt(temp) * Math.Sqrt(temp2);
        }
    }
}
