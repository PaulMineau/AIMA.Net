namespace aima.test.core.unit.search.framework
{

    using AIMA.Core.Agent;
    using AIMA.Core.Environment;
    using AIMA.Core.Search.Framework;
    using AIMA.Core.Search.Uninformed;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using AIMA;
    using AIMA.Core.Environment.Map;

    [TestClass]
    public class SolutionCheckerTest
    {

        [TestMethod]
        public void testMultiGoalProblem()
        {
            Map romaniaMap = new SimplifiedRoadMapOfPartOfRomania();
            Problem problem = new Problem(SimplifiedRoadMapOfPartOfRomania.ARAD,
                    MapFunctionFactory.getActionsFunction(romaniaMap),
                    MapFunctionFactory.getResultFunction(), new DualMapGoalTest(
                            SimplifiedRoadMapOfPartOfRomania.BUCHAREST,
                            SimplifiedRoadMapOfPartOfRomania.HIRSOVA),
                    new MapStepCostFunction(romaniaMap));

            Search search = new BreadthFirstSearch(new GraphSearch());

            SearchAgent agent = new SearchAgent(problem, search);
            Assert
                    .Equals(
                            "[Action[name==moveTo, location==Sibiu], Action[name==moveTo, location==Fagaras], Action[name==moveTo, location==Bucharest], Action[name==moveTo, location==Urziceni], Action[name==moveTo, location==Hirsova]]",
                            agent.getActions().ToString());
            Assert.Equals(5, agent.getActions().Count);
            Assert.Equals("14", agent.getInstrumentation()[
                    "nodesExpanded"]);
            Assert.Equals("1", agent.getInstrumentation()[
                    "queueSize"]);
            Assert.Equals("5", agent.getInstrumentation()[
                    "maxQueueSize"]);
        }

        class DualMapGoalTest : SolutionChecker
        {
            public System.String goalState1 = null;
            public System.String goalState2 = null;

            private Set<System.String> goals = new Set<System.String>();

            public DualMapGoalTest(System.String goalState1, System.String goalState2)
            {
                this.goalState1 = goalState1;
                this.goalState2 = goalState2;
                goals.Add(goalState1);
                goals.Add(goalState2);
            }

            public bool isGoalState(System.Object state)
            {
                return goalState1.Equals(state) || goalState2.Equals(state);
            }

            public bool isAcceptableSolution(List<Action> actions, System.Object goal)
            {
                goals.Remove(goal.ToString());
                return goals.Count==0;
            }
        }
    }
}