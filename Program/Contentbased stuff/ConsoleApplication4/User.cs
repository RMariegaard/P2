using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    class User
    {
        public int Id;
        public List<Userartist> Artists = new List<Userartist>();
        public List<Tag> Tags = new List<Tag>();

        /* 
        Bliver udregnet når den binærefil skrives:
        public void TagCalc()
        {
            foreach(Userartist artist in Artists)
            {
                artist.ThisArtist.TagIds.OrderBy(T => T.amount);
                for (int i = 0; i < 5; i++)
                {
                    if (Tags.Contains(artist.ThisArtist.TagIds[i]))
                    {
                        Tags.Find(p => p.Id == artist.ThisArtist.TagIds[i].Id).amount += artist.amount;
                    }

                    else
                    {
                        Tag Temptag = artist.ThisArtist.TagIds[i];
                        Temptag.amount = artist.amount;
                        Tags.Add(Temptag);
                    }
                }
            }                  
        } 
        */
    }
}
