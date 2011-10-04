namespace CosmicFlow.AIMA.Core.Probability.Decision
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    /**
     * @author Ravi Mohan
     * 
     */
    public class MDPPolicy<STATE_TYPE, ACTION_TYPE>
    {
        Dictionary<STATE_TYPE, ACTION_TYPE> stateToAction;

        public MDPPolicy()
        {
            stateToAction = new Dictionary<STATE_TYPE, ACTION_TYPE>();
        }

        public ACTION_TYPE getAction(STATE_TYPE state)
        {
            return stateToAction[state];
        }

        public void setAction(STATE_TYPE state, ACTION_TYPE action)
        {
            if (stateToAction.ContainsKey(state))
            {
                stateToAction[state] = action;
            }
            else
            {
                stateToAction.Add(state, action);
            }
        }

        public override String ToString()
        {
            return stateToAction.ToString();
        }

        public List<STATE_TYPE> states()
        {

            return stateToAction.Keys.ToList<STATE_TYPE>();
        }
    }
}