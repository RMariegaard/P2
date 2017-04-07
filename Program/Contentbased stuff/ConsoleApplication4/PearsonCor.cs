using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class PearsonCor
    {


        public double Calculate(Artist artist1, Artist artist2)
        {

            double Artist1Mean = 0.0;
            artist1.Tags.Values.ToList().ForEach(tag => Artist1Mean += tag.Weight);
            Artist1Mean /= artist1.TotalTagAmount;

            double Artist2Mean = 0.0;
            artist2.Tags.Values.ToList().ForEach(tag => Artist2Mean += tag.Weight);
            Artist2Mean /= artist2.TotalTagAmount;

            double Top = 0.0;

            foreach(var tag in artist1.Tags)
            {
                if (artist2.Tags.ContainsKey(tag.Key))
                {
                    Top += (tag.Value.Weight - Artist1Mean) * (artist2.Tags[tag.Value.Id].Weight - Artist2Mean);
                }
            }
            double Buttom = Math.Sqrt(Top);



            return Top / Buttom;
        }

        //Calcuates the Pearson Correlation between two users based on artists
        public static double CalculateUser(User user, User otherUser)
        {
            //Calculates the mean of the artist weight for the two users
            double userMean = 0.0;
            user.Artists.Values.ToList().ForEach(artist => userMean += artist.Weight);
            userMean /= user.Artists.Count;

            double otherUserMean = 0.0;
            otherUser.Artists.Values.ToList().ForEach(artist => otherUserMean += artist.Weight);
            otherUserMean /= otherUser.Artists.Count;

            //Calculates the numerator of the Pearson Correlation
            double numerator = 0.0;
            foreach (var userArtist in user.Artists)
            { 
                if (otherUser.Artists.ContainsKey(userArtist.Key))
                {
                    numerator += (userArtist.Value.Weight - userMean) * (otherUser.Artists[userArtist.Key].Weight - otherUserMean);
                }
            }

            //Calculates the denumerator of the Pearson Correlation
            double denumerator = Math.Sqrt(numerator);

            //Returns the Pearson Correlation
            return numerator / denumerator;
        }
        

    }
}
