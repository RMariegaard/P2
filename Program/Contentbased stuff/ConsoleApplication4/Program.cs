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
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            Console.WriteLine("Reading File");
            Dictionary<int, User> Users = BinarySerialization.ReadFromBinaryFile<Dictionary<int, User>>(startupPath + @"\users.bin");
            Dictionary<int, Artist> Artists = BinarySerialization.ReadFromBinaryFile<Dictionary<int, Artist>>(startupPath + @"\artists.bin");
            Console.WriteLine("Done Reading File");
            Console.Clear();

            var cos = new Cosine();
            var pear = new PearsonCor();
            while (true) { 
            double prevCos = 0;
            Artist hej = null;
            Console.WriteLine(";");
            int h = int.Parse(Console.ReadLine());

            Artist jeh = Artists.ToList().Find(a => a.Value.Id == h).Value;
                Dictionary<int, double> rec = new Dictionary<int, double>(); 
            double temp = 0;
            foreach (var art in Artists)
            {
                temp = pear.Calculate(jeh, art.Value);
                    if (art.Value.Id == jeh.Id)
                    {
                        //
                    }
                    else
                    {
                        rec.Add(art.Value.Id, temp);
                    }


            }
                var nname = new ConvertIdName();
            Console.WriteLine(pear.Calculate(jeh, jeh));
            Console.WriteLine(jeh.Name);
                Console.WriteLine( );
               var list = rec.OrderByDescending(art => art.Value);

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(nname.ArtistFromID((list.Skip(i).First().Key), Artists).Name + " " + list.Skip(i).First().Value);
                }


            }
            
/*
            //Variables
            KeyWordSearch Search = new KeyWordSearch();
            ConvertIdName Convert = new ConvertIdName();
            List<Artist> DataArtist = Artists.ToList<Artist>();
            List<Tag> test = new List<Tag>();
            Artist BaseArtist = new Artist(0, "Default");
            BestMatch SingleBest = new BestMatch();
            double ProcentMatch = 0.0;
            Artist BestMatch = new Artist(0, "Default");
            
            while (true)
            {
                Console.Clear();
                //UserInput
                UserInput User = new UserInput();
                Console.Write("Enter An Artist Name or ID: ");
                while (!User.TryGetArtistFromUserInput(Console.ReadLine(), DataArtist, out BaseArtist))
                {
                    Console.WriteLine("Cant Find The Artist You Are Looking For");
                    Console.Write("Enter An Artist Name or ID: ");
                }
                if (!BaseArtist.TagIds.Any())
                {
                    Console.WriteLine("This artist has no tag");
                }
                else
                {
                    //Finding the best match
                    FindBestMatch Match = new FindBestMatch();
                    BestMatch = Match.BestMatch(DataArtist, BaseArtist, out ProcentMatch);
                    Console.Clear();

                    //Printing the result:
                    Console.WriteLine($"Found The Best Match For: {BaseArtist.Id}: {BaseArtist.Name}");
                    Console.WriteLine($"The best match is: {BestMatch.Id}: {BestMatch.Name}");
                    Console.WriteLine($"The Best Match Has {ProcentMatch:0.00}% Tags In Commen With Base");
                    Console.WriteLine("\n\nThe Commen Tags Are:");
                    foreach (Tag item in Search.CompareTwoArtists(BestMatch, BaseArtist))
                    {
                        try
                        {
                            Console.WriteLine($"TagID: {item.Id}\tTag Name: {Convert.GetTagName(item.Id)}");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"TagID: {item.Id}\tTag Name: ERROR");
                        }
                    }
                }
                Console.WriteLine("\nPress Any Key To Return");
                Console.ReadKey();
            }*/
        }
    }

    public static class BinarySerialization
    {
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }
        
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
