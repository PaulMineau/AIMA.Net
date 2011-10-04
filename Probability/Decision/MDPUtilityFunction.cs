namespace CosmicFlow.AIMA.Core.Probability.Decision
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class MDPUtilityFunction<STATE_TYPE>
    {
        private Dictionary<STATE_TYPE, Double> hash;

        public MDPUtilityFunction()
        {
            hash = new Dictionary<STATE_TYPE, Double>();
        }

        public Double getUtility(STATE_TYPE state)
        {
            Double d = hash.ContainsKey(state)? hash[state] : Double.MinValue;
            if (d == Double.MinValue)
            {
                System.Console.WriteLine("no value for " + state);
            }
            return d;
        }

        public void setUtility(STATE_TYPE state, double utility)
        {
            if (hash.ContainsKey(state))
            {
                hash[state] = utility;
            }
            else
            {
                hash.Add(state, utility);
            }
        }

        public MDPUtilityFunction<STATE_TYPE> copy()
        {
            MDPUtilityFunction<STATE_TYPE> other = new MDPUtilityFunction<STATE_TYPE>();
            foreach (STATE_TYPE state in hash.Keys)
            {
                other.setUtility(state, hash[state]);
            }
            return other;
        }

        public override String ToString()
        {
            return hash.ToString();
        }

        public bool hasUtilityFor(STATE_TYPE state)
        {

            return hash.ContainsKey(state);
        }
    }
}