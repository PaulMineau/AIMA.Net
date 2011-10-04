namespace AIMA.Core.Agent.Impl
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Agent;

/**
 * Simple environment view which uses the standard
 * output stream to inform about relevant events.
 * @author Ruediger Lunde
 */
public class SimpleEnvironmentView : EnvironmentView {

	public override void agentActed(Agent agent, Action action,
			EnvironmentState resultingState) {
		System.Console.WriteLine("Agent acted: " + action.ToString());
	}

	public override void agentAdded(Agent agent, EnvironmentState resultingState) {
		System.Console.WriteLine("Agent added.");
	}

	public override void notify(String msg) {
		System.Console.WriteLine("Message: " + msg);
	}
}
}