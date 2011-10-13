namespace AIMA.Test.Core.Unit.Logic.FOL.Parsing
{

    using AIMA.Core.Logic.FOL.Domain;
    using AIMA.Core.Logic.FOL.Parsing;
    using AIMA.Core.Logic.FOL.Parsing.AST;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class FOLParserTest
    {
        FOLLexer lexer;

        FOLParser parser;

        [TestInitialize]
        public void setUp()
        {
            FOLDomain domain = DomainFactory.crusadesDomain();

            lexer = new FOLLexer(domain);
            parser = new FOLParser(lexer);
        }

        [TestMethod]
        public void testParseSimpleVariable()
        {
            parser.setUpToParse("x");
            Term v = parser.parseVariable();
            Assert.AreEqual(v.ToString(), (new Variable("x")).ToString());
        }

        [TestMethod]
        public void testParseIndexedVariable()
        {
            parser.setUpToParse("x1");
            Term v = parser.parseVariable();
            Assert.AreEqual(v.ToString(), (new Variable("x1")).ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void testNotAllowedParseLeadingIndexedVariable()
        {
            parser.setUpToParse("1x");
            parser.parseVariable();
        }

        [TestMethod]
        public void testParseSimpleConstant()
        {
            parser.setUpToParse("John");
            Term c = parser.parseConstant();
            Assert.AreEqual(c.ToString(), (new Constant("John")).ToString());
        }

        [TestMethod]
        public void testParseFunction()
        {
            parser.setUpToParse("BrotherOf(John)");
            Term f = parser.parseFunction();
            Assert.AreEqual(f.ToString(), getBrotherOfFunction(new Constant("John")).ToString());
        }

        [TestMethod]
        public void testParseMultiArityFunction()
        {
            parser.setUpToParse("LegsOf(John,Saladin,Richard)");
            Term f = parser.parseFunction();
            Assert.AreEqual(f.ToString(), getLegsOfFunction().ToString());
            Assert.AreEqual(3, ((Function)f).getTerms().Count);
        }

        [TestMethod]
        public void testPredicate()
        {
            // parser.setUpToParse("King(John)");
            Predicate p = (Predicate)parser.parse("King(John)");
            Assert.AreEqual(p.ToString(), getKingPredicate(new Constant("John")).ToString());
        }

        [TestMethod]
        public void testTermEquality()
        {
            try
            {
                TermEquality te = (TermEquality)parser
                        .parse("BrotherOf(John) = EnemyOf(Saladin)");
                Assert.AreEqual(te.ToString(), (new TermEquality(
                        getBrotherOfFunction(new Constant("John")),
                        getEnemyOfFunction())).ToString());
            }
            catch (ApplicationException)
            {
                Assert.Fail("RuntimeException thrown");
            }
        }

        [TestMethod]
        public void testTermEquality2()
        {
            try
            {
                TermEquality te = (TermEquality)parser
                        .parse("BrotherOf(John) = x)");
                Assert.AreEqual(te.ToString(), (new TermEquality(
                        getBrotherOfFunction(new Constant("John")), new Variable(
                                "x"))).ToString());
            }
            catch (ApplicationException)
            {
                Assert.Fail("RuntimeException thrown");
            }
        }

        [TestMethod]
        public void testNotSentence()
        {
            NotSentence ns = (NotSentence)parser
                    .parse("NOT BrotherOf(John) = EnemyOf(Saladin)");
            Assert.AreEqual(ns.getNegated().ToString(), (new TermEquality(
                    getBrotherOfFunction(new Constant("John")),
                    getEnemyOfFunction())).ToString());
        }

        [TestMethod]
        public void testSimpleParanthizedSentence()
        {
            Sentence ps = parser.parse("(NOT King(John))");
            Assert.AreEqual(ps.ToString(), (new NotSentence(getKingPredicate(new Constant(
                    "John")))).ToString());
        }

        [TestMethod]
        public void testExtraParanthizedSentence()
        {
            Sentence ps = parser.parse("(((NOT King(John))))");
            Assert.AreEqual(ps.ToString(), (new NotSentence(getKingPredicate(new Constant(
                    "John")))).ToString());
        }

        [TestMethod]
        public void testParseComplexParanthizedSentence()
        {
            Sentence ps = parser.parse("(NOT BrotherOf(John) = EnemyOf(Saladin))");
            Assert.AreEqual(ps.ToString(), (new NotSentence(new TermEquality(
                    getBrotherOfFunction(new Constant("John")),
                    getEnemyOfFunction()))).ToString());
        }

        [TestMethod]
        public void testParseSimpleConnectedSentence()
        {
            Sentence ps = parser.parse("(King(John) AND NOT King(Richard))");

            Assert.AreEqual(ps.ToString(), (new ConnectedSentence("AND",
                    getKingPredicate(new Constant("John")), new NotSentence(
                            getKingPredicate(new Constant("Richard"))))).ToString());

            ps = parser.parse("(King(John) AND King(Saladin))");
            Assert.AreEqual(ps.ToString(), (new ConnectedSentence("AND",
                    getKingPredicate(new Constant("John")),
                    getKingPredicate(new Constant("Saladin")))).ToString());
        }

        [TestMethod]
        public void testComplexConnectedSentence1()
        {
            Sentence ps = parser
                    .parse("((King(John) AND NOT King(Richard)) OR King(Saladin))");

            Assert.AreEqual(ps.ToString(), (new ConnectedSentence("OR",
                    new ConnectedSentence("AND", getKingPredicate(new Constant(
                            "John")), new NotSentence(
                            getKingPredicate(new Constant("Richard")))),
                    getKingPredicate(new Constant("Saladin")))).ToString());
        }

        [TestMethod]
        public void testQuantifiedSentenceWithSingleVariable()
        {
            Sentence qs = parser.parse("FORALL x  King(x)");
            List<Variable> vars = new List<Variable>();
            vars.Add(new Variable("x"));
            Assert.AreEqual(qs.ToString(), (new QuantifiedSentence("FORALL", vars,
                    getKingPredicate(new Variable("x")))).ToString());
        }

        [TestMethod]
        public void testQuantifiedSentenceWithTwoVariables()
        {
            Sentence qs = parser
                    .parse("EXISTS x,y  (King(x) AND BrotherOf(x) = y)");
            List<Variable> vars = new List<Variable>();
            vars.Add(new Variable("x"));
            vars.Add(new Variable("y"));
            ConnectedSentence cse = new ConnectedSentence("AND",
                    getKingPredicate(new Variable("x")), new TermEquality(
                            getBrotherOfFunction(new Variable("x")), new Variable(
                                    "y")));
            Assert.AreEqual(qs.ToString(), (new QuantifiedSentence("EXISTS", vars, cse)).ToString());
        }

        [TestMethod]
        public void testQuantifiedSentenceWithPathologicalParanthising()
        {
            Sentence qs = parser
                    .parse("(( (EXISTS x,y  (King(x) AND (BrotherOf(x) = y)) ) ))");
            List<Variable> vars = new List<Variable>();
            vars.Add(new Variable("x"));
            vars.Add(new Variable("y"));
            ConnectedSentence cse = new ConnectedSentence("AND",
                    getKingPredicate(new Variable("x")), new TermEquality(
                            getBrotherOfFunction(new Variable("x")), new Variable(
                                    "y")));
            Assert.AreEqual(qs.ToString(), new QuantifiedSentence("EXISTS", vars, cse).ToString());
        }

        [TestMethod]
        public void testParseMultiArityFunctionEquality()
        {
            parser.setUpToParse("LegsOf(John,Saladin,Richard)");
            Term f = parser.parseFunction();

            parser.setUpToParse("LegsOf(John,Saladin,Richard)");
            Term f2 = parser.parseFunction();
            Assert.AreEqual(f.ToString(), f2.ToString());
            Assert.AreEqual(3, ((Function)f).getTerms().Count);
        }

        [TestMethod]
        public void testConnectedImplication()
        {
            parser = new FOLParser(DomainFactory.weaponsDomain());
            parser
                    .parse("((Missile(m) AND Owns(Nono,m)) => Sells(West , m ,Nono))");
        }

        //
        // PRIVATE METHODS
        //
        private Function getBrotherOfFunction(Term t)
        {
            List<Term> l = new List<Term>();
            l.Add(t);
            return new Function("BrotherOf", l);
        }

        private Function getEnemyOfFunction()
        {
            List<Term> l = new List<Term>();
            l.Add(new Constant("Saladin"));
            return new Function("EnemyOf", l);
        }

        private Function getLegsOfFunction()
        {
            List<Term> l = new List<Term>();
            l.Add(new Constant("John"));
            l.Add(new Constant("Saladin"));
            l.Add(new Constant("Richard"));
            return new Function("LegsOf", l);
        }

        private Predicate getKingPredicate(Term t)
        {
            List<Term> l = new List<Term>();
            l.Add(t);
            return new Predicate("King", l);
        }
    }
}