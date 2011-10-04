namespace CosmicFlow.AIMA.Test.Core.Unit.Probability
{

    using CosmicFlow.AIMA.Core.Probability;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class BayesNetNodeTest
    {
        private BayesNetNode a, b, c, d, e;

        [TestInitialize]
        public void setUp()
        {
            a = new BayesNetNode("A");
            b = new BayesNetNode("B");
            c = new BayesNetNode("C");
            d = new BayesNetNode("D");
            e = new BayesNetNode("E");

            c.influencedBy(a, b);
            d.influencedBy(c);
            e.influencedBy(c);
        }

        [TestMethod]
        public void testInfluenceSemantics()
        {
            Assert.AreEqual(1, a.getChildren().Count);
            Assert.IsTrue(a.getChildren().Contains(c));
            Assert.AreEqual(0, a.getParents().Count);

            Assert.AreEqual(1, b.getChildren().Count);
            Assert.IsTrue(b.getChildren().Contains(c));
            Assert.AreEqual(0, b.getParents().Count);

            Assert.AreEqual(2, c.getChildren().Count);
            Assert.IsTrue(c.getChildren().Contains(d));
            Assert.IsTrue(c.getChildren().Contains(e));
            Assert.AreEqual(2, c.getParents().Count);
            Assert.IsTrue(c.getParents().Contains(a));
            Assert.IsTrue(c.getParents().Contains(b));

            Assert.AreEqual(0, d.getChildren().Count);
            Assert.AreEqual(1, d.getParents().Count);
            Assert.IsTrue(d.getParents().Contains(c));

            Assert.AreEqual(0, e.getChildren().Count);
            Assert.AreEqual(1, e.getParents().Count);
            Assert.IsTrue(e.getParents().Contains(c));
        }
    }
}