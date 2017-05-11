using NUnit.Framework;
using Recommender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using RoskileRecommenderTests2;

namespace Recommender.Tests
{
    [TestFixture()]
    public class UserTests
    {
        MakeTestSubjects makeTestSubjects = new MakeTestSubjects();
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
            Assert.AreEqual(userID, testUser.Id);
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

            for (int i = 0; i < amountOfArtists; i++)
            {
                if (i == ID)
                {
                    testUser.Artists.Add(i, new Userartist(i, amount, new Artist(i, null, "Test")));
                }
                testUser.Artists.Add(i, new Userartist(i, 0, new Artist(i, null, "Test")));
            }
            Assert.AreEqual(testUser.Artists[ID].Amount, amount);
        }

        // This test will test if userartists reference to artist is the correct ID/key:
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 50)]
        [TestCase(1, 1000)]
        [TestCase(2, 2)]
        [TestCase(50, 50)]
        [TestCase(1234, 1234)]
        [TestCase(17, 50)]
        [TestCase(142, 1000)]
        public void UserArtistDictContainsCorrectValuesIDTest(int ID, int amountOfArtists)
        {
            User testUser = new User(1);

            for (int i = 0; i < amountOfArtists; i++)
            {
                testUser.Artists.Add(i, new Userartist(i, 0, new Artist(i, null, "Test")));
            }
            Assert.AreEqual(testUser.Artists[ID].Id, ID);
        }



        [Test()]
        public void UserTagHandlingTest()
        {
            Assert.Fail();
        }

        // Test that the method CalculateArtistWeight() calculates correct weight value:
        [TestCase(0, 1, new int[] { 1 }, 100)]
        [TestCase(0, 1, new int[] { 100 }, 100)]
        [TestCase(0, 1, new int[] { 100000 }, 100)]
        [TestCase(0, 2, new int[] { 1, 1 }, 50)]
        [TestCase(1, 2, new int[] { 1, 1 }, 50)]
        public void CalculateArtistWeightTest(int ID, int amountOfArtists, int[] artistsAmounts, double expectedWeight)
        {
            User testUser = new User(1);

            for (int i = 0; i < amountOfArtists; i++)
            {
                testUser.Artists.Add(i, new Userartist(i, artistsAmounts[i], new Artist(i, null, "Test")));
            }
            testUser.CalculateArtistWeight();

            Assert.AreEqual(expectedWeight, testUser.Artists[ID].Weight);
        }

       



    }
}