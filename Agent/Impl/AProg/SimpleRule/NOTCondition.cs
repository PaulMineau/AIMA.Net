namespace AIMA.Core.Agent.Impl.AProg.SimpleRule
{
    using System;
    using System.Collections.Generic;
    using System.Text;
using AIMA.Core.Agent.Impl;

/**
 * Implementation of a NOT condition.
 *
 */

/**
 * @author Ciaran O'Reilly
 * 
 */
public class NOTCondition : Condition {
	private Condition con;

	public NOTCondition(Condition aCon) {
		assert (null != aCon);

		con = aCon;
	}

	public override bool evaluate(ObjectWithDynamicAttributes p) {
		return (!con.evaluate(p));
	}

	public override String ToString() {
		StringBuilder sb = new StringBuilder();

		return sb.append("![").append(con).append("]").ToString();
	}
}
}