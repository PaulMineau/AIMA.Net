namespace AIMA.Core.Environment.Map
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Agent;
using AIMA.Core.Agent.Impl;

/**
 * Represents the environment a MapAgent can navigate.
 */

/**
 * @author Ciaran O'Reilly
 * 
 */
public class MapEnvironment : AbstractEnvironment {

	private Map aMap = null;
	private MapEnvironmentState state = new MapEnvironmentState();

	public MapEnvironment(Map aMap) {
		this.aMap = aMap;
	}

	public void addAgent(Agent a, String startLocation) {
		// Ensure the agent state information is tracked before
		// adding to super, as super will notify the registered
		// EnvironmentViews that is was added.
		state.setAgentLocationAndTravelDistance(a, startLocation, 0.0);
		super.AddAgent(a);
	}

	public String getAgentLocation(Agent a) {
		return state.getAgentLocation(a);
	}

	public Double getAgentTravelDistance(Agent a) {
		return state.getAgentTravelDistance(a);
	}

	public override EnvironmentState getCurrentState() {
		return state;
	}

	public override EnvironmentState executeAction(Agent agent, Action a) {

		if (!a.isNoOp()) {
			MoveToAction act = (MoveToAction) a;

			String currLoc = getAgentLocation(agent);
			Double distance = aMap.getDistance(currLoc, act.getToLocation());
			if (distance != null) {
				double currTD = getAgentTravelDistance(agent);
				state.setAgentLocationAndTravelDistance(agent, act
						.getToLocation(), currTD + distance);
			}
		}

		return state;
	}

	public override Percept getPerceptSeenBy(Agent anAgent) {
		return new DynamicPercept(DynAttributeNames.PERCEPT_IN,
				getAgentLocation(anAgent));
	}

	public Map getMap() {
		return aMap;
	}
}
}