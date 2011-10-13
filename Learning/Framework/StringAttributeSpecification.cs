namespace AIMA.Core.Learning.Framework
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class StringAttributeSpecification : AttributeSpecification
    {
        String attributeName;

        List<String> attributePossibleValues;

        public StringAttributeSpecification(String attributeName,
                List<String> attributePossibleValues)
        {
            this.attributeName = attributeName;
            this.attributePossibleValues = attributePossibleValues;
        }

        public StringAttributeSpecification(String attributeName,
                String[] attributePossibleValues) : this(attributeName, new List<String>(attributePossibleValues))
        {
            
        }

        public bool isValid(String value)
        {
            return (attributePossibleValues.Contains(value));
        }

        /**
         * @return Returns the attributeName.
         */
        public String getAttributeName()
        {
            return attributeName;
        }

        public List<String> possibleAttributeValues()
        {
            return attributePossibleValues;
        }

        public LearningAttribute createAttribute(String rawValue)
        {
            return new StringAttribute(rawValue, this);
        }
    }
}
