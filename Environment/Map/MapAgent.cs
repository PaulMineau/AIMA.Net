namespace AIMA.Core.Environment.Map
{
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Search.Framework;
    using AIMA.Core.Agent.Impl;

/**
 * @author Ciaran O'Reilly
 * 
 */
public class MapAgent : SimpleProblemSolvingAgent {
	private Map map = null;

	private EnvironmentViewNotifier notifier = null;

	private DynamicState state = new DynamicState();

	private Search _search = null;

	private System.String[] goalTests = null;

	private int goalTestPos = 0;

	public MapAgent(Map map, EnvironmentViewNotifier notifier, Search search) {
		this.map = map;
		this.notifier = notifier;
        this._search = search;
	}

	public MapAgent(Map map, EnvironmentViewNotifier notifier, Search search,
			int maxGoalsToFormulate) : base ( maxGoalsToFormulate) {
		
		this.map = map;
		this.notifier = notifier;
        this._search = search;
	}

	public MapAgent(Map map, EnvironmentViewNotifier notifier, Search search,
			System.String[] goalTests) : base (goalTests.Length){
		
		this.map = map;
		this.notifier = notifier;
        this._search = search;
        this.goalTests = new System.String[goalTests.Length];
        System.Array.Copy(goalTests, 0, this.goalTests, 0, goalTests.Length);
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

    protected override System.Object formulateGoal()
    {
        System.Object goal = null;
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

    protected override Problem formulateProblem(System.Object goal)
    {
        return new BidirectionalMapProblem(map, (System.String)state
                .getAttribute(DynAttributeNames.AGENT_LOCATION), (System.String)goal);
	}

	protected override List<Action> search(Problem problem) {
		List<Action> actions = new List<Action>();
		try {
            List<Action> sactions = _search.search(problem);
			foreach (Action action in sactions) {
				actions.Add(action);
			}
        }
        catch (System.Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
		}
		return actions;
	}

	protected override void notifyViewOfMetrics() {
        HashSet<System.String> keys = _search.getMetrics().keySet();
        foreach (System.String key in keys)
        {
			notifier.notifyViews("METRIC[" + key + "]="
                    + _search.getMetrics().get(key));
		}
	}
}
}
