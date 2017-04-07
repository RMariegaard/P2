using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class CollaborativeFiltering
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
            

            return listOfNeighbours.OrderByDescending(x=> x.similarity).Take(k).ToList();
        }

        public static List<SimilarUser> KNearestNeighbours(Func<User, User, double> correlationMeasure, User newUser, Dictionary<int, User> users) {
            return KNearestNeighbours(correlationMeasure, newUser, users, 5);
        }

        

        public static Dictionary<int,RecommendedArtist> RecommendArtists(User newUser, Dictionary<int, User> allUsers)
        {
            Dictionary<int, RecommendedArtist> dicOfRecommendations = new Dictionary<int, RecommendedArtist>();

            List<SimilarUser> KNN = KNearestNeighbours(PearsonCor.CalculateUser, newUser, allUsers);
            

            foreach(SimilarUser user in KNN)
            {
                foreach(var artist in allUsers[user.Id].Artists.OrderByDescending(x => x.Value.Weight).Take(10)) //Hvor mange??
                {
                    //Checks whether artist is already in dic
                    if (dicOfRecommendations.ContainsKey(artist.Key))
                    {
                        continue;
                    }
                    else if(newUser.Artists.ContainsKey(artist.Key))
                    // Hvad nu hvis at brugeren har hørt kunsteren lidt men recmonden value er høj?
                    {
                        // Håndter dette problem 
                        //Evt med top Artist
                    }
                    else
                    {
                        dicOfRecommendations.Add(artist.Key, new RecommendedArtist(artist.Value.ThisArtist));
                        dicOfRecommendations[artist.Key].CollaborativeFilteringRating = (user.similarity + artist.Value.Weight) / 2;
                    }
                }
            }
            return dicOfRecommendations;
        }





    }
}
