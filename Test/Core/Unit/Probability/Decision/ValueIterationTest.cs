namespace CosmicFlow.AIMA.Test.Core.Unit.Probability.Decision
{

    using CosmicFlow.AIMA.Core.Environment.CellWorld;
    using CosmicFlow.AIMA.Core.Probability.Decision;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class ValueIterationTest
    {
        private MDP<CellWorldPosition, String> fourByThreeMDP;

        [TestInitialize]
        public void setUp()
        {
            fourByThreeMDP = MDPFactory.createFourByThreeMDP();
        }

        [TestMethod]
        public void testValueIterationInCellWorld()
        {
            MDPUtilityFunction<CellWorldPosition> uf = fourByThreeMDP
                    .valueIterationTillMAximumUtilityGrowthFallsBelowErrorMargin(1,
                            0.00001);

            // AIMA2e check against Fig 17.3
            Assert.AreEqual(0.705, uf.getUtility(new CellWorldPosition(1, 1)),
                    0.001);
            Assert.AreEqual(0.655, uf.getUtility(new CellWorldPosition(1, 2)),
                    0.001);
            Assert.AreEqual(0.611, uf.getUtility(new CellWorldPosition(1, 3)),
                    0.001);
            Assert.AreEqual(0.388, uf.getUtility(new CellWorldPosition(1, 4)),
                    0.001);

            Assert.AreEqual(0.762, uf.getUtility(new CellWorldPosition(2, 1)),
                    0.001);
            Assert.AreEqual(0.660, uf.getUtility(new CellWorldPosition(2, 3)),
                    0.001);
            Assert.AreEqual(-1.0, uf.getUtility(new CellWorldPosition(2, 4)),
                    0.001);

            Assert.AreEqual(0.812, uf.getUtility(new CellWorldPosition(3, 1)),
                    0.001);
            Assert.AreEqual(0.868, uf.getUtility(new CellWorldPosition(3, 2)),
                    0.001);
            Assert.AreEqual(0.918, uf.getUtility(new CellWorldPosition(3, 3)),
                    0.001);
            Assert.AreEqual(1.0, uf.getUtility(new CellWorldPosition(3, 4)),
                    0.001);

            Assert.AreEqual(0.868, uf.getUtility(new CellWorldPosition(3, 2)),
                    0.001);
        }
    }
}