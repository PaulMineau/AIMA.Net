namespace CosmicFlow.AIMA.Core.Learning.Learners
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Learning.Framework;
    using CosmicFlow.AIMA.Core.Learning.Inductive;

    /**
     * @author Ravi Mohan
     * 
     */
    public class DecisionListLearner : Learner
    {
        public const String FAILURE = "Failure";

        private DecisionList decisionList;

        private String positive, negative;

        private DLTestFactory testFactory;

        public DecisionListLearner(String positive, String negative,
                DLTestFactory testFactory)
        {
            this.positive = positive;
            this.negative = negative;
            this.testFactory = testFactory;
        }

        public void train(DataSet ds)
        {
            this.decisionList = decisionListLearning(ds);
        }

        public String predict(Example e)
        {
            if (decisionList == null)
            {
                throw new ApplicationException(
                        "learner has not been trained with dataset yet!");
            }
            return decisionList.predict(e);
        }

        public int[] test(DataSet ds) {
		int[] results = new int[] { 0, 0 };

		foreach (Example e in ds.examples) {
			if (e.targetValue().Equals(decisionList.predict(e))) {
				results[0] = results[0] + 1;
			} else {
				results[1] = results[1] + 1;
			}
		}
		return results;
	}

        /**
         * @return Returns the decisionList.
         */
        public DecisionList getDecisionList()
        {
            return decisionList;
        }

        //
        // PRIVATE METHODS
        //
        private DecisionList decisionListLearning(DataSet ds)
        {
            if (ds.size() == 0)
            {
                return new DecisionList(positive, negative);
            }
            List<DLTest> possibleTests = testFactory
                    .createDLTestsWithAttributeCount(ds, 1);
            DLTest test = getValidTest(possibleTests, ds);
            if (test == null)
            {
                return new DecisionList(null, FAILURE);
            }
            // at this point there is a test that classifies some subset of examples
            // with the same target value
            DataSet matched = test.matchedExamples(ds);
            DecisionList list = new DecisionList(positive, negative);
            list.add(test, matched.getExample(0).targetValue());
            return list.mergeWith(decisionListLearning(test.unmatchedExamples(ds)));
        }

        private DLTest getValidTest(List<DLTest> possibleTests, DataSet ds) {
		foreach (DLTest test in possibleTests) {
			DataSet matched = test.matchedExamples(ds);
			if (!(matched.size() == 0)) {
				if (allExamplesHaveSameTargetValue(matched)) {
					return test;
				}
			}

		}
		return null;
	}

        private bool allExamplesHaveSameTargetValue(DataSet matched) {
		// assumes at least i example in dataset
		String targetValue = matched.getExample(0).targetValue();
		foreach (Example e in matched.examples) {
			if (!(e.targetValue().Equals(targetValue))) {
				return false;
			}
		}
		return true;
	}
    }
}