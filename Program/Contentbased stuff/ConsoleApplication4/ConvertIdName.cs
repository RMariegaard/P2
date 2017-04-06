using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    class ConvertIdName
    {
        //Artist
        public Artist ArtistFromID(int ID, Dictionary<int,Artist> Artists)
        {
            return Artists[ID];
           // throw new Exception("ID Dosent Match an Artist Name");
        }

        public Artist ArtistFromName(string Name, Dictionary<int, Artist> Artists)
        {
            for (int i = 0; i < Artists.Count(); i++)
            {

                if (Artists.ElementAt(i).Value.Name.ToUpper() == Name.ToUpper())
                    return Artists.ElementAt(i).Value;
            }
            throw new Exception("Name Dosent Match an Artist Name");
        }

        //Tag
        public string GetTagName(int ID)
        {
            string startupPath = Environment.CurrentDirectory;
            startupPath = Path.GetDirectoryName(startupPath);
            startupPath = Path.GetDirectoryName(startupPath);

            string name = "";
            string[] tagsFil = System.IO.File.ReadAllLines(startupPath + @"\tags.dat");
            foreach (var line in tagsFil)
            {
                string[] data = line.Split('\t');
                if (ID.ToString() == data[0])
                {
                    name = data[1];
                }
            }

            if (name == "")
            {
                throw new Exception("ID dosent match a tag");
            }
            return name;
        }
    }
}
