namespace aima.test.core.unit.agent.impl.aprog
{

    using AIMA.Core.Agent;
    using AIMA.Core.Agent.Impl;
    using aima.test.core.unit.agent.impl;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AIMA;
    using System.Collections.Generic;
    using AIMA.Core.Agent.Impl.AProg;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class TableDrivenAgentProgramTest
    {

        private static readonly Action ACTION_1 = new DynamicAction("action1");
        private static readonly Action ACTION_2 = new DynamicAction("action2");
        private static readonly Action ACTION_3 = new DynamicAction("action3");

        private AbstractAgent agent;

        [TestInitialize]
        public void setUp()
        {
            Map<List<Percept>, Action> perceptSequenceActions = new Map<List<Percept>, Action>();
            perceptSequenceActions.Add(createPerceptSequence(new DynamicPercept(
                    "key1", "value1")), ACTION_1);
            perceptSequenceActions.Add(createPerceptSequence(new DynamicPercept(
                    "key1", "value1"), new DynamicPercept("key1", "value2")),
                    ACTION_2);
            perceptSequenceActions.Add(createPerceptSequence(new DynamicPercept(
                    "key1", "value1"), new DynamicPercept("key1", "value2"),
                    new DynamicPercept("key1", "value3")), ACTION_3);

            agent = new MockAgent(new TableDrivenAgentProgram(
                    perceptSequenceActions));
        }

        [TestMethod]
        public void testExistingSequences()
        {
            Assert.Equals(ACTION_1, agent.execute(new DynamicPercept("key1",
                    "value1")));
            Assert.Equals(ACTION_2, agent.execute(new DynamicPercept("key1",
                    "value2")));
            Assert.Equals(ACTION_3, agent.execute(new DynamicPercept("key1",
                    "value3")));
        }

        [TestMethod]
        public void testNonExistingSequence()
        {
            Assert.Equals(ACTION_1, agent.execute(new DynamicPercept("key1",
                    "value1")));
            Assert.Equals(NoOpAction.NO_OP, agent.execute(new DynamicPercept(
                    "key1", "value3")));
        }

        private static List<Percept> createPerceptSequence(params Percept[] percepts)
        {
            List<Percept> perceptSequence = new List<Percept>();

            foreach (Percept p in percepts)
            {
                perceptSequence.Add(p);
            }

            return perceptSequence;
        }
    }
}