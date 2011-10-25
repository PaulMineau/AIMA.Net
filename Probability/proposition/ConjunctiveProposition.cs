
using System;
using System.Text;

namespace AIMA.Probability
{

    public class ConjunctiveProposition : AbstractProposition,
                                          BinarySentenceProposition
    {

        private IProposition left = null;
        private IProposition right = null;
        //
        private String _toString = null;

        public ConjunctiveProposition(IProposition left, IProposition right)
        {
            if (null == left)
            {
                throw new IllegalArgumentException(
                    "Left side of conjunction must be specified.");
            }
            if (null == right)
            {
                throw new IllegalArgumentException(
                    "Right side of conjunction must be specified.");
            }
            // Track nested scope
            addScope(left.getScope());
            addScope(right.getScope());
            addUnboundScope(left.getUnboundScope());
            addUnboundScope(right.getUnboundScope());

            this.left = left;
            this.right = right;
        }


        public bool holds(Map<RandomVariable, Object> possibleWorld)
        {
            return left.holds(possibleWorld) && right.holds(possibleWorld);
        }


        public String toString()
        {
            if (null == _toString)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("(");
                sb.Append(left.ToString());
                sb.Append(" AND ");
                sb.Append(right.ToString());
                sb.Append(")");

                _toString = sb.ToString();
            }

            return _toString;
        }
    }
}