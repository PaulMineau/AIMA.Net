namespace CosmicFlow.AIMA.Test.Core.Unit.Learning.Reinforcement
{


    using CosmicFlow.AIMA.Core.Learning.Reinforcement;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class QTableTest
    {

        private QTable<String, String> qt;

        [TestInitialize]
        public void setUp()
        {
            // simple qlearning example from
            // http://people.revoledu.com/kardi/tutorial/ReinforcementLearning/index.
            // html
            // List<String> states = Arrays.asList(new String[] { "A", "B", "C",
            // "D",
            // "E", "F" });
            List<String> actions = new List<String>() { "toA", "toB",
				"toC", "toD", "toE", "toF" };
            qt = new QTable<String, String>(actions);
        }

        [TestMethod]
        public void testInitialSetup()
        {
            Assert.AreEqual(0.0, qt.getQValue("A", "toB"), 0.001);
            Assert.AreEqual(0.0, qt.getQValue("F", "toF"), 0.001);
        }

        [TestMethod]
        public void testQUpDate()
        {
            Assert.AreEqual(0.0, qt.getQValue("B", "toF"), 0.001);

            qt.upDateQ("B", "toF", "B", 1.0, 100, 0.8);
            Assert.AreEqual(100.0, qt.getQValue("B", "toF"), 0.001);

            qt.upDateQ("D", "toB", "B", 1.0, 0, 0.8);
            Assert.AreEqual(80.0, qt.getQValue("D", "toB"), 0.001);

            qt.upDateQ("A", "toB", "B", 1.0, 0, 0.8);
            Assert.AreEqual(80.0, qt.getQValue("A", "toB"), 0.001);

            qt.upDateQ("A", "toD", "D", 1.0, 0, 0.8);
            Assert.AreEqual(64.0, qt.getQValue("A", "toD"), 0.001);
        }
    }
}