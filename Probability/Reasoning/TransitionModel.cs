namespace CosmicFlow.AIMA.Core.Probability.Reasoning
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Util.DataStructure;
    using CosmicFlow.AIMA.Core.Util.Math;

    /**
     * @author Ravi Mohan
     * 
     */
    public class TransitionModel
    {

        private Table<String, String, Double> table;

        private List<String> states;

        public TransitionModel(List<String> states, List<String> actions) {
		this.states = states;
		List<String> state_actions = new List<String>();
		foreach (String state in states) {
			foreach (String action in actions) {
				state_actions.Add(String.Concat(state,action));
			}
		}
		table = new Table<String, String, Double>(state_actions, states);
	}

        public TransitionModel(List<String> states) : this(states, new List<String>(){ HmmConstants.DO_NOTHING})
        {
            // no actions possible thus the only "action" is to "wait" till the next
            // perception is observed

        }

        public void setTransitionProbability(String startState, String endState,
                Double probability)
        {
            String start_state_plus_action = String.Concat(startState
                    ,HmmConstants.DO_NOTHING);
            table.set(start_state_plus_action, endState, probability);
        }

        public void setTransitionProbability(String startState, String action,
                String endState, Double probability)
        {
            String start_state_plus_action = String.Concat(startState, action);
            table.set(start_state_plus_action, endState, probability);
        }

        public double get(String old_state_action, String newState)
        {
            return table.get(old_state_action, newState).Value;
        }

        public Matrix asMatrix(String action)
        {
            Matrix transitionMatrix = new Matrix(states.Count, states.Count);
            for (int i = 0; i < states.Count; i++)
            {
                String oldState = states[i];
                String old_state_action = String.Concat(oldState,action);
                for (int j = 0; j < states.Count; j++)
                {
                    String newState = states[j];
                    double transitionProbability = get(old_state_action, newState);
                    transitionMatrix.set(i, j, transitionProbability);
                }
            }
            return transitionMatrix;
        }

        public Matrix asMatrix()
        {
            return asMatrix(HmmConstants.DO_NOTHING);
        }

        public Matrix unitMatrix()
        {
            Matrix m = asMatrix();
            return Matrix.identity(m.getRowDimension(), m.getColumnDimension());
        }

        public String getStateForProbability(String oldState, double probability)
        {
            return getStateForGivenActionAndProbability(oldState,
                    HmmConstants.DO_NOTHING, probability);
        }

        public String getStateForProbability(String oldState, String action,
                double probability)
        {
            return getStateForGivenActionAndProbability(oldState, action,
                    probability);
        }

        public String getStateForGivenActionAndProbability(String oldState,
                String action, double probability)
        {
            String state_action = oldState + action;

            double total = 0.0;
            foreach (String state in states)
            {
                total += table.get(state_action, state).Value;
                if (total >= probability)
                {
                    return state;
                }
            }
            return null;
        }
    }
}