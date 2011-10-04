namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.Propositional.Algorithms
{



    using CosmicFlow.AIMA.Core.Logic.Propositional.Algorithms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class KnowledgeBaseTest
    {
        private KnowledgeBase kb;

        [TestInitialize]
        public void setUp()
        {
            kb = new KnowledgeBase();
        }

        [TestMethod]
        public void testTellInsertsSentence()
        {
            kb.tell("(A AND B)");
            Assert.AreEqual(1, kb.size());
        }

        [TestMethod]
        public void testTellDoesNotInsertSameSentenceTwice()
        {
            kb.tell("(A AND B)");
            Assert.AreEqual(1, kb.size());
            kb.tell("(A AND B)");
            Assert.AreEqual(1, kb.size());
        }

        [TestMethod]
        public void testEmptyKnowledgeBaseIsAnEmptyString()
        {
            Assert.AreEqual("", kb.ToString());
        }

        [TestMethod]
        public void testKnowledgeBaseWithOneSentenceToString()
        {
            kb.tell("(A AND B)");
            Assert.AreEqual(" ( A AND B )", kb.ToString());
        }

        [TestMethod]
        public void testKnowledgeBaseWithTwoSentencesToString()
        {
            kb.tell("(A AND B)");
            kb.tell("(C AND D)");
            Assert
                    .AreEqual(" (  ( A AND B ) AND  ( C AND D ) )", kb
                            .ToString());
        }

        [TestMethod]
        public void testKnowledgeBaseWithThreeSentencesToString()
        {
            kb.tell("(A AND B)");
            kb.tell("(C AND D)");
            kb.tell("(E AND F)");
            Assert.AreEqual(
                    " (  (  ( A AND B ) AND  ( C AND D ) ) AND  ( E AND F ) )", kb
                            .ToString());
        }
    }
}