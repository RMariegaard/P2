using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class FindBestMatch
    {
        ConvertIdName Convert = new ConvertIdName();
        BestMatch SingleBest = new BestMatch();
        List<Tag> test = new List<Tag>();
        KeyWordSearch Search = new KeyWordSearch();
        double ProcentMatch;
        int ProcentDoneMatchFinding;

        public Artist BestMatch(Dictionary<int, Artist> DataArtist, Artist BaseArtist, out double ProcentFinalMatch)
        {
            SingleBest.Procent = 0.00;
            for (int i = 0; i < DataArtist.Count(); i++)
            {
                test.RemoveRange(0, test.Count());

                test = Search.CompareTwoArtists(DataArtist.ElementAt(i).Value, BaseArtist);
                ProcentMatch = (100.00 / BaseArtist.TagIds.Count()) * test.Count();

                if (SingleBest.Procent < ProcentMatch && BaseArtist.Id != DataArtist.ElementAt(i).Value.Id)
                {
                    SingleBest.Procent = ProcentMatch;
                    SingleBest.ArtistID = DataArtist.ElementAt(i).Value.Id;
                    SingleBest.Element = i;
                }

                //Skriver procent
                if ((100.00 / DataArtist.Count()) * i > ProcentDoneMatchFinding)
                {
                    ProcentDoneMatchFinding++;
                    Console.Clear();
                    Console.WriteLine($"Looking for the best match to: ID: {BaseArtist.Id}\tName:{BaseArtist.Name}");
                    Console.WriteLine($"Procent Done: {ProcentDoneMatchFinding}%");
                }
            }
            ProcentFinalMatch = SingleBest.Procent;
            return Convert.ArtistFromID(SingleBest.ArtistID, DataArtist);
        }
    }
    internal class BestMatch
    {
        public int ArtistID;
        public double Procent;
        public int Element;
    }
}
