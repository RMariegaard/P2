using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Recommender
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryData Data = new BinaryData();

            Data.DoItAll();

            Console.WriteLine("done");
            Console.ReadKey();    
        }
    }
}
