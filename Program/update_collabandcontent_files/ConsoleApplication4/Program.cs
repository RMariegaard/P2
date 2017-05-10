using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Recommender
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*************************************** The Program ***************************************/
            CreateRecommendations Recommender = new CreateRecommendations();
            Recommender.LoadFiles();
            Dictionary<int, RecommendedArtist> collab;
            Dictionary<int, RecommendedArtist> content;
            List<double> collabRatings = new List<double>();
            List<double> contentRating = new List<double>();
            for (int i = 2; i < 400; i++)
            {
                Recommender.GenerateRecommendations(i);
                collab = Recommender.RecommendedCollabArtists;
                content = Recommender.RecommendedContetArtists;
                if (collab != null && collab.Count() != 0)
                {
                    foreach (var element in collab)
                    {
                        element.Value.userID = i;
                        collabRatings.Add(element.Value.CollaborativeFilteringRating);
                    }
                }
                if (content != null && content.Count() != 0)
                {
                    foreach (var element in content)
                    {
                        element.Value.userID = i;
                        contentRating.Add(element.Value.ContentBasedFilteringRating);
                    }
                }
            }

            contentRating = contentRating.OrderByDescending(x => x).ToList();
            collabRatings = collabRatings.OrderByDescending(x => x).ToList();

            string startupPath = Environment.CurrentDirectory;
            //Does it twice to go back two folders
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            BinarySerialization.WriteToBinaryFile<List<double>>(startupPath + @"\DataFiles\CollabRatings.bin", collabRatings);
            BinarySerialization.WriteToBinaryFile<List<double>>(startupPath + @"\DataFiles\contentRating.bin", contentRating);

            Application.EnableVisualStyles();
            Application.Run(new graph(collabRatings, contentRating));

            Console.WriteLine("DONE");
            Console.ReadKey();

        }
    }
}
