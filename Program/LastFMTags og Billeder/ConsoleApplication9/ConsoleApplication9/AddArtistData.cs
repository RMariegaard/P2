using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lastfm;
using Lastfm.Services;
using Lastfm.Scrobbling;

namespace ConsoleApplication9
{
    class AddArtistData
    {
        public static void Start(Session session, List<RArtist> RList, Dictionary<RArtist, string> ImageDic, string date)
        {
            foreach (string navn in GetArtistFromFile.Names(date))
            {
                Artist kusnt = new Artist(navn, session);
                RArtist ny = new RArtist(navn);
                try
                {
                    kusnt.GetTopTags().ToList().ForEach(x => ny.TagIds.Add(new RTag(x.Item.Name, x.Weight)));
                    RList.Add(ny);
                    ImageDic.Add(ny, kusnt.GetImageURL());
                    

                }
                catch (ServiceException)
                {
                    Console.WriteLine("Artist not found " + ny.Name);
                }
            }

        }


    }
}
