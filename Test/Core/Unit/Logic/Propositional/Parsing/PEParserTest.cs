namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.Propositional.Parsing
{



    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class PEParserTest
    {
        private PEParser parser;

        [TestInitialize]
        public void setUp()
        {
            parser = new PEParser();
        }

        [TestMethod]
        public void testAtomicSentenceTrueParse()
        {
            AtomicSentence sen = (AtomicSentence)parser.parse("true");
            Assert.AreEqual(typeof(TrueSentence), sen.GetType());
            sen = (AtomicSentence)parser.parse("(true)");
            Assert.AreEqual(typeof(TrueSentence), sen.GetType());
            sen = (AtomicSentence)parser.parse("((true))");
            Assert.AreEqual(typeof(TrueSentence), sen.GetType());
        }

        [TestMethod]
        public void testAtomicSentenceFalseParse()
        {
            AtomicSentence sen = (AtomicSentence)parser.parse("faLse");
            Assert.AreEqual(typeof(FalseSentence), sen.GetType());
        }

        [TestMethod]
        public void testAtomicSentenceSymbolParse()
        {
            AtomicSentence sen = (AtomicSentence)parser.parse("AIMA");
            Assert.AreEqual(typeof(Symbol), sen.GetType());
        }

        [TestMethod]
        public void testNotSentenceParse()
        {
            UnarySentence sen = (UnarySentence)parser.parse("NOT AIMA");
            Assert.AreEqual(typeof(UnarySentence), sen.GetType());
        }

        [TestMethod]
        public void testBinarySentenceParse()
        {
            BinarySentence sen = (BinarySentence)parser
                    .parse("(PETER  AND  NORVIG)");
            Assert.AreEqual(typeof(BinarySentence), sen.GetType());
        }

        [TestMethod]
        public void testMultiSentenceAndParse()
        {
            MultiSentence sen = (MultiSentence)parser
                    .parse("(AND  NORVIG AIMA LISP)");
            Assert.AreEqual(typeof(MultiSentence), sen.GetType());
        }

        [TestMethod]
        public void testMultiSentenceOrParse()
        {
            MultiSentence sen = (MultiSentence)parser
                    .parse("(OR  NORVIG AIMA LISP)");
            Assert.AreEqual(typeof(MultiSentence), sen.GetType());
        }

        [TestMethod]
        public void testMultiSentenceBracketedParse()
        {
            MultiSentence sen = (MultiSentence)parser
                    .parse("((OR  NORVIG AIMA LISP))");
            Assert.AreEqual(typeof(MultiSentence), sen.GetType());
        }

        [TestMethod]
        public void testComplexSentenceParse()
        {
            BinarySentence sen = (BinarySentence)parser
                    .parse("((OR  NORVIG AIMA LISP) AND TRUE)");
            Assert.AreEqual(typeof(BinarySentence), sen.GetType());

            sen = (BinarySentence)parser
                    .parse("((OR  NORVIG AIMA LISP) AND (((LISP => COOL))))");
            Assert.AreEqual(typeof(BinarySentence), sen.GetType());
            Assert.AreEqual(
                    " ( ( OR NORVIG AIMA LISP  )  AND  ( LISP => COOL ) )", sen
                            .ToString());

            String s = "((NOT (P AND Q ))  AND ((NOT (R AND S))))";
            sen = (BinarySentence)parser.parse(s);
            Assert.AreEqual(
                    " (  ( NOT  ( P AND Q ) )  AND  ( NOT  ( R AND S ) )  )", sen
                            .ToString());

            s = "((P AND Q) OR (S AND T))";
            sen = (BinarySentence)parser.parse(s);
            Assert
                    .AreEqual(" (  ( P AND Q ) OR  ( S AND T ) )", sen
                            .ToString());
            Assert.AreEqual("OR", sen.getOperator());

            s = "(NOT ((P AND Q) => (S AND T)))";
            UnarySentence nsen = (UnarySentence)parser.parse(s);
            // AreEqual("=>",sen.getOperator());
            s = "(NOT (P <=> (S AND T)))";
            nsen = (UnarySentence)parser.parse(s);
            Assert.AreEqual(" ( NOT  ( P <=>  ( S AND T ) ) ) ", nsen
                    .ToString());

            s = "(P <=> (S AND T))";
            sen = (BinarySentence)parser.parse(s);

            s = "(P => Q)";
            sen = (BinarySentence)parser.parse(s);

            s = "((P AND Q) => R)";
            sen = (BinarySentence)parser.parse(s);
        }
    }
}