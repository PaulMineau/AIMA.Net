namespace AIMA.Test.Core.Unit.Search.Uninformed
{

using AIMA.Core.Agent;
using AIMA.Core.Environment.Map;
using AIMA.Core.Environment.NQueens;
using AIMA.Core.Search.Framework;
using AIMA.Core.Search.Uninformed;

/**
 * @author Ciaran O'Reilly
 * 
 */
[Test]
public class UniformCostSearchTest {

	@Test
	public void testUniformCostSuccesfulSearch() throws Exception {
		Problem problem = new Problem(new NQueensBoard(8),
				NQueensFunctionFactory.getIActionsFunction(),
				NQueensFunctionFactory.getResultFunction(),
				new NQueensGoalTest());
		Search search = new UniformCostSearch();
		SearchAgent agent = new SearchAgent(problem, search);

		List<Action> actions = agent.getActions();

		Assert.assertEquals(8, actions.size());

		Assert.assertEquals("1965", agent.getInstrumentation().getProperty(
				"nodesExpanded"));

		Assert.assertEquals("8.0", agent.getInstrumentation().getProperty(
				"pathCost"));
	}

	@Test
	public void testUniformCostUnSuccesfulSearch() throws Exception {
		Problem problem = new Problem(new NQueensBoard(3),
				NQueensFunctionFactory.getIActionsFunction(),
				NQueensFunctionFactory.getResultFunction(),
				new NQueensGoalTest());
		Search search = new UniformCostSearch();
		SearchAgent agent = new SearchAgent(problem, search);

		List<Action> actions = agent.getActions();

		Assert.assertEquals(0, actions.size());

		Assert.assertEquals("6", agent.getInstrumentation().getProperty(
				"nodesExpanded"));

		// Will be 0 as did not reach goal state.
		Assert.assertEquals("0", agent.getInstrumentation().getProperty(
				"pathCost"));
	}

	@Test
	public void testAIMA3eFigure3_15() throws Exception {
		Map romaniaMap = new SimplifiedRoadMapOfPartOfRomania();
		Problem problem = new Problem(SimplifiedRoadMapOfPartOfRomania.SIBIU,
				MapFunctionFactory.getActionsFunction(romaniaMap),
				MapFunctionFactory.getResultFunction(), new DefaultGoalTest(
						SimplifiedRoadMapOfPartOfRomania.BUCHAREST),
				new MapStepCostFunction(romaniaMap));

		Search search = new UniformCostSearch();
		SearchAgent agent = new SearchAgent(problem, search);

		List<Action> actions = agent.getActions();

		Assert
				.assertEquals(
						"[Action[name==moveTo, location==RimnicuVilcea], Action[name==moveTo, location==Pitesti], Action[name==moveTo, location==Bucharest]]",
						actions.ToString());
		Assert.assertEquals("278.0", search.getMetrics().get(
				QueueSearch.METRIC_PATH_COST));
	}
}
}