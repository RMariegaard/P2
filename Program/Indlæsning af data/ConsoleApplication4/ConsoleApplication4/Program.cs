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
            /*
            List<User> Users = BinarySerialization.ReadFromBinaryFile<List<User>>(@"C: \Users\Mark\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\users.bin");
            Artist[] Artists = BinarySerialization.ReadFromBinaryFile<Artist[]>(@"C: \Users\Mark\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\artists.bin");

            Console.WriteLine("done");

            foreach (Artist element in Artists)
            {
                Console.WriteLine(element.Id);
            }


            Console.Read();*/

            // Fil Indlæsning:
            string Username = Environment.UserName;
            string[] file = System.IO.File.ReadAllLines(@"C:\Users\"+Username+@"\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\user_artists.dat");
                string[] artist_file = System.IO.File.ReadAllLines(@"C:\Users\" + Username + @"\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\artists.dat");
                string[] tag_fil = System.IO.File.ReadAllLines(@"C:\Users\" + Username + @"\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\user_taggedartists.dat");

                // Initialisering af User list og Kunstner Arrayet:
                Dictionary<int, User> UsersDic = new Dictionary<int, User>();
            var Artists = new Dictionary<int, Artist>();
            var Users = new List<User>();

                // "Artists" dic udfyldes med alle kunstnere i datasættet:
                foreach (var String in artist_file.Skip(1))
                {
                    string[] data = String.Split('\t');
                    Artists.Add(int.Parse(data[0]), new Artist(int.Parse(data[0]), data[1]));


                }

                // Users listen bliver udfyldt:
                // 0 = bruger ID, 1 = Artist Id, 2 = weight
                int prev = 0;
                int index = -1;
                var file1 = file.Skip(1);

                foreach (var line in file1)
                {
                    // Filen opsplittes:
                    string[] data = line.Split('\t');  

                    // Hvis ikke User ID allerede eksisteres laves der en ny User:
                    if (prev != int.Parse(data[0]))
                    {
                        User _User = new User(int.Parse(data[0]));
                        Users.Add(_User);
                        index++;
                    }

                    // Der laves en tempartist til senere:

                    int tempId = int.Parse(data[1]);

                    // Artist ID og antal afspilninger bliver indsat i Users Artist liste. Samtidig bliver der oprettet en "pointer" til den instans af artist der allerede eksisterer:
                    Users[index].Artists.Add(tempId, new Userartist(tempId, int.Parse(data[2]), Artists[tempId]));
                     
                    // Det nuværende ID sættes over I Prev:
                    prev = int.Parse(data[0]);

                }

            // Tags bliver overført til respektive kunstnere:
                int TempTagID;
                foreach (string streng in tag_fil.Skip(1))
                {
                    string[] data = streng.Split('\t');
                    TempTagID = int.Parse(data[2]);
                    foreach (var artist in Artists)
                    {
                    if (int.Parse(data[1]) == artist.Value.Id)
                    {
                        if (artist.Value.Tags.ContainsKey(TempTagID))
                            {
                                artist.Value.Tags[TempTagID].Amount++;
                            }
                       else
                            {
                                artist.Value.Tags.Add(TempTagID, new Tag(TempTagID));
                            }
                        break;
                        }
                    }
                }

            foreach (var artist in Artists)
            {
                artist.Value.CalcTagWeight();
            }

            foreach ( User user in Users)
            {
                user.TagCalc();
                user.CalculateArtistWeight();
            }

            //Midlertidigt
           foreach(var userr in Users)
            {
                UsersDic.Add(userr.Id, userr);
            }


                BinarySerialization.WriteToBinaryFile<Dictionary<int, User>>(@"C: \Users\" + Username + @"\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\users.bin", UsersDic);
                BinarySerialization.WriteToBinaryFile< Dictionary<int, Artist>> (@"C: \Users\" + Username + @"\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication4\ConsoleApplication4\artists.bin", Artists);



                Console.WriteLine("done");
            Console.ReadKey();

               
                
        }
    }

    /// <summary>
    /// Functions for performing common binary Serialization operations.
    /// <para>All properties and variables will be serialized.</para>
    /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
    /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
    /// </summary>
    public static class BinarySerialization
    {
        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the XML file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the XML file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the XML.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
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
