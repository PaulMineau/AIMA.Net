namespace AIMA.Core.Search.CSP
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Util.DataStructure;

    public class ImprovedBacktrackingStrategy : BacktrackingStrategy
    {
        protected Selection selectionStrategy = Selection.DEFAULT_ORDER;
        protected Inference inferenceStrategy = Inference.NONE;
        protected bool isLCVHeuristicEnabled;

        /** Creates a strategy which is by default equivalent to plain backtracking. */
        public ImprovedBacktrackingStrategy()
        {
        }

        /** Creates a backtracking strategy with the specified features. */
        public ImprovedBacktrackingStrategy(bool enableMRV, bool enableDeg,
                bool enableAC3, bool enableLCV)
        {
            if (enableMRV)
                setVariableSelection(enableDeg ? Selection.MRV_DEG : Selection.MRV);
            if (enableAC3)
                setInference(Inference.AC3);
            enableLCV(enableLCV);
        }


        /** Selects the algorithm for SELECT-UNASSIGNED-VARIABLE */
        public void setVariableSelection(Selection sStrategy)
        {
            selectionStrategy = sStrategy;
        }

        /** Selects the algorithm for INFERENCE. */
        public void setInference(Inference iStrategy)
        {
            inferenceStrategy = iStrategy;
        }

        /**
         * Selects the least constraining value heuristic as implementation for
         * ORDER-DOMAIN-VALUES.
         */
        public void enableLCV(bool state)
        {
            isLCVHeuristicEnabled = state;
        }

        /**
         * Starts with a constraint propagation if AC-3 is enabled and then calls
         * the super class implementation.
         */
        public Assignment solve(CSP csp)
        {
            if (inferenceStrategy == Inference.AC3)
            {
                DomainRestoreInfo info = new AC3Strategy().reduceDomains(csp);
                if (!info.isEmpty())
                {
                    fireStateChanged(csp);
                    if (info.isEmptyDomainFound())
                        return null;
                }
            }
            return super.solve(csp);
        }

        /**
         * Primitive operation, selecting a not yet assigned variable.
         */
        protected override Variable selectUnassignedVariable(Assignment assignment, CSP csp)
        {
            switch (selectionStrategy)
            {
                case MRV:
                    return applyMRVHeuristic(csp, assignment).get(0);
                case MRV_DEG:
                    List<Variable> vars = applyMRVHeuristic(csp, assignment);
                    return applyDegreeHeuristic(vars, assignment, csp).get(0);
                default:
                    foreach (Variable var in csp.getVariables())
                    {
                        if (!(assignment.hasAssignmentFor(var)))
                            return var;
                    }
            }
            return null;
        }

        /**
         * Primitive operation, ordering the domain values of the specified
         * variable.
         */
        protected override Iterable? orderDomainValues(Variable var,
                Assignment assignment, CSP csp)
        {
            if (!isLCVHeuristicEnabled)
            {
                return csp.getDomain(var);
            }
            else
            {
                return applyLeastConstrainingValueHeuristic(var, csp);
            }
        }

        /**
         * Primitive operation, which tries to prune out values from the CSP which
         * are not possible anymore when extending the given assignment to a
         * solution.
         * 
         * @return An object which provides informations about (1) whether changes
         *         have been performed, (2) possibly inferred empty domains , and
         *         (3) how to restore the domains.
         */
        protected override DomainRestoreInfo inference(Variable var, Assignment assignment,
                CSP csp)
        {
            switch (inferenceStrategy)
            {
                case FORWARD_CHECKING:
                    return doForwardChecking(var, assignment, csp);
                case AC3:
                    return new AC3Strategy().reduceDomains(var, assignment
                            .getAssignment(var), csp);
                default:
                    return new DomainRestoreInfo().compactify();
            }
        }

        // //////////////////////////////////////////////////////////////
        // heuristics for selecting the next unassigned variable and domain ordering

        /** : the minimum-remaining-values heuristic. */
        private List<Variable> applyMRVHeuristic(CSP csp, Assignment assignment)
        {
            List<Variable> result = new List<Variable>();
            int mrv = int.MAX_VALUE;
            foreach (Variable var in csp.getVariables())
            {
                if (!assignment.hasAssignmentFor(var))
                {
                    int num = csp.getDomain(var).Count;
                    if (num <= mrv)
                    {
                        if (num < mrv)
                        {
                            result.clear();
                            mrv = num;
                        }
                        result.Add(var);
                    }
                }
            }
            return result;
        }

        /** : the degree heuristic. */
        private List<Variable> applyDegreeHeuristic(List<Variable> vars,
                Assignment assignment, CSP csp)
        {
            List<Variable> result = new List<Variable>();
            int maxDegree = int.MIN_VALUE;
            foreach (Variable var in vars)
            {
                int degree = 0;
                foreach (Constraint constraint in csp.getConstraints(var))
                {
                    Variable neighbor = csp.getNeighbor(var, constraint);
                    if (!assignment.hasAssignmentFor(neighbor)
                            && csp.getDomain(neighbor).Count > 1)
                        ++degree;
                }
                if (degree >= maxDegree)
                {
                    if (degree > maxDegree)
                    {
                        result.clear();
                        maxDegree = degree;
                    }
                    result.Add(var);
                }
            }
            return result;
        }

        /** : the least constraining value heuristic. */
        private List<Object> applyLeastConstrainingValueHeuristic(Variable var,
                CSP csp)
        {
            List<Pair<Object, int>> pairs = new List<Pair<Object, int>>();
            foreach (Object value in csp.getDomain(var))
            {
                int num = countLostValues(var, value, csp);
                pairs.Add(new Pair<Object, int>(value, num));
            }
            // TODO
            //Collections.sort(pairs, new Comparator<Pair<Object, int>>() {
            //    public int compare(Pair<Object, int> o1,
            //            Pair<Object, int> o2) {
            //        return o1.getSecond() < o2.getSecond() ? -1
            //                : o1.getSecond() > o2.getSecond() ? 1 : 0;
            //    }
            //});
            List<Object> result = new List<Object>();
            foreach (Pair<Object, int> pair in pairs)
                result.Add(pair.getFirst());
            return result;
        }

        private int countLostValues(Variable var, Object value, CSP csp)
        {
            int result = 0;
            Assignment assignment = new Assignment();
            assignment.setAssignment(var, value);
            foreach (Constraint constraint in csp.getConstraints(var))
            {
                Variable neighbor = csp.getNeighbor(var, constraint);
                foreach (Object nValue in csp.getDomain(neighbor))
                {
                    assignment.setAssignment(neighbor, nValue);
                    if (!constraint.isSatisfiedWith(assignment))
                    {
                        ++result;
                    }
                }
            }
            return result;
        }

        // //////////////////////////////////////////////////////////////
        // inference algorithms

        /** : forward checking. */
        private DomainRestoreInfo doForwardChecking(Variable var,
                Assignment assignment, CSP csp)
        {
            DomainRestoreInfo result = new DomainRestoreInfo();
            foreach (Constraint constraint in csp.getConstraints(var))
            {
                List<Variable> scope = constraint.getScope();
                if (scope.Count == 2)
                {
                    foreach (Variable neighbor in constraint.getScope())
                    {
                        if (!assignment.hasAssignmentFor(neighbor))
                        {
                            if (revise(neighbor, constraint, assignment, csp,
                                    result))
                            {
                                if (csp.getDomain(neighbor).isEmpty())
                                {
                                    result.setEmptyDomainFound(true);
                                    return result;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private bool revise(Variable var, Constraint constraint,
                Assignment assignment, CSP csp, DomainRestoreInfo info)
        {

            bool revised = false;
            foreach (Object value in csp.getDomain(var))
            {
                assignment.setAssignment(var, value);
                if (!constraint.isSatisfiedWith(assignment))
                {
                    info.storeDomainFor(var, csp.getDomain(var));
                    csp.removeValueFromDomain(var, value);
                    revised = true;
                }
                assignment.removeAssignment(var);
            }
            return revised;
        }

        // //////////////////////////////////////////////////////////////
        // two enumerations

        public enum Selection
        {
            DEFAULT_ORDER, MRV, MRV_DEG
        }

        public enum Inference
        {
            NONE, FORWARD_CHECKING, AC3
        }
    }
}