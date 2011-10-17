namespace AIMA.Core.Agent.Impl
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;

/**
 * @author Ravi Mohan
 * @author Ciaran O'Reilly
 */
public class DynamicEnvironmentState : ObjectWithDynamicAttributes
		, EnvironmentState {
	public DynamicEnvironmentState() {

	}

	public String describeType() {
		return typeof(EnvironmentState).Name;
	}
}
}