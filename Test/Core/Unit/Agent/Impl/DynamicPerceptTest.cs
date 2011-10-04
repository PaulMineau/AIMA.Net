namespace aima.test.core.unit.agent.impl;

using org.junit.Assert;
using org.junit.Test;

using AIMA.Core.Agent.Impl.DynamicPercept;

public class DynamicPerceptTest {

	@Test
	public void testToString() {
		DynamicPercept p = new DynamicPercept("key1", "value1");

		Assert.assertEquals("Percept[key1==value1]", p.ToString());

		p = new DynamicPercept("key1", "value1", "key2", "value2");

		Assert
				.assertEquals("Percept[key1==value1, key2==value2]", p
						.ToString());
	}

	@Test
	public void testEquals() {
		DynamicPercept p1 = new DynamicPercept();
		DynamicPercept p2 = new DynamicPercept();

		Assert.assertEquals(p1, p2);

		p1 = new DynamicPercept("key1", "value1");

		Assert.assertNotSame(p1, p2);

		p2 = new DynamicPercept("key1", "value1");

		Assert.assertEquals(p1, p2);
	}
}
