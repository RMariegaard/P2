using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lastfm.Services;
using Lastfm.Scrobbling;
using Lastfm;
using System.IO;


namespace ConsoleApplication9
{
    class Program
    {
        static void Main(string[] args)
        {

            // Get your own API_KEY and API_SECRET from http://www.last.fm/api/account
            string API_KEY = "809d18d1dde72ebeeb181b29c9856571";
            string API_SECRET = "85d5bcb1141a692e9750baa8b446a503";

            // Create your session
            Session session = new Session(API_KEY, API_SECRET);

            var RList = new List<RArtist>();
            var ImageDic = new Dictionary<RArtist, string>();


            foreach (string date in RoskildeArtistsFiler.allFiles)
            {
                Console.Write(date +": ");
                AddArtistData.Start(session, RList, ImageDic, date);
                Console.WriteLine("Done!");
            }

            List<string> forFile = new List<string>(); 
            foreach (var artist in RList)
            {
                forFile.Add(artist.Id.ToString() + "\t" + artist.Name);
                foreach(var tag in artist.TagIds)
                {
                    forFile.Add(tag.Name + "\t" + tag.amount);
                }
                forFile.Add("");
            }

            System.IO.File.WriteAllLines(@"C:\Users\Casper\Documents\GitHub\P2\Tidsplan roskilde\test.txt", forFile);

            foreach (var picture in ImageDic)
            {
                if (picture.Value != "")
                    using (WebClient client = new WebClient())
                    {
                        try {
                            client.DownloadFile(new Uri(picture.Value), $"C:\\Users\\Casper\\Documents\\GitHub\\P2\\Tidsplan roskilde\\pics\\{picture.Key.Name}.png");
                        }
                        catch (Exception)
                        {
                            //do nothing - skip
                        }
                        }
            }



            
            Console.Read();


            }
        }
    }
