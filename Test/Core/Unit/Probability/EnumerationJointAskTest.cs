namespace CosmicFlow.AIMA.Test.Core.Unit.Probability
{

    using CosmicFlow.AIMA.Core.Probability;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class EnumerationJointAskTest
    {

        [TestMethod]
        public void testBasicUsage()
        {
            // >>> P[T, T, T] = 0.108; P[T, T, F] = 0.012; P[F, T, T] = 0.072; P[F,
            // T, F] = 0.008
            // >>> P[T, F, T] = 0.016; P[T, F, F] = 0.064; P[F, F, T] = 0.144; P[F,
            // F, F] = 0.576

            ProbabilityDistribution jp = new ProbabilityDistribution("ToothAche",
                    "Cavity", "Catch");
            jp.set(0.108, true, true, true);
            jp.set(0.012, true, true, false);
            jp.set(0.072, false, true, true);
            jp.set(0.008, false, true, false);
            jp.set(0.016, true, false, true);
            jp.set(0.064, true, false, false);
            jp.set(0.144, false, false, true);
            jp.set(0.008, false, false, false);

            Query q = new Query("Cavity", new String[] { "ToothAche" },
                    new bool[] { true });
            double[] probs = EnumerateJointAsk.ask(q, jp);
            Assert.AreEqual(0.6, probs[0], 0.001);
            Assert.AreEqual(0.4, probs[1], 0.001);
        }
    }
}