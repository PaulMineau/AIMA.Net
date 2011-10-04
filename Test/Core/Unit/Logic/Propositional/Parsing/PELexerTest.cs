namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.Propositional.Parsing
{

    using CosmicFlow.AIMA.Core.Logic.Common;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class PELexerTest
    {

        [TestMethod]
        public void testLexBasicExpression()
        {
            PELexer pelexer = new PELexer();
            pelexer.setInput("(P)");
            Assert.AreEqual(new Token((int)LogicTokenTypes.LPAREN, "("), pelexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.SYMBOL, "P"), pelexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.RPAREN, ")"), pelexer
                    .nextToken());

            Assert.AreEqual(new Token((int)LogicTokenTypes.EOI, "EOI"), pelexer
                    .nextToken());
        }

        [TestMethod]
        public void testLexNotExpression()
        {
            PELexer pelexer = new PELexer();
            pelexer.setInput("(NOT P)");
            Assert.AreEqual(new Token((int)LogicTokenTypes.LPAREN, "("), pelexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.CONNECTOR, "NOT"),
                    pelexer.nextToken());

            Assert.AreEqual(new Token((int)LogicTokenTypes.SYMBOL, "P"), pelexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.RPAREN, ")"), pelexer
                    .nextToken());

            Assert.AreEqual(new Token((int)LogicTokenTypes.EOI, "EOI"), pelexer
                    .nextToken());
        }

        [TestMethod]
        public void testLexImpliesExpression()
        {
            PELexer pelexer = new PELexer();
            pelexer.setInput("(P => Q)");
            Assert.AreEqual(new Token((int)LogicTokenTypes.LPAREN, "("), pelexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.SYMBOL, "P"), pelexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.CONNECTOR, "=>"), pelexer
                    .nextToken());
        }

        [TestMethod]
        public void testLexBiCOnditionalExpression()
        {
            PELexer pelexer = new PELexer();
            pelexer.setInput("(B11 <=> (P12 OR P21))");
            Assert.AreEqual(new Token((int)LogicTokenTypes.LPAREN, "("), pelexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.SYMBOL, "B11"), pelexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.CONNECTOR, "<=>"),
                    pelexer.nextToken());
        }
    }
}