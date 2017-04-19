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
        [TestCase(new double[] { 0.0, 0.0, 0.0 }, new double[] { 0.0, 0.0, 0.0 },ExpectedResult = 0.00)]
        [TestCase(new double[] { 1.0, 1.0, 1.0 }, new double[] { 1.0, 1.0, 1.0 },ExpectedResult = 1.00)]
        public double GetCosineTest(double[] user, double[] artist)
        {
            User testUser = createTestType<User>(user);
            Artist testArtist = createTestType<Artist>(artist);
            Cosine cosineTest = new Cosine();
            double actual = cosineTest.GetCosine(testUser, testArtist);
            return Math.Round(actual, 5);
        }
        [TestCase(1.0, 1.0, ExpectedResult = 1.00)]
        public double CalculateTheCosineTest(double dot, double denumerator)
        {
            Cosine cosineTest = new Cosine();
            
            return cosineTest.CalculateTheCosine(dot, denumerator);
        }


        [TestCase(new double[] { 0.0, 0.0, 0.0 }, new double[] { 0.0, 0.0, 0.0 }, ExpectedResult = 0)]
        [TestCase(new double[] { 1.0, 1.0, 1.0 }, new double[] { 1.0, 1.0, 1.0 }, ExpectedResult = 3.0)]
        [TestCase(new double[] { 0.0, 1.0, 0.0 }, new double[] { 1.0, 1.0, 1.0 }, ExpectedResult = 1.0)]
        public double CalcDotInCosineTest(double[] user, double[] artist)
        {
            User testUser = createTestType<User>(user);
            Artist testArtist = createTestType<Artist>(artist);
            Cosine cosineTest = new Cosine();

            double dot = cosineTest.CalcDotInCosine(testUser, testArtist);
            return dot;
        }

        [TestCase(new int[] { 1, 2, 3 }, new double[] { 0.0, 1.0, 1.0 },
                  new int[] { 1, 2, 4 }, new double[] { 2.0, 1.0, 1.0 }, ExpectedResult = 1.0)]

        [TestCase(new int[] { 1, 2, 3 }, new double[] { 0.0, 1.0, 1.0 },
                  new int[] { 0, 4, 6 }, new double[] { 2.0, 1.0, 1.0 }, ExpectedResult = 0.0)]

        public double CalcDotInCosineTest_CheckWithEmptykeys(int[] userKeys, double[] user, int[] artistKeys, double[] artist)
        {
            User testUser = createTestType<User>(userKeys, user);
            Artist testArtist = createTestType<Artist>(artistKeys, artist);
            Cosine cosineTest = new Cosine();

            double dot = cosineTest.CalcDotInCosine(testUser, testArtist);
            return dot;
        }

        [TestCase(new double[] { 0.0, 0.0, 0.0 }, new double[] { 0.0, 0.0, 0.0 }, ExpectedResult = 0.0)]
        [TestCase(new double[] { 1.0, 1.0, 1.0 }, new double[] { 1.0, 1.0, 1.0 }, ExpectedResult = 3.0)]
        [TestCase(new double[] { 100.0, 100.0, 100.0 }, new double[] { 100.0, 100.0, 100.0 }, ExpectedResult = 30000.0)]
        [TestCase(new double[] { 1, 2.0, 0.0 }, new double[] { 1.0, 1.0, 1.0 }, ExpectedResult = 3.87298)]
        public double GetDeuneratorTest(double[] user, double[] artist)
        {
            Cosine cosineTest = new Cosine();
            User testUser = createTestType<User>(user);
            Artist testArtist = createTestType<Artist>(artist);
            return Math.Round(cosineTest.GetDenumerator(testUser, testArtist), 5);
        }

        private T createTestType<T>(params double[] array)
        {
            var returnDic = new Dictionary<int, Tag>();
            int length = array.Length;
            for (int i = 0; i < length; i++)
            {
                returnDic.Add(i, new Tag(array[i]));
            }
            object valueobj;
            if (typeof(T) == typeof(User))
                valueobj = new User(1, returnDic);
            else
                valueobj = new Artist(1, returnDic);

            return (T)valueobj;
        }
        private T createTestType<T>(int[] key, double[] value)
        {
            var returnDic = new Dictionary<int, Tag>();
            int length = value.Length;
            for (int i = 0; i < length; i++)
            {
                returnDic.Add(key[i], new Tag(value[i]));
            }
            object valueobj;
            if (typeof(T) == typeof(User))
                valueobj = new User(1, returnDic);
            else
                valueobj = new Artist(1, returnDic);

            return (T)valueobj;
        }

       
    }
}