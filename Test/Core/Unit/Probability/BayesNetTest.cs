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
public class BayesNetTest {
	BayesNet net;

	[TestInitialize]
	public void setUp() {
		net = createBurglaryNetwork();
	}

	[TestMethod]
	public void testVariablesAreCorrectlyObtainedFromBayesNetwork() {
		List<String> variables = net.getVariables();
		Assert.AreEqual(5, variables.Count);
		Assert.AreEqual("Burglary", (String) variables[0]);
		Assert.AreEqual("EarthQuake", (String) variables[1]);
		Assert.AreEqual("Alarm", (String) variables[2]);
		Assert.AreEqual("JohnCalls", (String) variables[3]);
		Assert.AreEqual("MaryCalls", (String) variables[4]);
	}

	//
	// PRIVATE METHODS
	//
	private BayesNet createBurglaryNetwork() {
		BayesNetNode burglary = new BayesNetNode("Burglary");
		BayesNetNode earthquake = new BayesNetNode("EarthQuake");
		BayesNetNode alarm = new BayesNetNode("Alarm");
		BayesNetNode johnCalls = new BayesNetNode("JohnCalls");
		BayesNetNode maryCalls = new BayesNetNode("MaryCalls");

		alarm.influencedBy(burglary, earthquake);
		johnCalls.influencedBy(alarm);
		maryCalls.influencedBy(alarm);

		burglary.setProbability(true, 0.001);
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