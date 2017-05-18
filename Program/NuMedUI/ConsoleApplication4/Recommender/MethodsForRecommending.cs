using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class MethodsForRecommending : IRecommendationsMethods
    {
        //Methods for getting Cosine 

        //Metod used to get Cosine. 
        public double GetCosine<T>(T element1, T element2)
        where T : ITaggable
        {
            double denominator = CalculateProductOfLengths(element1, element2);
            double numerator = CalculateDotProductInCosine(element1, element2);
            double value;
            //Can't devide by zero
            //The denominator is zero when atleast one of the vectors has length 0
            //that means that one of the elemnts has no tag and 
            //therfore the correlation should be zero
            if (denominator == 0)
            {
                value = 0;
            }
            else
            {
                value = numerator / denominator;
            }
            return value;
        }
        
        //Method used in GetCosine
        private double CalculateDotProductInCosine<T>(T element1, T element2)
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
        private double CalculateProductOfLengths<T>(T element1, T element2)
            where T : ITaggable
        {
            double temp = element1.Tags.Sum(x => Math.Pow(x.Value.Weight, 2));
            double temp2 = element2.Tags.Sum(x => Math.Pow(x.Value.Weight, 2));
            return Math.Sqrt(temp) * Math.Sqrt(temp2);
        }

        //Calcuates the Pearson Correlation between two users based on artists
        public double GetPearson(User user, User otherUser, Dictionary<int, Artist> allArtists)
        {
            List<int> listOfArtists = new List<int>();
            listOfArtists = user.Artists.Keys.Union(otherUser.Artists.Keys).ToList();

            //Calculates the mean of the artist weight for the two users
            int count = allArtists.Count;
            double userMean = CalculateUserMean(user, count);
            double otherUserMean = CalculateUserMean(otherUser, count);

            //Calculates the numerator of the Pearson Correlation
            double numerator = CalculatePearsonNumerator(user, otherUser, userMean, otherUserMean, listOfArtists, count);
            //Calculates the denumerator of the Pearson Correlation
            double denumerator = CalculatePearsonDenuminator(user, otherUser, userMean, otherUserMean, listOfArtists, count);

            //Returns the Pearson Correlation
            if (denumerator == 0.0)
                return 0.0;
            else
                return numerator / denumerator;
        }
        //Method for gettin PearsonCorrelation 

        private double CalculateUserMean(User user, int totalNumberOfArtists)
        {
            double temp = 0.0;
            temp = user.Artists.Values.Sum(x => x.Amount);
            temp /= totalNumberOfArtists;
            return temp;
        }

        private double CalculatePearsonNumerator(User user, User otherUser, double userMean, double otherUserMean, List<int> listOfArtists, int count)
        {
            //first the temp value for all the artists who have been heard by one of the users is calculated
            double temp = 0.0;
            foreach (int artistId in listOfArtists)
            {
                if (user.Artists.ContainsKey(artistId))
                {
                    if (otherUser.Artists.ContainsKey(artistId))
                    {
                        temp += (user.Artists[artistId].Amount - userMean) *
                                (otherUser.Artists[artistId].Amount - otherUserMean);
                    }
                    else
                    {

                        temp += (user.Artists[artistId].Amount - userMean) *
                                (0 - otherUserMean);
                    }
                }
                else if (otherUser.Artists.ContainsKey(artistId))
                {

                    temp += (0 - userMean) *
                            (otherUser.Artists[artistId].Amount - otherUserMean);
                }
            }
            //now the remaining artists who have not been calculated all have values of 0, so this can be calculated.
            int remainingCount = count - listOfArtists.Count();
            temp += ((0 - userMean) * (0 - otherUserMean)) * remainingCount;

            return temp;


        }

        private double CalculatePearsonDenuminator(User user, User otherUser, double userMean, double otherUserMean, List<int> listOfArtists, int count)
        {
            double temp = 0.0;
            double temp2 = 0.0;
            foreach (int artistId in listOfArtists)
            {
                if (user.Artists.ContainsKey(artistId))
                {
                    temp += Math.Pow(user.Artists[artistId].Amount - userMean, 2);
                }
                else if (!user.Artists.ContainsKey(artistId))
                {
                    temp += Math.Pow(0 - userMean, 2);
                }
                if (otherUser.Artists.ContainsKey(artistId))
                {
                    temp2 += Math.Pow(otherUser.Artists[artistId].Amount - otherUserMean, 2);
                }
                else if (!otherUser.Artists.ContainsKey(artistId))
                {
                    temp2 += Math.Pow(0 - otherUserMean, 2);
                }
            }
            int remainingCount = count - listOfArtists.Count;
           
            temp += Math.Pow(0 - userMean, 2) * remainingCount;
            temp2 += Math.Pow(0 - otherUserMean, 2) * remainingCount;

            return Math.Sqrt(temp * temp2);
        }

        //Methods Used for CollaborativeFiltering
        public Dictionary<int, RecommendedArtist> RecommendArtistsCollaborative(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser,
                            Dictionary<int, User> allUsers, Dictionary<int, RoskildeArtist> roskildeArtist, Dictionary<int, Artist> allArtists)
        {
            Dictionary<int, RecommendedArtist> dicOfRecommendations = new Dictionary<int, RecommendedArtist>();
            List<SimilarUser> KNN = KNearestNeighbours(correlationMeasure, newUser, allUsers, allArtists);

            //Used to sum up the weight times the correlation value
            double sum;
            //n represents the number of users listened to the artist
            int n;

            //Goes through all the roskilde artists and checks if any similar user have heard if
            //if this is the case the score contributed by the user is added to the sum, which then is diveded
            //by the number of users who have heard the artist
            foreach (var artist in roskildeArtist)
            {
                n = 0;
                sum = 0.0;
                foreach (SimilarUser user in KNN)
                {
                    if (user.Artists.ContainsKey(artist.Key))
                    {
                        sum += user.Similarity * user.Artists[artist.Key].Weight;
                        n++;
                    }
                }
                //n is larger than zero if the artist have been heard by any similar user
                if (n > 0)
                {
                    dicOfRecommendations.Add(artist.Key, new RecommendedArtist(roskildeArtist[artist.Key]));
                    dicOfRecommendations[artist.Key].FilteringRating = sum / n;
                }
            }
            //Takes the top 10 artist based on the filtering rating and returns them in a dictonary of recommended artists
            var final = dicOfRecommendations
                .OrderByDescending(x => x.Value.FilteringRating)
                .Take(10)
                .ToDictionary(x => x.Key, x => x.Value);

            return final;
        }
        //Finds the users with the highest correlation based on a given correltion measure
        private List<SimilarUser> KNearestNeighbours(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser, Dictionary<int, User> users, int k, Dictionary<int, Artist> allArtists)
        {
            List<SimilarUser> listOfNeighbours = new List<SimilarUser>();

            //Calculates the correlation
            foreach (var user in users.Values)
            {
                SimilarUser tempUser = new SimilarUser(user.ID);
                tempUser.Similarity = correlationMeasure(newUser, user, allArtists);
                tempUser.Artists = user.Artists;
                listOfNeighbours.Add(tempUser);
            }
            return listOfNeighbours.OrderByDescending(x => x.Similarity).Where(x => x.Similarity > 0).Take(k).ToList();
        }

        private List<SimilarUser> KNearestNeighbours(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser, Dictionary<int, User> users, Dictionary<int, Artist> allArtists)
        {
            return KNearestNeighbours(correlationMeasure, newUser, users, 15, allArtists);
        }


        //Method Used for getting ContentBasedFiltering
        public Dictionary<int, RecommendedArtist> RecommedArtistsContentBased(Func<ITaggable, ITaggable, double> correlationMeasure, User newUser, Dictionary<int, RoskildeArtist> roskildeArtist, int k)
        {
            var recommendedArtist = new Dictionary<int, RecommendedArtist>();

            //Calculates the correlation for the artists and adds them to the dictionary above
            foreach (RoskildeArtist artist in roskildeArtist.Values)
            {
                var tempArtist = new RecommendedArtist(artist);
                tempArtist.FilteringRating = correlationMeasure(newUser, artist);
                recommendedArtist.Add(tempArtist.Id, tempArtist);

            }
            //Takes the k best artists and returns them as a dictornary
            var final = recommendedArtist
                .OrderByDescending(x => x.Value.FilteringRating)
                .Take(k)
                .ToDictionary((x => x.Key), (x => x.Value));

            return final;
        }



    }
}
