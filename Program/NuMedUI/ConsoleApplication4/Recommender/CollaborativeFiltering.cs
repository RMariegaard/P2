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

            foreach (var neighbour in KNN)
            {
                if (neighbour.Id == newUser.Id)
                {
                    continue;
                }
                //Adds Neighbours top 3 artists to recommendations
                foreach (var artist in neighbour.Artists.OrderByDescending(x => x.Value.Weight).Take(3))
                {
                    if (roskildeArtist.ContainsKey(artist.Key) && !dicOfRecommendations.ContainsKey(artist.Key))
                    {
                        dicOfRecommendations.Add(artist.Key, new RecommendedArtist(roskildeArtist[artist.Key]));
                        dicOfRecommendations[artist.Key].CollaborativeFilteringRating = neighbour.similarity;
                    }
                }
            }

            return dicOfRecommendations;

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
                     }
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
    */ /*  double tempCorelation = 0.0;
            
            int n = 0;
            int secondCount = 0;
            foreach (var artist in roskildeArtist)
            {
                n = 0;
                tempCorelation = 0.0;
                foreach (SimilarUser user in KNN)
                {
                    if (user.Artists.ContainsKey(artist.Key))
                    {
                        tempCorelation += user.similarity * (user.Artists[artist.Key].Weight);
                        n++;

                    }
                }
                if (n != 0)
                {
                    dicOfRecommendations.Add(artist.Key, new RecommendedArtist(roskildeArtist[artist.Key]));
                    dicOfRecommendations[artist.Key].CollaborativeFilteringRating = tempCorelation / n;
                    secondCount++;
                }
            }

            var final = dicOfRecommendations
                .OrderByDescending(x => x.Value.CollaborativeFilteringRating)
                .Take(10)
                .ToDictionary(x => x.Key, x => x.Value);
            
            //.OrderByDescending(x => x.Value.CollaborativeFilteringRating).Take(5).ToDictionary(x => x.Key, x => x.Value);
            return final;*/
        }
    }
} 
