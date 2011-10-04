namespace CosmicFlow.AIMA.Core.Learning.Inductive
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Learning.Framework;
    using System.Text;

    /**
     * @author Ravi Mohan
     * 
     */
    public class DecisionList
    {
        private String positive, negative;

        private List<DLTest> tests;

        private Dictionary<DLTest, String> testOutcomes;

        public DecisionList(String positive, String negative)
        {
            this.positive = positive;
            this.negative = negative;
            this.tests = new List<DLTest>();
            testOutcomes = new Dictionary<DLTest, String>();
        }

        public String predict(Example example)
        {
            if (tests.Count == 0)
            {
                return negative;
            }
            foreach (DLTest test in tests)
            {
                if (test.matches(example))
                {
                    return testOutcomes[test];
                }
            }
            return negative;
        }

        public void add(DLTest test, String outcome)
        {
            tests.Add(test);
            testOutcomes.Add(test, outcome);
        }

        public DecisionList mergeWith(DecisionList dlist2)
        {
            DecisionList merged = new DecisionList(positive, negative);
            foreach (DLTest test in tests)
            {
                merged.add(test, testOutcomes[test]);
            }
            foreach (DLTest test in dlist2.tests)
            {
                merged.add(test, dlist2.testOutcomes[test]);
            }
            return merged;
        }

        public override String ToString()
        {
            StringBuilder buf = new StringBuilder();
            foreach (DLTest test in tests)
            {
                buf.Append(test.ToString() + " => " + testOutcomes[test]
                        + " ELSE \n");
            }
            buf.Append("END");
            return buf.ToString();
        }
    }
}
