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
        [Test()]
        public void CalculateUserTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CalculateUserMeanTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CalculateNumeratorTest()
        {
            Assert.Fail();
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