using System;
using AIMA.Probability.Domain;
using AIMA.Probability.Proposition;

namespace AIMA.Probability.Util
{
    /**
     * Default implementation of the RandomVariable interface.
     * 
     * Note: Also implements the TermProposition interface so its easy to use
     * RandomVariables in conjunction with propositions about them in the
     * Probability Model APIs.
     * 
     * @author Ciaran O'Reilly
     */

    public class RandVar : RandomVariable, TermProposition
    {
        private String name = null;
        private IDomain domain = null;
        private Set<RandomVariable> scope = new Set<RandomVariable>();

        public RandVar(String name, IDomain domain)
        {
            ProbUtil.checkValidRandomVariableName(name);
            if (null == domain)
            {
                throw new ArgumentException(
                    "Domain of RandomVariable must be specified.");
            }

            this.name = name;
            this.domain = domain;
            this.scope.add(this);
        }

        //
        // START-RandomVariable

        public String getName()
        {
            return name;
        }

        public IDomain getDomain()
        {
            return domain;
        }

        // END-RandomVariable
        //

        //
        // START-TermProposition
        public RandomVariable getTermVariable()
        {
            return this;
        }

        public Set<RandomVariable> getScope()
        {
            return scope;
        }

        public Set<RandomVariable> getUnboundScope()
        {
            return scope;
        }

        public bool holds(Map<RandomVariable, Object> possibleWorld)
        {
            return possibleWorld.containsKey(getTermVariable());
        }

        // END-TermProposition
        //

        public bool equals(Object o)
        {

            if (this == o)
            {
                return true;
            }
            if (!(o is RandomVariable))
            {
                return false;
            }

            // The name (not the name:domain combination) uniquely identifies a
            // Random Variable
            RandomVariable other = (RandomVariable) o;

            return this.name.Equals(other.getName());
        }

        public int hashCode()
        {
            return name.GetHashCode();
        }

        public String toString()
        {
            return getName();
        }
    }
}
