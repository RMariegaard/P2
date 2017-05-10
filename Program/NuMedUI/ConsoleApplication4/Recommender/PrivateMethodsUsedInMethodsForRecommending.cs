using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class PrivateMethodsUsedInMethodsForRecommending
    {

        //Method used in GetCosine
        protected double CalculateDotProductInCosine<T>(T element1, T element2)
            where T : ITaggable
        {
            double dot = 0.0;
            foreach (var tag in element1.Tags)
            {
                if (element2.Tags.ContainsKey(tag.Key))
                {
                    dot += element2.Tags[tag.Key].Weight * tag.Value.Weight;
                }
            }
            return dot;
        }

        //Method used in GetCosine
        //Calculates the length of each vector and multiplies them
        //The length is calculated by the coordinates squares summed and then taking the square root
        protected double CalculateProductOfLengths<T>(T element1, T element2)
            where T : ITaggable
        {
            double temp = element1.Tags.Sum(x => Math.Pow(x.Value.Weight, 2));
            double temp2 = element2.Tags.Sum(x => Math.Pow(x.Value.Weight, 2));
            return Math.Sqrt(temp) * Math.Sqrt(temp2);
        }

        //Method for gettin PearsonCorrelation 

        protected double CalculateUserMean(User user, int totalNumberOfArtists)
        {
            double temp = 0.0;
            temp = user.Artists.Values.Sum(x => x.Amount);
            temp /= totalNumberOfArtists;
            return temp;
        }

        protected double CalculateNumerator(User user, User otherUser, double userMean, double otherUserMean, Dictionary<int, Artist> allArtists)
        {
            double temp = 0.0;
            foreach (Artist artist in allArtists.Values)
            {
                int artistID = artist.Id;
                if (user.Artists.ContainsKey(artist.Id))
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

        protected double CalculateDenuminator(User user, User otherUser, double userMean, double otherUserMean, Dictionary<int, Artist> allArtists)
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

        //Methods used in CollaborativeFiltering 

        //Finds the users with the highest correlation based on a given correltion measure
        protected List<SimilarUser> KNearestNeighbours(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser, Dictionary<int, User> users, int k, Dictionary<int, Artist> allArtists)
        {
            List<SimilarUser> listOfNeighbours = new List<SimilarUser>();

            //Calculates the correlation
            foreach (var user in users.Values)
            {
                SimilarUser tempUser = new SimilarUser(user.Id);
                tempUser.similarity = correlationMeasure(newUser, user, allArtists);
                tempUser.Artists = user.Artists;
                listOfNeighbours.Add(tempUser);
            }
            return listOfNeighbours.OrderByDescending(x => x.similarity).Where(x => x.similarity > 0).Take(k).ToList();
        }

        protected List<SimilarUser> KNearestNeighbours(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser, Dictionary<int, User> users, Dictionary<int, Artist> allArtists)
        {
            return KNearestNeighbours(correlationMeasure, newUser, users, 15, allArtists);
        }

        //Methods used in ContentBasedFiltering
    }
}
