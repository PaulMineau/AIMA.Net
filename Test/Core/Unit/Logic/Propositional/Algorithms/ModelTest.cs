namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.Propositional.Algorithms
{

    using CosmicFlow.AIMA.Core.Logic.Propositional.Algorithms;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;


    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class ModelTest
    {
        private Model m;

        private PEParser parser;

        Sentence trueSentence, falseSentence, andSentence, orSentence,
                impliedSentence, biConditionalSentence;

        [TestInitialize]
        public void setUp()
        {
            parser = new PEParser();
            trueSentence = (Sentence)parser.parse("true");
            falseSentence = (Sentence)parser.parse("false");
            andSentence = (Sentence)parser.parse("(P  AND  Q)");
            orSentence = (Sentence)parser.parse("(P  OR  Q)");
            impliedSentence = (Sentence)parser.parse("(P  =>  Q)");
            biConditionalSentence = (Sentence)parser.parse("(P  <=>  Q)");
            m = new Model();
        }

        [TestMethod]
        public void testEmptyModel()
        {
            Assert.AreEqual(null, m.getStatus(new Symbol("P")));
            Assert.AreEqual(true, m.isUnknown(new Symbol("P")));
        }

        [TestMethod]
        public void testExtendModel()
        {
            String p = "P";
            m = m.extend(new Symbol(p), true);
            Assert.AreEqual(true, m.getStatus(new Symbol("P")));
        }

        [TestMethod]
        public void testTrueFalseEvaluation()
        {
            Assert.AreEqual(true, m.isTrue(trueSentence));
            Assert.AreEqual(false, m.isFalse(trueSentence));
            Assert.AreEqual(false, m.isTrue(falseSentence));
            Assert.AreEqual(true, m.isFalse(falseSentence));
        }

        [TestMethod]
        public void testSentenceStatusWhenPTrueAndQTrue()
        {
            String p = "P";
            String q = "Q";
            m = m.extend(new Symbol(p), true);
            m = m.extend(new Symbol(q), true);
            Assert.AreEqual(true, m.isTrue(andSentence));
            Assert.AreEqual(true, m.isTrue(orSentence));
            Assert.AreEqual(true, m.isTrue(impliedSentence));
            Assert.AreEqual(true, m.isTrue(biConditionalSentence));
        }

        [TestMethod]
        public void testSentenceStatusWhenPFalseAndQFalse()
        {
            String p = "P";
            String q = "Q";
            m = m.extend(new Symbol(p), false);
            m = m.extend(new Symbol(q), false);
            Assert.AreEqual(true, m.isFalse(andSentence));
            Assert.AreEqual(true, m.isFalse(orSentence));
            Assert.AreEqual(true, m.isTrue(impliedSentence));
            Assert.AreEqual(true, m.isTrue(biConditionalSentence));
        }

        [TestMethod]
        public void testSentenceStatusWhenPTrueAndQFalse()
        {
            String p = "P";
            String q = "Q";
            m = m.extend(new Symbol(p), true);
            m = m.extend(new Symbol(q), false);
            Assert.AreEqual(true, m.isFalse(andSentence));
            Assert.AreEqual(true, m.isTrue(orSentence));
            Assert.AreEqual(true, m.isFalse(impliedSentence));
            Assert.AreEqual(true, m.isFalse(biConditionalSentence));
        }

        [TestMethod]
        public void testSentenceStatusWhenPFalseAndQTrue()
        {
            String p = "P";
            String q = "Q";
            m = m.extend(new Symbol(p), false);
            m = m.extend(new Symbol(q), true);
            Assert.AreEqual(true, m.isFalse(andSentence));
            Assert.AreEqual(true, m.isTrue(orSentence));
            Assert.AreEqual(true, m.isTrue(impliedSentence));
            Assert.AreEqual(true, m.isFalse(biConditionalSentence));
        }

        [TestMethod]
        public void testComplexSentence()
        {
            String p = "P";
            String q = "Q";
            m = m.extend(new Symbol(p), true);
            m = m.extend(new Symbol(q), false);
            Sentence sent = (Sentence)parser.parse("((P OR Q) AND  (P => Q))");
            Assert.IsFalse(m.isTrue(sent));
            Assert.IsTrue(m.isFalse(sent));
            Sentence sent2 = (Sentence)parser.parse("((P OR Q) AND  (Q))");
            Assert.IsFalse(m.isTrue(sent2));
            Assert.IsTrue(m.isFalse(sent2));
        }
    }
}