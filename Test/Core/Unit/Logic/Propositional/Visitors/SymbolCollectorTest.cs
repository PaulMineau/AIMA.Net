namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.Propositional.Visitors
{


    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Visitors;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class SymbolCollectorTest
    {
        private PEParser parser;

        private SymbolCollector collector;

        [TestInitialize]
        public void setUp()
        {
            parser = new PEParser();
            collector = new SymbolCollector();
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
        public void testCollectSymbolsFromComplexSentence()
        {
            Sentence sentence = (Sentence)parser
                    .parse(" (  (  ( NOT B11 )  OR  ( P12 OR P21 ) ) AND  (  ( B11 OR  ( NOT P12 )  ) AND  ( B11 OR  ( NOT P21 )  ) ) )");
            List<Symbol> s = collector.getSymbolsIn(sentence);
            Assert.AreEqual(3, s.Count);
            Sentence b11 = (Sentence)parser.parse("B11");
            Sentence p21 = (Sentence)parser.parse("P21");
            Sentence p12 = (Sentence)parser.parse("P12");
            Assert.IsTrue(FindSentence(s, b11));
            Assert.IsTrue(FindSentence(s, p21));
            Assert.IsTrue(FindSentence(s, p12));
        }
    }
}