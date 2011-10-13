namespace AIMA.Test.Core.Unit.Logic.Propositional.Visitors
{

    using AIMA.Core.Logic.Propositional.Parsing;
    using AIMA.Core.Logic.Propositional.Parsing.Ast;
    using AIMA.Core.Logic.Propositional.Visitors;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class CNFClauseGathererTest
    {
        private CNFClauseGatherer gatherer;

        private PEParser parser;

        [TestInitialize]
        public void setUp()
        {
            parser = new PEParser();
            gatherer = new CNFClauseGatherer();
        }

        [TestMethod]
        public void testSymbol()
        {
            Sentence simple = (Sentence)parser.parse("A");
            Sentence a = (Sentence)parser.parse("A");
            List<Sentence> clauses = gatherer.getClausesFrom(simple);
            Assert.IsNotNull(clauses);
            Assert.AreEqual(1, clauses.Count);
            Assert.IsTrue(clauses.Contains(a));
        }

        [TestMethod]
        public void testNotSentence()
        {
            Sentence simple = (Sentence)parser.parse("(NOT A)");
            Sentence a = (Sentence)parser.parse("(NOT A)");
            List<Sentence> clauses = gatherer.getClausesFrom(simple);
            Assert.IsNotNull(clauses);
            Assert.AreEqual(1, clauses.Count);
            Assert.IsTrue(clauses.Contains(a));
        }

        [TestMethod]
        public void testSimpleAndClause()
        {
            Sentence simple = (Sentence)parser.parse("(A AND B)");
            Sentence a = (Sentence)parser.parse("A");
            Sentence b = (Sentence)parser.parse("B");
            List<Sentence> clauses = gatherer.getClausesFrom(simple);
            Assert.AreEqual(2, clauses.Count);
            Assert.IsTrue(clauses.Contains(a));
            Assert.IsTrue(clauses.Contains(b));
        }

        [TestMethod]
        public void testMultiAndClause()
        {
            Sentence simple = (Sentence)parser.parse("((A AND B) AND C)");
            List<Sentence> clauses = gatherer.getClausesFrom(simple);
            Assert.AreEqual(3, clauses.Count);
            Sentence a = (Sentence)parser.parse("A");
            Sentence b = (Sentence)parser.parse("B");
            Sentence c = (Sentence)parser.parse("C");
            Assert.IsTrue(clauses.Contains(a));
            Assert.IsTrue(clauses.Contains(b));
            Assert.IsTrue(clauses.Contains(c));
        }

        [TestMethod]
        public void testMultiAndClause2()
        {
            Sentence simple = (Sentence)parser.parse("(A AND (B AND C))");
            List<Sentence> clauses = gatherer.getClausesFrom(simple);
            Assert.AreEqual(3, clauses.Count);
            Sentence a = (Sentence)parser.parse("A");
            Sentence b = (Sentence)parser.parse("B");
            Sentence c = (Sentence)parser.parse("C");
            Assert.IsTrue(clauses.Contains(a));
            Assert.IsTrue(clauses.Contains(b));
            Assert.IsTrue(clauses.Contains(c));
        }

        [TestMethod]
        public void testAimaExample()
        {
            Sentence aimaEg = (Sentence)parser.parse("( B11 <=> (P12 OR P21))");
            CNFTransformer transformer = new CNFTransformer();
            Sentence transformed = transformer.transform(aimaEg);
            List<Sentence> clauses = gatherer.getClausesFrom(transformed);
            Sentence clause1 = (Sentence)parser.parse("( B11 OR  ( NOT P12 )  )");
            Sentence clause2 = (Sentence)parser.parse("( B11 OR  ( NOT P21 )  )");
            Sentence clause3 = (Sentence)parser
                    .parse("(  ( NOT B11 )  OR  ( P12 OR P21 ) )");
            Assert.AreEqual(3, clauses.Count);
            Assert.IsTrue(clauses.Contains(clause1));
            Assert.IsTrue(clauses.Contains(clause2));
            Assert.IsTrue(clauses.Contains(clause3));
        }
    }
}