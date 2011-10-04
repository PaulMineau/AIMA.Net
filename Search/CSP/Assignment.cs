namespace AIMA.Core.Search.CSP
{
    using System;
    using System.Collections.Generic;
    /**
     * An assignment assigns values to some or all variables of a CSP.
     * 
     * @author Ruediger Lunde
     */
    public class Assignment
    {
        /**
         * Contains all assigned variables. Positions reflect the the order in which
         * the variables were assigned to values.
         */
        List<Variable> variables;
        /** Maps variables to their assigned values. */
        Dictionary<Variable, Object> variableToValue;

        public Assignment()
        {
            variables = new List<Variable>();
            variableToValue = new Dictionary<Variable, Object>();
        }

        public List<Variable> getVariables()
        {
            return Collections.unmodifiableList(variables);
        }

        public Object getAssignment(Variable var)
        {
            return variableToValue.get(var);
        }

        public void setAssignment(Variable var, Object value)
        {
            if (!variableToValue.containsKey(var))
                variables.Add(var);
            variableToValue.put(var, value);
        }

        public void removeAssignment(Variable var)
        {
            if (hasAssignmentFor(var))
            {
                variables.remove(var);
                variableToValue.remove(var);
            }
        }

        public bool hasAssignmentFor(Variable var)
        {
            return variableToValue.get(var) != null;
        }

        /**
         * Returns true if this assignment does not violate any constraints of
         * <code>constraints</code>.
         */
        public bool isConsistent(List<Constraint> constraints)
        {
            foreach (Constraint cons in constraints)
                if (!cons.isSatisfiedWith(this))
                    return false;
            return true;
        }

        /**
         * Returns true if this assignment assigns values to every variable of
         * <code>vars</code>.
         */
        public bool isComplete(List<Variable> vars)
        {
            foreach (Variable var in vars)
            {
                if (!hasAssignmentFor(var))
                    return false;
            }
            return true;
        }

        /**
         * Returns true if this assignment assigns values to every variable of
         * <code>vars</code>.
         */
        public bool isComplete(Variable[] vars)
        {
            foreach (Variable var in vars)
            {
                if (!hasAssignmentFor(var))
                    return false;
            }
            return true;
        }

        /**
         * Returns true if this assignment is consistent as well as complete with
         * respect to the given CSP.
         */
        public bool isSolution(CSP csp)
        {
            return isConsistent(csp.getConstraints())
                    && isComplete(csp.getVariables());
        }

        public Assignment copy()
        {
            Assignment copy = new Assignment();
            foreach (Variable var in variables)
            {
                copy.setAssignment(var, variableToValue.get(var));
            }
            return copy;
        }

        public override String ToString()
        {
            bool comma = false;
            StringBuffer result = new StringBuffer("{");
            foreach (Variable var in variables)
            {
                if (comma)
                    result.append(", ");
                result.append(var + "=" + variableToValue.get(var));
                comma = true;
            }
            result.append("}");
            return result.ToString();
        }
    }
}