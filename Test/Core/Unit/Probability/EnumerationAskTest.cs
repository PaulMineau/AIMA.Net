namespace AIMA.Test.Core.Unit.Probability
{

    using AIMA.Core.Probability;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class EnumerationAskTest
    {

        [TestMethod]
        public void testEnumerationAskAimaExample()
        {
            Query q = new Query("Burglary",
                    new String[] { "JohnCalls", "MaryCalls" }, new bool[] {
						true, true });
            double[] probs = EnumerationAsk.ask(q, createBurglaryNetwork());
            Assert.AreEqual(0.284, probs[0], 0.001);
            Assert.AreEqual(0.716, probs[1], 0.001);
        }

        [TestMethod]
        public void testEnumerationAllVariablesExcludingQueryKnown()
        {
            Query q = new Query("Alarm", new String[] { "Burglary", "EarthQuake",
				"JohnCalls", "MaryCalls" }, new bool[] { false, false, true,
				true });

            double[] probs = EnumerationAsk.ask(q, createBurglaryNetwork());
            Assert.AreEqual(0.557, probs[0], 0.001);
            Assert.AreEqual(0.442, probs[1], 0.001);
        }

        //
        // PRIVATE METHODS
        // 
        private BayesNet createBurglaryNetwork()
        {
            BayesNetNode burglary = new BayesNetNode("Burglary");
            BayesNetNode earthquake = new BayesNetNode("EarthQuake");
            BayesNetNode alarm = new BayesNetNode("Alarm");
            BayesNetNode johnCalls = new BayesNetNode("JohnCalls");
            BayesNetNode maryCalls = new BayesNetNode("MaryCalls");

            alarm.influencedBy(burglary, earthquake);
            johnCalls.influencedBy(alarm);
            maryCalls.influencedBy(alarm);

            burglary.setProbability(true, 0.001);// TODO behaviour changes if
            // root node
            earthquake.setProbability(true, 0.002);

            alarm.setProbability(true, true, 0.95);
            alarm.setProbability(true, false, 0.94);
            alarm.setProbability(false, true, 0.29);
            alarm.setProbability(false, false, 0.001);

            johnCalls.setProbability(true, 0.90);
            johnCalls.setProbability(false, 0.05);

            maryCalls.setProbability(true, 0.70);
            maryCalls.setProbability(false, 0.01);

            BayesNet net = new BayesNet(burglary, earthquake);
            return net;
        }
    }
}