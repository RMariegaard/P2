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
            //Create dictionaries of weights, makes it easier to test

            Dictionary<int, double> userArrayWeigth = user.Tags.ToDictionary(x => x.Key, x => x.Value.Weight);
            Dictionary<int, double> artistArrayWeigth = artist.Tags.ToDictionary(x => x.Key, x => x.Value.Weight);

            dot = CalcDotInCosine(userArrayWeigth, artistArrayWeigth);

            //length
            lengthUser = GetLength(userArrayWeigth);

            lengthArtist = GetLength(artistArrayWeigth);
            //Result
            if (lengthUser * lengthArtist == 0.0)
                return 0;
            return (dot) / (lengthUser * lengthArtist);
        }
        public double CalcDotInCosine(Dictionary<int, double> user, Dictionary<int, double> artist)
        {
            double dot = 0.0;
            foreach (var element in user)
            {
                if (artist.ContainsKey(element.Key))
                {
                    dot += artist[element.Key] * element.Value;
                }
            }
            return dot;
        }
        public double GetLength(Dictionary<int, double> vector)
        {
            return vector.Sum(x => x.Value);
        }
    }
}
