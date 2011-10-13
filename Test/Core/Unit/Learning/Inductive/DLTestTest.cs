namespace AIMA.Test.Core.Unit.Learning.Inductive
{

    using AIMA.Core.Learning.Framework;
    using AIMA.Core.Learning.Inductive;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class DLTestTest
    {

        [TestMethod]
        public void testDecisionList()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            List<DLTest> dlTests = new DLTestFactory()
                    .createDLTestsWithAttributeCount(ds, 1);
            Assert.AreEqual(26, dlTests.Count);
        }

        [TestMethod]
        public void testDLTestMatchSucceedsWithMatchedExample()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            Example e = ds.getExample(0);
            DLTest test = new DLTest();
            test.add("type", "French");
            Assert.IsTrue(test.matches(e));
        }

        [TestMethod]
        public void testDLTestMatchFailsOnMismatchedExample()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            Example e = ds.getExample(0);
            DLTest test = new DLTest();
            test.add("type", "Thai");
            Assert.IsFalse(test.matches(e));
        }

        [TestMethod]
        public void testDLTestMatchesEvenOnMismatchedTargetAttributeValue()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            Example e = ds.getExample(0);
            DLTest test = new DLTest();
            test.add("type", "French");
            Assert.IsTrue(test.matches(e));
        }

        [TestMethod]
        public void testDLTestReturnsMatchedAndUnmatchedExamplesCorrectly()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            DLTest test = new DLTest();
            test.add("type", "Burger");

            DataSet matched = test.matchedExamples(ds);
            Assert.AreEqual(4, matched.size());

            DataSet unmatched = test.unmatchedExamples(ds);
            Assert.AreEqual(8, unmatched.size());
        }
    }
}