using System;

namespace AIMA.Probability.Domain
{



    public class FiniteIntegerDomain : AbstractFiniteDomain
    {

        private Set<int> possibleValues = null;

        public FiniteIntegerDomain(params int[] pValues)
        {
            // Keep consistent order
            possibleValues = new Set<int>();
            foreach (int v in pValues)
            {
                possibleValues.add(v);
            }

            indexPossibleValues(possibleValues);
        }

        //
        // START-Domain

        public override int size()
        {
            return possibleValues.size();
        }

        public override bool isOrdered()
        {
            return true;
        }

        // END-Domain
        //

        //
        // START-DiscreteDomain

        public override Set<object> getPossibleValues()
        {
            Set<object> set = new Set<object>();
            foreach(int i in possibleValues)
            {
                set.add(i);
            }
            return set;
        }

        // END-DiscreteDomain
        //


        public bool equals(Object o)
        {

            if (this == o)
            {
                return true;
            }
            if (!(o
            is FiniteIntegerDomain))
            {
                return false;
            }

            FiniteIntegerDomain other = (FiniteIntegerDomain) o;

            return this.possibleValues.Equals(other.possibleValues);
        }


        public override int GetHashCode()
        {
            return possibleValues.GetHashCode();
        }
    }
}