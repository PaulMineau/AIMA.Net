namespace aima.test.core.unit.agent.impl.aprog;

using java.util.ArrayList;
using java.util.HashMap;
using java.util.List;
using java.util.Map;

using org.junit.Assert;
using org.junit.Before;
using org.junit.Test;

using AIMA.Core.Agent.Action;
using AIMA.Core.Agent.Percept;
using AIMA.Core.Agent.Impl.AbstractAgent;
using AIMA.Core.Agent.Impl.DynamicAction;
using AIMA.Core.Agent.Impl.DynamicPercept;
using AIMA.Core.Agent.Impl.NoOpAction;
using AIMA.Core.Agent.Impl.AProg.TableDrivenAgentProgram;
using aima.test.core.unit.agent.impl.MockAgent;

/**
 * @author Ciaran O'Reilly
 * 
 */
public class TableDrivenAgentProgramTest {

	private const Action ACTION_1 = new DynamicAction("action1");
	private const Action ACTION_2 = new DynamicAction("action2");
	private const Action ACTION_3 = new DynamicAction("action3");

	private AbstractAgent agent;

	@Before
	public void setUp() {
		Map<List<Percept>, Action> perceptSequenceActions = new HashMap<List<Percept>, Action>();
		perceptSequenceActions.put(createPerceptSequence(new DynamicPercept(
				"key1", "value1")), ACTION_1);
		perceptSequenceActions.put(createPerceptSequence(new DynamicPercept(
				"key1", "value1"), new DynamicPercept("key1", "value2")),
				ACTION_2);
		perceptSequenceActions.put(createPerceptSequence(new DynamicPercept(
				"key1", "value1"), new DynamicPercept("key1", "value2"),
				new DynamicPercept("key1", "value3")), ACTION_3);

		agent = new MockAgent(new TableDrivenAgentProgram(
				perceptSequenceActions));
	}

	@Test
	public void testExistingSequences() {
		Assert.assertEquals(ACTION_1, agent.execute(new DynamicPercept("key1",
				"value1")));
		Assert.assertEquals(ACTION_2, agent.execute(new DynamicPercept("key1",
				"value2")));
		Assert.assertEquals(ACTION_3, agent.execute(new DynamicPercept("key1",
				"value3")));
	}

	@Test
	public void testNonExistingSequence() {
		Assert.assertEquals(ACTION_1, agent.execute(new DynamicPercept("key1",
				"value1")));
		Assert.assertEquals(NoOpAction.NO_OP, agent.execute(new DynamicPercept(
				"key1", "value3")));
	}

	private static List<Percept> createPerceptSequence(Percept... percepts) {
		List<Percept> perceptSequence = new ArrayList<Percept>();

		for (Percept p : percepts) {
			perceptSequence.add(p);
		}

		return perceptSequence;
	}
}
