namespace AIMA.Test.Core.Unit.Search.Uninformed;

using java.util.List;

using org.junit.Assert;
using org.junit.Test;

using AIMA.Core.Agent.Action;
using AIMA.Core.Environment.NQueens.NQueensBoard;
using AIMA.Core.Environment.NQueens.NQueensFunctionFactory;
using AIMA.Core.Environment.NQueens.NQueensGoalTest;
using AIMA.Core.Search.Framework.GraphSearch;
using AIMA.Core.Search.Framework.Problem;
using AIMA.Core.Search.Framework.Search;
using AIMA.Core.Search.Framework.SearchAgent;
using AIMA.Core.Search.Uninformed.DepthFirstSearch;

public class DepthFirstSearchTest {

	@Test
	public void testDepthFirstSuccesfulSearch() throws Exception {
		Problem problem = new Problem(new NQueensBoard(8),
				NQueensFunctionFactory.getIActionsFunction(),
				NQueensFunctionFactory.getResultFunction(),
				new NQueensGoalTest());
		Search search = new DepthFirstSearch(new GraphSearch());
		SearchAgent agent = new SearchAgent(problem, search);
		List<Action> actions = agent.getActions();
		assertCorrectPlacement(actions);
		Assert.assertEquals("113", agent.getInstrumentation().getProperty(
				"nodesExpanded"));
	}

	@Test
	public void testDepthFirstUnSuccessfulSearch() throws Exception {
		Problem problem = new Problem(new NQueensBoard(3),
				NQueensFunctionFactory.getIActionsFunction(),
				NQueensFunctionFactory.getResultFunction(),
				new NQueensGoalTest());
		Search search = new DepthFirstSearch(new GraphSearch());
		SearchAgent agent = new SearchAgent(problem, search);
		List<Action> actions = agent.getActions();
		Assert.assertEquals(0, actions.size());
		Assert.assertEquals("6", agent.getInstrumentation().getProperty(
				"nodesExpanded"));
	}

	//
	// PRIVATE METHODS
	//
	private void assertCorrectPlacement(List<Action> actions) {
		Assert.assertEquals(8, actions.size());
		Assert.assertEquals(
				"Action[name==placeQueenAt, location== ( 0 , 7 ) ]", actions
						.get(0).ToString());
		Assert.assertEquals(
				"Action[name==placeQueenAt, location== ( 1 , 3 ) ]", actions
						.get(1).ToString());
		Assert.assertEquals(
				"Action[name==placeQueenAt, location== ( 2 , 0 ) ]", actions
						.get(2).ToString());
		Assert.assertEquals(
				"Action[name==placeQueenAt, location== ( 3 , 2 ) ]", actions
						.get(3).ToString());
		Assert.assertEquals(
				"Action[name==placeQueenAt, location== ( 4 , 5 ) ]", actions
						.get(4).ToString());
		Assert.assertEquals(
				"Action[name==placeQueenAt, location== ( 5 , 1 ) ]", actions
						.get(5).ToString());
		Assert.assertEquals(
				"Action[name==placeQueenAt, location== ( 6 , 6 ) ]", actions
						.get(6).ToString());
		Assert.assertEquals(
				"Action[name==placeQueenAt, location== ( 7 , 4 ) ]", actions
						.get(7).ToString());
	}
}
