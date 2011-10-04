namespace AIMA.Core.Search.Uninformed
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Search.Framework;
    using AIMA.Core.Util.DataStructure;

    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): Figure 3.11, page 82.
     * <code>
     * function BREADTH-FIRST-SEARCH(problem) returns a solution, or failure
     *   node <- a node with STATE = problem.INITIAL-STATE, PATH-COST=0
     *   if problem.GOAL-TEST(node.STATE) then return SOLUTION(node)
     *   frontier <- a FIFO queue with node as the only element
     *   explored <- an empty set
     *   loop do
     *      if EMPTY?(frontier) then return failure
     *      node <- POP(frontier) // chooses the shallowest node in frontier
     *      add node.STATE to explored
     *      for each action in problem.ACTIONS(node.STATE) do
     *          child <- CHILD-NODE(problem, node, action)
     *          if child.STATE is not in explored or frontier then
     *              if problem.GOAL-TEST(child.STATE) then return SOLUTION(child)
     *              frontier <- INSERT(child, frontier)
     * </code> 
     * Figure 3.11 Breadth-first search on a graph.
     */

    /**
     * Note: Supports both Tree and Graph based versions by assigning an instance
     * of TreeSearch or GraphSearch to its constructor.
     */

    /**
     * @author Ciaran O'Reilly
     */
    public class BreadthFirstSearch : Search
    {

        private QueueSearch search;

        public BreadthFirstSearch()
        {
            this(new GraphSearch());
        }

        public BreadthFirstSearch(QueueSearch search)
        {
            // Goal test is to be applied to each node when it is generated
            // rather than when it is selected for expansion.
            search.setCheckGoalBeforeAddingToFrontier(true);
            this.search = search;
        }

        public List<Action> search(Problem p)
        {
            return search.search(p, new FIFOQueue<Node>());
        }

        public Metrics getMetrics()
        {
            return search.getMetrics();
        }
    }
}