namespace AIMA.Core.Search.Informed
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Search.Framework;

    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 92.
     * 
     * Greedy best-first search tries to expand the node that is closest to the goal,
     * on the grounds that this is likely to lead to a solution quickly. Thus, it evaluates
     * nodes by using just the heuristic function; that is, f(n) = h(n)
     */

    /**
     * @author Ravi Mohan
     * 
     */
    public class GreedyBestFirstSearch : BestFirstSearch
    {

        public GreedyBestFirstSearch(QueueSearch search, HeuristicFunction hf) : base(search, new GreedyBestFirstEvaluationFunction(hf))
        {
            
        }
    }
}