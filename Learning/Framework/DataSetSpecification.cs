namespace CosmicFlow.AIMA.Core.Learning.Framework
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class DataSetSpecification
    {
        List<AttributeSpecification> attributeSpecifications;

        private String targetAttribute;

        public DataSetSpecification()
        {
            this.attributeSpecifications = new List<AttributeSpecification>();
        }

        public bool isValid(List<String> uncheckedAttributes)
        {
            if (attributeSpecifications.Count != uncheckedAttributes.Count)
            {
                throw new ApplicationException("size mismatch specsize = "
                        + attributeSpecifications.Count + " attrbutes size = "
                        + uncheckedAttributes.Count);
            }
            List<AttributeSpecification>.Enumerator attributeSpecIter = attributeSpecifications.GetEnumerator();
            List<String>.Enumerator valueIter = uncheckedAttributes.GetEnumerator();
            while (valueIter.MoveNext() && attributeSpecIter.MoveNext())
            {
                if (!(attributeSpecIter.Current.isValid(valueIter.Current)))
                {
                    return false;
                }
            }
            return true;
        }

        /**
         * @return Returns the targetAttribute.
         */
        public String getTarget()
        {
            return targetAttribute;
        }

        public List<String> getPossibleAttributeValues(String attributeName) {
		foreach (AttributeSpecification attSpec in attributeSpecifications) {
			if (attSpec.getAttributeName().Equals(attributeName)) {
                return ((StringAttributeSpecification)attSpec)
						.possibleAttributeValues();
			}
		}
		throw new ApplicationException("No such attribute" + attributeName);
	}

        public virtual List<String> getAttributeNames() {
		List<String> names = new List<String>();
		foreach (AttributeSpecification attSpec in attributeSpecifications) {
            names.Add(attSpec.getAttributeName());
		}
		return names;
	}

        public void defineStringAttribute(String name, String[] attributeValues)
        {
            attributeSpecifications.Add(new StringAttributeSpecification(name,
                    attributeValues));
            setTarget(name);// target defaults to last column added
        }

        /**
         * @param target
         *            The targetAttribute to set.
         */
        public void setTarget(String target)
        {
            this.targetAttribute = target;
        }

        public AttributeSpecification getAttributeSpecFor(String name) {
		foreach (AttributeSpecification spec in attributeSpecifications) {
			if (spec.getAttributeName().Equals(name)) {
				return spec;
			}
		}
		throw new ApplicationException("no attribute spec for  " + name);
	}

        public void defineNumericAttribute(String name)
        {
            attributeSpecifications.Add(new NumericAttributeSpecification(name));
        }

        public List<String> getNamesOfStringAttributes()
        {
            List<String> names = new List<String>();
            foreach (AttributeSpecification spec in attributeSpecifications)
            {
                if (spec is StringAttributeSpecification)
                {
                    names.Add(spec.getAttributeName());
                }
            }
            return names;
        }
    }
}