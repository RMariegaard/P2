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
    public class CosineTests
    {
        [TestCase(1.0, 1.0, 1.0, ExpectedResult = 1.0)]
        [TestCase(0.0, 0.0, 0.0, ExpectedResult = 0.0)]
        [TestCase(0.3, 1.0, 1.0, ExpectedResult = 0.3)]
        public double GetCosineTest(double dot, double length1, double length2)
        {
            if (length1 * length2 == 0)
                return 0;
            return (dot) / (length1 * length2);
        }

        [TestCase(new double[] { 0.0, 0.0, 0.0 }, new double[] { 0.0, 0.0, 0.0 }, ExpectedResult = 0)]
        [TestCase(new double[] { 1.0, 1.0, 1.0 }, new double[] { 1.0, 1.0, 1.0 }, ExpectedResult = 3.0)]
        [TestCase(new double[] {0.0, 1.0, 0.0 }, new double[] { 1.0, 1.0, 1.0 }, ExpectedResult = 1.0)]
        public double CalcDotInCosineTest(double[] user, double[] artist)
        {
            Cosine cosineTest = new Cosine();
            Dictionary<int, double> testUser = createTestDictionary(user);
            Dictionary<int, double> testArtist = createTestDictionary(artist);

            double dot = cosineTest.CalcDotInCosine(testUser, testArtist);
            return dot;
        }
        [TestCase(new int[] { 1, 2, 3}, new double[] { 0.0, 1.0, 1.0 }, 
                  new int[] { 1, 2, 4 }, new double[] { 2.0, 1.0, 1.0 }, ExpectedResult = 1.0)]

        [TestCase(new int[] { 1, 2, 3 }, new double[] { 0.0, 1.0, 1.0 },
                  new int[] { 0, 4, 6 }, new double[] { 2.0, 1.0, 1.0 }, ExpectedResult = 0.0)]

        public double CalcDotInCosineTest_CheckWithEmtykeys(int[] userKeys, double[] user, int[] artistKeys, double[] artist)
        {
            Cosine cosineTest = new Cosine();
            Dictionary<int, double> testUser = createTestDictionary(userKeys, user);
            Dictionary<int, double> testArtist = createTestDictionary(artistKeys, artist);

            double dot = cosineTest.CalcDotInCosine(testUser, testArtist);
            return dot;
        }

        [TestCase(new double[] { 0.0, 0.0, 0.0}, ExpectedResult = 0.0)]
        [TestCase(new double[] { 1.0, 1.0, 1.0 }, ExpectedResult = 3.0)]
        [TestCase(new double[] { 100.0, 100.0, 100.0 }, ExpectedResult = 300.0)]
        public double GetLengthTest(double[] array)
        {
            Cosine cosineTest = new Cosine();
            Dictionary<int, double> testLength = createTestDictionary(array);
            return cosineTest.GetLength(testLength);
        }

        private Dictionary<int, double> createTestDictionary(params double[] array)
        {
            var returnDic = new Dictionary<int, double>();
            int length = array.Length;
            for(int i = 0; i < length; i++)
            {
                returnDic.Add(i, array[i]);
            }
            return returnDic;
        }
        private Dictionary<int, double> createTestDictionary( int[] keys, params double[] values)
        {
            var returnDic = new Dictionary<int, double>();
            int length = values.Length;
            for (int i = 0; i < length; i++)
            {
                returnDic.Add(keys[i], values[i]);
            }
            return returnDic;
        }
    }
}