/*
 
 This Projekt is for a one time use, at the start of a new user.
 It is for reading the .txt files with users, artists and so on. And put this into a binary file.
 Futhermore it will also put the roskilde artists that are not in the Last.fm dataset, into the binary files.
 And calculate the weight of the tags for the artists.
 */

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
            //Simple DOItAll funktion
            BinaryData Data = new BinaryData();
            Data.DoItAll();

            Console.WriteLine("done");
            Console.ReadKey();    
        }
    }
}
