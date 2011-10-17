namespace AIMA.Core.Agent.Impl.AProg.SimpleRule
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AIMA.Core.Agent.Impl;
    using System.Diagnostics;

/**
 * Implementation of an EQUALity condition.
 *
 */

/**
 * @author Ciaran O'Reilly
 * 
 */
public class EQUALCondition : Condition {
	private Object key;

	private Object value;

	public EQUALCondition(Object aKey, Object aValue) {
		Debug.Assert (null != aKey);
        Debug.Assert(null != aValue);

		key = aKey;
		value = aValue;
	}

	public override bool evaluate(ObjectWithDynamicAttributes p) {
		return value.Equals(p.getAttribute(key));
	}

	public override String ToString() {
		StringBuilder sb = new StringBuilder();

        return sb.Append(key).Append("==").Append(value).ToString();
	}
}
}