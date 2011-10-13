namespace AIMA.Test.Core.Unit.Logic.Propositional.Algorithms
{

    using AIMA.Core.Logic.Propositional.Algorithms;
    using AIMA.Core.Logic.Propositional.Parsing;
    using AIMA.Core.Logic.Propositional.Parsing.Ast;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class PLResolutionTest
    {
        private PLResolution resolution;

        private PEParser parser;

        [TestInitialize]
        public void setUp()
        {
            resolution = new PLResolution();
            parser = new PEParser();
        }

        [TestMethod]
        public void testPLResolveWithOneLiteralMatching()
        {
            Sentence one = (Sentence)parser.parse("(A OR B)");
            Sentence two = (Sentence)parser.parse("((NOT B) OR C)");
            Sentence expected = (Sentence)parser.parse("(A OR C)");
            List<Sentence> resolvents = resolution.plResolve(one, two);
            Assert.AreEqual(1, resolvents.Count);
            Assert.IsTrue(resolvents.Contains(expected));
        }

        [TestMethod]
        public void testPLResolveWithNoLiteralMatching()
        {
            Sentence one = (Sentence)parser.parse("(A OR B)");
            Sentence two = (Sentence)parser.parse("(C OR D)");
            List<Sentence> resolvents = resolution.plResolve(one, two);
            Assert.AreEqual(0, resolvents.Count);
        }

        [TestMethod]
        public void testPLResolveWithOneLiteralSentencesMatching()
        {
            Sentence one = (Sentence)parser.parse("A");
            Sentence two = (Sentence)parser.parse("(NOT A)");
            // Sentence expected =(Sentence) parser.parse("(A OR C)");
            List<Sentence> resolvents = resolution.plResolve(one, two);
            Assert.AreEqual(1, resolvents.Count);
            Assert.IsTrue(resolvents.Contains(new Symbol("EMPTY_CLAUSE")));
        }

        [TestMethod]
        public void testPLResolveWithTwoLiteralsMatching()
        {
            Sentence one = (Sentence)parser.parse("((NOT P21) OR B11)");
            Sentence two = (Sentence)parser.parse("(((NOT B11) OR P21) OR P12)");
            Sentence expected1 = (Sentence)parser
                    .parse("(  ( P12 OR P21 ) OR  ( NOT P21 )  )");
            Sentence expected2 = (Sentence)parser
                    .parse("(  ( B11 OR P12 ) OR  ( NOT B11 )  )");
            List<Sentence> resolvents = resolution.plResolve(one, two);

            Assert.AreEqual(2, resolvents.Count);
            Assert.IsTrue(resolvents.Contains(expected1));
            Assert.IsTrue(resolvents.Contains(expected2));
        }

        [TestMethod]
        public void testPLResolve1()
        {
            bool b = resolution.plResolution("((B11 =>  (NOT P11)) AND B11)",
                    "(P11)");
            Assert.AreEqual(false, b);
        }

        [TestMethod]
        public void testPLResolve2()
        {
            bool b = resolution.plResolution("(A AND B)", "B");
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void testPLResolve3()
        {
            bool b = resolution.plResolution("((B11 =>  (NOT P11)) AND B11)",
                    "(NOT P11)");
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void testPLResolve4()
        {
            bool b = resolution.plResolution("(A OR B)", "B");
            Assert.AreEqual(false, b);
        }

        [TestMethod]
        public void testPLResolve5()
        {
            bool b = resolution.plResolution("((B11 =>  (NOT P11)) AND B11)",
                    "(NOT B11)");
            Assert.AreEqual(false, b);
        }

        [TestMethod]
        public void testMultipleClauseResolution()
        {
            // test (and fix) suggested by Huy Dinh. Thanks Huy!
            PLResolution plr = new PLResolution();
            KnowledgeBase kb = new KnowledgeBase();
            String fact = "((B11 <=> (P12 OR P21)) AND (NOT B11))";
            kb.tell(fact);
            plr.plResolution(kb, "(B)");
        }

        // public void testPLResolutionWithChadCarfBugReportData() {
        // commented out coz this needs a major fix wait for a rewrite
        // KnowledgeBase kb = new KnowledgeBase();
        // kb.tell("(B12 <=> (P11 OR (P13 OR (P22 OR P02))))");
        // kb.tell("(B21 <=> (P20 OR (P22 OR (P31 OR P11))))");
        // kb.tell("(B01 <=> (P00 OR (P02 OR P11)))");
        // kb.tell("(B10 <=> (P11 OR (P20 OR P00)))");
        // kb.tell("(NOT B21)");
        // kb.tell("(NOT B12)");
        // kb.tell("(B10)");
        // kb.tell("(B01)");
        // IsTrue(resolution.plResolution(kb.asSentence().ToString(), "(P00)"));
        // //IsFalse(kb.askWithDpll("(NOT P00)"));
        //		
        //		
        // }
    }
}