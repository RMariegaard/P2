using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public interface ITaggable
    {
        //The interface only requires a getter
        Dictionary<int, Tag> Tags { get;}
    }
}
