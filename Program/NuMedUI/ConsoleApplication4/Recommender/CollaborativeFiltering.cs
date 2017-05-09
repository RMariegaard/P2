using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class CollaborativeFiltering
    {
        //Finds the users with the highest correlation based on a given correltion measure
        public static List<SimilarUser> KNearestNeighbours(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser, Dictionary<int, User> users, int k, Dictionary<int, Artist> allArtists)
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
            return listOfNeighbours.OrderByDescending(x => x.similarity).Where(x => x.similarity > 0).ToList();
        }

        public static List<SimilarUser> KNearestNeighbours(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser, Dictionary<int, User> users, Dictionary<int, Artist> allArtists)
        {
            return KNearestNeighbours(correlationMeasure, newUser, users, 10, allArtists);
        }

        public static Dictionary<int, RecommendedArtist> RecommendArtists(Func<User, User, Dictionary<int, Artist>, double> correlationMeasure, User newUser, 
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
                    dicOfRecommendations[artist.Key].CollaborativeFilteringRating = sum / n;
                }
            }
            //Takes the top 10 artist based on the filtering rating and returns them in a dictonary of recommended artists
            var final = dicOfRecommendations
                .OrderByDescending(x => x.Value.CollaborativeFilteringRating)
                .Take(10)
                .ToDictionary(x => x.Key, x => x.Value);
            
            return final;
        }
    }
} 
