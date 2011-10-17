namespace AIMA.Core.Agent.Impl
{
    using System.Collections.Generic;
    using AIMA.Core.Agent;

/**
 * Simple environment view which uses the standard
 * output stream to inform about relevant events.
 * @author Ruediger Lunde
 */
public class SimpleEnvironmentView : EnvironmentView {

	public void agentActed(IAgent agent, Action action,
			EnvironmentState resultingState) {
		System.Console.WriteLine("Agent acted: " + action.ToString());
	}

    public void agentAdded(IAgent agent, EnvironmentState resultingState)
    {
		System.Console.WriteLine("Agent added.");
	}

	public void notify(System.String msg) {
		System.Console.WriteLine("Message: " + msg);
	}
}
}