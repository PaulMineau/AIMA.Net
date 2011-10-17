namespace aima.test.core.unit.agent.impl
{

using System.Collections;

using AIMA.Core.Agent;
using AIMA.Core.Agent.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

/**
 * @author Ciaran O'Reilly
 * 
 */
[TestClass]
public class PerceptSequenceTest {

    [TestMethod]
	public void testToString() {
		List<Percept> ps = new List<Percept>();
		ps.Add(new DynamicPercept("key1", "value1"));

		Assert.Equals("[Percept[key1==value1]]", ps.ToString());

		ps.Add(new DynamicPercept("key1", "value1", "key2", "value2"));

        Assert.Equals(
				"[Percept[key1==value1], Percept[key1==value1, key2==value2]]",
				ps.ToString());
	}

	[TestMethod]
	public void testEquals() {
		List<Percept> ps1 = new List<Percept>();
		List<Percept> ps2 = new List<Percept>();

        Assert.Equals(ps1, ps2);

		ps1.Add(new DynamicPercept("key1", "value1"));

		Assert.AreNotEqual(ps1, ps2);

		ps2.Add(new DynamicPercept("key1", "value1"));

		Assert.Equals(ps1, ps2);
	}
}
}