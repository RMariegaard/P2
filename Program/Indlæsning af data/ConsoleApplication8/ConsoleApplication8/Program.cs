using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fil = System.IO.File.ReadAllLines(@"C:\Users\Lasse\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\user_artists.dat");
            string[] kunst_fil = System.IO.File.ReadAllLines(@"C:\Users\Lasse\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\artists.dat");
            string[] tag_fil = System.IO.File.ReadAllLines(@"C:\Users\Lasse\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\user_taggedartists.dat");
            List<User> Users = new List<User>();
            Artist[] kunstere = new Artist[17632];
            int i = 0;
            foreach (var streng in kunst_fil.Skip(1))
            {
                string[] data = streng.Split('\t');
                Artist ny = new Artist(int.Parse(data[0]), data[1]);
                
                ny.Id = int.Parse(data[0]);
                ny.Name = data[1];
                kunstere.SetValue(ny, i);
                i++;
            }


            List<int> Alltags = new List<int>();
            List<int> Relevanttags = new List<int>();
            
            //Lav hvor du tilføjer tags til kunstere array
            foreach (var streng in tag_fil.Skip(1))
            {  
                    string[] data = streng.Split('\t');

                if (!Alltags.Contains(int.Parse(data[2])))
                {
                    Alltags.Add(int.Parse(data[2]));
                }

                else if (!Relevanttags.Contains(int.Parse(data[2])))
                {
                    Relevanttags.Add(int.Parse(data[2]));
                }

                foreach (Artist kunstner in kunstere)
                {
                    if (int.Parse(data[1]) == kunstner.Id)
                    {                                                                                            
                        if (!kunstner.TagIds.Id.Contains(int.Parse(data[2])))
                        {
                            kunstner.TagIds.Add(new Tags(int.Parse(data[2]));
                            kunstner.TagIds.Tags.Amount++;
                        }         

                    }

                }

            }



            
            int prev = 0;
            int index = -1;
            var fil1 = fil.Skip(1);

            foreach (var linje in fil1)
            {

                string[] data = linje.Split('\t');
                // 0 = bruger ID, 1 = Artist Id, 2 = weithgs

                if (prev != int.Parse(data[0]))
                {
                    User _User = new User();
                    _User.Id = int.Parse(data[0]);
                    _User.Artits.Add((new Artist(int.Parse(data[1]), int.Parse(data[2]))));
                    Users.Add(_User);
                    index++;
                }
                else{
                    Users[index].Artits.Add(new Artist(int.Parse(data[1]), int.Parse(data[2])));
                }
                prev = int.Parse(data[0]);

            }

            foreach(Artist kunsterne in Users[0].Artits)
                {
                    Console.WriteLine(kunsterne.Id);
                }

            Console.Read();

        }
    }

    class User
    {
        public int Id;
        public List<Artist> Artits = new List<Artist>();
    }

    class Artist
    {
        public int Id;
        public string Name;
        public List<Tags> TagIds = new List<Tags>();
        public int Weight;
        public Artist(int ID, int W)
        {
            this.Id = ID;
            Weight = W;
        }
        public Artist(int ID, string navn)
        {
            Id = ID;
            Name = navn;
        }
    }

    class Tags
    {
        public int Id;
        public int Amount;

        public Tags(int id)
        {
            Id = id;
        }
    }

}
