namespace AIMA.Core.Agent.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Text;
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
		return getClass().getSimpleName();
	}

	public String describeAttributes() {
		StringBuilder sb = new StringBuilder();

		sb.append("[");
		bool first = true;
		foreach (Object key in attributes.keySet()) {
			if (first) {
				first = false;
			} else {
				sb.append(", ");
			}

			sb.append(key);
			sb.append("==");
			sb.append(attributes.get(key));
		}
		sb.append("]");

		return sb.ToString();
	}

	public HashSet<Object> getKeySet() {
		return Collections.unmodifiableSet(attributes.keySet());
	}

	public void setAttribute(Object key, Object value) {
		attributes.put(key, value);
	}

	public Object getAttribute(Object key) {
		return attributes.get(key);
	}

	public void removeAttribute(Object key) {
		attributes.remove(key);
	}

	public ObjectWithDynamicAttributes copy() {
		ObjectWithDynamicAttributes copy = null;

		try {
			copy = getClass().newInstance();
			copy.attributes.putAll(attributes);
		} catch (Exception ex) {
			ex.printStackTrace();
		}

		return copy;
	}

	public override bool Equals(Object o) {
		if (o == null || getClass() != o.getClass()) {
			return super.Equals(o);
		}
		return attributes.Equals(((ObjectWithDynamicAttributes) o).attributes);
	}

	public override int HashCode() {
		return attributes.HashCode();
	}

	public override String ToString() {
		StringBuilder sb = new StringBuilder();

		sb.append(describeType());
		sb.append(describeAttributes());

		return sb.ToString();
	}
}
}