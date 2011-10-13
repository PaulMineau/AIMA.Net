namespace AIMA.Core.Learning.Framework
{
    using System;
    using System.Collections.Generic;
/**
 * @author Ravi Mohan
 * 
 */
public class Example {
    Dictionary<String, LearningAttribute> attributes;

    private LearningAttribute targetAttribute;

    public Example(Dictionary<String, LearningAttribute> attributes,
            LearningAttribute targetAttribute)
    {
		this.attributes = attributes;
		this.targetAttribute = targetAttribute;
	}

	public String getAttributeValueAsString(String attributeName) {
		return attributes[attributeName].valueAsString();
	}

	public double getAttributeValueAsDouble(String attributeName) {
        LearningAttribute attribute = attributes[attributeName];
		if (attribute == null || !(attribute is NumericAttribute)) {
			throw new ApplicationException    (
					"cannot return numerical value for non numeric attribute");
		}
		return ((NumericAttribute) attribute).valueAsDouble();
	}

	public override String ToString() {
		return attributes.ToString();
	}

	public String targetValue() {
		return getAttributeValueAsString(targetAttribute.name());
	}

	public override bool Equals(Object o) {
		if (this == o) {
			return true;
		}
		if ((o == null) || !(o is Example)) {
			return false;
		}
		Example other = (Example) o;
		return attributes.Equals(other.attributes);
	}

	public override int GetHashCode() {
		return attributes.GetHashCode();
	}

	public Example numerize(
			Dictionary<String, Dictionary<String, int>> attrValueToNumber) {
                Dictionary<String, LearningAttribute> numerizedExampleData = new Dictionary<String, LearningAttribute>();
		foreach (String key in attributes.Keys) {
            LearningAttribute attribute = attributes[key];
			if (attribute is StringAttribute) {
				int correspondingNumber = attrValueToNumber[key][
						attribute.valueAsString()];
				NumericAttributeSpecification spec = new NumericAttributeSpecification(
						key);
				numerizedExampleData.Add(key, new NumericAttribute(
						correspondingNumber, spec));
			} else {// Numeric Attribute
				numerizedExampleData.Add(key, attribute);
			}
		}
		return new Example(numerizedExampleData, numerizedExampleData[targetAttribute.name()]);
	}
}
}