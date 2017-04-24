using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    public class User
    {
        public int Id { get; private set; }
        public Dictionary<int, Userartist> Artists { get; private set; }
        public Dictionary<int, Tag> Tags { get; private set; }
        private double _total_tag_amount;
        private double _totalt_listen_amount;

        public double TotalTagAmount { get { return _total_tag_amount; } }
        public double TotalListenAmount { get { return _totalt_listen_amount; } }

        public User(int id)
        {
            Artists = new Dictionary<int, Userartist>();
            Tags = new Dictionary<int, Tag>();
            Id = id; 
        }

        //To testing
        public User(int id, Dictionary<int, Tag> tags)
        {
            Tags = tags;
            Id = id;
        }

        //Same
        public User(int id, Dictionary<int, Userartist> artists)
        {
            Artists = artists;
            Id = id;
        }
        // Makes the users tag list depending on the users its heard, their top 5 tags, and how much the user has heard theese artists:
        public void TagCalc()
        {
            foreach(KeyValuePair<int, Userartist> artist in Artists)
            {
                Tag[] ArrayOfTags = artist.Value.ThisArtist.Tags.Values.ToArray().OrderByDescending(p => p.Amount).ToArray();

                for (int i = 0; i < 5 && i < ArrayOfTags.Count(); i++)
                {
                    if (Tags.ContainsKey(ArrayOfTags[i].Id))
                    {
                        Tags[ArrayOfTags[i].Id].Amount += artist.Value.Amount; 
                    }

                    else
                    {
                        Tag Temptag = new Tag(ArrayOfTags[i].Id);
                        Temptag.Amount = artist.Value.Amount; 
                        Tags.Add(Temptag.Id, Temptag);
                    }
                }
            }

            foreach(var tag in Tags)
            {
                _total_tag_amount += tag.Value.Amount;
            }
            foreach (var tag in Tags)
            {
                tag.Value.Weight = (100 / _total_tag_amount) * tag.Value.Amount;
            }
        } 

        public void CalculateArtistWeight()
        {
            foreach(var artist in Artists)
            {
                _totalt_listen_amount += artist.Value.Amount;
            }
            foreach(var artist in Artists)
            {
                artist.Value.Weight = (100 / _totalt_listen_amount) * artist.Value.Amount;
            }
        }
    }
}
