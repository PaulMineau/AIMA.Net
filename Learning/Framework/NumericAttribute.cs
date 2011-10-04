namespace CosmicFlow.AIMA.Core.Learning.Framework
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class NumericAttribute : LearningAttribute
    {
        double value;

        private NumericAttributeSpecification spec;

        public NumericAttribute(double rawvalue, NumericAttributeSpecification spec)
        {
            this.value = rawvalue;
            this.spec = spec;
        }

        public String valueAsString()
        {
            return value.ToString();
        }

        public String name()
        {
            return spec.getAttributeName().Trim();
        }

        public double valueAsDouble()
        {
            return value;
        }
    }
}