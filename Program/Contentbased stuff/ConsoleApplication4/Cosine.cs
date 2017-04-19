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
            Dictionary<int, double> userArrayWeigth = user.Tags.ToDictionary(x => x.Key, x => x.Value.Weight);
            Dictionary<int, double> artistArrayWeigth = artist.Tags.ToDictionary(x => x.Key, x => x.Value.Weight);

            Dot = CalcDotInCosine(userArrayWeigth, artistArrayWeigth);

            //length
            LengthUser = GetLength(userArrayWeigth);

            LengthArtist = GetLength(artistArrayWeigth);

            //Result
            return (Dot) / (LengthUser * LengthArtist);
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
