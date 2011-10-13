namespace AIMA.Core.Learning.Reinforcement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
using AIMA.Core.Probability.Decision;
using AIMA.Core.Util;
using AIMA.Core.Util.DataStructure;

/**
 * @author Ravi Mohan
 * 
 */
public class QTable<STATE_TYPE, ACTION_TYPE> {

	Dictionary<Pair<STATE_TYPE, ACTION_TYPE>, Double> table;

	private List<ACTION_TYPE> allPossibleActions;

	public QTable(List<ACTION_TYPE> allPossibleActions) {
		this.table = new Dictionary<Pair<STATE_TYPE, ACTION_TYPE>, Double>();
		this.allPossibleActions = allPossibleActions;
	}

	public Double getQValue(STATE_TYPE state, ACTION_TYPE action) {
		Pair<STATE_TYPE, ACTION_TYPE> stateActionPair = new Pair<STATE_TYPE, ACTION_TYPE>(
				state, action);
		if (!(table.ContainsKey(stateActionPair))) {
			return 0.0;
		} else {
			return table[stateActionPair];
		}
	}

	public Pair<ACTION_TYPE, Double> maxDiff(STATE_TYPE startState,
			ACTION_TYPE action, STATE_TYPE endState) {
		Double maxDiff = 0.0;
		ACTION_TYPE maxAction = default(ACTION_TYPE);
		// randomly choose an action so that it doesn't return the same action
		// every time if all Q(a,s) are zero
		maxAction = Util.selectRandomlyFromList(allPossibleActions);
		maxDiff = getQValue(endState, maxAction)
				- getQValue(startState, action);

		foreach (ACTION_TYPE anAction in allPossibleActions) {
			Double diff = getQValue(endState, anAction)
					- getQValue(startState, action);
			if (diff > maxDiff) {
				maxAction = anAction;
				maxDiff = diff;
			}
		}

		return new Pair<ACTION_TYPE, Double>(maxAction, maxDiff);
	}

	public void setQValue(STATE_TYPE state, ACTION_TYPE action, Double d) {
		Pair<STATE_TYPE, ACTION_TYPE> stateActionPair = new Pair<STATE_TYPE, ACTION_TYPE>(
				state, action);
        if (table.ContainsKey(stateActionPair))
        {
            table[stateActionPair] = d;
        }
        else
        {
            table.Add(stateActionPair, d);
        }
	}

	public ACTION_TYPE upDateQ(STATE_TYPE startState, ACTION_TYPE action,
			STATE_TYPE endState, double alpha, double reward, double phi) {
		double oldQValue = getQValue(startState, action);
		Pair<ACTION_TYPE, Double> actionAndMaxDiffValue = maxDiff(startState,
				action, endState);
		double addedValue = alpha
				* (reward + (phi * actionAndMaxDiffValue.getSecond()));
		setQValue(startState, action, oldQValue + addedValue);
		return actionAndMaxDiffValue.getFirst();
	}

	public void normalize() {
		Double maxValue = findMaximumValue();
		if (maxValue != 0.0) {
			foreach (Pair<STATE_TYPE, ACTION_TYPE> key in table.Keys) {
				Double presentValue = table[key];
				table.Add(key, presentValue / maxValue);
			}
		}
	}

	public MDPPolicy<STATE_TYPE, ACTION_TYPE> getPolicy() {
		MDPPolicy<STATE_TYPE, ACTION_TYPE> policy = new MDPPolicy<STATE_TYPE, ACTION_TYPE>();
		List<STATE_TYPE> startingStatesRecorded = getAllStartingStates();

		foreach (STATE_TYPE state in startingStatesRecorded) {
			ACTION_TYPE action = getRecordedActionWithMaximumQValue(state);
			policy.setAction(state, action);
		}
		return policy;
	}

	public override String ToString() {
		return table.ToString();
	}

	//
	// PRIVATE METHODS
	//

	private Double findMaximumValue() {
		if (table.Keys.Count > 0) {

            Double maxValue = table.First().Value;
			foreach (Pair<STATE_TYPE, ACTION_TYPE> key in table.Keys) {
				Double v = table[key];
				if (v > maxValue) {
					maxValue = v;
				}
			}
			return maxValue;

		} else {
			return 0.0;
		}
	}

	private ACTION_TYPE getRecordedActionWithMaximumQValue(STATE_TYPE state) {
		Double maxValue = Double.MinValue;
		ACTION_TYPE action = default(ACTION_TYPE);
		foreach (Pair<STATE_TYPE, ACTION_TYPE> stateActionPair in table.Keys) {
			if (stateActionPair.getFirst().Equals(state)) {
				ACTION_TYPE ac = stateActionPair.getSecond();
				Double value = table[stateActionPair];
				if (value > maxValue) {
					maxValue = value;
					action = ac;
				}
			}
		}
		return action;
	}

	private List<STATE_TYPE> getAllStartingStates() {
		List<STATE_TYPE> states = new List<STATE_TYPE>();
		foreach (Pair<STATE_TYPE, ACTION_TYPE> stateActionPair in table.Keys) {
			STATE_TYPE state = stateActionPair.getFirst();
			if (!(states).Contains(state)) {
				states.Add(state);
			}
		}
		return states;
	}
}
}