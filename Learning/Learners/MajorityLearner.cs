namespace AIMA.Core.Learning.Learners
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Learning.Framework;
    using AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public class MajorityLearner : Learner
    {

        private String result;

        public void train(DataSet ds)
        {
            List<String> targets = new List<String>();
            foreach (Example e in ds.examples)
            {
                targets.Add(e.targetValue());
            }
            result = Util.mode(targets);
        }

        public String predict(Example e)
        {
            return result;
        }

        public int[] test(DataSet ds)
        {
            int[] results = new int[] { 0, 0 };

            foreach (Example e in ds.examples)
            {
                if (e.targetValue().Equals(result))
                {
                    results[0] = results[0] + 1;
                }
                else
                {
                    results[1] = results[1] + 1;
                }
            }
            return results;
        }
    }
}