using Recommender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskileRecommenderTests2
{
    // This is a class containing methods to make testsubjects for UnitTesting.
    class MakeTestSubjects
    {
        // Method to make Users with dictionaries of Userartists containing ID:
        public User MakeSimpleUserArtistTestSubject(int ID, int[] keys)
        {
            int count = 0;
            User testUser = new User(ID);

            foreach (int key in keys)
            {
                testUser.Artists.Add(key, new Userartist(key, 0, null));
            }

            return testUser;
        }

        // Method to make Users with dictionaries of Userartists containing ID, weight and amount values:
        public User MakeUserWithArtistWeightAndAmountOnlyTestSubject(int ID, int[] artistKeys, double[] weightValues, int[] amountValues)
        {
            if (artistKeys.Length != weightValues.Length || weightValues.Length != amountValues.Length)
            {
                throw new Exception("Error when making users (MakeUserWithWeightAndAmountOnlyArtistsTestSubject) for test! All arrays of data most be same length!");
            }
            int count = 0;
            User testUser = new User(ID);

            foreach (int key in artistKeys)
            {
                testUser.Artists.Add(key, new Userartist(key, amountValues[count], null));
                testUser.Artists[key].Weight = weightValues[count];
            }

            return testUser;
        }

        // Method for making Users with a dictionary of Userartist containing ID, weight, amount and has a reference to an existing artist:
        public User MakeUserWithArtistsWithTagsTestSubject(int ID, int[] keys, double[] weightValues, int[] amountValues, Artist[] artists)
        {
            // If methods is called with empty artist array, default artists will be made:
            if (artists == null || artists.Length == 0)
            {
                int limit = keys.Length;
                artists = new Artist[limit];
                for (int i = 0; i < limit; i++)
                {
                    artists[i] = MakeSimpleArtistTestSubject(i);
                }
            }
            // Check if method is called correct:
            if (keys.Length != weightValues.Length || weightValues.Length != amountValues.Length ||
                amountValues.Length != artists.Length)
            {
                throw new Exception("Error when making users (MakeUserWithArtists) for test! All arrays of data most be same length!");
            }

            int count = 0;
            User testUser = new User(ID);

            foreach (int key in keys)
            {
                testUser.Artists.Add(key, new Userartist(key, amountValues[count], artists[count]));
                testUser.Artists[key].Weight = weightValues[count];
            }

            return testUser;
        }

        // Method for making simple artists containing a default name and empty tag dictionary:
        public Artist MakeSimpleArtistTestSubject(int ID)
        {
            return new Artist(ID, null, "test");
        }

        // Method for making artists containing Tagdata.
        public Artist MakeArtistWithTagsTestSubject(int ID, int[] tagAmounts)
        {
            Dictionary<int, Tag> tags = new Dictionary<int, Tag>();
            int tagID = 0;
            foreach (int tagAmount in tagAmounts)
            {
                tagID++;
                tags.Add(tagID, new Tag(tagID));
                tags[tagID].Amount = tagAmount;
            }
            Artist testArtist = new Artist(ID, tags, "test");
            return testArtist;
        }
    }
}
