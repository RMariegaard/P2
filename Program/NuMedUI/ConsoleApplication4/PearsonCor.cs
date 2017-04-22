using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class PearsonCor
    {/*
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
        */
        //Calcuates the Pearson Correlation between two users based on artists
        public double CalculateUser(User user, User otherUser)
        {
            //Calculates the mean of the artist weight for the two users
            double userMean = CalculateUserMean(user);
            double otherUserMean = CalculateUserMean(otherUser);

            //Calculates the numerator of the Pearson Correlation
            double numerator = CalculateNumerator(user, otherUser, userMean, otherUserMean);
            //Calculates the denumerator of the Pearson Correlation
            double denumerator = Math.Sqrt(numerator);

            //Returns the Pearson Correlation
            if (denumerator == 0.0)
                return 0.0;
            else
                return numerator / denumerator;
        }

        public double CalculateUserMean(User user)
        {
            double temp = 0.0;
            temp = user.Artists.Values.Sum(x => x.Weight);
            temp /= user.Artists.Count;
            return temp;
        }
        public double CalculateNumerator(User user, User otherUser, double userMean, double otherUserMean)
        {
            double temp = 0.0;
            foreach (var userArtist in user.Artists)
            {
                if (otherUser.Artists.ContainsKey(userArtist.Key))
                {
                    temp += (userArtist.Value.Weight - userMean) * (otherUser.Artists[userArtist.Key].Weight - otherUserMean);
                }
            }
            return temp;
        }
    }
}
