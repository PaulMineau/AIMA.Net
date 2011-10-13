namespace AIMA.Test.Core.Unit.Learning.Learners
{

    using AIMA.Core.Learning.Framework;
    using AIMA.Core.Learning.Inductive;
    using AIMA.Core.Learning.Learners;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class EnsembleLearningTest
    {

        private const String YES = "Yes";

        [TestMethod]
        public void testAdaBoostEnablesCollectionOfStumpsToClassifyDataSetAccurately()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            List<DecisionTree> stumps = DecisionTree.getStumpsFor(ds, YES, "No");
            List<Learner> learners = new List<Learner>();
            foreach (Object stump in stumps)
            {
                DecisionTree sl = (DecisionTree)stump;
                StumpLearner stumpLearner = new StumpLearner(sl, "No");
                learners.Add(stumpLearner);
            }
            AdaBoostLearner learner = new AdaBoostLearner(learners, ds);
            learner.train(ds);
            int[] result = learner.test(ds);
            Assert.AreEqual(12, result[0]);
            Assert.AreEqual(0, result[1]);
        }
    }
}
