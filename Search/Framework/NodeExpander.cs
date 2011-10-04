namespace AIMA.Core.Search.Framework
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;

    /**
     * @author Ravi Mohan
     * 
     */
    public class NodeExpander
    {
        public const String METRIC_NODES_EXPANDED = "nodesExpanded";

        protected Metrics metrics;

        public NodeExpander()
        {
            metrics = new Metrics();
        }

        public void clearInstrumentation()
        {
            metrics.set(METRIC_NODES_EXPANDED, 0);
        }

        public int getNodesExpanded()
        {
            return metrics.getInt(METRIC_NODES_EXPANDED);
        }

        public Metrics getMetrics()
        {
            return metrics;
        }

        public List<Node> expandNode(Node node, Problem problem)
        {
            List<Node> childNodes = new List<Node>();

            ActionsFunction actionsFunction = problem.getActionsFunction();
            ResultFunction resultFunction = problem.getResultFunction();
            StepCostFunction stepCostFunction = problem.getStepCostFunction();

            foreach (Action action in actionsFunction.actions(node.getState()))
            {
                Object successorState = resultFunction.result(node.getState(),
                        action);

                double stepCost = stepCostFunction.c(node.getState(), action,
                        successorState);
                childNodes.Add(new Node(successorState, node, action, stepCost));
            }
            metrics.set(METRIC_NODES_EXPANDED, metrics
                    .getInt(METRIC_NODES_EXPANDED) + 1);

            return childNodes;
        }
    }
}