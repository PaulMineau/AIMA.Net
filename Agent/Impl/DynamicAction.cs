namespace AIMA.Core.Agent.Impl
{
    using System.Collections.Generic;
    using AIMA.Core.Agent;

/**
 * @author Ciaran O'Reilly
 */
public class DynamicAction : ObjectWithDynamicAttributes ,
		Action {
    public const System.String ATTRIBUTE_NAME = "name";

	//

    public DynamicAction(System.String name)
    {
		this.setAttribute(ATTRIBUTE_NAME, name);
	}

    public System.String getName()
    {
        return (System.String)getAttribute(ATTRIBUTE_NAME);
	}

	//
	// START-Action
	public bool isNoOp() {
		return false;
	}

	// END-Action
	//

	public System.String describeType() {
		return this.GetType().Name;
	}
}
}