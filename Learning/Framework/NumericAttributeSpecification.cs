namespace AIMA.Core.Learning.Framework
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class NumericAttributeSpecification : AttributeSpecification
    {

        // a simple attribute representing a number reprsented as a double .
        private String name;

        public NumericAttributeSpecification(String name)
        {
            this.name = name;
        }

        public bool isValid(String s)
        {
            try
            {
                Double.Parse(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public String getAttributeName()
        {
            return name;
        }

        public LearningAttribute createAttribute(String rawValue)
        {
            return new NumericAttribute(Double.Parse(rawValue), this);
        }
    }
}