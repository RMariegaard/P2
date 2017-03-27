using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagstest
{
    class Artist
    {
        public string _name;
        public Tags[] artisttags;

        private bool done = false;
        int i = 0;

        public Artist(int[] TagID, int[] TagWeight, string Name) // Denne constructer skal laves om så den læser fra binær fil.
        {
            _name = Name;

            while (!done)
            {
                artisttags[i] = new Tags(TagID[i], TagWeight[i]);
                i++;
                if(i == TagID.Length)
                {
                    done = true;
                }
            }
        }
    }
}
