using System;

namespace AIMA.Probability.Domain
{


/**
 * Artificial Intelligence A Modern Approach (3rd Edition): page 486.
 * 
 * A Boolean random variable has the domain {true,false}.
 * 
 * @author Ciaran O'Reilly.
 */

    public class BooleanDomain : AbstractFiniteDomain
    {

        private static Set<Boolean> _possibleValues = null;

        static BooleanDomain()
        {
            // Keep consistent order
            _possibleValues = new Set<Boolean>();
            _possibleValues.add(true);
            _possibleValues.add(false);
            // Ensure cannot be modified
            //_possibleValues = Collections.unmodifiableSet(_possibleValues);
        }

        public BooleanDomain()
        {
            indexPossibleValues(_possibleValues);
        }

        //
        // START-Domain

        public override int size()
        {
            return 2;
        }

         

        public override bool isOrdered()
        {
            return false;
        }

        // END-Domain
        //

        //
        // START-DiscreteDomain
         

        public override Set<object> getPossibleValues()
        {
            Set<object> results = new Set<object>();
            foreach(bool b in _possibleValues)
            {
                results.add(b);

            }
            return results;
        }

        // END-DiscreteDomain
        //

  

        public override bool Equals(Object o)
        {
            return o
            is BooleanDomain;
        }

         

        public override int GetHashCode()
        {
            return _possibleValues.GetHashCode();
        }
    }
}