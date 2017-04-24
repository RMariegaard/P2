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
        public static List<SimilarUser> KNearestNeighbours(Func<User, User, double> correlationMeasure, User newUser, Dictionary<int, User> users, int k)
        {
            List<SimilarUser> listOfNeighbours = new List<SimilarUser>();

            //Calculates the correlation
            foreach (var user in users.Values)
            {
                SimilarUser tempUser = new SimilarUser(user.Id);
                tempUser.similarity = correlationMeasure(newUser, user);
                tempUser.Artists = user.Artists;
                listOfNeighbours.Add(tempUser);

            }

            return listOfNeighbours.OrderByDescending(x => x.similarity).ToList();
        }

        public static List<SimilarUser> KNearestNeighbours(Func<User, User, double> correlationMeasure, User newUser, Dictionary<int, User> users)
        {
            return KNearestNeighbours(correlationMeasure, newUser, users, 10);
        }

        public static Dictionary<int, RecommendedArtist> RecommendArtists(Func<User, User, double> correlationMeasure, User newUser, Dictionary<int, User> allUsers, Dictionary<int, RoskildeArtist> roskildeArtist)
        {
            Dictionary<int, RecommendedArtist> dicOfRecommendations = new Dictionary<int, RecommendedArtist>();
            List<SimilarUser> KNN = KNearestNeighbours(correlationMeasure, newUser, allUsers);
            /*
            foreach (SimilarUser user in KNN)
            {
                foreach (var artist in allUsers[user.Id].Artists.OrderByDescending(x => x.Value.Weight))
                {
                    //Checks whether artist is already in dic
                    if (dicOfRecommendations.ContainsKey(artist.Key))
                    {
                        continue;
                    }
                    /* else if(newUser.Artists.ContainsKey(artist.Key))
                     // Hvad nu hvis at brugeren har hørt kunsteren lidt men recmonden value er høj?
                     {
                         // Håndter dette problem 
                         //Evt med top Artist
                     }//
                    else
                    {
                        if (roskildeArtist.ContainsKey(artist.Key))
                        {
                            dicOfRecommendations.Add(artist.Key, new RecommendedArtist(roskildeArtist[artist.Key]));
                            dicOfRecommendations[artist.Key].CollaborativeFilteringRating = (user.similarity + artist.Value.Weight) / 2;
                        }
                    }
                }
            }
    */
            double tempSum = 0.0;
            int count = 0;
            foreach (var artist in roskildeArtist)
            {
                foreach (var similarUser in KNN)
                {
                    if (similarUser.Artists.ContainsKey(artist.Key))
                    {
                        tempSum += similarUser.similarity * similarUser.Artists[artist.Key].Weight;
                        count++;
                    }
                }
                dicOfRecommendations.Add(artist.Key, new RecommendedArtist(roskildeArtist[artist.Key]));
                if (count != 0)
                    dicOfRecommendations[artist.Key].CollaborativeFilteringRating = (tempSum / count);
                else if (newUser.Artists.ContainsKey(artist.Key))
                    dicOfRecommendations[artist.Key].CollaborativeFilteringRating = 5.55555;
            }


            var final = dicOfRecommendations.OrderByDescending(x => x.Value.CollaborativeFilteringRating).Take(10).ToDictionary(x => x.Key, x => x.Value);
           
            
            /*foreach (var artist in dicOfRecommendations)
            {
                if (roskildeArtist.Any(x => x.Key == artist.Key))
                {
                    final.Add(artist.Key, artist.Value);
                }
            }
            */
            //.OrderByDescending(x => x.Value.CollaborativeFilteringRating).Take(5).ToDictionary(x => x.Key, x => x.Value);
            return final;
        }
    }
}
