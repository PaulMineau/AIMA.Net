namespace aima.test.core.unit.agent.impl;

using java.util.ArrayList;
using java.util.List;

using org.junit.Assert;
using org.junit.Test;

using AIMA.Core.Agent.Percept;
using AIMA.Core.Agent.Impl.DynamicPercept;

/**
 * @author Ciaran O'Reilly
 * 
 */
public class PerceptSequenceTest {

	@Test
	public void testToString() {
		List<Percept> ps = new ArrayList<Percept>();
		ps.add(new DynamicPercept("key1", "value1"));

		Assert.assertEquals("[Percept[key1==value1]]", ps.ToString());

		ps.add(new DynamicPercept("key1", "value1", "key2", "value2"));

		Assert.assertEquals(
				"[Percept[key1==value1], Percept[key1==value1, key2==value2]]",
				ps.ToString());
	}

	@Test
	public void testEquals() {
		List<Percept> ps1 = new ArrayList<Percept>();
		List<Percept> ps2 = new ArrayList<Percept>();

		Assert.assertEquals(ps1, ps2);

		ps1.add(new DynamicPercept("key1", "value1"));

		Assert.assertNotSame(ps1, ps2);

		ps2.add(new DynamicPercept("key1", "value1"));

		Assert.assertEquals(ps1, ps2);
	}
}
