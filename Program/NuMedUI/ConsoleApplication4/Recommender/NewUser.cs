using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    [Serializable]
    public class NewUser : User
    {
        //Construtor for the new users with the highest user ID and the artists rated by the user
        public NewUser(Dictionary<int, Userartist> ratedArtistByUser, int lastUserID) : base(lastUserID+1)
        {
            AddArtists(ratedArtistByUser);
        }

        //Adds the artist rated and calculates tags and weight
        private void AddArtists(Dictionary<int, Userartist> ratedArtistByUser)
        {
            foreach (var artist in ratedArtistByUser)
            {
                Artists.Add(artist.Key, artist.Value);
            }
            UserTagHandling();
            CalculateArtistWeight();
        }
    }
}
