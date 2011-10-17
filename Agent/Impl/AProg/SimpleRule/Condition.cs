namespace AIMA.Core.Agent.Impl.AProg.SimpleRule
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent.Impl;

/**
 * Base abstract class for describing conditions.
 *
 */

/**
 * @author Ciaran O'Reilly
 * 
 */
public abstract class Condition {
	public abstract bool evaluate(ObjectWithDynamicAttributes p);

	public bool Equals(Object o) {
		if (o == null || !(o is Condition)) {
			return base.Equals(o);
		}
		return (ToString().Equals(((Condition) o).ToString()));
	}

	public override int GetHashCode() {
		return ToString().GetHashCode();
	}
}
}