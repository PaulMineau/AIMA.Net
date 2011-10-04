namespace AIMA.Core.Agent.Impl.AProg.SimpleRule
{
    using System;
    using System.Collections.Generic;
    using System.Text;
using AIMA.Core.Agent.Impl;

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
		assert (null != aKey);
		assert (null != aValue);

		key = aKey;
		value = aValue;
	}

	public override bool evaluate(ObjectWithDynamicAttributes p) {
		return value.Equals(p.getAttribute(key));
	}

	public override String ToString() {
		StringBuilder sb = new StringBuilder();

		return sb.append(key).append("==").append(value).ToString();
	}
}
}