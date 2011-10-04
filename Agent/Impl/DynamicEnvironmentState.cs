namespace AIMA.Core.Agent.Impl
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Agent.EnvironmentState;

/**
 * @author Ravi Mohan
 * @author Ciaran O'Reilly
 */
public class DynamicEnvironmentState : ObjectWithDynamicAttributes
		, EnvironmentState {
	public DynamicEnvironmentState() {

	}

	public override String describeType() {
		return typeof(EnvironmentState).Name;
	}
}
}