namespace AIMA.Core.Agent.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Diagnostics;
/**
 * @author Ravi Mohan
 * @author Ciaran O'Reilly
 */
public abstract class ObjectWithDynamicAttributes {
	private Dictionary<Object, Object> attributes = new Dictionary<Object, Object>();

	//
	// PUBLIC METHODS
	//
	public String describeType() {
		return this.GetType().Name;
	}

	public String describeAttributes() {
		StringBuilder sb = new StringBuilder();

		sb.Append("[");
		bool first = true;
		foreach (Object key in attributes.Keys) {
			if (first) {
				first = false;
			} else {
                sb.Append(", ");
			}

            sb.Append(key);
            sb.Append("==");
            sb.Append(attributes[key]);
		}
        sb.Append("]");

		return sb.ToString();
	}

	public HashSet<Object> getKeySet() {
		return new Set<Object>(attributes.Keys);
	}

	public void setAttribute(Object key, Object value) {
		attributes[key] = value;
	}

	public Object getAttribute(Object key) {
		return attributes[key];
	}

	public void removeAttribute(Object key) {
		attributes.Remove(key);
	}

	public ObjectWithDynamicAttributes copy() {
		ObjectWithDynamicAttributes copy = null;

		try {
			copy = (ObjectWithDynamicAttributes)this.GetType().GetConstructor(System.Type.EmptyTypes).Invoke(null);
            foreach (object val in attributes)
            {
                copy.attributes.Add(val, attributes[val]);
            }
		} catch (Exception ex) {
			Debug.WriteLine(ex.ToString());
		}

		return copy;
	}

	public override bool Equals(Object o) {
		if (o == null || this.GetType() != o.GetType()) {
			return base.Equals(o);
		}
		return attributes.Equals(((ObjectWithDynamicAttributes) o).attributes);
	}

	public override int GetHashCode() {
		return attributes.GetHashCode();
	}

	public override String ToString() {
		StringBuilder sb = new StringBuilder();

		sb.Append(describeType());
		sb.Append(describeAttributes());

		return sb.ToString();
	}
}
}