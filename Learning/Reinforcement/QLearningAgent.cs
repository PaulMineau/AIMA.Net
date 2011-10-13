namespace AIMA.Core.Learning.Reinforcement
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Probability.Decision;
using AIMA.Core.Util;
using AIMA.Core.Util.DataStructure;

/**
 * @author Ravi Mohan
 * 
 */
public class QLearningAgent<STATE_TYPE, ACTION_TYPE> :
		MDPAgent<STATE_TYPE, ACTION_TYPE> {

	private Dictionary<Pair<STATE_TYPE, ACTION_TYPE>, Double> Q;

	private FrequencyCounter<Pair<STATE_TYPE, ACTION_TYPE>> stateActionCount;

	private Double previousReward;

	private QTable<STATE_TYPE, ACTION_TYPE> qTable;

	private int actionCounter;

	public QLearningAgent(MDP<STATE_TYPE, ACTION_TYPE> mdp) : base(mdp) {
		
		Q = new Dictionary<Pair<STATE_TYPE, ACTION_TYPE>, Double>();
		qTable = new QTable<STATE_TYPE, ACTION_TYPE>(mdp.getAllActions());
		stateActionCount = new FrequencyCounter<Pair<STATE_TYPE, ACTION_TYPE>>();
		actionCounter = 0;
	}

	public override ACTION_TYPE decideAction(MDPPerception<STATE_TYPE> perception) {
		currentState = perception.getState();
		currentReward = perception.getReward();

		if (startingTrial()) {
			ACTION_TYPE chosenAction = selectRandomAction();
			updateLearnerState(chosenAction);
			return previousAction;
		}

		if (mdp.isTerminalState(currentState)) {
			incrementStateActionCount(previousState, previousAction);
			updateQ(0.8);
			previousAction = default(ACTION_TYPE);
			previousState = default(STATE_TYPE);
			previousReward = double.MinValue;
			return previousAction;
		}

		else {
			incrementStateActionCount(previousState, previousAction);
			ACTION_TYPE chosenAction = updateQ(0.8);
			updateLearnerState(chosenAction);
			return previousAction;
		}

	}

	public Dictionary<Pair<STATE_TYPE, ACTION_TYPE>, Double> getQ() {
		return Q;
	}

	public QTable<STATE_TYPE, ACTION_TYPE> getQTable() {
		return qTable;
	}

	//
	// PRIVATE METHODS
	//

	private void updateLearnerState(ACTION_TYPE chosenAction) {
		// previousAction = actionMaximizingLearningFunction();
		previousAction = chosenAction;
		previousAction = chosenAction;
		previousState = currentState;
		previousReward = currentReward;
	}

	private ACTION_TYPE updateQ(double gamma) {

		actionCounter++;
		// qtable update

		double alpha = calculateProbabilityOf(previousState, previousAction);
		ACTION_TYPE ac = qTable.upDateQ(previousState, previousAction,
				currentState, alpha, currentReward, 0.8);

		return ac;
	}

	private double calculateProbabilityOf(STATE_TYPE state, ACTION_TYPE action) {
        if (state == null) return 0.0;
		Double den = 0.0;
		Double num = 0.0;
		foreach (Pair<STATE_TYPE, ACTION_TYPE> stateActionPair in stateActionCount
				.getStates()) {
                    if (stateActionPair.getFirst() == null)
                    {
                        continue;
                    }
			if (stateActionPair.getFirst().Equals(state)) {
				den += 1;
				if (stateActionPair.getSecond().Equals(action)) {
					num += 1;
				}
			}
		}
		return num / den;
	}

	private ACTION_TYPE actionMaximizingLearningFunction() {
		ACTION_TYPE maxAct = default(ACTION_TYPE);
		Double maxValue = Double.MinValue;
		foreach (ACTION_TYPE action in mdp.getAllActions()) {
			Double qValue = qTable.getQValue(currentState, action);
			Double lfv = learningFunction(qValue);
			if (lfv > maxValue) {
				maxValue = lfv;
				maxAct = action;
			}
		}
		return maxAct;
	}

	private Double learningFunction(Double utility) {
		if (actionCounter > 3) {
			actionCounter = 0;
			return 1.0;
		} else {
			return utility;
		}
	}

	private ACTION_TYPE selectRandomAction() {
		List<ACTION_TYPE> allActions = mdp.getAllActions();
		return allActions[0];
		// return Util.selectRandomlyFromList(allActions);
	}

	private bool startingTrial() {
		return (previousAction == null) && (previousState == null)
				&& (previousReward == null)
				&& (currentState.Equals(mdp.getInitialState()));
	}

	private void incrementStateActionCount(STATE_TYPE state, ACTION_TYPE action) {
		Pair<STATE_TYPE, ACTION_TYPE> stateActionPair = new Pair<STATE_TYPE, ACTION_TYPE>(
				state, action);
		stateActionCount.incrementFor(stateActionPair);
	}
}
}