using NUnit.Framework;
using Recommender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Tests
{
    [TestFixture()]
    public class MethodsForRecommendingTests
    {
        private MethodsForRecommending _test;
        private Dictionary<int, Artist> allArtists;
        [SetUp]
        public void Init()
        {
            _test = new MethodsForRecommending();
            allArtists = new Dictionary<int, Artist>();
            for (int i = 0; i < 10; i++)
                allArtists.Add(i, null);
        }
        [TestCase(new double[] {1.0, 1.0, 1.0 }, new double[] { 1.0, 1.0, 1.0}, new int[] { 0, 1, 2 }, new int[] { 4, 5, 6})]
        [TestCase(new double[] {1.0, 1.0, 1.0 }, new double[] {}, new int[] { 0, 1, 2 }, new int[] {})]
        public void Cosine_NoTagInCommon_AssertZero(double[] userTags, double[] artistTags, int[] userTagId, int[] artistTagId)
        {
            ITaggable testUser = createTestTypeCosine<User>(userTagId, userTags);
            ITaggable testArtist = createTestTypeCosine<Artist>(artistTagId, artistTags);

            Assert.AreEqual(0, _test.GetCosine(testUser, testArtist));
        }
        [TestCase(new double[] { 1.0, 1.0, 1.0 }, new double[] { 1.0, 1.0, 1.0 }, new int[] { 0, 1, 2 }, new int[] { 0, 1, 2 })]
        [TestCase(new double[] { 2.0, 34.0, 3.0 }, new double[] { 2.0, 34.0, 3.0 }, new int[] { 0, 1, 2 }, new int[] { 0, 1, 2 })]
        public void Cosine_AllTagAndAmountInCommon_AssertOne(double[] userTags, double[] artistTags, int[] userTagId, int[] artistTagId)
        {
            ITaggable testUser = createTestTypeCosine<User>(userTagId, userTags);
            ITaggable testArtist = createTestTypeCosine<Artist>(artistTagId, artistTags);

            Assert.AreEqual(1, Math.Round(_test.GetCosine(testUser, testArtist),4));
        }
        //This test the cosine algorithm for specific numbers, calculated from this site "http://scistatcalc.blogspot.dk/2015/11/cosine-similarity-calculator.html#"
        //Notise the id's in the first two does not match for user and artist.
        [TestCase(new double[] {11.0, 12.0, 13.0, 14.0 }, new double[] { 4.0, 5.0, 6.0, 7.0 }, new int[] {0, 1, 2, 3}, new int[] {0, 1, 2, 4}, ExpectedResult = 0.65)]
        [TestCase(new double[] { 10.0, 10.0, 10.0, 10.0 }, new double[] { 10.0, 10.0, 10.0, 10.0 }, new int[] { 0, 1, 2, 3 }, new int[] { 0, 1, 2, 4 }, ExpectedResult = 0.75)]
        [TestCase(new double[] {100.0, 400.0, 7000.0 }, new double[] {900.0, 288.0, 1234.0}, new int[] {0, 1, 2}, new int[] {0, 1, 2}, ExpectedResult = 0.81)]
        public double Cosine_TestForSpecificResult_ReturnValue(double[] userTags, double[] artistTags, int[] userTagId, int[] artistTagId)
        {
            ITaggable testUser = createTestTypeCosine<User>(userTagId, userTags);
            ITaggable testArtist = createTestTypeCosine<Artist>(artistTagId, artistTags);

            return Math.Round(_test.GetCosine(testUser, testArtist),2);
        }
        //With inCommon means that both have or havent listened to that specific artist
        [TestCase(new int[] { 1, 1, 1, 1, 1}, new int[] { 1, 1, 1, 1, 1}, new int[] { 0, 1, 2, 3, 4 }, new int[] { 5, 6, 7, 8, 9})]
        [TestCase(new int[] { 34, 21, 12, 65, 23}, new int[] { 23, 34, 55, 66, 44 }, new int[] { 0, 1, 2, 3, 4 }, new int[] { 5, 6, 7, 8, 9 })]
        public void Pearson_NoArtistInCommon_AssertNegative(int[] user1ArtistAmount, int[] User2ArtistAmount, int[] user1ArtistId, int[] user2ArtistId)
        {
            User user1 = createTestUserPearson(user1ArtistId, user1ArtistAmount);
            User user2 = createTestUserPearson(user2ArtistId, User2ArtistAmount);
            Assert.Negative(_test.GetPearson(user1, user2, allArtists));
        }
        [TestCase(new int[] { }, new int[] {}, new int[] { }, new int[] {})]
        [TestCase(new int[] { 0, 0, 0, 0}, new int[] {0, 0, 0, 0 }, new int[] { 1,2,3,4}, new int[] { 3,4,5,6})]
        public void Pearson_NoArtistListenedTo_AssertZero(int[] user1ArtistAmount, int[] User2ArtistAmount, int[] user1ArtistId, int[] user2ArtistId)
        {
            User user1 = createTestUserPearson(user1ArtistId, user1ArtistAmount);
            User user2 = createTestUserPearson(user2ArtistId, User2ArtistAmount);
            Assert.Zero(_test.GetPearson(user1, user2, allArtists));
        }
        [TestCase(new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 0, 1, 2, 3, 4 }, new int[] { 0, 1, 2, 3, 4 })]
        [TestCase(new int[] { 34, 21, 12, 65, 23 }, new int[] { 36, 44, 55, 33, 11 }, new int[] { 0, 1, 2, 3, 4 }, new int[] { 0,1,3,6,7})]
        public void Pearson_SomeArtistInCommon_AsserPossitive(int[] user1ArtistAmount, int[] User2ArtistAmount, int[] user1ArtistId, int[] user2ArtistId)
        {
            User user1 = createTestUserPearson(user1ArtistId, user1ArtistAmount);
            User user2 = createTestUserPearson(user2ArtistId, User2ArtistAmount);
            Assert.Positive(_test.GetPearson(user1, user2, allArtists));
        }

        //The Expected result are calculated on this site http://www.socscistatistics.com/tests/pearson/Default2.aspx
        [TestCase(new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 0, 1, 2, 3, 4 }, new int[] { 0, 1, 2, 3, 4 },ExpectedResult = 1.000)]
        [TestCase(new int[] { 1, 1, 1, 1 }, new int[] { 1, 1, 1, 1 }, new int[] { 0, 1, 2, 3 }, new int[] { 4, 5, 6, 7 }, ExpectedResult = -0.667)]
        [TestCase(new int[] { 23, 32, 0, 44, 32 }, new int[] { 0, 44, 10, 55, 43 }, new int[] { 0, 1, 2, 3, 4 }, new int[] { 0, 1, 2, 3, 4 }, ExpectedResult = 0.896)]
        public double Pearson_TestSpecifikCase_AssertResultInReturn(int[] user1ArtistAmount, int[] User2ArtistAmount, int[] user1ArtistId, int[] user2ArtistId)
        {
            User user1 = createTestUserPearson(user1ArtistId, user1ArtistAmount);
            User user2 = createTestUserPearson(user2ArtistId, User2ArtistAmount);
            return Math.Round(_test.GetPearson(user1, user2, allArtists),3);
        }

        private T createTestTypeCosine<T>(int[] key, double[] value)
        {
            var returnDic = new Dictionary<int, Tag>();
            User testUser = new User(0);
            Artist testArtist = new Artist(0, "test");
            int length = value.Length;
            for (int i = 0; i < length; i++)
            {
                returnDic.Add(key[i], new Tag(i) { Weight = value[i]});
            }
            object valueobj;
            if (typeof(T) == typeof(User))
            {
                foreach(var element in returnDic)
                {
                    testUser.Tags.Add(element.Key, element.Value);
                }
                valueobj = testUser;
            }

            else
            {
                foreach (var element in returnDic)
                {
                    testArtist.Tags.Add(element.Key, element.Value);
                }
                valueobj = testArtist;
            }

            return (T)valueobj;
        }
        private User createTestUserPearson(int[] key, int[] amount)
        {
            var returnDic = new Dictionary<int, Userartist>();
            User returnUser = new User(0);
            int length = amount.Length;
            for (int i = 0; i < length; i++)
            {
                returnDic.Add(key[i], new Userartist(i, amount[i], null));
            }
            foreach(var element in returnDic)
            {
                returnUser.Artists.Add(element.Key, element.Value);
            }
            return returnUser;
        }
    }
}