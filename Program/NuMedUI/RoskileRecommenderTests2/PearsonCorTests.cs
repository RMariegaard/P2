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
    public class PearsonCorTests
    {
        private static User user1;
        private static User user2;
        private PearsonCor TestCase;
        [SetUp]
        public void Init()
        {
            //SetUp
            user1 = createTestType(new int[] { 1, 2, 3, 4, }, new double[] { 1.0, 2.0, 3.0, 4.0 });
            user2 = createTestType(new int[] { 1, 2, 3, 4, 5, 6 }, new double[] { 1.0, 5.0, 5.0, 5.0, 5.0, 5.0 });
            TestCase = new PearsonCor();

        }
        [Test()]
        public void CalculateUserTest()
        {
            //Assert
            double expected = 2.45;
            double actual = TestCase.CalculateUser(user1, user2);
            Assert.AreEqual(expected, Math.Round(actual, 2));

        }

        [Test()]
        public void CalculateUserMeanTest()
        {
            double actual = TestCase.CalculateUserMean(user1);
            double expected = 2.5;
            Assert.AreEqual(expected, actual);

        }

        [Test()]
        public void CalculateNumeratorTest()
        {
            double expected = 6.0;
            double actual = TestCase.CalculateNumerator(user1, user2, 2.5, 5.0);
            Assert.AreEqual(expected, actual);
        }



        private User createTestType(params double[] array)
        {
            var returnDic = new Dictionary<int, Userartist>();
            int length = array.Length;
            for (int i = 0; i < length; i++)
            {
                returnDic.Add(i, new Userartist(array[i]));
            }
            return new User(1, returnDic);

        }
        private User createTestType(int[] key, double[] value)
        {
            var returnDic = new Dictionary<int, Userartist>();
            int length = value.Length;
            for (int i = 0; i < length; i++)
            {
                returnDic.Add(key[i], new Userartist(value[i]));
            }
            return new User(1, returnDic);
        }

    }
}