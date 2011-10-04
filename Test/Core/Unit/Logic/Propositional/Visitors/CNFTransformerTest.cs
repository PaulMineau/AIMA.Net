namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.Propositional.Visitors
{

    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Visitors;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class CNFTransformerTest
    {
        private PEParser parser = new PEParser();

        private CNFTransformer transformer;

        [TestInitialize]
        public void setUp()
        {
            transformer = new CNFTransformer();
        }

        [TestMethod]
        public void testSymbolTransform()
        {
            Sentence symbol = (Sentence)parser.parse("A");
            Sentence transformed = transformer.transform(symbol);
            Assert.AreEqual("A", transformed.ToString());
        }

        [TestMethod]
        public void testBasicSentenceTransformation()
        {
            Sentence and = (Sentence)parser.parse("(A AND B)");
            Sentence transformedAnd = transformer.transform(and);
            Assert.AreEqual(and.ToString(), transformedAnd.ToString());

            Sentence or = (Sentence)parser.parse("(A OR B)");
            Sentence transformedOr = transformer.transform(or);
            Assert.AreEqual(or.ToString(), transformedOr.ToString());

            Sentence not = (Sentence)parser.parse("(NOT C)");
            Sentence transformedNot = transformer.transform(not);
            Assert.AreEqual(not.ToString(), transformedNot.ToString());
        }

        [TestMethod]
        public void testImplicationTransformation()
        {
            Sentence impl = (Sentence)parser.parse("(A => B)");
            Sentence expected = (Sentence)parser.parse("((NOT A) OR B)");
            Sentence transformedImpl = transformer.transform(impl);
            Assert.AreEqual(expected.ToString(), transformedImpl.ToString());
        }

        [TestMethod]
        public void testBiConditionalTransformation()
        {
            Sentence bic = (Sentence)parser.parse("(A <=> B)");
            Sentence expected = (Sentence)parser
                    .parse("(((NOT A) OR B)  AND ((NOT B) OR A)) ");
            Sentence transformedBic = transformer.transform(bic);
            Assert.AreEqual(expected.ToString(), transformedBic.ToString());
        }

        [TestMethod]
        public void testTwoSuccessiveNotsTransformation()
        {
            Sentence twoNots = (Sentence)parser.parse("(NOT (NOT A))");
            Sentence expected = (Sentence)parser.parse("A");
            Sentence transformed = transformer.transform(twoNots);
            Assert.AreEqual(expected.ToString(), transformed.ToString());
        }

        [TestMethod]
        public void testThreeSuccessiveNotsTransformation()
        {
            Sentence threeNots = (Sentence)parser.parse("(NOT (NOT (NOT A)))");
            Sentence expected = (Sentence)parser.parse("(NOT A)");
            Sentence transformed = transformer.transform(threeNots);
            Assert.AreEqual(expected.ToString(), transformed.ToString());
        }

        [TestMethod]
        public void testFourSuccessiveNotsTransformation()
        {
            Sentence fourNots = (Sentence)parser
                    .parse("(NOT (NOT (NOT (NOT A))))");
            Sentence expected = (Sentence)parser.parse("A");
            Sentence transformed = transformer.transform(fourNots);
            Assert.AreEqual(expected.ToString(), transformed.ToString());
        }

        [TestMethod]
        public void testDeMorgan1()
        {
            Sentence dm = (Sentence)parser.parse("(NOT (A AND B))");
            Sentence expected = (Sentence)parser.parse("((NOT A) OR (NOT B))");
            Sentence transformed = transformer.transform(dm);
            Assert.AreEqual(expected.ToString(), transformed.ToString());
        }

        [TestMethod]
        public void testDeMorgan2()
        {
            Sentence dm = (Sentence)parser.parse("(NOT (A OR B))");
            Sentence expected = (Sentence)parser.parse("((NOT A) AND (NOT B))");
            Sentence transformed = transformer.transform(dm);
            Assert.AreEqual(expected.ToString(), transformed.ToString());
        }

        [TestMethod]
        public void testOrDistribution1()
        {
            Sentence or = (Sentence)parser.parse("((A AND B) OR C)");
            Sentence expected = (Sentence)parser.parse("((C OR A) AND (C OR B))");
            Sentence transformed = transformer.transform(or);
            Assert.AreEqual(expected.ToString(), transformed.ToString());
        }

        [TestMethod]
        public void testOrDistribution2()
        {
            Sentence or = (Sentence)parser.parse("(A OR (B AND C))");
            Sentence expected = (Sentence)parser.parse("((A OR B) AND (A OR C))");
            Sentence transformed = transformer.transform(or);
            Assert.AreEqual(expected.ToString(), transformed.ToString());
        }

        [TestMethod]
        public void testAimaExample()
        {
            Sentence aimaEg = (Sentence)parser.parse("( B11 <=> (P12 OR P21))");
            Sentence expected = (Sentence)parser
                    .parse(" (  (  ( NOT B11 )  OR  ( P12 OR P21 ) ) AND  (  ( B11 OR  ( NOT P12 )  ) AND  ( B11 OR  ( NOT P21 )  ) ) )");
            Sentence transformed = transformer.transform(aimaEg);
            Assert.AreEqual(expected.ToString(), transformed.ToString());
        }
    }
}
