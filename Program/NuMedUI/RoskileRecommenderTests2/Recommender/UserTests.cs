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

        // Test that checks if users tagdictionary works as intended:
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        [TestCase(10000)]
        public void UserTagDicHasCorrectAmountTest(int amountOfTags)
        {
            User testUser = new User(1);

            for (int i = 0; i < amountOfTags; i++)
            {
                testUser.Tags.Add(i, new Tag(i));
            }
            Assert.AreEqual(testUser.Tags.Count, amountOfTags);
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

        // This test will test if userartists amount is correct Value:
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

        // Test for UserTagHandling testing Tag Weight, with different values for artist amount on the user (listening time(High numbers increase TagWeight))
        [TestCase(1, new int[] { 1 }, ExpectedResult = 10.0)]
        [TestCase(1, new int[] { 10 }, ExpectedResult = 10.0)]
        [TestCase(1, new int[] { 100 }, ExpectedResult = 10.0)]
        [TestCase(1, new int[] { 10, 10 }, ExpectedResult = 5.0)]
        [TestCase(10, new int[] { 10, 10 }, ExpectedResult = 5.0)]
        [TestCase(11, new int[] { 10, 10 }, ExpectedResult = 5.0)]
        [TestCase(20, new int[] { 10, 10 }, ExpectedResult = 5.0)]
        [TestCase(7, new int[] { 0, 100 }, ExpectedResult = 0.0)]
        [TestCase(18, new int[] { 0, 100 }, ExpectedResult = 10.0)]
        [TestCase(7, new int[] { 1, 10 }, ExpectedResult = 0.91)]
        [TestCase(18, new int[] { 1, 10 }, ExpectedResult = 9.09)]
        [TestCase(7, new int[] { 10, 100 }, ExpectedResult = 0.91)]
        [TestCase(18, new int[] { 10, 100 }, ExpectedResult = 9.09)]
        [TestCase(7, new int[] { 1, 100 }, ExpectedResult = 0.10)]
        [TestCase(18, new int[] { 1, 100 }, ExpectedResult = 9.90)]
        [TestCase(5, new int[] { 10, 10, 10, 10, 10}, ExpectedResult = 2.00)]
        [TestCase(15, new int[] { 10, 10, 10, 10, 10 }, ExpectedResult = 2.00)]
        [TestCase(25, new int[] { 10, 10, 10, 10, 10 }, ExpectedResult = 2.00)]
        [TestCase(35, new int[] { 10, 10, 10, 10, 10 }, ExpectedResult = 2.00)]
        [TestCase(45, new int[] { 10, 10, 10, 10, 10 }, ExpectedResult = 2.00)]
        public double UserTagHandlingTagWeightDifferentArtistAmountTest(int ID, int[] artistAmounts)
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
                    testTags.Add(tagCount, tempTag);
                    // Tag count makes sure all tags are unique for this test:
                    tagCount++;
                }
                testUser.Artists.Add(i, new Userartist(i, artistamount, new Artist(i, testTags, "Test")));
                i++;
            }
            testUser.UserTagHandling();

            return Math.Round(testUser.Tags[ID].Weight, 2);
        }

        // Test for testing UserTagHandling calculates correct Tagweight for the user when Artists has different amount of tags(High numbers decrease weight):
        [TestCase(1, new int[] { 1 }, ExpectedResult = 100.0)]
        [TestCase(1, new int[] { 2 }, ExpectedResult = 50.0)]
        [TestCase(2, new int[] { 2 }, ExpectedResult = 50.0)]
        [TestCase(1, new int[] { 10 }, ExpectedResult = 10.0)]
        [TestCase(1, new int[] { 100 }, ExpectedResult = 10.0)]
        [TestCase(10, new int[] { 100 }, ExpectedResult = 10.0)]
        [TestCase(1, new int[] { 1, 1 }, ExpectedResult = 50.0)]
        [TestCase(1, new int[] { 2, 2 }, ExpectedResult = 25.0)]
        [TestCase(4, new int[] { 2, 2 }, ExpectedResult = 25.0)]
        [TestCase(1, new int[] { 10, 10 }, ExpectedResult = 5.0)]
        [TestCase(15, new int[] { 10, 10 }, ExpectedResult = 5.0)]
        [TestCase(1, new int[] { 100, 100 }, ExpectedResult = 5.0)]
        [TestCase(1, new int[] { 10, 100 }, ExpectedResult = 9.09)]
        [TestCase(11, new int[] { 10, 100 }, ExpectedResult = 0.91)]
        [TestCase(1, new int[] { 1, 100 }, ExpectedResult = 90.91)]
        [TestCase(11, new int[] { 1, 100 }, ExpectedResult = 0.91)]
        [TestCase(1, new int[] { 10, 10, 10 }, ExpectedResult = 3.33)]
        [TestCase(11, new int[] { 10, 10, 10 }, ExpectedResult = 3.33)]
        [TestCase(21, new int[] { 10, 10, 10 }, ExpectedResult = 3.33)]
        [TestCase(1, new int[] { 1, 10, 10 }, ExpectedResult = 33.33)]
        [TestCase(11, new int[] { 1, 10, 10 }, ExpectedResult = 3.33)]
        [TestCase(21, new int[] { 1, 10, 10 }, ExpectedResult = 3.33)]
        public double UserTagHandlingTagWeightDifferentNumberOfTagTest(int ID, int[] amountOfTags)
        {
            User testUser = new User(1);
            int tagCount = 1;
            int i = 0;
            // Each int in amountOfTags should represent a new artist:
            foreach(int element in amountOfTags)
            {
                Dictionary<int, Tag> testTags = new Dictionary<int, Tag>();
                // The int array of AmountOfTags represent how many tags the i´th artist has:
                for (int j = 0; j < element; j++)
                {
                    Tag tempTag = new Tag(tagCount);
                    testTags.Add(tagCount, tempTag);
                    // Tag count will keep track of where in the array data must be used from:
                    tagCount++;
                }
                testUser.Artists.Add(i, new Userartist(i, 10, new Artist(i, testTags, "Test")));
                i++;
            }
            testUser.UserTagHandling();

            return Math.Round(testUser.Tags[ID].Weight, 2);
        }

        // Test for testing UserTagHandling calculates correct Tagweight for the user when artists Tags has different amounts/weight:
        [TestCase(1, new int[] { 1 }, 1, 1, ExpectedResult = 100.0)]
        [TestCase(1, new int[] { 10 }, 1, 1, ExpectedResult = 100.0)]
        [TestCase(1, new int[] { 1000 }, 1, 1, ExpectedResult = 100.0)]

        // Testcases for when there are multiple artists with 1 tag each (for theese differences in the tag amount should not result in any changes to the tag weight!):
        [TestCase(1, new int[] { 1, 1 }, 2, 1, ExpectedResult = 50.0)]
        [TestCase(2, new int[] { 1, 1 }, 2, 1, ExpectedResult = 50.0)]

        [TestCase(1, new int[] { 10, 10 }, 2, 1, ExpectedResult = 50.0)]
        [TestCase(1, new int[] { 1, 10 }, 2, 1, ExpectedResult = 50.0)]
        [TestCase(1, new int[] { 1, 10000 }, 2, 1, ExpectedResult = 50.0)]

        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 10, 1, ExpectedResult = 10.0)]
        [TestCase(5, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 10, 1, ExpectedResult = 10.0)]
        [TestCase(10, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 10, 1, ExpectedResult = 10.0)]

        [TestCase(1, new int[] { 10, 1, 1, 1, 1000, 1, 1, 1, 1, 1684 }, 10, 1, ExpectedResult = 10.0)]
        [TestCase(5, new int[] { 10, 1, 1, 1, 1000, 1, 1, 1, 1, 1684 }, 10, 1, ExpectedResult = 10.0)]
        [TestCase(10, new int[] { 10, 1, 1, 1, 1000, 1, 1, 1, 1, 1684 }, 10, 1, ExpectedResult = 10.0)]

        // Testing for when there is 1 artist with multiple different tags:
        [TestCase(1, new int[] { 1, 1 }, 1, 2, ExpectedResult = 50.0)]
        [TestCase(2, new int[] { 1, 1 }, 1, 2, ExpectedResult = 50.0)]

        [TestCase(1, new int[] { 1, 1, 1, 1, 1 }, 1, 5, ExpectedResult = 20.0)]
        [TestCase(5, new int[] { 1, 1, 1, 1, 1 }, 1, 5, ExpectedResult = 20.0)]
        [TestCase(5, new int[] { 10, 10, 10, 10, 10 }, 1, 5, ExpectedResult = 20.0)]

        [TestCase(1, new int[] { 1, 1, 1, 1, 4 }, 1, 5, ExpectedResult = 12.5)]
        [TestCase(5, new int[] { 1, 1, 1, 1, 4 }, 1, 5, ExpectedResult = 50.0)]

        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 1, 10, ExpectedResult = 10.0)]
        [TestCase(5, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 1, 10, ExpectedResult = 10.0)]
        [TestCase(10, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 1, 10, ExpectedResult = 10.0)]
        [TestCase(1, new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 1, 10, ExpectedResult = 10.0)]
        [TestCase(5, new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 1, 10, ExpectedResult = 10.0)]
        [TestCase(10, new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 1, 10, ExpectedResult = 10.0)]

        [TestCase(1, new int[] { 110, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 1, 10, ExpectedResult = 55.0)]
        [TestCase(5, new int[] { 110, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 1, 10, ExpectedResult = 5.0)]
        [TestCase(10, new int[] { 110, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 1, 10, ExpectedResult = 5.0)]
        [TestCase(1, new int[] { 210, 10, 10, 10, 210, 10, 10, 10, 10, 10 }, 1, 10, ExpectedResult = 42.0)]
        [TestCase(5, new int[] { 210, 10, 10, 10, 210, 10, 10, 10, 10, 10 }, 1, 10, ExpectedResult = 42.0)]
        [TestCase(10, new int[] { 210, 10, 10, 10, 210, 10, 10, 10, 10, 10 }, 1, 10, ExpectedResult = 2.0)]
        // A max of top 10 tag for each artist is used, so adding more than 10 tags to an artist 
        // should not have an effect for the those with the lowest amount:
        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 1, 14, ExpectedResult = 10.0)]
        [TestCase(5, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 1, 14, ExpectedResult = 10.0)]
        [TestCase(10, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 1, 14, ExpectedResult = 10.0)]

        [TestCase(1, new int[] { 11, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 1, 14, ExpectedResult = 55.0)]
        [TestCase(5, new int[] { 11, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 1, 14, ExpectedResult = 5.0)]
        [TestCase(10, new int[] { 11, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 1, 14, ExpectedResult = 5.0)]

        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 11 }, 1, 14, ExpectedResult = 5.0)]
        [TestCase(5, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 11 }, 1, 14, ExpectedResult = 5.0)]
        [TestCase(9, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 11 }, 1, 14, ExpectedResult = 5.0)]
        [TestCase(14, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 11 }, 1, 14, ExpectedResult = 55.0)]

        [TestCase(1, new int[] { 1, 1, 1, 21, 1, 1, 1, 1, 1, 1, 1, 1, 21, 1 }, 1, 14, ExpectedResult = 2.0)]
        [TestCase(4, new int[] { 1, 1, 1, 21, 1, 1, 1, 1, 1, 1, 1, 1, 21, 1 }, 1, 14, ExpectedResult = 42.0)]
        [TestCase(9, new int[] { 1, 1, 1, 21, 1, 1, 1, 1, 1, 1, 1, 1, 21, 1 }, 1, 14, ExpectedResult = 2.0)]
        [TestCase(13, new int[] { 1, 1, 1, 21, 1, 1, 1, 1, 1, 1, 1, 1, 21, 1 }, 1, 14, ExpectedResult = 42.0)]

        // Testing for when there is several artists with several tags:
        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 2, 5, ExpectedResult = 10.0)]
        [TestCase(5, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 2, 5, ExpectedResult = 10.0)]
        [TestCase(10, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 2, 5, ExpectedResult = 10.0)]
        [TestCase(1, new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 2, 5, ExpectedResult = 10.0)]
        [TestCase(5, new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 2, 5, ExpectedResult = 10.0)]
        [TestCase(10, new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 2, 5, ExpectedResult = 10.0)]
        // Testing that the artists tags are shared equally:
        // 2 artist has up too 50 each, 4 has 25 as Amount for artists are the same in this test scenario.
        [TestCase(1, new int[] { 40, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 2, 5, ExpectedResult = 25.0)]
        [TestCase(5, new int[] { 40, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 2, 5, ExpectedResult = 6.25)]
        [TestCase(6, new int[] { 40, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 2, 5, ExpectedResult = 10.0)]
        [TestCase(10, new int[] { 40, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 2, 5, ExpectedResult = 10.0)]
        [TestCase(1, new int[] { 210, 10, 10, 10, 10, 40, 10, 10, 10, 10 }, 2, 5, ExpectedResult = 42.0)]
        [TestCase(5, new int[] { 210, 10, 10, 10, 10, 40, 10, 10, 10, 10 }, 2, 5, ExpectedResult = 2.0)]
        [TestCase(6, new int[] { 210, 10, 10, 10, 10, 40, 10, 10, 10, 10 }, 2, 5, ExpectedResult = 25.0)]
        [TestCase(10, new int[] { 210, 10, 10, 10, 10, 40, 10, 10, 10, 10 }, 2, 5, ExpectedResult = 6.25)]
        // If artists has more than 10 tags:
        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 2, 11, ExpectedResult = 5.0)]
        [TestCase(10, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 2, 11, ExpectedResult = 5.0)]
        [TestCase(12, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 2, 11, ExpectedResult = 5.0)]
        [TestCase(21, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 2, 11, ExpectedResult = 5.0)]

        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 11,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 21, 1, 1, 1 }, 2, 14, ExpectedResult = 2.43)]
        [TestCase(14, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 11,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 21, 1, 1, 1 }, 2, 14, ExpectedResult = 26.71)]
        [TestCase(15, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 21, 1, 1, 1 }, 2, 14, ExpectedResult = 1.79)]
        [TestCase(25, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 21, 1, 1, 1 }, 2, 14, ExpectedResult = 37.5)]
        public double UserTagHandlingTagWeightDifferentTagAmountTest(int ID, int[] tagAmounts, int amountOfArtists, int amountOfTagsPerArtist)
        {
            User testUser = new User(1);
            int tagCount = 1;
            for(int i = 0; i < amountOfArtists; i++)
            {
                Dictionary<int, Tag> testTags = new Dictionary<int, Tag>();
                for (int j = 0; j < amountOfTagsPerArtist; j++)
                {
                    Tag tempTag = new Tag(tagCount);
                    tempTag.Amount = tagAmounts[tagCount-1];
                    testTags.Add(tagCount, tempTag);
                    // Tag count will keep track of where in the array data must be used from:
                    tagCount++;
                }
                testUser.Artists.Add(i, new Userartist(i, 10, new Artist(i, testTags, "Test")));
            }
            testUser.UserTagHandling();
            // Math.Round is used to make the numbers possible to hit. This does however result in the total tagweight not being 100 as it should.
            return Math.Round(testUser.Tags[ID].Weight, 2);
        }


        // Test for testing UserTagHandling calculates correct Tagweight for the user when artists has some identical tags:
        [TestCase(1, new int[] { 1 }, 1, 1, ExpectedResult = 100)]
        [TestCase(100, new int[] { 100 }, 1, 1, ExpectedResult = 100)]
        [TestCase(1, new int[] { 1, 1 }, 1, 2, ExpectedResult = 100)]
        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 1, 10, ExpectedResult = 100)]
        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 10, 1, ExpectedResult = 100)]
        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 2, 5, ExpectedResult = 100)]
        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 5, 2, ExpectedResult = 100)]
        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 1, 15, ExpectedResult = 100)]
        [TestCase(1, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 15, 1, ExpectedResult = 100)]
        [TestCase(1, new int[] { 1, 2 }, 1, 2, ExpectedResult = 50)]
        [TestCase(1, new int[] { 1, 2, 1, 2 }, 2, 2, ExpectedResult = 50)]
        [TestCase(1, new int[] { 1, 1, 1, 2 }, 1, 4, ExpectedResult = 75)]
        [TestCase(2, new int[] { 1, 1, 1, 2 }, 1, 4, ExpectedResult = 25)]
        [TestCase(1, new int[] { 1, 1, 1, 2 }, 2, 2, ExpectedResult = 75)]
        [TestCase(2, new int[] { 1, 1, 1, 2 }, 2, 2, ExpectedResult = 25)]
        public double UserTagHandlingTagWeightIdenticalTagIDTest(int ID, int[] tagIDs, int amountOfArtists, int amountOfTagsPerArtist)
        {
            User testUser = new User(1);
            int tagCount = 0;
            for (int i = 0; i < amountOfArtists; i++)
            {
                Dictionary<int, Tag> testTags = new Dictionary<int, Tag>();
                // The int array of AmountOfTags represent how many tags the i´th artist has:
                for (int j = 0; j < amountOfTagsPerArtist; j++)
                {
                    Tag tempTag = new Tag(tagIDs[tagCount]);
                    // If current artist already has the tag, the amount will be increased:
                    if (testTags.ContainsKey(tagIDs[tagCount]))
                        testTags[tagIDs[tagCount]].Amount += 1;
                    else
                        testTags.Add(tagIDs[tagCount], tempTag);
                    // Tag count will keep track of where in the array data must be used from:
                    tagCount++;
                }
                testUser.Artists.Add(i, new Userartist(i, 10, new Artist(i, testTags, "Test")));
            }
            testUser.UserTagHandling();
            // Math.Round is used to make the numbers possible to hit. This does however result in the total tagweight not being 100 as it should.
            return Math.Round(testUser.Tags[ID].Weight, 2);
        }

        // Test that the method CalculateArtistWeight() calculates correct weight value:
        [TestCase(0, 1, new int[] { 1 }, ExpectedResult = 100)]
        [TestCase(0, 1, new int[] { 100 }, ExpectedResult = 100)]
        [TestCase(0, 1, new int[] { 100000 }, ExpectedResult = 100)]
        [TestCase(0, 2, new int[] { 1, 1 }, ExpectedResult = 50)]
        [TestCase(0, 2, new int[] { 10, 10 }, ExpectedResult = 50)]
        [TestCase(0, 2, new int[] { 1000, 1000 }, ExpectedResult = 50)]
        [TestCase(1, 2, new int[] { 1, 1 }, ExpectedResult = 50)]
        [TestCase(1, 2, new int[] { 0, 87 }, ExpectedResult = 100)]
        [TestCase(1, 2, new int[] { 452, 0 }, ExpectedResult = 0)]
        [TestCase(9, 10, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, ExpectedResult = 10)]
        [TestCase(1, 10, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, ExpectedResult = 10)]
        [TestCase(4, 10, new int[] { 12, 1335, 123, 1123, 0, 1213, 1332, 114, 1456, 1 }, ExpectedResult = 0)]
        [TestCase(1, 10, new int[] { 12, 1335, 123, 1123, 0, 1213, 1332, 114, 1456, 1 }, ExpectedResult = 19.90)]
        public double CalculateArtistWeightTest(int ID, int amountOfArtists, int[] artistsAmounts)
        {
            User testUser = new User(1);
            Dictionary<int, Tag> emptyTags = new Dictionary<int, Tag>();

            for (int i = 0; i < amountOfArtists; i++)
            {
                testUser.Artists.Add(i, new Userartist(i, artistsAmounts[i], new Artist(i, emptyTags, "Test")));
            }
            testUser.CalculateArtistWeight();

            return Math.Round(testUser.Artists[ID].Weight, 2);
        }
    }
}