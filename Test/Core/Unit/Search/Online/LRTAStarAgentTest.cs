namespace aima.test.core.unit.search.online;

using org.junit.Assert;
using org.junit.Before;
using org.junit.Test;

using AIMA.Core.Agent.Action;
using AIMA.Core.Agent.Agent;
using AIMA.Core.Agent.EnvironmentState;
using AIMA.Core.Agent.EnvironmentView;
using AIMA.Core.Environment.Map.ExtendableMap;
using AIMA.Core.Environment.Map.MapEnvironment;
using AIMA.Core.Environment.Map.MapFunctionFactory;
using AIMA.Core.Environment.Map.MapStepCostFunction;
using AIMA.Core.Search.Framework.DefaultGoalTest;
using AIMA.Core.Search.Framework.HeuristicFunction;
using AIMA.Core.Search.Online.LRTAStarAgent;
using AIMA.Core.Search.Online.OnlineSearchProblem;

public class LRTAStarAgentTest {
	ExtendableMap aMap;

	StringBuffer envChanges;

	HeuristicFunction hf;

	@Before
	public void setUp() {
		aMap = new ExtendableMap();
		aMap.addBidirectionalLink("A", "B", 4.0);
		aMap.addBidirectionalLink("B", "C", 4.0);
		aMap.addBidirectionalLink("C", "D", 4.0);
		aMap.addBidirectionalLink("D", "E", 4.0);
		aMap.addBidirectionalLink("E", "F", 4.0);
		hf = new HeuristicFunction() {
			public double h(Object state) {
				return 1;
			}
		};

		envChanges = new StringBuffer();
	}

	@Test
	public void testAlreadyAtGoal() {
		MapEnvironment me = new MapEnvironment(aMap);
		LRTAStarAgent agent = new LRTAStarAgent(new OnlineSearchProblem(
				MapFunctionFactory.getActionsFunction(aMap),
				new DefaultGoalTest("A"), new MapStepCostFunction(aMap)),
				MapFunctionFactory.getPerceptToStateFunction(), hf);
		me.addAgent(agent, "A");
		me.addEnvironmentView(new TestEnvironmentView());
		me.stepUntilDone();

		Assert.assertEquals("Action[name==NoOp]->", envChanges.ToString());
	}

	@Test
	public void testNormalSearch() {
		MapEnvironment me = new MapEnvironment(aMap);
		LRTAStarAgent agent = new LRTAStarAgent(new OnlineSearchProblem(
				MapFunctionFactory.getActionsFunction(aMap),
				new DefaultGoalTest("F"), new MapStepCostFunction(aMap)),
				MapFunctionFactory.getPerceptToStateFunction(), hf);
		me.addAgent(agent, "A");
		me.addEnvironmentView(new TestEnvironmentView());
		me.stepUntilDone();

		Assert
				.assertEquals(
						"Action[name==moveTo, location==B]->Action[name==moveTo, location==A]->Action[name==moveTo, location==B]->Action[name==moveTo, location==C]->Action[name==moveTo, location==B]->Action[name==moveTo, location==C]->Action[name==moveTo, location==D]->Action[name==moveTo, location==C]->Action[name==moveTo, location==D]->Action[name==moveTo, location==E]->Action[name==moveTo, location==D]->Action[name==moveTo, location==E]->Action[name==moveTo, location==F]->Action[name==NoOp]->",
						envChanges.ToString());
	}

	@Test
	public void testNoPath() {
		MapEnvironment me = new MapEnvironment(aMap);
		LRTAStarAgent agent = new LRTAStarAgent(new OnlineSearchProblem(
				MapFunctionFactory.getActionsFunction(aMap),
				new DefaultGoalTest("G"), new MapStepCostFunction(aMap)),
				MapFunctionFactory.getPerceptToStateFunction(), hf);
		me.addAgent(agent, "A");
		me.addEnvironmentView(new TestEnvironmentView());
		// Note: Will search forever if no path is possible,
		// Therefore restrict the number of steps to something
		// reasonablbe, against which to test.
		me.step(14);

		Assert
				.assertEquals(
						"Action[name==moveTo, location==B]->Action[name==moveTo, location==A]->Action[name==moveTo, location==B]->Action[name==moveTo, location==C]->Action[name==moveTo, location==B]->Action[name==moveTo, location==C]->Action[name==moveTo, location==D]->Action[name==moveTo, location==C]->Action[name==moveTo, location==D]->Action[name==moveTo, location==E]->Action[name==moveTo, location==D]->Action[name==moveTo, location==E]->Action[name==moveTo, location==F]->Action[name==moveTo, location==E]->",
						envChanges.ToString());
	}

	private class TestEnvironmentView : EnvironmentView {
		public void notify(String msg) {
			envChanges.append(msg).append("->");
		}

		public void agentAdded(Agent agent, EnvironmentState state) {
			// Nothing.
		}

		public void agentActed(Agent agent, Action action,
				EnvironmentState state) {
			envChanges.append(action).append("->");
		}
	}
}
