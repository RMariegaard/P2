using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Recommender
{
    public class Program
    {
        static void Main(string[] args)
        {
            CreateRecommendations Recommender = new CreateRecommendations();
            Console.WriteLine("Do you want to update the datafiles? (Y/N)");
            bool readInput = (Console.ReadKey().KeyChar == 'Y')?true:false;
            if (readInput)
            {
                UpdateData.UpdateDataFiles();
            }
            Console.Clear();
            /*************************************** The Program ***************************************/
            Recommender.LoadFiles();

            while (true)
            {
                Recommender.Recommender();
            }
        }
    }
}
