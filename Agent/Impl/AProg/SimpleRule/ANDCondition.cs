namespace AIMA.Core.Agent.Impl.AProg.SimpleRule
{
    using System;
    using System.Collections.Generic;
    using System.Text;
using AIMA.Core.Agent.Impl;

/**
 * Implementation of an AND condition.
 *
 */

/**
 * @author Ciaran O'Reilly
 * 
 */
public class ANDCondition : Condition {
	private Condition left;

	private Condition right;

	public ANDCondition(Condition aLeftCon, Condition aRightCon) {
		assert (null != aLeftCon);
		assert (null != aRightCon);

		left = aLeftCon;
		right = aRightCon;
	}

	public override bool evaluate(ObjectWithDynamicAttributes p) {
		return (left.evaluate(p) && right.evaluate(p));
	}

	public override String ToString() {
		StringBuilder sb = new StringBuilder();

		return sb.append("[").append(left).append(" && ").append(right).append(
				"]").ToString();
	}
}
}