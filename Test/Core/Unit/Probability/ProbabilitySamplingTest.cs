namespace CosmicFlow.AIMA.Test.Core.Unit.Probability
{

    using CosmicFlow.AIMA.Core.Probability;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System;


    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class ProbabilitySamplingTest
    {

        [TestMethod]
        public void testPriorSample()
        {
            BayesNet net = createWetGrassNetwork();
            MockRandomizer r = new MockRandomizer(
                    new double[] { 0.5, 0.5, 0.5, 0.5 });
            Dictionary<String,bool> table = net.getPriorSample(r);
            Assert.AreEqual(4, table.Count);
            Assert.AreEqual(true, table["Cloudy"]);
            Assert.AreEqual(false, table["Sprinkler"]);
            Assert.AreEqual(true, table["Rain"]);
            Assert.AreEqual(true, table["WetGrass"]);
        }

        [TestMethod]
        public void testRejectionSample()
        {
            BayesNet net = createWetGrassNetwork();
            MockRandomizer r = new MockRandomizer(new double[] { 0.1 });
            Dictionary<String, bool> evidence = new Dictionary<String, bool>();
            evidence.Add("Sprinkler", true);
            double[] results = net.rejectionSample("Rain", evidence, 100, r);
            Assert.AreEqual(1.0, results[0], 0.001);
            Assert.AreEqual(0.0, results[1], 0.001);
        }

        [TestMethod]
        public void testLikelihoodWeighting()
        {
            MockRandomizer r = new MockRandomizer(
                    new double[] { 0.5, 0.5, 0.5, 0.5 });
            BayesNet net = createWetGrassNetwork();
            Dictionary<String, bool> evidence = new Dictionary<String, bool>();
            evidence.Add("Sprinkler", true);
            double[] results = net.likelihoodWeighting("Rain", evidence, 1000, r);

            Assert.AreEqual(1.0, results[0], 0.001);
            Assert.AreEqual(0.0, results[1], 0.001);
        }

        [TestMethod]
        public void testMCMCask()
        {
            BayesNet net = createWetGrassNetwork();
            MockRandomizer r = new MockRandomizer(
                    new double[] { 0.5, 0.5, 0.5, 0.5 });

            Dictionary<String, bool> evidence = new Dictionary<String, bool>();
            evidence.Add("Sprinkler", true);
            double[] results = net.mcmcAsk("Rain", evidence, 1, r);

            Assert.AreEqual(0.333, results[0], 0.001);
            Assert.AreEqual(0.666, results[1], 0.001);
        }

        [TestMethod]
        public void testMCMCask2()
        {
            BayesNet net = createWetGrassNetwork();
            MockRandomizer r = new MockRandomizer(
                    new double[] { 0.5, 0.5, 0.5, 0.5 });

            Dictionary<String, bool> evidence = new Dictionary<String, bool>();
            evidence.Add("Sprinkler", true);
            double[] results = net.mcmcAsk("Rain", evidence, 1, r);

            Assert.AreEqual(0.333, results[0], 0.001);
            Assert.AreEqual(0.666, results[1], 0.001);
        }

        [TestMethod]
        public void testEnumerationAskinMCMC()
        {
            BayesNet net = createWetGrassNetwork();
            MockRandomizer r = new MockRandomizer(
                    new double[] { 0.5, 0.5, 0.5, 0.5 });
            Dictionary<String, bool> evidence = new Dictionary<String, bool>();
            evidence.Add("Rain", true);
            evidence.Add("Sprinkler", true);
            Query q = new Query("Cloudy", new String[] { "Sprinkler", "Rain" },
                    new bool[] { true, true });
            double[] results = EnumerationAsk.ask(q, net);
            double[] results2 = net.mcmcAsk("Cloudy", evidence, 1000);
        }

        //
        // PRIVATE METHODS
        //
        private BayesNet createWetGrassNetwork()
        {
            BayesNetNode cloudy = new BayesNetNode("Cloudy");
            BayesNetNode sprinkler = new BayesNetNode("Sprinkler");
            BayesNetNode rain = new BayesNetNode("Rain");
            BayesNetNode wetGrass = new BayesNetNode("WetGrass");

            sprinkler.influencedBy(cloudy);
            rain.influencedBy(cloudy);
            wetGrass.influencedBy(rain, sprinkler);

            cloudy.setProbability(true, 0.5);
            sprinkler.setProbability(true, 0.10);
            sprinkler.setProbability(false, 0.50);

            rain.setProbability(true, 0.8);
            rain.setProbability(false, 0.2);

            wetGrass.setProbability(true, true, 0.99);
            wetGrass.setProbability(true, false, 0.90);
            wetGrass.setProbability(false, true, 0.90);
            wetGrass.setProbability(false, false, 0.00);

            BayesNet net = new BayesNet(cloudy);
            return net;
        }
    }
}