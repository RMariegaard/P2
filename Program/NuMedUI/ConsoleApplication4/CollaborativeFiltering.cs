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
                listOfNeighbours.Add(tempUser);
            }

            return listOfNeighbours.OrderByDescending(x => x.similarity).Take(k).ToList();
        }

        public static List<SimilarUser> KNearestNeighbours(Func<User, User, double> correlationMeasure, User newUser, Dictionary<int, User> users)
        {
            return KNearestNeighbours(correlationMeasure, newUser, users, 10);
        }

        public static Dictionary<int, RecommendedArtist> RecommendArtists(Func<User, User, double> correlationMeasure, User newUser, Dictionary<int, User> allUsers, Dictionary<int, RoskildeArtist> roskildeArtist)
        {
            Dictionary<int, RecommendedArtist> dicOfRecommendations = new Dictionary<int, RecommendedArtist>();
            List<SimilarUser> KNN = KNearestNeighbours(correlationMeasure, newUser, allUsers);

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
                     }*/
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

            var final = new Dictionary<int, RecommendedArtist>();
            foreach (var artist in dicOfRecommendations)
            {
                if (roskildeArtist.Any(x => x.Key == artist.Key))
                {
                    final.Add(artist.Key, artist.Value);
                }
            }

            //.OrderByDescending(x => x.Value.CollaborativeFilteringRating).Take(5).ToDictionary(x => x.Key, x => x.Value);
            return final;
        }
    }
}
