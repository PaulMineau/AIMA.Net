namespace aima.test.core.unit.environment.vacuum;

using AIMA.Core.Agent.Action;
using AIMA.Core.Agent.Agent;
using AIMA.Core.Agent.EnvironmentState;
using AIMA.Core.Agent.EnvironmentView;

public class EnvironmentViewActionTracker : EnvironmentView {
	private StringBuilder actions = null;

	public EnvironmentViewActionTracker(StringBuilder envChanges) {
		this.actions = envChanges;
	}

	//
	// START-EnvironmentView
	public void notify(String msg) {
		// Do nothing by default.
	}

	public void agentAdded(Agent agent, EnvironmentState state) {
		// Do nothing by default.
	}

	public void agentActed(Agent agent, Action action, EnvironmentState state) {
		actions.append(action);
	}

	// END-EnvironmentView
	//
}
