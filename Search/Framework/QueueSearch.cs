namespace AIMA.Core.Search.Framework
{
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Util;
    using AIMA.Core.Util.DataStructure;
    using System.Threading;

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public abstract class QueueSearch : NodeExpander
    {
        public const System.String METRIC_QUEUE_SIZE = "queueSize";

        public const System.String METRIC_MAX_QUEUE_SIZE = "maxQueueSize";

        public const System.String METRIC_PATH_COST = "pathCost";

        //
        //
        private Queue<Node> frontier = null;
        private bool checkGoalBeforeAddingToFrontier = false;

        public bool isFailure(List<Action> result)
        {
            return 0 == result.Count;
        }

        /**
         * 
         * @param problem
         * @param frontier
         * @return if goal found, the list of actions to the Goal. If already at the
         *         goal you will receive a List with a single NoOp Action in it. If
         *         fail to find the Goal, an empty list will be returned to indicate
         *         that the search failed.
         */
        public List<Action> search(Problem problem, Queue<Node> frontier)
        {
            this.frontier = frontier;

            clearInstrumentation();
            // initialize the frontier using the initial state of the problem
            Node root = new Node(problem.getInitialState());
            if (isCheckGoalBeforeAddingToFrontier())
            {
                if (SearchUtils.isGoalState(problem, root))
                {
                    return SearchUtils.actionsFromNodes(root.getPathFromRoot());
                }
            }
            frontier.Enqueue(root);
            setQueueSize(frontier.Count);
            while (!(frontier.Count==0))
            {
                // choose a leaf node and remove it from the frontier
                Node nodeToExpand = popNodeFromFrontier();
                setQueueSize(frontier.Count);
                // Only need to check the nodeToExpand if have not already
                // checked before adding to the frontier
                if (!isCheckGoalBeforeAddingToFrontier())
                {
                    // if the node contains a goal state then return the
                    // corresponding solution
                    if (SearchUtils.isGoalState(problem, nodeToExpand))
                    {
                        setPathCost(nodeToExpand.getPathCost());
                        return SearchUtils.actionsFromNodes(nodeToExpand
                                .getPathFromRoot());
                    }
                }
                // expand the chosen node, adding the resulting nodes to the
                // frontier
                foreach (Node fn in getResultingNodesToAddToFrontier(nodeToExpand,
                        problem))
                {
                    if (isCheckGoalBeforeAddingToFrontier())
                    {
                        if (SearchUtils.isGoalState(problem, fn))
                        {
                            setPathCost(fn.getPathCost());
                            return SearchUtils.actionsFromNodes(fn
                                    .getPathFromRoot());
                        }
                    }
                    frontier.Enqueue(fn);
                }
                setQueueSize(frontier.Count);
            }
            // if the frontier is empty then return failure
            return failure();
        }

        public bool isCheckGoalBeforeAddingToFrontier()
        {
            return checkGoalBeforeAddingToFrontier;
        }

        public void setCheckGoalBeforeAddingToFrontier(
                bool checkGoalBeforeAddingToFrontier)
        {
            this.checkGoalBeforeAddingToFrontier = checkGoalBeforeAddingToFrontier;
        }

        public Node popNodeFromFrontier()
        {
            return frontier.Dequeue();
        }

        public bool removeNodeFromFrontier(Node toRemove)
        {
            return false; // TODO
        }

        public abstract List<Node> getResultingNodesToAddToFrontier(
                Node nodeToExpand, Problem p);

        public  void clearInstrumentation()
        {
            base.clearInstrumentation();
            metrics.set(METRIC_QUEUE_SIZE, 0);
            metrics.set(METRIC_MAX_QUEUE_SIZE, 0);
            metrics.set(METRIC_PATH_COST, 0);
        }

        public int getQueueSize()
        {
            return metrics.getInt("queueSize");
        }

        public void setQueueSize(int queueSize)
        {

            metrics.set(METRIC_QUEUE_SIZE, queueSize);
            int maxQSize = metrics.getInt(METRIC_MAX_QUEUE_SIZE);
            if (queueSize > maxQSize)
            {
                metrics.set(METRIC_MAX_QUEUE_SIZE, queueSize);
            }
        }

        public int getMaxQueueSize()
        {
            return metrics.getInt(METRIC_MAX_QUEUE_SIZE);
        }

        public double getPathCost()
        {
            return metrics.getDouble(METRIC_PATH_COST);
        }

        public void setPathCost(double pathCost)
        {
            metrics.set(METRIC_PATH_COST, pathCost);
        }

        //
        // PRIVATE METHODS
        //
        private List<Action> failure()
        {
            return new List<Action>();
        }
    }
}