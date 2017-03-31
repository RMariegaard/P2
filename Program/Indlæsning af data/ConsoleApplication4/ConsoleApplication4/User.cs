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

        
        public void TagCalc()
        {
            foreach (Userartist artist in Artists)
            {
               
                for (int i = 0; i < 5 && i < artist.ThisArtist.TagIds.Count(); i++)
                {
                    if (Tags.Exists(t => t.Id == artist.ThisArtist.TagIds[i].Id))
                    {
                        Tags.Find(p => p.Id == artist.ThisArtist.TagIds[i].Id).amount += artist.amount;
                    }

                    else
                    {
                        Tag Temptag = new Tag(artist.ThisArtist.TagIds[i].Id);
                        Temptag.amount = artist.amount;
                        Tags.Add(Temptag);
                    }
                }
            }
        } 
    }
}
