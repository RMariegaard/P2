using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    public class UpdateData
    {
        public static void UpdateDataFiles()
        {
            /*************************************** Write file ***************************************/
            Console.WriteLine("Reading text files, and writing binary files");
            BinaryData Data = new BinaryData();
            Data.MakeBinaryFiles();
            /*try
            {
                Data.MakeBinaryFiles();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }*/
            Console.WriteLine("Done writing files");
        }
    }
}
