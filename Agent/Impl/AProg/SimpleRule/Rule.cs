namespace AIMA.Core.Agent.Impl.AProg.SimpleRule
{
    using System;
    using System.Collections.Generic;
    using System.Text;
using AIMA.Core.Agent.Action;
using AIMA.Core.Agent.Impl;

/**
 *  A simple implementation of a "condition-action rule".
 *
 */

/**
 * @author Ciaran O'Reilly
 * 
 */
public class Rule {
	private Condition con;

	private Action action;

	public Rule(Condition aCon, Action anAction) {
		assert (null != aCon);
		assert (null != anAction);

		con = aCon;
		action = anAction;
	}

	public bool evaluate(ObjectWithDynamicAttributes p) {
		return (con.evaluate(p));
	}

	public Action getAction() {
		return action;
	}

	public override bool Equals(Object o) {
		if (o == null || !(o is Rule)) {
			return super.Equals(o);
		}
		return (ToString().Equals(((Rule) o).ToString()));
	}

	public override int GetHashCode() {
		return ToString().GetHashCode();
	}

	public override String ToString() {
		StringBuilder sb = new StringBuilder();

		return sb.append("if ").append(con).append(" then ").append(action)
				.append(".").ToString();
	}
}
}