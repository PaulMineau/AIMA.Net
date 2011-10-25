using System;
using System.Collections.Generic;

namespace AIMA.Probability
{


    public abstract class AbstractProposition : IProposition
    {

        private Set<RandomVariable> scope = new Set<RandomVariable>();
        private Set<RandomVariable> unboundScope = new Set<RandomVariable>();

        public AbstractProposition()
        {

        }

        //
        // START-Proposition
        public Set<RandomVariable> getScope()
        {
            return scope;
        }

        public Set<RandomVariable> getUnboundScope()
        {
            return unboundScope;
        }

        public abstract bool holds(Map<RandomVariable, Object> possibleWorld);

        // END-Proposition
        //

        //
        // Protected Methods
        //
        protected void addScope(RandomVariable var)
        {
            scope.Add(var);
        }

        protected void addScope(List<RandomVariable> vars)
        {
            scope.addAll(vars);
        }

        protected void addUnboundScope(RandomVariable var)
        {
            unboundScope.add(var);
        }

        protected void addUnboundScope(List<RandomVariable> vars)
        {
            unboundScope.addAll(vars);
        }
    }
}
