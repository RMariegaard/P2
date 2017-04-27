using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    public class User : ITag
    {
        public int Id { get; private set; }
        public Dictionary<int, Userartist> Artists { get; private set; }
        public Dictionary<int, Tag> Tags { get; private set; }
        private double _total_tag_amount;
        private double _totalt_listen_amount;
        private double _averageContentRating;
        private double _averageColabRating;
        public double AverageContentRating { get { return _averageContentRating; } }
        public double AverageColabRating { get { return _averageColabRating; } }


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
                Tag[] ArrayOfTags = artist.Value.ThisArtist.Tags.Values.OrderByDescending(p => p.Amount).ToArray();

                //Set limit to numbers of tags if it is below 10
                int limit = 10 > ArrayOfTags.Count() ? ArrayOfTags.Count() : 10; 

                for (int i = 0; i < limit; i++)
                {
                    int currentTagID = ArrayOfTags[i].Id;
                    if (Tags.ContainsKey(currentTagID))
                    {
                        Tags[currentTagID].Amount += artist.Value.Amount * artist.Value.ThisArtist.Tags[currentTagID].Weight; 
                    }

                    else
                    {
                        Tag Temptag = new Tag(currentTagID);
                        Temptag.Amount = artist.Value.Amount * artist.Value.ThisArtist.Tags[currentTagID].Weight; 
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
                artist.Value.Weight = (artist.Value.Amount / _totalt_listen_amount) * 100;
            }
        }

        // After Recommendations are made, the users average rating value is calculated for deciding which recommendation to choose in a possible conflict.
        public void CalculateAverageOfUserRatings(List<RecommendedArtist> listOfRecommendations)
        {
            double totalColabRating = 0;
            double totalContentRating = 0;
            foreach (RecommendedArtist artist in listOfRecommendations)
            {
                totalColabRating += artist.CollaborativeFilteringRating;
                totalContentRating += artist.ContentBasedFilteringRating;

            }
            _averageColabRating = (100 / totalColabRating) * listOfRecommendations.Count;
            _averageContentRating = (100 / totalContentRating) * listOfRecommendations.Count;
        }
    }
}
