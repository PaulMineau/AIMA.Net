namespace CosmicFlow.AIMA.Core.Learning.Reinforcement
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Probability.Decision;
    using CosmicFlow.AIMA.Core.Util.DataStructure;

    /**
     * @author Ravi Mohan
     * 
     */
    public class PassiveADPAgent<STATE_TYPE, ACTION_TYPE> :
            MDPAgent<STATE_TYPE, ACTION_TYPE>
    {
        private MDPPolicy<STATE_TYPE, ACTION_TYPE> policy;

        private MDPUtilityFunction<STATE_TYPE> utilityFunction;

        private Dictionary<Pair<STATE_TYPE, ACTION_TYPE>, Double> nsa;

        private Dictionary<MDPTransition<STATE_TYPE, ACTION_TYPE>, Double> nsasdash;

        public PassiveADPAgent(MDP<STATE_TYPE, ACTION_TYPE> mdp,
                MDPPolicy<STATE_TYPE, ACTION_TYPE> policy) : base(mdp.emptyMdp())
        {
            
            this.policy = policy;
            this.utilityFunction = new MDPUtilityFunction<STATE_TYPE>();
            this.nsa = new Dictionary<Pair<STATE_TYPE, ACTION_TYPE>, Double>();
            this.nsasdash = new Dictionary<MDPTransition<STATE_TYPE, ACTION_TYPE>, Double>();

        }

        public override ACTION_TYPE decideAction(MDPPerception<STATE_TYPE> perception)
        {

            if (!(utilityFunction.hasUtilityFor(perception.getState())))
            { // if
                // perceptionState
                // is
                // new
                utilityFunction.setUtility(perception.getState(), perception
                        .getReward());
                mdp.setReward(perception.getState(), perception.getReward());
            }
            if (!(previousState == null))
            {
                Pair<STATE_TYPE,ACTION_TYPE> prevState = new Pair<STATE_TYPE, ACTION_TYPE>(previousState, previousAction);
                
                if (!nsa.ContainsKey(prevState))
                {
                    nsa.Add(prevState, 1.0);
                }
                else
                {
                    nsa[prevState]++;
                }
                MDPTransition<STATE_TYPE, ACTION_TYPE> prevTransition = new MDPTransition<STATE_TYPE, ACTION_TYPE>(
                                previousState, previousAction, currentState);
                
                if (!nsasdash.ContainsKey(prevTransition))
                {
                    nsasdash.Add(prevTransition, 1.0);

                }
                else
                {
                    nsasdash[prevTransition]++;
                }
                foreach (MDPTransition<STATE_TYPE, ACTION_TYPE> transition in nsasdash
                        .Keys)
                {
                    if (nsasdash[transition] != 0.0)
                    {
                        double newValue = nsasdash[transition]
                                / nsa[new Pair<STATE_TYPE, ACTION_TYPE>(
                                        transition.getInitialState(), transition
                                                .getAction())];
                        mdp.setTransitionProbability(transition, newValue);
                    }
                }
                List<MDPTransition<STATE_TYPE, ACTION_TYPE>> validTransitions = mdp
                        .getTransitionsWith(previousState, policy
                                .getAction(previousState));
                utilityFunction = valueDetermination(validTransitions, 1);
            }

            if (mdp.isTerminalState(currentState))
            {
                previousState = default(STATE_TYPE);
                previousAction = default(ACTION_TYPE);
            }
            else
            {
                previousState = currentState;
                previousAction = policy.getAction(currentState);
            }
            return previousAction;
        }

        public MDPUtilityFunction<STATE_TYPE> getUtilityFunction()
        {
            return utilityFunction;
        }

        //
        // PRIVATE METHODS
        //
        private MDPUtilityFunction<STATE_TYPE> valueDetermination(
                List<MDPTransition<STATE_TYPE, ACTION_TYPE>> validTransitions,
                double gamma)
        {
            MDPUtilityFunction<STATE_TYPE> uf = utilityFunction.copy();
            double additional = 0.0;
            if (validTransitions.Count > 0)
            {
                STATE_TYPE initState = validTransitions[0].getInitialState();
                double reward = mdp.getRewardFor(initState);
                foreach (MDPTransition<STATE_TYPE, ACTION_TYPE> transition in validTransitions)
                {
                    additional += mdp.getTransitionProbability(transition)
                            * utilityFunction.getUtility(transition
                                    .getDestinationState());
                }
                uf.setUtility(initState, reward + (gamma * additional));
            }

            return uf;
        }
    }
}
