namespace AIMA.Core.Search.Informed
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Search.Framework;

    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 92.
     * 
     * Best-first search is an instance of the general TREE-SEARCH or GRAPH-SEARCH algorithm 
     * in which a node is selected for expansion based on an evaluation function, f(n). The 
     * evaluation function is construed as a cost estimate, so the node with the lowest evaluation 
     * is expanded first. The implementation of best-first graph search is identical to that for 
     * uniform-cost search (Figure 3.14), except for the use of f instead of g to order the 
     * priority queue.
     */

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class BestFirstSearch : PrioritySearch
    {

        private EvaluationFunction evaluationFunction;

        public BestFirstSearch(QueueSearch search, EvaluationFunction ef)
        {
            this.search = search;
            evaluationFunction = ef;
        }

        //
        // PROTECTED METHODS
        //
        protected Comparator<Node> getComparator()
        {
            Comparator<Node> f = new Comparator<Node>()
            {
                //public int compare(Node n1, Node n2) {
                //    Double f1 = evaluationFunction.f(n1);
                //    Double f2 = evaluationFunction.f(n2);

                //    return f1.compareTo(f2);
                //}
            };

            if (this.search is GraphSearch)
            {
                ((GraphSearch)this.search)
                        .setReplaceFrontierNodeAtStateCostFunction(f);
            }

            return f;
        }
    }
}
