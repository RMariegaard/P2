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
        public double total_tag_amount;
        public double totalt_listen_amount;

        public void TagCalc() //Makes the users tag list depending on the users its heard, their top 5 tags, and how much the user has heard theese artists:
        {
            foreach(Userartist artist in Artists)
            {
               
                for (int i = 0; i < 5 && i < artist.ThisArtist.TagIds.Count(); i++)
                {
                    if (Tags.Exists(t => t.Id == artist.ThisArtist.TagIds[i].Id))
                    {
                        Tags.Find(p => p.Id == artist.ThisArtist.TagIds[i].Id).amount += artist.amount; // Indsæt en tagweightcalc metode
                    }

                    else
                    {
                        Tag Temptag = new Tag(artist.ThisArtist.TagIds[i].Id);
                        Temptag.amount = artist.amount; // Indsæt en tagweightcalc metode
                        Tags.Add(Temptag);
                    }
                }
            }

            foreach(Tag tag in Tags)
            {
                total_tag_amount += tag.amount;
            }
            foreach (Tag tag in Tags)
            {
                tag.weight = (100 / total_tag_amount) * tag.amount;
            }
        } 

        public void CalculateArtistWeight()
        {
            foreach(Userartist artist in Artists)
            {
                totalt_listen_amount += artist.amount;
            }
            foreach(Userartist artist in Artists)
            {
                artist.weight = (100 / totalt_listen_amount) * artist.amount;
            }
        }
    }
}
