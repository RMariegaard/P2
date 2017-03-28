﻿using System;
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

            List<User> Users = BinarySerialization.ReadFromBinaryFile<List<User>>(@"C: \Users\Mark\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\users.bin");
            Artist[] Artists = BinarySerialization.ReadFromBinaryFile<Artist[]>(@"C: \Users\Mark\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\artists.bin");

            Console.WriteLine("done");

            foreach (Artist element in Artists)
            {
                Console.WriteLine(element.Id);
            }


            Console.Read();
            /*
                // Fil Indlæsning:
                string[] file = System.IO.File.ReadAllLines(@"C:\Users\Casper\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\user_artists.dat");
                string[] artist_file = System.IO.File.ReadAllLines(@"C:\Users\Casper\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\artists.dat");
                string[] tag_fil = System.IO.File.ReadAllLines(@"C:\Users\Casper\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\user_taggedartists.dat");

                // Initialisering af User list og Kunstner Arrayet:
                List<User> Users = new List<User>();
                Artist[] Artists = new Artist[17632];

                // "Artists" array udfyldes med alle kunstnere i datasættet:
                int i = 0;
                foreach (var String in artist_file.Skip(1))
                {
                    string[] data = String.Split('\t');
                    Artist New = new Artist(int.Parse(data[0]), data[1]);

                    New.Id = int.Parse(data[0]);
                    New.Name = data[1];
                    Artists.SetValue(New, i);
                    i++;
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
                        User _User = new User();
                        _User.Id = int.Parse(data[0]);
                        Users.Add(_User);
                        index++;
                    }

                    // Artist ID og antal afspilninger bliver indsat i Users Artist liste:
                    Users[index].Artists.Add(new Userartist(int.Parse(data[1]), int.Parse(data[2])));

                    // Det nuværende ID sættes over I Prev:
                    prev = int.Parse(data[0]);

                }

                // Tags bliver overført til respektive kunstnere:
                foreach (string streng in tag_fil.Skip(1))
                {
                    string[] data = streng.Split('\t');
                    foreach (Artist artist in Artists)
                    {
                        if(int.Parse(data[1]) == artist.Id)
                        {
                            artist.TagIds.Add(new Tag(int.Parse(data[2])));
                        }
                    }
                }
                BinarySerialization.WriteToBinaryFile<List<User>>(@"C: \Users\Casper\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\users.bin", Users);
                BinarySerialization.WriteToBinaryFile<Artist[]>(@"C: \Users\Casper\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\artists.bin", Artists);



                Console.WriteLine("done");

                BinarySerialization.WriteToBinaryFile<List<User>>(@"C: \Users\Casper\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\users.bin", Users);
                BinarySerialization.WriteToBinaryFile<Artist[]>(@"C: \Users\Casper\Documents\GitHub\P2\Program\Indlæsning af data\ConsoleApplication8\ConsoleApplication8\artists.bin", Artists);
                */
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
