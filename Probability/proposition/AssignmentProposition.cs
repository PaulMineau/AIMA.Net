using System;
using System.Text;

namespace AIMA.Probability
{

    public class AssignmentProposition : AbstractTermProposition
    {
        private Object value = null;
        //
        private String toString = null;

        public AssignmentProposition(RandomVariable forVariable, Object value)
            : base(forVariable)
        {

            setValue(value);
        }

        public Object getValue()
        {
            return value;
        }

        public void setValue(Object value)
        {
            if (null == value)
            {
                throw new ArgumentException(
                    "The value for the Random Variable must be specified.");
            }
            this.value = value;
        }

        public override bool holds(Map<RandomVariable, Object> possibleWorld)
        {
            return value.Equals(possibleWorld[getTermVariable()]);
        }


        public override String ToString()
        {
            if (null == toString)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(getTermVariable().getName());
                sb.Append(" = ");
                sb.Append(value);

                toString = sb.ToString();
            }
            return toString;
        }
    }
}