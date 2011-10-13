namespace AIMA.Test.Core.Unit.Logic.Fol.Inference
{

    using AIMA.Core.Logic.FOL.Domain;
    using AIMA.Core.Logic.FOL.Inference;
    using AIMA.Core.Logic.FOL.KB.Data;
    using AIMA.Core.Logic.FOL.Parsing;
    using AIMA.Core.Logic.FOL.Parsing.AST;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    [TestClass]
    public class DemodulationTest
    {

        private Demodulation demodulation = null;

        [TestInitialize]
        public void setUp()
        {
            demodulation = new Demodulation();
        }

        // Note: Based on:
        // http://logic.stanford.edu/classes/cs157/2008/lectures/lecture15.pdf
        // Slide 22.
        [TestMethod]
        public void testSimpleAtomicExamples()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addConstant("C");
            domain.addConstant("D");
            domain.addConstant("E");
            domain.addPredicate("P");
            domain.addFunction("F");
            domain.addFunction("G");
            domain.addFunction("H");
            domain.addFunction("J");

            FOLParser parser = new FOLParser(domain);

            Predicate expression = (Predicate)parser
                    .parse("P(A,F(B,G(A,H(B)),C),D)");
            TermEquality assertion = (TermEquality)parser.parse("B = E");

            Predicate altExpression = (Predicate)demodulation.apply(assertion,
                    expression);

            Assert.IsFalse(expression.Equals(altExpression));
            Assert
                    .AreEqual("P(A,F(E,G(A,H(B)),C),D)", altExpression
                            .ToString());

            altExpression = (Predicate)demodulation
                    .apply(assertion, altExpression);

            Assert
                    .AreEqual("P(A,F(E,G(A,H(E)),C),D)", altExpression
                            .ToString());

            assertion = (TermEquality)parser.parse("G(x,y) = J(x)");

            altExpression = (Predicate)demodulation.apply(assertion, expression);

            Assert.AreEqual("P(A,F(B,J(A),C),D)", altExpression.ToString());
        }

        // Note: Based on:
        // http://logic.stanford.edu/classes/cs157/2008/lectures/lecture15.pdf
        // Slide 23.
        [TestMethod]
        public void testSimpleAtomicNonExample()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addConstant("C");
            domain.addConstant("D");
            domain.addConstant("E");
            domain.addPredicate("P");
            domain.addFunction("F");
            domain.addFunction("G");
            domain.addFunction("H");
            domain.addFunction("J");

            FOLParser parser = new FOLParser(domain);

            Predicate expression = (Predicate)parser.parse("P(A,G(x,B),C)");
            TermEquality assertion = (TermEquality)parser.parse("G(A,y) = J(y)");

            Predicate altExpression = (Predicate)demodulation.apply(assertion,
                    expression);

            Assert.IsNull(altExpression);
        }

        [TestMethod]
        public void testSimpleClauseExamples()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addConstant("C");
            domain.addConstant("D");
            domain.addConstant("E");
            domain.addPredicate("P");
            domain.addPredicate("Q");
            domain.addPredicate("W");
            domain.addFunction("F");
            domain.addFunction("G");
            domain.addFunction("H");
            domain.addFunction("J");

            FOLParser parser = new FOLParser(domain);

            List<Literal> lits = new List<Literal>();
            Predicate p1 = (Predicate)parser.parse("Q(z, G(D,B))");
            Predicate p2 = (Predicate)parser.parse("P(x, G(A,C))");
            Predicate p3 = (Predicate)parser.parse("W(z,x,u,w,y)");
            lits.Add(new Literal(p1));
            lits.Add(new Literal(p2));
            lits.Add(new Literal(p3));

            Clause clExpression = new Clause(lits);

            TermEquality assertion = (TermEquality)parser.parse("G(x,y) = x");

            Clause altClExpression = demodulation.apply(assertion, clExpression);

            Assert.AreEqual("[P(x,G(A,C)), Q(z,D), W(z,x,u,w,y)]",
                    altClExpression.ToString());

            altClExpression = demodulation.apply(assertion, altClExpression);

            Assert.AreEqual("[P(x,A), Q(z,D), W(z,x,u,w,y)]", altClExpression
                    .ToString());
        }

        [TestMethod]
        public void testSimpleClauseNonExample()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addConstant("C");
            domain.addPredicate("P");
            domain.addFunction("F");

            FOLParser parser = new FOLParser(domain);

            List<Literal> lits = new List<Literal>();
            Predicate p1 = (Predicate)parser.parse("P(y, F(A,y))");
            lits.Add(new Literal(p1));

            Clause clExpression = new Clause(lits);

            TermEquality assertion = (TermEquality)parser.parse("F(x,B) = C");

            Clause altClExpression = demodulation.apply(assertion, clExpression);

            Assert.IsNull(altClExpression);
        }

        [TestMethod]
        public void testBypassReflexivityAxiom()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addConstant("C");
            domain.addPredicate("P");
            domain.addFunction("F");

            FOLParser parser = new FOLParser(domain);

            List<Literal> lits = new List<Literal>();
            Predicate p1 = (Predicate)parser.parse("P(y, F(A,y))");
            lits.Add(new Literal(p1));

            Clause clExpression = new Clause(lits);

            TermEquality assertion = (TermEquality)parser.parse("x = x");

            Clause altClExpression = demodulation.apply(assertion, clExpression);

            Assert.IsNull(altClExpression);
        }
    }
}