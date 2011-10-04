namespace aima.test.core.unit.search.framework;

using java.util.HashSet;
using java.util.List;
using java.util.Set;

using org.junit.Assert;
using org.junit.Test;

using AIMA.Core.Agent.Action;
using AIMA.Core.Environment.Map.Map;
using AIMA.Core.Environment.Map.MapFunctionFactory;
using AIMA.Core.Environment.Map.MapStepCostFunction;
using AIMA.Core.Environment.Map.SimplifiedRoadMapOfPartOfRomania;
using AIMA.Core.Search.Framework.GraphSearch;
using AIMA.Core.Search.Framework.Problem;
using AIMA.Core.Search.Framework.Search;
using AIMA.Core.Search.Framework.SearchAgent;
using AIMA.Core.Search.Framework.SolutionChecker;
using AIMA.Core.Search.Uninformed.BreadthFirstSearch;

public class SolutionCheckerTest {

	@Test
	public void testMultiGoalProblem() throws Exception {
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
				.assertEquals(
						"[Action[name==moveTo, location==Sibiu], Action[name==moveTo, location==Fagaras], Action[name==moveTo, location==Bucharest], Action[name==moveTo, location==Urziceni], Action[name==moveTo, location==Hirsova]]",
						agent.getActions().ToString());
		Assert.assertEquals(5, agent.getActions().size());
		Assert.assertEquals("14", agent.getInstrumentation().getProperty(
				"nodesExpanded"));
		Assert.assertEquals("1", agent.getInstrumentation().getProperty(
				"queueSize"));
		Assert.assertEquals("5", agent.getInstrumentation().getProperty(
				"maxQueueSize"));
	}

	class DualMapGoalTest : SolutionChecker {
		public String goalState1 = null;
		public String goalState2 = null;

		private Set<String> goals = new HashSet<String>();

		public DualMapGoalTest(String goalState1, String goalState2) {
			this.goalState1 = goalState1;
			this.goalState2 = goalState2;
			goals.add(goalState1);
			goals.add(goalState2);
		}

		public bool isGoalState(Object state) {
			return goalState1.equals(state) || goalState2.equals(state);
		}

		public bool isAcceptableSolution(List<Action> actions, Object goal) {
			goals.remove(goal);
			return goals.isEmpty();
		}
	}
}
