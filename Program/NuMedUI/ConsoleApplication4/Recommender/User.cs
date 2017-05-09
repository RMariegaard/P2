using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    public class User : ITaggable
    {
        // The id of the user is stored here:
        private int _id;
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }
        // Dictionaries for Artists and Tags:
        // Artist dictionay is filled while loading the data and making the user:
        public Dictionary<int, Userartist> Artists { get; private set; }
        // Tags dictionary will be filled by using the method UserTagHandling(), which will be called when making the data:
        public Dictionary<int, Tag> Tags { get; private set; }

        private double _totalTagAmount;
        public double TotalTagAmount { get { return _totalTagAmount; } }
        private double _totaltListenAmount;
        public double TotalListenAmount { get { return _totaltListenAmount; } }

        // Constructer:
        public User(int id)
        {
            Artists = new Dictionary<int, Userartist>();
            Tags = new Dictionary<int, Tag>();
            Id = id; 
        }

        // Method that makes the TagsDictionary, relative to the Artists this user has heard:
        public void UserTagHandling()
        {
            // Itterating though all artists in the users ArtistDictionary:
            foreach(KeyValuePair<int, Userartist> artist in Artists)
            {
                // And array of tags is made of the tags the current artist has:
                Tag[] ArrayOfTags = artist.Value.ThisArtist.Tags.Values.OrderByDescending(p => p.Amount).ToArray();

                //Set limit to numbers of tags if it is below 10, so its only the top 10 most used tag for an artist that is used:
                int limit = 10 > ArrayOfTags.Count() ? ArrayOfTags.Count() : 10; 

                for (int i = 0; i < limit; i++)
                {
                    int currentTagID = ArrayOfTags[i].Id;
                    // If this tag is already represented in this Users TagDictionary, then the Tag.Amount will be increased by 
                    // the amount of listens the User has on the artist, and the weight of that Tag on the artist:
                    if (Tags.ContainsKey(currentTagID))
                    {
                        Tags[currentTagID].Amount += artist.Value.Amount * artist.Value.ThisArtist.Tags[currentTagID].Weight; 
                    }
                    // If its a new tag a new tag is added:
                    else
                    {
                        Tag Temptag = new Tag(currentTagID);
                        Temptag.Amount = artist.Value.Amount * artist.Value.ThisArtist.Tags[currentTagID].Weight; 
                        Tags.Add(Temptag.Id, Temptag);
                    }
                }
            }
            // After the Tag Dictionay is made, the total tag amount, and the weight of each individual tags will be calculated:
            foreach(var tag in Tags)
            {
                _totalTagAmount += tag.Value.Amount;
            }
            foreach (var tag in Tags)
            {
                tag.Value.Weight = (100 / _totalTagAmount) * tag.Value.Amount;
            }
        } 

        // Calculates the percentage an artist has been heard by this user, based on all the artists the user has heard:
        public void CalculateArtistWeight()
        {
            foreach(var artist in Artists)
            {
                _totaltListenAmount += artist.Value.Amount;
            }
            foreach(var artist in Artists)
            {
                artist.Value.Weight = (artist.Value.Amount / _totaltListenAmount) * 100;
            }
        }