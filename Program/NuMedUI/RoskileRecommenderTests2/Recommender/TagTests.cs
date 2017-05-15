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
    public class TagTests
    {
        [TestCase(-100)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void ConstructerParameterIsEqualToIDTagTest(int ID)
        {
            Tag tag = new Tag(ID);
            Assert.AreEqual(ID, tag.Id);
        }

        [TestCase(-100)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void TagAmountSetIsEqualToTagAmountTest(int amount)
        {
            Tag tag = new Tag(1);
            tag.Amount = amount;
            Assert.AreEqual(amount, tag.Amount);
        }

        [TestCase(-100)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void TagWeightSetIsEqualToTagWeightTest(int weight)
        {
            Tag tag = new Tag(1);
            tag.Weight = weight;
            Assert.AreEqual(weight, tag.Weight);
        }
    }
}