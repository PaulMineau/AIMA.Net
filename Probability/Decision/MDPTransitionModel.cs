namespace AIMA.Core.Probability.Decision
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AIMA.Core.Util.DataStructure;

    /**
     * @author Ravi Mohan
     * 
     */
    public class MDPTransitionModel<STATE_TYPE, ACTION_TYPE>
    {

        private Dictionary<MDPTransition<STATE_TYPE, ACTION_TYPE>, Double> transitionToProbability = new Dictionary<MDPTransition<STATE_TYPE, ACTION_TYPE>, Double>();

        private List<STATE_TYPE> terminalStates;

        public MDPTransitionModel(List<STATE_TYPE> terminalStates)
        {
            this.terminalStates = terminalStates;

        }

        public void setTransitionProbability(STATE_TYPE initialState,
                ACTION_TYPE action, STATE_TYPE finalState, double probability)
        {
            if (!(isTerminal(initialState)))
            {
                MDPTransition<STATE_TYPE, ACTION_TYPE> t = new MDPTransition<STATE_TYPE, ACTION_TYPE>(
                        initialState, action, finalState);
                if (transitionToProbability.ContainsKey(t))
                {
                    transitionToProbability[t] = probability;
                }
                else
                {
                    transitionToProbability.Add(t, probability);
                }
            }
        }

        public double getTransitionProbability(STATE_TYPE initialState,
                ACTION_TYPE action, STATE_TYPE finalState)
        {
            MDPTransition<STATE_TYPE, ACTION_TYPE> key = new MDPTransition<STATE_TYPE, ACTION_TYPE>(
                    initialState, action, finalState);
            if (transitionToProbability.ContainsKey(key))
            {
                return transitionToProbability[key];
            }
            else
            {
                return 0.0;
            }
        }

        public override String ToString()
        {
            StringBuilder buf = new StringBuilder();
            foreach (MDPTransition<STATE_TYPE, ACTION_TYPE> transition in transitionToProbability
                    .Keys)
            {
                buf.Append(transition.ToString() + " -> "
                        + transitionToProbability[transition] + " \n");
            }
            return buf.ToString();
        }

        public Pair<ACTION_TYPE, Double> getTransitionWithMaximumExpectedUtility(
                STATE_TYPE s, MDPUtilityFunction<STATE_TYPE> uf)
        {

            if ((isTerminal(s)))
            {
                return new Pair<ACTION_TYPE, Double>(default(ACTION_TYPE), 0.0);
            }

            List<MDPTransition<STATE_TYPE, ACTION_TYPE>> transitionsStartingWithS = getTransitionsStartingWith(s);
            Dictionary<ACTION_TYPE, Double> actionsToUtilities = getExpectedUtilityForSelectedTransitions(
                    transitionsStartingWithS, uf);

            return getActionWithMaximumUtility(actionsToUtilities);

        }

        public Pair<ACTION_TYPE, Double> getTransitionWithMaximumExpectedUtilityUsingPolicy(
                MDPPolicy<STATE_TYPE, ACTION_TYPE> policy, STATE_TYPE s,
                MDPUtilityFunction<STATE_TYPE> uf)
        {
            if ((isTerminal(s)))
            {
                return new Pair<ACTION_TYPE, Double>(default(ACTION_TYPE), 0.0);
            }
            List<MDPTransition<STATE_TYPE, ACTION_TYPE>> transitionsWithStartingStateSAndActionFromPolicy = getTransitionsWithStartingStateAndAction(
                    s, policy.getAction(s));
            Dictionary<ACTION_TYPE, Double> actionsToUtilities = getExpectedUtilityForSelectedTransitions(
                    transitionsWithStartingStateSAndActionFromPolicy, uf);

            return getActionWithMaximumUtility(actionsToUtilities);

        }

        public List<MDPTransition<STATE_TYPE, ACTION_TYPE>> getTransitionsWithStartingStateAndAction(
                STATE_TYPE s, ACTION_TYPE a)
        {
            List<MDPTransition<STATE_TYPE, ACTION_TYPE>> result = new List<MDPTransition<STATE_TYPE, ACTION_TYPE>>();
            foreach (MDPTransition<STATE_TYPE, ACTION_TYPE> transition in transitionToProbability
                    .Keys)
            {
                if ((transition.getInitialState().Equals(s))
                        && (transition.getAction().Equals(a)))
                {
                    result.Add(transition);
                }
            }
            return result;
        }

        public ACTION_TYPE randomActionFor(STATE_TYPE s)
        {
            List<MDPTransition<STATE_TYPE, ACTION_TYPE>> transitions = getTransitionsStartingWith(s);
            // MDPTransition<STATE_TYPE, ACTION_TYPE> randomTransition = Util
            // .selectRandomlyFromList(transitions);
            return transitions[0].getAction();
            // return randomTransition.getAction();
        }

        //
        // PRIVATE METHODS
        //

        private bool isTerminal(STATE_TYPE s)
        {
            return terminalStates.Contains(s);
        }

        private Pair<ACTION_TYPE, Double> getActionWithMaximumUtility(
                Dictionary<ACTION_TYPE, Double> actionsToUtilities)
        {
            Pair<ACTION_TYPE, Double> highest = new Pair<ACTION_TYPE, Double>(default(ACTION_TYPE),
                    Double.MinValue);
            foreach (ACTION_TYPE key in actionsToUtilities.Keys)
            {
                Double value = actionsToUtilities[key];
                if (value > highest.getSecond())
                {
                    highest = new Pair<ACTION_TYPE, Double>(key, value);
                }
            }
            return highest;
        }

        private Dictionary<ACTION_TYPE, Double> getExpectedUtilityForSelectedTransitions(

        List<MDPTransition<STATE_TYPE, ACTION_TYPE>> transitions,
                MDPUtilityFunction<STATE_TYPE> uf)
        {
            Dictionary<ACTION_TYPE, Double> actionsToUtilities = new Dictionary<ACTION_TYPE, Double>();
            foreach (MDPTransition<STATE_TYPE, ACTION_TYPE> triplet in transitions)
            {
                STATE_TYPE s = triplet.getInitialState();
                ACTION_TYPE action = triplet.getAction();
                STATE_TYPE destinationState = triplet.getDestinationState();
                double probabilityOfTransition = getTransitionProbability(s,
                        action, destinationState);
                double expectedUtility = (probabilityOfTransition * uf
                        .getUtility(destinationState));
                Double presentValue = actionsToUtilities.ContainsKey(action)? actionsToUtilities[action]: Double.MinValue;

                if (presentValue == Double.MinValue)
                {
                    actionsToUtilities.Add(action, expectedUtility);
                }
                else
                {
                    actionsToUtilities[action]+= expectedUtility;
                }
            }
            return actionsToUtilities;
        }

        private List<MDPTransition<STATE_TYPE, ACTION_TYPE>> getTransitionsStartingWith(
                STATE_TYPE s)
        {
            List<MDPTransition<STATE_TYPE, ACTION_TYPE>> result = new List<MDPTransition<STATE_TYPE, ACTION_TYPE>>();
            foreach (MDPTransition<STATE_TYPE, ACTION_TYPE> transition in transitionToProbability
                    .Keys)
            {
                if (transition.getInitialState().Equals(s))
                {
                    result.Add(transition);
                }
            }
            return result;
        }
    }
}