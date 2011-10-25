using AIMA.Probability.Example;

namespace AIMA.Probability.MDP
{



    /**
     * Default implementation of the MarkovDecisionProcess<S, A> interface.
     * 
     * @param <S>
     *            the state type.
     * @param <A>
     *            the action type.
     * 
     * @author Ciaran O'Reilly
     * @author Ravi Mohan
     */

    public class MDP<S, A> : MarkovDecisionProcess<S, A>
    {
        private Set<S> _states = null;
        private S initialState = default(S);
        private ActionsFunction<S, A> actionsFunction = null;
        private TransitionProbabilityFunction<S, A> transitionProbabilityFunction = null;
        private RewardFunction<S> rewardFunction = null;

        public MDP(Set<S> states, S initialState,
                   ActionsFunction<S, A> actionsFunction,
                   TransitionProbabilityFunction<S, A> transitionProbabilityFunction,
                   RewardFunction<S> rewardFunction)
        {
            this._states = states;
            this.initialState = initialState;
            this.actionsFunction = actionsFunction;
            this.transitionProbabilityFunction = transitionProbabilityFunction;
            this.rewardFunction = rewardFunction;
        }

        //
        // START-MarkovDecisionProcess

        public Set<S> states()
        {
            return _states;
        }

        public S getInitialState()
        {
            return initialState;
        }

        public Set<A> actions(S s)
        {
            return actionsFunction.actions(s);
        }

        public double transitionProbability(S sDelta, S s, A a)
        {
            return transitionProbabilityFunction.probability(sDelta, s, a);
        }

        public double reward(S s)
        {
            return rewardFunction.reward(s);
        }

        // END-MarkovDecisionProcess
        //
    }
}