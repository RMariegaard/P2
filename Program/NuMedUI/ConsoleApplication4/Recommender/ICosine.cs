using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public interface ICosine
    {
        double CalculateProductOfLengths<T>(T element1, T element2)
            where T : ITaggable;
        double CalculateDotProductInCosine<T>(T element1, T element2)
            where T : ITaggable;
        double GetCosine<T>(T element1, T element2)
            where T : ITaggable;

    }
}
