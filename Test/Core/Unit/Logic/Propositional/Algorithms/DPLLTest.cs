namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.Propositional.Algorithms
{

    using CosmicFlow.AIMA.Core.Logic.Propositional.Algorithms;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Visitors;
    using CosmicFlow.AIMA.Core.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class DPLLTest
    {

        private DPLL dpll;

        private PEParser parser;

        [TestInitialize]
        public void setUp()
        {
            parser = new PEParser();
            dpll = new DPLL();
        }

        [TestMethod]
        public void testDPLLReturnsTrueWhenAllClausesTrueInModel()
        {
            Model model = new Model();
            model = model.extend(new Symbol("A"), true).extend(new Symbol("B"),
                    true);
            Sentence sentence = (Sentence)parser.parse("((A AND B) AND (A OR B))");
            bool satisfiable = dpll.dpllSatisfiable(sentence, model);
            Assert.AreEqual(true, satisfiable);
        }

        [TestMethod]
        public void testDPLLReturnsFalseWhenOneClauseFalseInModel()
        {
            Model model = new Model();
            model = model.extend(new Symbol("A"), true).extend(new Symbol("B"),
                    false);
            Sentence sentence = (Sentence)parser.parse("((A OR B) AND (A => B))");
            bool satisfiable = dpll.dpllSatisfiable(sentence, model);
            Assert.AreEqual(false, satisfiable);
        }

        [TestMethod]
        public void testDPLLFiltersClausesTheStatusOfWhichAreKnown()
        {
            Model model = new Model();
            model = model.extend(new Symbol("A"), true).extend(new Symbol("B"),
                    true);
            Sentence sentence = (Sentence)parser
                    .parse("((A AND B) AND (B AND C))");
            List<Sentence> clauseList = new CNFClauseGatherer()
                            .getClausesFrom(new CNFTransformer()
                                    .transform(sentence));
            List<Sentence> clausesWithNonTrueValues = dpll
                    .clausesWithNonTrueValues(clauseList, model);
            Assert.AreEqual(1, clausesWithNonTrueValues.Count);
            Sentence nonTrueClause = (Sentence)parser.parse("(B AND C)");
            clausesWithNonTrueValues.Contains(nonTrueClause);
        }

        [TestMethod]
        public void testDPLLFilteringNonTrueClausesGivesNullWhenAllClausesAreKnown()
        {
            Model model = new Model();
            model = model.extend(new Symbol("A"), true).extend(new Symbol("B"),
                    true).extend(new Symbol("C"), true);
            Sentence sentence = (Sentence)parser
                    .parse("((A AND B) AND (B AND C))");
            List<Sentence> clauseList = new CNFClauseGatherer()
                            .getClausesFrom(new CNFTransformer()
                                    .transform(sentence));
            List<Sentence> clausesWithNonTrueValues = dpll
                    .clausesWithNonTrueValues(clauseList, model);
            Assert.AreEqual(0, clausesWithNonTrueValues.Count);
        }

        [TestMethod]
        public void testDPLLFindsPurePositiveSymbolsWhenTheyExist()
        {
            Model model = new Model();
            model = model.extend(new Symbol("A"), true).extend(new Symbol("B"),
                    true);
            Sentence sentence = (Sentence)parser
                    .parse("((A AND B) AND (B AND C))");
            List<Sentence> clauseList = new CNFClauseGatherer()
                            .getClausesFrom(new CNFTransformer()
                                    .transform(sentence));
            List<Symbol> symbolList = new SymbolCollector().getSymbolsIn(sentence);

            DPLL.SymbolValuePair sv = dpll.findPureSymbolValuePair(clauseList,
                    model, symbolList);
            Assert.IsNotNull(sv);
            Assert.AreEqual(new Symbol("C"), sv.symbol);
            Assert.AreEqual(true, sv.value);
        }

        [TestMethod]
        public void testDPLLFindsPureNegativeSymbolsWhenTheyExist()
        {
            Model model = new Model();
            model = model.extend(new Symbol("A"), true).extend(new Symbol("B"),
                    true);
            Sentence sentence = (Sentence)parser
                    .parse("((A AND B) AND ( B  AND (NOT C) ))");
            List<Sentence> clauseList = new CNFClauseGatherer()
                            .getClausesFrom(new CNFTransformer()
                                    .transform(sentence));
            List<Symbol> symbolList = new SymbolCollector().getSymbolsIn(sentence);

            DPLL.SymbolValuePair sv = dpll.findPureSymbolValuePair(clauseList,
                    model, symbolList);
            Assert.IsNotNull(sv);
            Assert.AreEqual(new Symbol("C"), sv.symbol);
            Assert.AreEqual(false, sv.value);
        }

        [TestMethod]
        public void testDPLLSucceedsWithAandNotA()
        {
            Sentence sentence = (Sentence)parser.parse("(A AND (NOT A))");
            bool satisfiable = dpll.dpllSatisfiable(sentence);
            Assert.AreEqual(false, satisfiable);
        }

        [TestMethod]
        public void testDPLLSucceedsWithChadCarffsBugReport()
        {
            KnowledgeBase kb = new KnowledgeBase();
            kb.tell("(B12 <=> (P11 OR (P13 OR (P22 OR P02))))");
            kb.tell("(B21 <=> (P20 OR (P22 OR (P31 OR P11))))");
            kb.tell("(B01 <=> (P00 OR (P02 OR P11)))");
            kb.tell("(B10 <=> (P11 OR (P20 OR P00)))");
            kb.tell("(NOT B21)");
            kb.tell("(NOT B12)");
            kb.tell("(B10)");
            kb.tell("(B01)");
            Assert.IsTrue(kb.askWithDpll("(P00)"));
            Assert.IsFalse(kb.askWithDpll("(NOT P00)"));
        }

        [TestMethod]
        public void testDPLLSucceedsWithStackOverflowBugReport1()
        {
            Sentence sentence = (Sentence)parser
                    .parse("((A OR (NOT A)) AND (A OR B))");
            Assert.IsTrue(dpll.dpllSatisfiable(sentence));
        }

        [TestMethod]
        public void testDPLLSucceedsWithChadCarffsBugReport2()
        {
            KnowledgeBase kb = new KnowledgeBase();
            kb.tell("(B10 <=> (P11 OR (P20 OR P00)))");
            kb.tell("(B01 <=> (P00 OR (P02 OR P11)))");
            kb.tell("(B21 <=> (P20 OR (P22 OR (P31 OR P11))))");
            kb.tell("(B12 <=> (P11 OR (P13 OR (P22 OR P02))))");
            kb.tell("(NOT B21)");
            kb.tell("(NOT B12)");
            kb.tell("(B10)");
            kb.tell("(B01)");
            Assert.IsTrue(kb.askWithDpll("(P00)"));
            Assert.IsFalse(kb.askWithDpll("(NOT P00)"));
        }

        [TestMethod]
        public void testDoesNotKnow()
        {
            KnowledgeBase kb = new KnowledgeBase();
            kb.tell("A");

            Assert.IsFalse(kb.askWithDpll("B"));
            Assert.IsFalse(kb.askWithDpll("(NOT B)"));
        }
    }
}