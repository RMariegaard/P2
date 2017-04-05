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
        public int Id { get; private set; }
        public Dictionary<int, Userartist> Artists { get; private set; }
        public Dictionary<int, Tag> Tags { get; private set; }
        public double total_tag_amount;
        public double totalt_listen_amount;

        

        public User(int id)
        {
            Artists = new Dictionary<int, Userartist>();
            Tags = new Dictionary<int, Tag>();
            Id = id;
        }



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
