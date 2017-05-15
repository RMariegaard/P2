using NUnit.Framework;
using Recommender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Tests
{
    [TestFixture()]
    public class UserTests
    {
        // TestCases for User ID. Not much to test here, other than it is possible to assign all kinds of integers as ID´s. 
        // ID´s are loaded from the dataset, and no real validation is made withing the class, only while loading the data.
        [TestCase(-100000000)]
        [TestCase(-100)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        [TestCase(100000000)]
        public void UserIDTest(int userID)
        {
            User testUser = new User(userID);
            Assert.AreEqual(userID, testUser.ID);
        }

        // These test will check if the UserArtist dictionary works as intended:
        // Test that a given amount of artist is actually added to the Users artist dictionary:
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(50)] // Most users have 50 userartist
        [TestCase(10000)]
        public void UserArtistDictHasCorrectAmountTest(int amountOfArtists)
        {
            User testUser = new User(1);

            for(int i = 0; i < amountOfArtists; i++)
            {
                testUser.Artists.Add(i, new Userartist(i, 0, null));
            }
            Assert.AreEqual(testUser.Artists.Count, amountOfArtists);
        }

        // This test will test if userartists amount correct Value:
        [TestCase(0, 1, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 1, 10)]
        [TestCase(0, 1, 1378)]
        [TestCase(0, 1, 1234456)]
        [TestCase(0, 50, 0)]
        [TestCase(0, 50, 1)]
        [TestCase(0, 50, 10)]
        [TestCase(0, 50, 1378)]
        [TestCase(0, 50, 1234456)]
        [TestCase(1, 2, 0)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 2, 10)]
        [TestCase(1, 2, 1378)]
        [TestCase(1, 2, 1234456)]
        [TestCase(1, 50, 0)]
        [TestCase(1, 50, 1)]
        [TestCase(1, 50, 10)]
        [TestCase(1, 50, 1378)]
        [TestCase(1, 50, 1234456)]
        [TestCase(49, 50, 0)]
        [TestCase(49, 50, 1)]
        [TestCase(49, 50, 10)]
        [TestCase(49, 50, 1378)]
        [TestCase(49, 50, 1234456)]
        [TestCase(0, 1749, 0)]
        [TestCase(49, 1749, 1)]
        [TestCase(799, 1749, 10)]
        [TestCase(1748, 1749, 1378)]
        [TestCase(1748, 1749, 1234456)]
        public void UserArtistDictContainsCorrectAmountTest(int ID, int amountOfArtists, int amount)
        {
            User testUser = new User(1);
            Dictionary<int, Tag> emptyTags = new Dictionary<int, Tag>();

            for (int i = 0; i < amountOfArtists; i++)
            {
                if (i == ID)
                {
                    testUser.Artists.Add(i, new Userartist(i, amount, new Artist(i, emptyTags, "Test")));
                }
                else
                {
                    testUser.Artists.Add(i, new Userartist(i, 0, new Artist(i, emptyTags, "Test")));
                }

            }
            Assert.AreEqual(testUser.Artists[ID].Amount, amount);
        }

        // This test will test if userartists reference to artist is the correct ID/key:
        [TestCase(0, 1)]
        [TestCase(1, 3)]
        [TestCase(1, 50)]
        [TestCase(1, 1000)]
        [TestCase(1, 2)]
        [TestCase(49, 50)]
        [TestCase(1233, 1234)]
        [TestCase(17, 50)]
        [TestCase(142, 1000)]
        public void UserArtistDictContainsCorrectValuesIDTest(int ID, int amountOfArtists)
        {
            User testUser = new User(1);
            Dictionary<int, Tag> emptyTags = new Dictionary<int, Tag>();

            for (int i = 0; i < amountOfArtists; i++)
            {
                testUser.Artists.Add(i, new Userartist(i, 0, new Artist(i, emptyTags, "Test")));
            }
            Assert.AreEqual(testUser.Artists[ID].ID, ID);
        }

        /* 
               // This Test will test if specific Tag weights are calculated correct:
               // First difference in how many tags artists has are being tested:
               [TestCase(1, new int[] { 10, 10 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4 }, 25.0)]
               [TestCase(3, new int[] { 10, 10 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4 }, 25.0)]
               [TestCase(2, new int[] { 10, 10 }, new int[] { 1, 1 }, new int[] { 1, 1 }, new int[] { 1, 2 }, 50.0)]
               [TestCase(1, new int[] { 10, 10 }, new int[] { 1, 1 }, new int[] { 1, 1 }, new int[] { 1, 2 }, 50.0)]
               [TestCase(3, new int[] { 10, 10 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4 }, 25.0)]
               [TestCase(3, new int[] { 10, 10 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4 }, 25.0)]
               [TestCase(1, new int[] { 10, 10 }, new int[] { 1, 0 }, new int[] { 1 }, new int[] { 1 }, 100.0)]
               [TestCase(1, new int[] { 10, 10 }, new int[] { 3, 2 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5 }, 16.67)]
               [TestCase(5, new int[] { 10, 10 }, new int[] { 3, 2 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5 }, 25.0)]

                    // Most artists has around 14 tag. So testing for 14 tags, and that Taghandling only takes 10:
                    [TestCase(1, new int[] { 10, 10 }, new int[] { 14, 14 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 }, 5.0)]
                    [TestCase(10, new int[] { 10, 10 }, new int[] { 14, 14 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 }, 5.0)]
                    [TestCase(15, new int[] { 10, 10 }, new int[] { 14, 14 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 }, 5.0)]
                    [TestCase(24, new int[] { 10, 10 }, new int[] { 14, 14 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 }, 5.0)]
                    [TestCase(1, new int[] { 10, 10 }, new int[] { 10, 10 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20}, 5.0)]
                    [TestCase(10, new int[] { 10, 10 }, new int[] { 10, 10 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }, 5.0)]
                    [TestCase(11, new int[] { 10, 10 }, new int[] { 10, 10 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }, 5.0)]
                    [TestCase(20, new int[] { 10, 10 }, new int[] { 10, 10 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }, 5.0)]

                    // Then differences in the artists amounts are being tested (How much the user has heard the artists):
                    [TestCase(1, new int[] { 1, 1 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4 }, 25.0)]
                    [TestCase(4, new int[] { 1, 1 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4 }, 25.0)]
                    [TestCase(1, new int[] { 1, 10 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5 }, 20234.0)]
                    [TestCase(5, new int[] { 1, 10 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5 }, 20.5550)]
                    [TestCase(1, new int[] { 1, 1000 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5 }, 20324.0)]
                    [TestCase(5, new int[] { 1, 1000 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5 }, 23240.0)]
                    [TestCase(1, new int[] { 50, 100 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5 }, 20324.0)]
                    [TestCase(5, new int[] { 50, 100 }, new int[] { 2, 2 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5 }, 23240.0)] 
        // Then differences in the Tag amounts for the artists (how many times the artists has been tagged with the tag): 


        // Then testing where ID´s are identical for some tags: 

        // Then a bit of random testing:

        public void UserTagHandlingTagWeightTest(int ID, int[] artistAmounts, int[] amountOfTags, int[] tagAmounts, int[] tagIDs, double expectedTagWeight)
        {
            User testUser = new User(1);
            int tagCount = 0;
            int i = 0;
            // Each int in ArtistAmount should represent a new artist:
            foreach (int artistamount in artistAmounts)
            {
                Dictionary<int, Tag> testTags = new Dictionary<int, Tag>();
                // The int array of AmountOfTags represent how many tags the i´th artist has:
                for (int j = 0; j < amountOfTags[i]; j++)
                {
                    Tag tempTag = new Tag(tagIDs[tagCount]);
                    tempTag.Amount = tagAmounts[tagCount];
                    testTags.Add(tagIDs[tagCount], tempTag);
                    // Tag count will keep track of where in the array data must be used from:
                    tagCount++;
                }
                testUser.Artists.Add(i, new Userartist(i, artistamount, new Artist(i, testTags, "Test")));
                i++;
            }
            testUser.UserTagHandling();

            Assert.AreEqual(expectedTagWeight, Math.Round(testUser.Tags[ID].Weight, 2));
        } */

        // Test for UserTagHandling testing Tag Weight, with different values for artist amount on the user (listening time)
        [TestCase(1, new int[] { 1 }, 10.0)]
        [TestCase(1, new int[] { 10 }, 10.0)]
        [TestCase(1, new int[] { 100 }, 10.0)]
        [TestCase(1, new int[] { 10, 10 }, 5.0)]
        [TestCase(10, new int[] { 10, 10 }, 5.0)]
        [TestCase(11, new int[] { 10, 10 }, 5.0)]
        [TestCase(20, new int[] { 10, 10 }, 5.0)]
        [TestCase(7, new int[] { 0, 100 }, 0.0)]
        [TestCase(18, new int[] { 0, 100 }, 10.0)]
        [TestCase(7, new int[] { 1, 10 }, 0.91)]
        [TestCase(18, new int[] { 1, 10 }, 9.09)]
    /*    [TestCase(7, new int[] { 10, 100 }, 0.91)]
        [TestCase(18, new int[] { 10, 100 }, 9.09)]
        [TestCase(7, new int[] { 1, 100 }, 0.91)]
        [TestCase(18, new int[] { 1, 100 }, 9.09)] */

        public void UserTagHandlingTagWeightDifferentArtistAmountTest(int ID, int[] artistAmounts, double expectedTagWeight)
        {
            User testUser = new User(1);
            int tagCount = 1;
            int i = 1;
            // Each int in ArtistAmount should represent a new artist:
            foreach (int artistamount in artistAmounts)
            {
                Dictionary<int, Tag> testTags = new Dictionary<int, Tag>();
                // In this testcase, each artist will have 10 tags with an amount of 1 (tag.weight = 10.0)
                for (int j = 0; j < 10; j++)
                {
                    Tag tempTag = new Tag(tagCount);
                    tempTag.Amount = 1;
                    testTags.Add(tagCount, tempTag);
                    // Tag count makes sure all tags are unique for this test:
                    tagCount++;
                }
                testUser.Artists.Add(i, new Userartist(i, artistamount, new Artist(i, testTags, "Test")));
                i++;
            }
            testUser.UserTagHandling();
            double value = Math.Round(testUser.Tags[ID].Weight, 2);
            Assert.AreEqual(expectedTagWeight, value);
        }


        // Test that the method CalculateArtistWeight() calculates correct weight value:
        [TestCase(0, 1, new int[] { 1 }, 100)]
        [TestCase(0, 1, new int[] { 100 }, 100)]
        [TestCase(0, 1, new int[] { 100000 }, 100)]
        [TestCase(0, 2, new int[] { 1, 1 }, 50)]
        [TestCase(0, 2, new int[] { 10, 10 }, 50)]
        [TestCase(0, 2, new int[] { 1000, 1000 }, 50)]
        [TestCase(1, 2, new int[] { 1, 1 }, 50)]
        [TestCase(1, 2, new int[] { 0, 87 }, 100)]
        [TestCase(1, 2, new int[] { 452, 0 }, 0)]
        [TestCase(9, 10, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 10)]
        [TestCase(1, 10, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 10)]
        [TestCase(4, 10, new int[] { 12, 1335, 123, 1123, 0, 1213, 1332, 114, 1456, 1 }, 0)]
        [TestCase(1, 10, new int[] { 12, 1335, 123, 1123, 0, 1213, 1332, 114, 1456, 1 }, 19.898643613057089)]
        public void CalculateArtistWeightTest(int ID, int amountOfArtists, int[] artistsAmounts, double expectedWeight)
        {
            User testUser = new User(1);
            Dictionary<int, Tag> emptyTags = new Dictionary<int, Tag>();

            for (int i = 0; i < amountOfArtists; i++)
            {
                testUser.Artists.Add(i, new Userartist(i, artistsAmounts[i], new Artist(i, emptyTags, "Test")));
            }
            testUser.CalculateArtistWeight();

            Assert.AreEqual(expectedWeight, testUser.Artists[ID].Weight);
        }
    }
}