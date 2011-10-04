namespace CosmicFlow.AIMA.Core.Learning.Reinforcement
{
    using System;
    using System.Collections.Generic;
using CosmicFlow.AIMA.Core.Probability.Decision;
using CosmicFlow.AIMA.Core.Util;

/**
 * @author Ravi Mohan
 * 
 */
public class PassiveTDAgent<STATE_TYPE, ACTION_TYPE> :
		MDPAgent<STATE_TYPE, ACTION_TYPE> {

	private MDPPolicy<STATE_TYPE, ACTION_TYPE> policy;

	private MDPUtilityFunction<STATE_TYPE> utilityFunction;

	// private Dictionary<STATE_TYPE,Double> stateCount;
	private FrequencyCounter<STATE_TYPE> stateCount;

	private Double previousReward;

	public PassiveTDAgent(MDP<STATE_TYPE, ACTION_TYPE> mdp,
			MDPPolicy<STATE_TYPE, ACTION_TYPE> policy) : base(mdp.emptyMdp()) {
		this.policy = policy;
		this.utilityFunction = new MDPUtilityFunction<STATE_TYPE>();
		this.stateCount = new FrequencyCounter<STATE_TYPE>();
	}

	public override ACTION_TYPE decideAction(MDPPerception<STATE_TYPE> perception) {

		if (!(utilityFunction.hasUtilityFor(perception.getState()))) { // if
			// perceptionState
			// is
			// new
			utilityFunction.setUtility(perception.getState(), perception
					.getReward());
			mdp.setReward(perception.getState(), perception.getReward());
		}
		if (!(previousState == null)) {
			stateCount.incrementFor(previousState);
			utilityFunction = updateUtilityFunction(1.0);
		}

		if (mdp.isTerminalState(currentState)) {
			previousState = default(STATE_TYPE);
			previousAction = default(ACTION_TYPE);
			previousReward = double.MinValue;
		} else {
			previousState = currentState;
			previousAction = policy.getAction(currentState);
			previousReward = currentReward;
		}
		return previousAction;
	}

	public MDPUtilityFunction<STATE_TYPE> getUtilityFunction() {
		return utilityFunction;
	}

	//
	// PRIVATE METHODS
	//

	private MDPUtilityFunction<STATE_TYPE> updateUtilityFunction(double gamma) {
		MDPUtilityFunction<STATE_TYPE> uf = utilityFunction.copy();
		double u_s = utilityFunction.getUtility(previousState);
		double gammaUtilDIff = ((gamma * utilityFunction
				.getUtility(currentState)) - utilityFunction
				.getUtility(previousState));
		double alphaTerm = stateCount.probabilityOf(previousState)
				* (previousReward + gammaUtilDIff);
		uf.setUtility(previousState, u_s + alphaTerm);
		return uf;
	}
}
}
