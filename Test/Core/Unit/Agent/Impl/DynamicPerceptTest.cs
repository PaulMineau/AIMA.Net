namespace aima.test.core.unit.agent.impl
{

    using AIMA.Core.Agent.Impl;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DynamicPerceptTest
    {

        [TestMethod]
        public void testToString()
        {
            DynamicPercept p = new DynamicPercept("key1", "value1");

            Assert.Equals("Percept[key1==value1]", p.ToString());

            p = new DynamicPercept("key1", "value1", "key2", "value2");

            Assert
                    .Equals("Percept[key1==value1, key2==value2]", p
                            .ToString());
        }

        [TestMethod]
        public void testEquals()
        {
            DynamicPercept p1 = new DynamicPercept();
            DynamicPercept p2 = new DynamicPercept();

            Assert.Equals(p1, p2);

            p1 = new DynamicPercept("key1", "value1");

            Assert.AreNotEqual(p1, p2);

            p2 = new DynamicPercept("key1", "value1");

            Assert.Equals(p1, p2);
        }
    }
}