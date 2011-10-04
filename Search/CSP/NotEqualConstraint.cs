namespace AIMA.Core.Search.CSP
{
    using System;
    using System.Collections.Generic;
    /**
     * Represents a binary constraint which forbids equal values.
     * 
     * @author Ruediger Lunde
     */
    public class NotEqualConstraint : Constraint
    {

        private Variable var1;
        private Variable var2;
        private List<Variable> scope;

        public NotEqualConstraint(Variable var1, Variable var2)
        {
            this.var1 = var1;
            this.var2 = var2;
            scope = new List<Variable>(2);
            scope.Add(var1);
            scope.Add(var2);
        }

        public override List<Variable> getScope()
        {
            return scope;
        }

        public override bool isSatisfiedWith(Assignment assignment)
        {
            Object value1 = assignment.getAssignment(var1);
            return value1 == null || !value1.Equals(assignment.getAssignment(var2));
        }
    }
}