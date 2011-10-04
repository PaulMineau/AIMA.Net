namespace AIMA.Core.Search.CSP
{
    using System;
    using System.Collections.Generic;
    /**
     * Artificial Intelligence A Modern Approach (3rd Ed.): Figure 6.1, Page 204.
     * The principal states and territories of Australia. Coloring this map can be
     * viewed as a constraint satisfaction problem (CSP). The goal is to assign
     * colors to each region so that no neighboring regions have the same color.
     * 
     * @author Ruediger Lunde
     */
    public class MapCSP : CSP
    {
        public const Variable NSW = new Variable("NSW");
        public const Variable NT = new Variable("NT");
        public const Variable Q = new Variable("Q");
        public const Variable SA = new Variable("SA");
        public const Variable T = new Variable("T");
        public const Variable V = new Variable("V");
        public const Variable WA = new Variable("WA");
        public const String RED = "RED";
        public const String GREEN = "GREEN";
        public const String BLUE = "BLUE";

        private static List<Variable> collectVariables()
        {
            List<Variable> variables = new List<Variable>();
            variables.Add(NSW);
            variables.Add(WA);
            variables.Add(NT);
            variables.Add(Q);
            variables.Add(SA);
            variables.Add(V);
            variables.Add(T);
            return variables;
        }

        public MapCSP()
        {
            super(collectVariables());

            Domain colors = new Domain(new Object[] { RED, GREEN, BLUE });

            foreach (Variable var in getVariables())
                setDomain(var, colors);

            addConstraint(new NotEqualConstraint(WA, NT));
            addConstraint(new NotEqualConstraint(WA, SA));
            addConstraint(new NotEqualConstraint(NT, SA));
            addConstraint(new NotEqualConstraint(NT, Q));
            addConstraint(new NotEqualConstraint(SA, Q));
            addConstraint(new NotEqualConstraint(SA, NSW));
            addConstraint(new NotEqualConstraint(SA, V));
            addConstraint(new NotEqualConstraint(Q, NSW));
            addConstraint(new NotEqualConstraint(NSW, V));
        }
    }
}