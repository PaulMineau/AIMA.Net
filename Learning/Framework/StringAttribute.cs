namespace AIMA.Core.Learning.Framework
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class StringAttribute : LearningAttribute
    {
        private StringAttributeSpecification spec;

        private String value;

        public StringAttribute(String value, StringAttributeSpecification spec)
        {
            this.spec = spec;
            this.value = value;
        }

        public String valueAsString()
        {
            return value.Trim();
        }

        public String name()
        {
            return spec.getAttributeName().Trim();
        }
    }
}