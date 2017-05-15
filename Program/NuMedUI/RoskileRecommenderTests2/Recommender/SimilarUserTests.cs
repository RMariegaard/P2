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
    public class SimilarUserTests
    {
        [TestCase(100)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void TestConstructorToSimilarUserTest(int Id)
        {
            SimilarUser similarUser = new SimilarUser(Id);
            Assert.AreEqual(similarUser.ID, Id);
        }

        [TestCase(100)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void TestSimilarityInSimilarUserTest(double similarity)
        {
            SimilarUser similarUser = new SimilarUser(5);
            similarUser.Similarity = similarity;
            Assert.AreEqual(similarUser.Similarity, similarity);

        }
        [Test]
        public void TestDictonaryInSimilarUserTest()
        {
            Dictionary<int, Artist> dictornaryArtist = new Dictionary<int, Artist>();
            Artist testArtist = new Artist(5, "testName");
            dictornaryArtist.Add(5, testArtist);

            Assert.AreEqual(dictornaryArtist[5], testArtist);
        }


    }


}