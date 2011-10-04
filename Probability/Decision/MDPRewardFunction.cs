namespace CosmicFlow.AIMA.Core.Probability.Decision
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class MDPRewardFunction<STATE_TYPE>
    {
        Dictionary<STATE_TYPE, Double> stateToReward;

        public MDPRewardFunction()
        {
            stateToReward = new Dictionary<STATE_TYPE, Double>();
        }

        public double getRewardFor(STATE_TYPE state)
        {
            return stateToReward[state];
        }

        public void setReward(STATE_TYPE state, Double reward)
        {
            if (stateToReward.ContainsKey(state))
            {
                stateToReward[state] = reward;
            }
            else
            {
                stateToReward.Add(state, reward);
            }
        }

        public override String ToString()
        {
            return stateToReward.ToString();
        }

        public MDPUtilityFunction<STATE_TYPE> asUtilityFunction() {
		MDPUtilityFunction<STATE_TYPE> uf = new MDPUtilityFunction<STATE_TYPE>();
		foreach (STATE_TYPE state in stateToReward.Keys) {
			uf.setUtility(state, getRewardFor(state));
		}
		return uf;
	}
    }
}
