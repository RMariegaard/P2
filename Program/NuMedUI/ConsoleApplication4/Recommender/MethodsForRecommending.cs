﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class MethodsForRecommending : PrivateMethodsUsedInMethodsForRecommending , IRecommendationsMethods
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


        //Methods Used for CollaborativeFiltering
        public Dictionary<int, RecommendedArtist> RecommendArtistsCollab(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser,
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
                        sum += user.similarity * user.Artists[artist.Key].Weight;
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


        //Method Used for getting ContentBasedFiltering
        public Dictionary<int, RecommendedArtist> RecommedArtistsContent(Func<ITaggable, ITaggable, double> correlationMeasure, User newUser, Dictionary<int, RoskildeArtist> roskildeArtist, int k)
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
