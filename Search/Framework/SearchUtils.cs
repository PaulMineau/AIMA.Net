namespace AIMA.Core.Search.Framework
{
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Agent.Impl;

    /**
     * @author Ravi Mohan
     * 
     */
    public class SearchUtils
    {

        public static List<Action> actionsFromNodes(List<Node> nodeList)
        {
            List<Action> actions = new List<Action>();
            if (nodeList.Count == 1)
            {
                // I'm at the root node, this indicates I started at the
                // Goal node, therefore just return a NoOp
                actions.Add(NoOpAction.NO_OP);
            }
            else
            {
                // ignore the root node this has no action
                // hence index starts from 1 not zero
                for (int i = 1; i < nodeList.Count; i++)
                {
                    Node node = nodeList[i];
                    actions.Add(node.getAction());
                }
            }
            return actions;
        }

        public static bool isGoalState(Problem p, Node n)
        {
            bool isGoal = false;
            GoalTest gt = p.getGoalTest();
            if (gt.isGoalState(n.getState()))
            {
                if (gt is SolutionChecker)
                {
                    isGoal = ((SolutionChecker)gt).isAcceptableSolution(
                            SearchUtils.actionsFromNodes(n.getPathFromRoot()), n
                                    .getState());
                }
                else
                {
                    isGoal = true;
                }
            }
            return isGoal;
        }
    }
}