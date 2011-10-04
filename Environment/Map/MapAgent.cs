namespace AIMA.Core.Environment.Map
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Agent;
using AIMA.Core.Search.Framework;

/**
 * @author Ciaran O'Reilly
 * 
 */
public class MapAgent : SimpleProblemSolvingAgent {
	private Map map = null;

	private EnvironmentViewNotifier notifier = null;

	private DynamicState state = new DynamicState();

	private Search search = null;

	private String[] goalTests = null;

	private int goalTestPos = 0;

	public MapAgent(Map map, EnvironmentViewNotifier notifier, Search search) {
		this.map = map;
		this.notifier = notifier;
		this.search = search;
	}

	public MapAgent(Map map, EnvironmentViewNotifier notifier, Search search,
			int maxGoalsToFormulate) {
		super(maxGoalsToFormulate);
		this.map = map;
		this.notifier = notifier;
		this.search = search;
	}

	public MapAgent(Map map, EnvironmentViewNotifier notifier, Search search,
			String[] goalTests) {
		super(goalTests.length);
		this.map = map;
		this.notifier = notifier;
		this.search = search;
		this.goalTests = new String[goalTests.length];
		System.arraycopy(goalTests, 0, this.goalTests, 0, goalTests.length);
	}

	//
	// PROTECTED METHODS
	//
	protected override State updateState(Percept p) {
		DynamicPercept dp = (DynamicPercept) p;

		state.setAttribute(DynAttributeNames.AGENT_LOCATION, dp
				.getAttribute(DynAttributeNames.PERCEPT_IN));

		return state;
	}

	protected override Object formulateGoal() {
		Object goal = null;
		if (null == goalTests) {
			goal = map.randomlyGenerateDestination();
		} else {
			goal = goalTests[goalTestPos];
			goalTestPos++;
		}
		notifier.notifyViews("CurrentLocation=In("
				+ state.getAttribute(DynAttributeNames.AGENT_LOCATION)
				+ "), Goal=In(" + goal + ")");

		return goal;
	}

	protected override Problem formulateProblem(Object goal) {
		return new BidirectionalMapProblem(map, (String) state
				.getAttribute(DynAttributeNames.AGENT_LOCATION), (String) goal);
	}

	protected override List<Action> search(Problem problem) {
		List<Action> actions = new List<Action>();
		try {
			List<Action> sactions = search.search(problem);
			foreach (Action action in sactions) {
				actions.Add(action);
			}
		} catch (Exception ex) {
			ex.printStackTrace();
		}
		return actions;
	}

	protected override void notifyViewOfMetrics() {
		Set<String> keys = search.getMetrics().keySet();
		foreach (String key in keys) {
			notifier.notifyViews("METRIC[" + key + "]="
					+ search.getMetrics().get(key));
		}
	}
}
}
