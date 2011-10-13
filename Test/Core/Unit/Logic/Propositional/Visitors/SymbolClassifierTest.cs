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
    public class SymbolClassifierTest
    {
        private SymbolClassifier classifier;

        private PEParser parser;

        [TestInitialize]
        public void setUp()
        {
            classifier = new SymbolClassifier();
            parser = new PEParser();

        }

        [TestMethod]
        public void testSimpleNegativeSymbol()
        {
            Sentence sentence = (Sentence)parser.parse("(NOT B)");

            List<Symbol> neg = classifier.getNegativeSymbolsIn(sentence);
            List<Symbol> pos = classifier.getPositiveSymbolsIn(sentence);

            List<Symbol> pureNeg = classifier.getPureNegativeSymbolsIn(sentence);
            List<Symbol> purePos = classifier.getPurePositiveSymbolsIn(sentence);

            List<Symbol> pure = classifier.getPureSymbolsIn(sentence);
            List<Symbol> impure = classifier.getImpureSymbolsIn(sentence);

            Sentence b = (Sentence)parser.parse("B");

            Assert.AreEqual(1, neg.Count);
            Assert.IsTrue(FindSentence(neg, b));

            Assert.AreEqual(0, pos.Count);

            Assert.AreEqual(1, pureNeg.Count);
            Assert.IsTrue(FindSentence(pureNeg, b));

            Assert.AreEqual(0, purePos.Count);

            Assert.AreEqual(1, pure.Count);
            Assert.IsTrue(FindSentence(pure, b));

            Assert.AreEqual(0, impure.Count);
        }

        private bool FindSentence(List<Symbol> symbols, Sentence s)
        {
            foreach (Symbol symbol in symbols)
            {
                if (symbol is Sentence && symbol.Equals(s))
                {
                    return true;
                }
            }
            return false;
        }

        [TestMethod]
        public void testSimplePositiveSymbol()
        {
            Sentence sentence = (Sentence)parser.parse("B");
            List<Symbol> neg = classifier.getNegativeSymbolsIn(sentence);
            List<Symbol> pos = classifier.getPositiveSymbolsIn(sentence);

            List<Symbol> pureNeg = classifier.getPureNegativeSymbolsIn(sentence);
            List<Symbol> purePos = classifier.getPurePositiveSymbolsIn(sentence);

            List<Symbol> pure = classifier.getPureSymbolsIn(sentence);
            List<Symbol> impure = classifier.getImpureSymbolsIn(sentence);

            Assert.AreEqual(0, neg.Count);

            Assert.AreEqual(1, pos.Count);
            Sentence b = (Sentence)parser.parse("B");
            Assert.IsTrue(FindSentence(pos, b));

            Assert.AreEqual(1, purePos.Count);
            Assert.IsTrue(FindSentence(purePos, b));

            Assert.AreEqual(0, pureNeg.Count);
            Assert.AreEqual(1, pure.Count);

            Assert.IsTrue(FindSentence(pure, b));

            Assert.AreEqual(0, impure.Count);
        }

        [TestMethod]
        public void testSingleSymbolPositiveAndNegative()
        {
            Sentence sentence = (Sentence)parser.parse("(B AND (NOT B))");
            List<Symbol> neg = classifier.getNegativeSymbolsIn(sentence);
            List<Symbol> pos = classifier.getPositiveSymbolsIn(sentence);

            List<Symbol> pureNeg = classifier.getPureNegativeSymbolsIn(sentence);
            List<Symbol> purePos = classifier.getPurePositiveSymbolsIn(sentence);

            List<Symbol> pure = classifier.getPureSymbolsIn(sentence);
            List<Symbol> impure = classifier.getImpureSymbolsIn(sentence);

            Sentence b = (Sentence)parser.parse("B");

            Assert.AreEqual(1, neg.Count);
            Assert.IsTrue(FindSentence(neg, b));

            Assert.AreEqual(1, pos.Count);
            Assert.IsTrue(FindSentence(pos, b));

            Assert.AreEqual(0, pureNeg.Count);
            Assert.AreEqual(0, purePos.Count);
            Assert.AreEqual(0, pure.Count);
            Assert.AreEqual(1, impure.Count);
        }

        [TestMethod]
        public void testAIMA2eExample()
        {
            // 2nd Edition Pg 221
            Sentence sentence = (Sentence)parser
                    .parse("(((A OR (NOT B)) AND ((NOT B) OR (NOT C))) AND (C OR A))");

            List<Symbol> neg = classifier.getNegativeSymbolsIn(sentence);
            List<Symbol> pos = classifier.getPositiveSymbolsIn(sentence);

            List<Symbol> pureNeg = classifier.getPureNegativeSymbolsIn(sentence);
            List<Symbol> purePos = classifier.getPurePositiveSymbolsIn(sentence);

            List<Symbol> pure = classifier.getPureSymbolsIn(sentence);
            List<Symbol> impure = classifier.getImpureSymbolsIn(sentence);

            Sentence a = (Sentence)parser.parse("A");
            Sentence b = (Sentence)parser.parse("B");
            Sentence c = (Sentence)parser.parse("C");

            Assert.AreEqual(2, neg.Count);
            Assert.IsTrue(FindSentence(neg, b));
            Assert.IsTrue(FindSentence(neg, c));

            Assert.AreEqual(2, pos.Count);
            Assert.IsTrue(FindSentence(pos, a));
            Assert.IsTrue(FindSentence(pos, c));

            Assert.AreEqual(1, pureNeg.Count);
            Assert.IsTrue(FindSentence(pureNeg, b));

            Assert.AreEqual(1, purePos.Count);
            Assert.IsTrue(FindSentence(purePos, a));

            Assert.AreEqual(2, pure.Count);
            Assert.IsTrue(FindSentence(pure, a));
            Assert.IsTrue(FindSentence(pure, b));

            Assert.AreEqual(1, impure.Count);
            Assert.IsTrue(FindSentence(impure, c));
        }
    }
}