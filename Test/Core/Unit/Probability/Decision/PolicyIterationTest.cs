namespace AIMA.Test.Core.Unit.Probability.Decision
{

    using AIMA.Core.Environment.CellWorld;
    using AIMA.Core.Probability.Decision;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class PolicyIterationTest
    {
        private MDP<CellWorldPosition, String> fourByThreeMDP;

        [TestInitialize]
        public void setUp()
        {
            fourByThreeMDP = MDPFactory.createFourByThreeMDP();
        }

        [TestMethod]
        public void testPolicyEvaluation()
        {
            MDPPolicy<CellWorldPosition, String> policy = fourByThreeMDP
                    .randomPolicy();
            MDPUtilityFunction<CellWorldPosition> uf1 = fourByThreeMDP
                    .initialUtilityFunction();

            MDPUtilityFunction<CellWorldPosition> uf2 = fourByThreeMDP
                    .policyEvaluation(policy, uf1, 1, 3);

            Assert.IsFalse(uf1.Equals(uf2));
        }

        [TestMethod]
        public void testPolicyIteration()
        {

            MDPPolicy<CellWorldPosition, String> policy = fourByThreeMDP
                    .policyIteration(1);
            // AIMA2e check With Figure 17.2 (a)

            Assert
                    .AreEqual("up", policy
                            .getAction(new CellWorldPosition(1, 1)));
            Assert
                    .AreEqual("up", policy
                            .getAction(new CellWorldPosition(2, 1)));
            Assert.AreEqual("right", policy.getAction(new CellWorldPosition(3,
                    1)));

            Assert.AreEqual("left", policy
                    .getAction(new CellWorldPosition(1, 2)));
            Assert.AreEqual("right", policy.getAction(new CellWorldPosition(3,
                    2)));

            Assert.AreEqual("left", policy
                    .getAction(new CellWorldPosition(1, 3)));
            Assert
                    .AreEqual("up", policy
                            .getAction(new CellWorldPosition(2, 3)));
            Assert.AreEqual("right", policy.getAction(new CellWorldPosition(3,
                    3)));

            Assert.AreEqual("left", policy
                    .getAction(new CellWorldPosition(1, 4)));
        }
    }
}