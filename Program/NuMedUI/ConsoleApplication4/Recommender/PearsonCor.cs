using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class PearsonCor
    {
        //Calcuates the Pearson Correlation between two users based on artists
        public double CalculateCorrelation(User user, User otherUser, Dictionary<int, Artist> allArtists)
        {
            //Calculates the mean of the artist weight for the two users
            double userMean = CalculateUserMean(user, allArtists.Count);
            double otherUserMean = CalculateUserMean(otherUser, allArtists.Count);

            //Calculates the numerator of the Pearson Correlation
            double numerator = CalculateNumerator(user, otherUser, userMean, otherUserMean, allArtists);
            //Calculates the denumerator of the Pearson Correlation
            double denumerator = CalculateDenuminator(user, otherUser, userMean, otherUserMean, allArtists);

            //Returns the Pearson Correlation
            if (denumerator == 0.0)
                return 0.0;
            else
                return numerator / denumerator;
        }
    
        private double CalculateUserMean(User user, int totalNumberOfArtists)
        {
            double temp = 0.0;
            temp = user.Artists.Values.Sum(x => x.Amount);
            temp /= totalNumberOfArtists;
            return temp;
        }

        private double CalculateNumerator(User user, User otherUser, double userMean, double otherUserMean, Dictionary<int, Artist> allArtists)
        {
           double temp = 0.0;
            foreach (Artist artist in allArtists.Values)
            {
                int artistID = artist.Id;
                if (user.Artists.ContainsKey(artist.Id) )
                {
                    if (otherUser.Artists.ContainsKey(artist.Id))
                    {
                        temp += (user.Artists[artistID].Amount - userMean) *
                                (otherUser.Artists[artistID].Amount - otherUserMean);
                    }
                    else
                    {

                        temp += (user.Artists[artistID].Amount - userMean) *
                                (0 - otherUserMean);
                    }
                }
                else if (otherUser.Artists.ContainsKey(artist.Id))
                {

                    temp += (0 - userMean) *
                            (otherUser.Artists[artistID].Amount - otherUserMean);
                }
                else
                {

                    temp += (0 - userMean) *
                            (0 - otherUserMean);
                }
            }
            return temp;


        }

        private double CalculateDenuminator(User user, User otherUser, double userMean, double otherUserMean, Dictionary<int, Artist> allArtists)
        {
            double temp = 0.0;
            double temp2 = 0.0;
            foreach (Artist artist in allArtists.Values)
            {
                int artistID = artist.Id;
                if (user.Artists.ContainsKey(artist.Id))
                {
                    temp += Math.Pow(user.Artists[artistID].Amount - userMean, 2);
                }
                else if (!user.Artists.ContainsKey(artistID))
                {
                    temp += Math.Pow(0 - userMean, 2);
                }

                if (otherUser.Artists.ContainsKey(artist.Id))
                {
                    temp2 += Math.Pow(otherUser.Artists[artistID].Amount - otherUserMean, 2);
                }
                else if (!otherUser.Artists.ContainsKey(artistID))
                {
                    temp2 += Math.Pow(0 - otherUserMean, 2);
                }
            }
            return Math.Sqrt(temp * temp2);
        }
    }
}
