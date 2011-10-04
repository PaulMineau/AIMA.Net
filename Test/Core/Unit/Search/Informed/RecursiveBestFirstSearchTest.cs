namespace aima.test.core.unit.search.informed;

using org.junit.Assert;
using org.junit.Before;
using org.junit.Test;

using AIMA.Core.Agent.Action;
using AIMA.Core.Agent.Agent;
using AIMA.Core.Agent.EnvironmentState;
using AIMA.Core.Agent.EnvironmentView;
using AIMA.Core.Environment.Map.Map;
using AIMA.Core.Environment.Map.MapAgent;
using AIMA.Core.Environment.Map.MapEnvironment;
using AIMA.Core.Environment.Map.SimplifiedRoadMapOfPartOfRomania;
using AIMA.Core.Search.Framework.HeuristicFunction;
using AIMA.Core.Search.Informed.AStarEvaluationFunction;
using AIMA.Core.Search.Informed.RecursiveBestFirstSearch;
using AIMA.Core.Util.DataStructure.Point2D;

/**
 * @author Ciaran O'Reilly
 * 
 */
public class RecursiveBestFirstSearchTest {

	StringBuffer envChanges;

	Map aMap;

	RecursiveBestFirstSearch recursiveBestFirstSearch;

	@Before
	public void setUp() {
		envChanges = new StringBuffer();

		aMap = new SimplifiedRoadMapOfPartOfRomania();

		HeuristicFunction heuristicFunction = new HeuristicFunction() {
			public double h(Object state) {
				Point2D pt1 = aMap.getPosition((String) state);
				Point2D pt2 = aMap
						.getPosition(SimplifiedRoadMapOfPartOfRomania.BUCHAREST);
				return pt1.distance(pt2);
			}
		};

		recursiveBestFirstSearch = new RecursiveBestFirstSearch(
				new AStarEvaluationFunction(heuristicFunction));
	}

	@Test
	public void testStartingAtGoal() {
		MapEnvironment me = new MapEnvironment(aMap);
		MapAgent ma = new MapAgent(me.getMap(), me, recursiveBestFirstSearch,
				new String[] { SimplifiedRoadMapOfPartOfRomania.BUCHAREST });

		me.addAgent(ma, SimplifiedRoadMapOfPartOfRomania.BUCHAREST);
		me.addEnvironmentView(new TestEnvironmentView());
		me.stepUntilDone();

		Assert
				.assertEquals(
						"CurrentLocation=In(Bucharest), Goal=In(Bucharest):Action[name==NoOp]:METRIC[pathCost]=0.0:METRIC[maxRecursiveDepth]=0:METRIC[nodesExpanded]=0:Action[name==NoOp]:",
						envChanges.ToString());
	}

	@Test
	public void testAIMA3eFigure3_27() {
		MapEnvironment me = new MapEnvironment(aMap);
		MapAgent ma = new MapAgent(me.getMap(), me, recursiveBestFirstSearch,
				new String[] { SimplifiedRoadMapOfPartOfRomania.BUCHAREST });

		me.addAgent(ma, SimplifiedRoadMapOfPartOfRomania.ARAD);
		me.addEnvironmentView(new TestEnvironmentView());
		me.stepUntilDone();

		Assert
				.assertEquals(
						"CurrentLocation=In(Arad), Goal=In(Bucharest):Action[name==moveTo, location==Sibiu]:Action[name==moveTo, location==RimnicuVilcea]:Action[name==moveTo, location==Pitesti]:Action[name==moveTo, location==Bucharest]:METRIC[pathCost]=418.0:METRIC[maxRecursiveDepth]=4:METRIC[nodesExpanded]=6:Action[name==NoOp]:",
						envChanges.ToString());
	}

	private class TestEnvironmentView : EnvironmentView {
		public void notify(String msg) {
			envChanges.append(msg).append(":");
		}

		public void agentAdded(Agent agent, EnvironmentState state) {
			// Nothing.
		}

		public void agentActed(Agent agent, Action action,
				EnvironmentState state) {
			envChanges.append(action).append(":");
		}
	}
}
