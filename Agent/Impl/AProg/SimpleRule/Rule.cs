namespace AIMA.Core.Agent.Impl.AProg.SimpleRule
{
    using System.Collections.Generic;
    using System.Text;
    using AIMA.Core.Agent;
    using AIMA.Core.Agent.Impl;
    using System.Diagnostics;

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

    public Rule(Condition aCon, Action anAction)
    {
        Debug.Assert(null != aCon);
        Debug.Assert(null != anAction);

		con = aCon;
		action = anAction;
	}

	public bool evaluate(ObjectWithDynamicAttributes p) {
		return (con.evaluate(p));
	}

	public Action getAction() {
		return action;
	}

	public override bool Equals(System.Object o) {
		if (o == null || !(o is Rule)) {
			return base.Equals(o);
		}
		return (ToString().Equals(((Rule) o).ToString()));
	}

	public override int GetHashCode() {
		return ToString().GetHashCode();
	}

    public override System.String ToString()
    {
		StringBuilder sb = new StringBuilder();

        return sb.Append("if ").Append(con).Append(" then ").Append(action)
                .Append(".").ToString();
	}
}
}