namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.FOL.Parsing
{

    using CosmicFlow.AIMA.Core.Logic.Common;
    using CosmicFlow.AIMA.Core.Logic.FOL.Domain;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class FOLLexerTest
    {
        FOLLexer lexer;

        [TestInitialize]
        public void setUp()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("P");
            domain.addConstant("John");
            domain.addConstant("Saladin");
            domain.addFunction("LeftLeg");
            domain.addFunction("BrotherOf");
            domain.addFunction("EnemyOf");
            domain.addPredicate("HasColor");
            domain.addPredicate("King");
            lexer = new FOLLexer(domain);
        }

        [TestMethod]
        public void testLexBasicExpression()
        {
            lexer.setInput("( P )");
            Assert.AreEqual(new Token((int)LogicTokenTypes.LPAREN, "("), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.CONSTANT, "P"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.RPAREN, ")"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.EOI, "EOI"), lexer
                    .nextToken());
        }

        [TestMethod]
        public void testConnectors()
        {
            lexer.setInput(" p  AND q");
            Assert.AreEqual(new Token((int)LogicTokenTypes.VARIABLE, "p"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.CONNECTOR, "AND"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.VARIABLE, "q"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.EOI, "EOI"), lexer
                    .nextToken());
        }

        [TestMethod]
        public void testFunctions()
        {
            lexer.setInput(" LeftLeg(q)");
            Assert.AreEqual(new Token((int)LogicTokenTypes.FUNCTION, "LeftLeg"),
                    lexer.nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.LPAREN, "("), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.VARIABLE, "q"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.RPAREN, ")"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.EOI, "EOI"), lexer
                    .nextToken());
        }

        [TestMethod]
        public void testPredicate()
        {
            lexer.setInput(" HasColor(r)");
            Assert.AreEqual(new Token((int)LogicTokenTypes.PREDICATE, "HasColor"),
                    lexer.nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.LPAREN, "("), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.VARIABLE, "r"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.RPAREN, ")"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.EOI, "EOI"), lexer
                    .nextToken());
        }

        [TestMethod]
        public void testMultiArgPredicate()
        {
            lexer.setInput(" King(x,y)");
            Assert.AreEqual(new Token((int)LogicTokenTypes.PREDICATE, "King"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.LPAREN, "("), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.VARIABLE, "x"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.COMMA, ","), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.VARIABLE, "y"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.RPAREN, ")"), lexer
                    .nextToken());
        }

        [TestMethod]
        public void testQuantifier()
        {
            lexer.setInput("FORALL x,y");
            Assert.AreEqual(new Token((int)LogicTokenTypes.QUANTIFIER, "FORALL"),
                    lexer.nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.VARIABLE, "x"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.COMMA, ","), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.VARIABLE, "y"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.EOI, "EOI"), lexer
                    .nextToken());
        }

        [TestMethod]
        public void testTermEquality()
        {
            lexer.setInput("BrotherOf(John) = EnemyOf(Saladin)");
            Assert.AreEqual(new Token((int)LogicTokenTypes.FUNCTION, "BrotherOf"),
                    lexer.nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.LPAREN, "("), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.CONSTANT, "John"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.RPAREN, ")"), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.EQUALS, "="), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.FUNCTION, "EnemyOf"),
                    lexer.nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.LPAREN, "("), lexer
                    .nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.CONSTANT, "Saladin"),
                    lexer.nextToken());
            Assert.AreEqual(new Token((int)LogicTokenTypes.RPAREN, ")"), lexer
                    .nextToken());
        }
    }
}