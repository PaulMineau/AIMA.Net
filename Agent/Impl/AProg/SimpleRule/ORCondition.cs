namespace AIMA.Core.Agent.Impl.AProg.SimpleRule
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AIMA.Core.Agent.Impl;
    using System.Diagnostics;

/**
 * Implementation of an OR condition.
 *
 */

/**
 * @author Ciaran O'Reilly
 * 
 */
public class ORCondition : Condition {
	private Condition left;

	private Condition right;

	public ORCondition(Condition aLeftCon, Condition aRightCon) {
		Debug.Assert(null != aLeftCon);
        Debug.Assert(null != aRightCon);

		left = aLeftCon;
		right = aRightCon;
	}

    public override bool evaluate(ObjectWithDynamicAttributes p)
    {
		return (left.evaluate(p) || right.evaluate(p));
	}

	public override String ToString() {
		StringBuilder sb = new StringBuilder();

        return sb.Append("[").Append(left).Append(" || ").Append(right).Append(
				"]").ToString();
	}
}
}