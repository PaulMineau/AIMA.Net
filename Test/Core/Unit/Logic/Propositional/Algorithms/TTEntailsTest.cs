namespace AIMA.Test.Core.Unit.Logic.Propositional.Algorithms
{

    using AIMA.Core.Logic.Propositional.Algorithms;
    using AIMA.Core.Logic.Propositional.Parsing.Ast;
    using Microsoft.VisualStudio.TestTools.UnitTesting;


    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class TTEntailsTest
    {
        TTEntails tte;

        KnowledgeBase kb;

        [TestInitialize]
        public void setUp()
        {
            tte = new TTEntails();
            kb = new KnowledgeBase();
        }

        [TestMethod]
        public void testSimpleSentence1()
        {
            kb.tell("(A AND B)");
            Assert.AreEqual(true, kb.askWithTTEntails("A"));
        }

        [TestMethod]
        public void testSimpleSentence2()
        {
            kb.tell("(A OR B)");
            Assert.AreEqual(false, kb.askWithTTEntails("A"));
        }

        [TestMethod]
        public void testSimpleSentence3()
        {
            kb.tell("((A => B) AND A)");
            Assert.AreEqual(true, kb.askWithTTEntails("B"));
        }

        [TestMethod]
        public void testSimpleSentence4()
        {
            kb.tell("((A => B) AND B)");
            Assert.AreEqual(false, kb.askWithTTEntails("A"));
        }

        [TestMethod]
        public void testSimpleSentence5()
        {
            kb.tell("A");
            Assert.AreEqual(false, kb.askWithTTEntails("NOT A"));
        }

        [TestMethod]
        public void testSUnkownSymbol()
        {
            kb.tell("((A => B) AND B)");
            Assert.AreEqual(false, kb.askWithTTEntails("X"));
        }

        [TestMethod]
        public void testSimpleSentence6()
        {
            kb.tell("NOT A");
            Assert.AreEqual(false, kb.askWithTTEntails("A"));
        }

        [TestMethod]
        public void testNewAIMAExample()
        {
            kb.tell("(NOT P11)");
            kb.tell("(B11 <=> (P12 OR P21))");
            kb.tell("(B21 <=> ((P11 OR P22) OR P31))");
            kb.tell("(NOT B11)");
            kb.tell("(B21)");

            Assert.AreEqual(true, kb.askWithTTEntails("NOT P12"));
            Assert.AreEqual(false, kb.askWithTTEntails("P22"));
        }

        [TestMethod]
        public void testTTEntailsSucceedsWithChadCarffsBugReport()
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

            Assert.IsTrue(kb.askWithTTEntails("(P00)"));
            Assert.IsFalse(kb.askWithTTEntails("(NOT P00)"));
        }

        [TestMethod]
        public void testDoesNotKnow()
        {
            KnowledgeBase kb = new KnowledgeBase();
            kb.tell("A");
            Assert.IsFalse(kb.askWithTTEntails("B"));
            Assert.IsFalse(kb.askWithTTEntails("(NOT B)"));
        }

        // public void testTTEntailsSucceedsWithCStackOverFlowBugReport() {
        // KnowledgeBase kb = new KnowledgeBase();
        //
        // IsTrue(kb.askWithTTEntails("((A OR (NOT A)) AND (A OR B))"));
        // }

        [TestMethod]
        public void testModelEvaluation()
        {
            kb.tell("(NOT P11)");
            kb.tell("(B11 <=> (P12 OR P21))");
            kb.tell("(B21 <=> ((P11 OR P22) OR P31))");
            kb.tell("(NOT B11)");
            kb.tell("(B21)");

            Model model = new Model();
            model = model.extend(new Symbol("B11"), false);
            model = model.extend(new Symbol("B21"), true);
            model = model.extend(new Symbol("P11"), false);
            model = model.extend(new Symbol("P12"), false);
            model = model.extend(new Symbol("P21"), false);
            model = model.extend(new Symbol("P22"), false);
            model = model.extend(new Symbol("P31"), true);

            Sentence kbs = kb.asSentence();
            Assert.AreEqual(true, model.isTrue(kbs));
        }
    }
}