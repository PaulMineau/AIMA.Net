namespace AIMA.Core.Environment.Map
{
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

    public void addAgent(IAgent a, System.String startLocation)
    {
		// Ensure the agent state information is tracked before
		// adding to super, as super will notify the registered
		// EnvironmentViews that is was added.
		state.setAgentLocationAndTravelDistance(a, startLocation, 0.0);
		base.addAgent(a);
	}

	public System.String getAgentLocation(IAgent a) {
		return state.getAgentLocation(a);
	}

    public System.Double getAgentTravelDistance(IAgent a)
    {
		return state.getAgentTravelDistance(a);
	}

	public override EnvironmentState getCurrentState() {
		return state;
	}

    public override EnvironmentState executeAction(IAgent agent, Action a)
    {

		if (!a.isNoOp()) {
			MoveToAction act = (MoveToAction) a;

            System.String currLoc = getAgentLocation(agent);
            System.Double distance = aMap.getDistance(currLoc, act.getToLocation());
			if (distance != null) {
				double currTD = getAgentTravelDistance(agent);
				state.setAgentLocationAndTravelDistance(agent, act
						.getToLocation(), currTD + distance);
			}
		}

		return state;
	}

	public override Percept getPerceptSeenBy(IAgent anAgent) {
		return new DynamicPercept(DynAttributeNames.PERCEPT_IN,
				getAgentLocation(anAgent));
	}

	public Map getMap() {
		return aMap;
	}
}
}