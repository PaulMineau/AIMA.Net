namespace AIMA.Test.Core.Unit.Learning.Learners
{

    using AIMA.Core.Learning.Framework;
    using AIMA.Core.Learning.Inductive;
    using AIMA.Core.Learning.Learners;
    using AIMA.Test.Core.Unit.Learning.Framework;
    using AIMA.Test.Core.Unit.Learning.Inductive;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class LearnerTests
    {

        [TestMethod]
        public void testMajorityLearner()
        {
            MajorityLearner learner = new MajorityLearner();
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            learner.train(ds);
            int[] result = learner.test(ds);
            Assert.AreEqual(6, result[0]);
            Assert.AreEqual(6, result[1]);
        }

        [TestMethod]
        public void testDefaultUsedWhenTrainingDataSetHasNoExamples()
        {
            // tests RecursionBaseCase#1
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            DecisionTreeLearner learner = new DecisionTreeLearner();

            DataSet ds2 = ds.emptyDataSet();
            Assert.AreEqual(0, ds2.size());

            learner.train(ds2);
            Assert.AreEqual("Unable To Classify", learner.predict(ds
                    .getExample(0)));
        }

        [TestMethod]
        public void testClassificationReturnedWhenAllExamplesHaveTheSameClassification()
        {
            // tests RecursionBaseCase#2
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            DecisionTreeLearner learner = new DecisionTreeLearner();

            DataSet ds2 = ds.emptyDataSet();

            // all 3 examples have the same classification (willWait = yes)
            ds2.add(ds.getExample(0));
            ds2.add(ds.getExample(2));
            ds2.add(ds.getExample(3));

            learner.train(ds2);
            Assert.AreEqual("Yes", learner.predict(ds.getExample(0)));
        }

        [TestMethod]
        public void testMajorityReturnedWhenAttributesToExamineIsEmpty()
        {
            // tests RecursionBaseCase#2
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            DecisionTreeLearner learner = new DecisionTreeLearner();

            DataSet ds2 = ds.emptyDataSet();

            // 3 examples have classification = "yes" and one ,"no"
            ds2.add(ds.getExample(0));
            ds2.add(ds.getExample(1));// "no"
            ds2.add(ds.getExample(2));
            ds2.add(ds.getExample(3));
            ds2.setSpecification(new MockDataSetSpecification("will_wait"));

            learner.train(ds2);
            Assert.AreEqual("Yes", learner.predict(ds.getExample(1)));
        }

        [TestMethod]
        public void testInducedTreeClassifiesDataSetCorrectly()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            DecisionTreeLearner learner = new DecisionTreeLearner();
            learner.train(ds);
            int[] result = learner.test(ds);
            Assert.AreEqual(12, result[0]);
            Assert.AreEqual(0, result[1]);
        }

        [TestMethod]
        public void testDecisionListLearnerReturnsNegativeDLWhenDataSetEmpty()
        {
            // tests first base case of DL Learner
            DecisionListLearner learner = new DecisionListLearner("Yes", "No",
                    new MockDLTestFactory(null));
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            DataSet empty = ds.emptyDataSet();
            learner.train(empty);
            Assert.AreEqual("No", learner.predict(ds.getExample(0)));
            Assert.AreEqual("No", learner.predict(ds.getExample(1)));
            Assert.AreEqual("No", learner.predict(ds.getExample(2)));
        }

        [TestMethod]
        public void testDecisionListLearnerReturnsFailureWhenTestsEmpty()
        {
            // tests second base case of DL Learner
            DecisionListLearner learner = new DecisionListLearner("Yes", "No",
                    new MockDLTestFactory(new List<DLTest>()));
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            learner.train(ds);
            Assert.AreEqual(DecisionListLearner.FAILURE, learner.predict(ds
                    .getExample(0)));
        }

        [TestMethod]
        public void testDecisionListTestRunOnRestaurantDataSet()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            DecisionListLearner learner = new DecisionListLearner("Yes", "No",
                    new DLTestFactory());
            learner.train(ds);

            int[] result = learner.test(ds);
            Assert.AreEqual(12, result[0]);
            Assert.AreEqual(0, result[1]);
        }

        [TestMethod]
        public void testCurrentBestLearnerOnRestaurantDataSet()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            CurrentBestLearner learner = new CurrentBestLearner("Yes");
            learner.train(ds);

            int[] result = learner.test(ds);
            Assert.AreEqual(12, result[0]);
            Assert.AreEqual(0, result[1]);
        }
    }
}