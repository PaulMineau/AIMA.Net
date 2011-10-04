namespace AIMA.Core.Search.Uninformed
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Search.Framework;

    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): Figure 3.17, page 88.
     * 
     * <code>
     * function DEPTH-LIMITED-SEARCH(problem, limit) returns a solution, or failure/cutoff
     *   return RECURSIVE-DLS(MAKE-NODE(problem.INITIAL-STATE), problem, limit)
     *   
     * function RECURSIVE-DLS(node, problem, limit) returns a solution, or failure/cutoff
     *   if problem.GOAL-TEST(node.STATE) then return SOLUTION(node)
     *   else if limit = 0 then return cutoff
     *   else
     *       cutoff_occurred? <- false
     *       for each action in problem.ACTIONS(node.STATE) do
     *           child <- CHILD-NODE(problem, node, action)
     *           result <- RECURSIVE-DLS(child, problem, limit - 1)
     *           if result = cutoff then cutoff_occurred? <- true
     *           else if result != failure then return result
     *       if cutoff_occurred? then return cutoff else return failure
     * </code>
     * Figure 3.17 A recursive implementation of depth-limited search.
     */

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public class DepthLimitedSearch : NodeExpander, Search
    {
        private static String PATH_COST = "pathCost";
        private static List<Action> cutoffResult = null;
        private int limit;

        public DepthLimitedSearch(int limit)
        {
            this.limit = limit;
        }

        public bool isCutOff(List<Action> result)
        {
            return 1 == result.Count
                    && CutOffIndicatorAction.CUT_OFF.Equals(result.get(0));
        }

        public bool isFailure(List<Action> result)
        {
            return 0 == result.Count;
        }

        // function DEPTH-LIMITED-SEARCH(problem, limit) returns a solution, or
        // failure/cutoff
        /**
         * @param p
         * @return if goal found, the list of actions to the Goal. If already at the
         *         goal you will receive a List with a single NoOp Action in it. If
         *         fail to find the Goal, an empty list will be returned to indicate
         *         that the search failed. If the search was cutoff (i.e. reached
         *         its limit without finding a goal) a List with one
         *         CutOffIndicatorAction.CUT_OFF in it will be returned (Note: this
         *         is a NoOp action).
         */
        public List<Action> search(Problem p)
        {
            clearInstrumentation();
            // return RECURSIVE-DLS(MAKE-NODE(INITIAL-STATE[problem]), problem,
            // limit)
            return recursiveDLS(new Node(p.getInitialState()), p, limit);
        }

        public override void clearInstrumentation()
        {
            base.clearInstrumentation();
            metrics.set(PATH_COST, 0);
        }

        public double getPathCost()
        {
            return metrics.getDouble(PATH_COST);
        }

        public void setPathCost(Double pathCost)
        {
            metrics.set(PATH_COST, pathCost);
        }

        //
        // PRIVATE METHODS
        //

        // function RECURSIVE-DLS(node, problem, limit) returns a solution, or
        // failure/cutoff
        private List<Action> recursiveDLS(Node node, Problem problem, int limit)
        {
            // if problem.GOAL-TEST(node.STATE) then return SOLUTION(node)
            if (SearchUtils.isGoalState(problem, node))
            {
                setPathCost(node.getPathCost());
                return SearchUtils.actionsFromNodes(node.getPathFromRoot());
            }
            else if (0 == limit)
            {
                // else if limit = 0 then return cutoff
                return cutoff();
            }
            else
            {
                // else
                // cutoff_occurred? <- false
                bool cutoff_occurred = false;
                // for each action in problem.ACTIONS(node.STATE) do
                foreach (Node child in this.expandNode(node, problem))
                {
                    // child <- CHILD-NODE(problem, node, action)
                    // result <- RECURSIVE-DLS(child, problem, limit - 1)
                    List<Action> result = recursiveDLS(child, problem, limit - 1);
                    // if result = cutoff then cutoff_occurred? <- true
                    if (isCutOff(result))
                    {
                        cutoff_occurred = true;
                    }
                    else if (!isFailure(result))
                    {
                        // else if result != failure then return result
                        return result;
                    }
                }

                // if cutoff_occurred? then return cutoff else return failure
                if (cutoff_occurred)
                {
                    return cutoff();
                }
                else
                {
                    return failure();
                }
            }
        }

        private List<Action> cutoff()
        {
            // Only want to created once
            if (null == cutoffResult)
            {
                cutoffResult = new List<Action>();
                cutoffResult.Add(CutOffIndicatorAction.CUT_OFF);
                // Ensure it cannot be modified externally.
                cutoffResult = Collections.unmodifiableList(cutoffResult);
            }
            return cutoffResult;
        }

        private List<Action> failure()
        {
            return Collections.emptyList();
        }
    }
}