namespace AIMA.Core.Search.CSP
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Util;

/**
 * A domain Di consists of a set of allowable values {v1, ... , vk} for the
 * corresponding variable Xi and defines a default order on those values. This
 * implementation guarantees, that domains are never changed after they have
 * been created. Domain reduction is implemented by replacement instead of
 * modification. So previous states can easily and safely be restored.
 * 
 * @author Ruediger Lunde
 */
public class Domain : Iterable<Object> {
	private const long serialVersionUID = 1L;

	private Object[] values;

	public Domain(List<T> values) {
		this.values = new Object[values.Count];
		for (int i = 0; i < values.Count; i++)
			this.values[i] = values.get(i);
	}

	public Domain(Object[] values) {
		this.values = new Object[values.length];
		for (int i = 0; i < values.length; i++)
			this.values[i] = values[i];
	}

	public int size() {
		return values.length;
	}

	public Object get(int index) {
		return values[index];
	}

	public bool isEmpty() {
		return values.length == 0;
	}

	public bool contains(Object value) {
		foreach (Object v in values)
			if (v.Equals(value))
				return true;
		return false;
	}

	public override Iterator<Object> iterator() {
		// TODO Auto-generated method stub
		return new ArrayIterator<Object>(values);
	}

	/** Not very efficient... */
	public List<Object> asList() {
		List<Object> result = new List<Object>();
		foreach (Object value in values)
			result.Add(value);
		return result;
	}

	public override bool Equals(Object obj) {
		if (obj is Domain) {
			Domain d = (Domain) obj;
			if (d.Count != values.length)
				return false;
			else
				for (int i = 0; i < values.length; i++)
					if (!values[i].Equals(d.values[i]))
						return false;
		}
		return true;
	}

	public override int GetHashCode() {
		int hash = 9; // arbitrary seed value
		int multiplier = 13; // arbitrary multiplier value
		for (int i = 0; i < values.length; i++)
			hash = hash * multiplier + values[i].GetHashCode();
		return hash;
	}

	public override String ToString() {
		StringBuffer result = new StringBuffer("{");
		bool comma = false;
		foreach (Object value in values) {
			if (comma)
				result.append(", ");
			result.append(value.ToString());
			comma = true;
		}
		result.append("}");
		return result.ToString();
	}
}
}