namespace AIMA.Core.Agent.Impl
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Agent.Action;

/**
 * @author Ciaran O'Reilly
 */
public class DynamicAction : ObjectWithDynamicAttributes ,
		Action {
	public const String ATTRIBUTE_NAME = "name";

	//

	public DynamicAction(String name) {
		this.setAttribute(ATTRIBUTE_NAME, name);
	}

	public String getName() {
		return (String) getAttribute(ATTRIBUTE_NAME);
	}

	//
	// START-Action
	public bool isNoOp() {
		return false;
	}

	// END-Action
	//

	public override String describeType() {
		return Action.GetType().Name;
	}
}
}