namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.FOL
{


    using CosmicFlow.AIMA.Core.Logic.FOL;
    using CosmicFlow.AIMA.Core.Logic.FOL.Domain;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing.AST;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    [TestClass]
    public class UnifierTest
    {

        private FOLParser parser;
        private Unifier unifier;
        private Dictionary<Variable, Term> theta;

        [TestInitialize]
        public void setUp()
        {
            parser = new FOLParser(DomainFactory.knowsDomain());
            unifier = new Unifier();
            theta = new Dictionary<Variable, Term>();
        }

        [TestMethod]
        public void testFailureIfThetaisNull()
        {
            Variable var = new Variable("x");
            Sentence sentence = parser.parse("Knows(x)");
            theta = null;
            Dictionary<Variable, Term> result = unifier.unify(var, sentence, theta);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void testUnificationFailure()
        {
            Variable var = new Variable("x");
            Sentence sentence = parser.parse("Knows(y)");
            theta = null;
            Dictionary<Variable, Term> result = unifier.unify(var, sentence, theta);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void testThetaPassedBackIfXEqualsYBothVariables()
        {
            Variable var1 = new Variable("x");
            Variable var2 = new Variable("x");

            theta.Add(new Variable("dummy"), new Variable("dummy"));
            Dictionary<Variable, Term> result = unifier.unify(var1, var2, theta);
            Assert.AreEqual(theta, result);
            Assert.AreEqual(1, theta.Keys.Count);
            Assert.IsTrue(theta.ContainsKey(new Variable("dummy")));
        }

        [TestMethod]
        public void testVariableEqualsConstant()
        {
            Variable var1 = new Variable("x");
            Constant constant = new Constant("John");

            Dictionary<Variable, Term> result = unifier.unify(var1, constant, theta);
            Assert.AreEqual(theta, result);
            Assert.AreEqual(1, theta.Keys.Count);
            Assert.IsTrue(theta.ContainsKey(var1));
            Assert.AreEqual(constant, theta[var1]);
        }

        [TestMethod]
        public void testSimpleVariableUnification()
        {
            Variable var1 = new Variable("x");
            List<Term> terms1 = new List<Term>();
            terms1.Add(var1);
            Predicate p1 = new Predicate("King", terms1); // King(x)

            List<Term> terms2 = new List<Term>();
            terms2.Add(new Constant("John"));
            Predicate p2 = new Predicate("King", terms2); // King(John)

            Dictionary<Variable, Term> result = unifier.unify(p1, p2, theta);
            Assert.AreEqual(theta, result);
            Assert.AreEqual(1, theta.Keys.Count);
            Assert.IsTrue(theta.ContainsKey(new Variable("x"))); // x =
            Assert.AreEqual(new Constant("John"), theta[var1]); // John
        }

        [TestMethod]
        public void testKnows1()
        {
            Sentence query = parser.parse("Knows(John,x)");
            Sentence johnKnowsJane = parser.parse("Knows(John,Jane)");
            Dictionary<Variable, Term> result = unifier.unify(query, johnKnowsJane, theta);
            Assert.AreEqual(theta, result);
            Assert.IsTrue(theta.ContainsKey(new Variable("x"))); // x =
            Assert.AreEqual(new Constant("Jane"), theta[new Variable("x")]); // Jane
        }

        [TestMethod]
        public void testKnows2()
        {
            Sentence query = parser.parse("Knows(John,x)");
            Sentence johnKnowsJane = parser.parse("Knows(y,Bill)");
            Dictionary<Variable, Term> result = unifier.unify(query, johnKnowsJane, theta);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(new Constant("Bill"), theta[new Variable("x")]); // x
            // =
            // Bill
            Assert.AreEqual(new Constant("John"), theta[new Variable("y")]); // y
            // =
            // John
        }

        [TestMethod]
        public void testKnows3()
        {
            Sentence query = parser.parse("Knows(John,x)");
            Sentence johnKnowsJane = parser.parse("Knows(y,Mother(y))");
            Dictionary<Variable, Term> result = unifier.unify(query, johnKnowsJane, theta);

            Assert.AreEqual(2, result.Count);

            List<Term> terms = new List<Term>();
            terms.Add(new Constant("John"));
            Function mother = new Function("Mother", terms);
            Assert.AreEqual(mother, theta[new Variable("x")]);
            Assert.AreEqual(new Constant("John"), theta[new Variable("y")]);
        }

        [TestMethod]
        public void testKnows5()
        {
            Sentence query = parser.parse("Knows(John,x)");
            Sentence johnKnowsJane = parser.parse("Knows(y,z)");
            Dictionary<Variable, Term> result = unifier.unify(query, johnKnowsJane, theta);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(new Variable("z"), theta[new Variable("x")]); // x
            // =
            // z
            Assert.AreEqual(new Constant("John"), theta[new Variable("y")]); // y
            // =
            // John
        }

        [TestMethod]
        public void testCascadedOccursCheck()
        {
            FOLDomain domain = new FOLDomain();
            domain.addPredicate("P");
            domain.addFunction("F");
            domain.addFunction("SF0");
            domain.addFunction("SF1");
            FOLParser parser = new FOLParser(domain);

            Sentence s1 = parser.parse("P(SF1(v2),v2)");
            Sentence s2 = parser.parse("P(v3,SF0(v3))");
            Dictionary<Variable, Term> result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("P(v1,SF0(v1),SF0(v1),SF0(v1),SF0(v1))");
            s2 = parser.parse("P(v2,SF0(v2),v2,     v3,     v2)");
            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser
                    .parse("P(v1,   F(v2),F(v2),F(v2),v1,      F(F(v1)),F(F(F(v1))),v2)");
            s2 = parser
                    .parse("P(F(v3),v4,   v5,   v6,   F(F(v5)),v4,      F(v3),      F(F(v5)))");
            result = unifier.unify(s1, s2);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void testAdditionalVariableMixtures()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addFunction("F");
            domain.addFunction("G");
            domain.addFunction("H");
            domain.addPredicate("P");

            FOLParser parser = new FOLParser(domain);

            // Test Cascade Substitutions handled correctly
            Sentence s1 = parser.parse("P(z, x)");
            Sentence s2 = parser.parse("P(x, a)");
            Dictionary<Variable, Term> result = unifier.unify(s1, s2);

            Assert.AreEqual("{z=a, x=a}", result.ToString());

            s1 = parser.parse("P(x, z)");
            s2 = parser.parse("P(a, x)");
            result = unifier.unify(s1, s2);

            Assert.AreEqual("{x=a, z=a}", result.ToString());

            s1 = parser.parse("P(w, w, w)");
            s2 = parser.parse("P(x, y, z)");
            result = unifier.unify(s1, s2);

            Assert.AreEqual("{w=z, x=z, y=z}", result.ToString());

            s1 = parser.parse("P(x, y, z)");
            s2 = parser.parse("P(w, w, w)");
            result = unifier.unify(s1, s2);

            Assert.AreEqual("{x=w, y=w, z=w}", result.ToString());

            s1 = parser.parse("P(x, B, F(y))");
            s2 = parser.parse("P(A, y, F(z))");
            result = unifier.unify(s1, s2);

            Assert.AreEqual("{x=A, y=B, z=B}", result.ToString());

            s1 = parser.parse("P(F(x,B), G(y),         F(z,A))");
            s2 = parser.parse("P(y,      G(F(G(w),w)), F(w,z))");
            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("P(F(G(A)), x,    F(H(z,z)), H(y,    G(w)))");
            s2 = parser.parse("P(y,       G(z), F(v     ), H(F(w), x   ))");
            result = unifier.unify(s1, s2);

            Assert.AreEqual(
                    "{y=F(G(A)), x=G(G(A)), v=H(G(A),G(A)), w=G(A), z=G(A)}",
                    result.ToString());
        }

        [TestMethod]
        public void testTermEquality()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addFunction("Plus");

            FOLParser parser = new FOLParser(domain);

            TermEquality te1 = (TermEquality)parser.parse("x = x");
            TermEquality te2 = (TermEquality)parser.parse("x = x");

            // Both term equalities the same,
            // should unify but no substitutions.
            Dictionary<Variable, Term> result = unifier.unify(te1, te2);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            // Different variable names but should unify.
            te1 = (TermEquality)parser.parse("x1 = x1");
            te2 = (TermEquality)parser.parse("x2 = x2");

            result = unifier.unify(te1, te2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("{x1=x2}", result.ToString());

            // Test simple unification with reflexivity axiom
            te1 = (TermEquality)parser.parse("x1 = x1");
            te2 = (TermEquality)parser.parse("Plus(A,B) = Plus(A,B)");

            result = unifier.unify(te1, te2);

            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("{x1=Plus(A,B)}", result.ToString());

            // Test more complex unification with reflexivity axiom
            te1 = (TermEquality)parser.parse("x1 = x1");
            te2 = (TermEquality)parser.parse("Plus(A,B) = Plus(A,z1)");

            result = unifier.unify(te1, te2);

            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("{x1=Plus(A,B), z1=B}", result.ToString());

            // Test reverse of previous unification with reflexivity axiom
            // Should still be the same.
            te1 = (TermEquality)parser.parse("x1 = x1");
            te2 = (TermEquality)parser.parse("Plus(A,z1) = Plus(A,B)");

            result = unifier.unify(te1, te2);

            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("{x1=Plus(A,B), z1=B}", result.ToString());

            // Test with nested terms
            te1 = (TermEquality)parser
                    .parse("Plus(Plus(Plus(A,B),B, A)) = Plus(Plus(Plus(A,B),B, A))");
            te2 = (TermEquality)parser
                    .parse("Plus(Plus(Plus(A,B),B, A)) = Plus(Plus(Plus(A,B),B, A))");

            result = unifier.unify(te1, te2);

            Assert.IsNotNull(result);

            Assert.AreEqual(0, result.Count);

            // Simple term equality unification fails
            te1 = (TermEquality)parser.parse("Plus(A,B) = Plus(B,A)");
            te2 = (TermEquality)parser.parse("Plus(A,B) = Plus(A,B)");

            result = unifier.unify(te1, te2);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void testNOTSentence()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addConstant("C");
            domain.addFunction("Plus");
            domain.addPredicate("P");

            FOLParser parser = new FOLParser(domain);

            Sentence s1 = parser.parse("NOT(P(A))");
            Sentence s2 = parser.parse("NOT(P(A))");

            Dictionary<Variable, Term> result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            s1 = parser.parse("NOT(P(A))");
            s2 = parser.parse("NOT(P(B))");

            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("NOT(P(A))");
            s2 = parser.parse("NOT(P(x))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new Constant("A"), result[new Variable("x")]);
        }

        [TestMethod]
        public void testConnectedSentence()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addConstant("C");
            domain.addFunction("Plus");
            domain.addPredicate("P");

            FOLParser parser = new FOLParser(domain);

            Sentence s1 = parser.parse("(P(A) AND P(B))");
            Sentence s2 = parser.parse("(P(A) AND P(B))");

            Dictionary<Variable, Term> result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            s1 = parser.parse("(P(A) AND P(B))");
            s2 = parser.parse("(P(A) AND P(C))");

            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("(P(A) AND P(B))");
            s2 = parser.parse("(P(A) AND P(x))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new Constant("B"), result[new Variable("x")]);

            s1 = parser.parse("(P(A) OR P(B))");
            s2 = parser.parse("(P(A) OR P(B))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            s1 = parser.parse("(P(A) OR P(B))");
            s2 = parser.parse("(P(A) OR P(C))");

            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("(P(A) OR P(B))");
            s2 = parser.parse("(P(A) OR P(x))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new Constant("B"), result[new Variable("x")]);

            s1 = parser.parse("(P(A) => P(B))");
            s2 = parser.parse("(P(A) => P(B))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            s1 = parser.parse("(P(A) => P(B))");
            s2 = parser.parse("(P(A) => P(C))");

            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("(P(A) => P(B))");
            s2 = parser.parse("(P(A) => P(x))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new Constant("B"), result[new Variable("x")]);

            s1 = parser.parse("(P(A) <=> P(B))");
            s2 = parser.parse("(P(A) <=> P(B))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            s1 = parser.parse("(P(A) <=> P(B))");
            s2 = parser.parse("(P(A) <=> P(C))");

            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("(P(A) <=> P(B))");
            s2 = parser.parse("(P(A) <=> P(x))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new Constant("B"), result[new Variable("x")]);

            s1 = parser.parse("((P(A) AND P(B)) OR (P(C) => (P(A) <=> P(C))))");
            s2 = parser.parse("((P(A) AND P(B)) OR (P(C) => (P(A) <=> P(C))))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            s1 = parser.parse("((P(A) AND P(B)) OR (P(C) => (P(A) <=> P(C))))");
            s2 = parser.parse("((P(A) AND P(B)) OR (P(C) => (P(A) <=> P(A))))");

            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("((P(A) AND P(B)) OR (P(C) => (P(A) <=> P(C))))");
            s2 = parser.parse("((P(A) AND P(B)) OR (P(C) => (P(A) <=> P(x))))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new Constant("C"), result[new Variable("x")]);
        }

        [TestMethod]
        public void testQuantifiedSentence()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addConstant("C");
            domain.addFunction("Plus");
            domain.addPredicate("P");

            FOLParser parser = new FOLParser(domain);

            Sentence s1 = parser
                    .parse("FORALL x,y ((P(x) AND P(A)) OR (P(A) => P(y)))");
            Sentence s2 = parser
                    .parse("FORALL x,y ((P(x) AND P(A)) OR (P(A) => P(y)))");

            Dictionary<Variable, Term> result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            s1 = parser.parse("FORALL x,y ((P(x) AND P(A)) OR (P(A) => P(y)))");
            s2 = parser.parse("FORALL x   ((P(x) AND P(A)) OR (P(A) => P(y)))");

            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("FORALL x,y ((P(x) AND P(A)) OR (P(A) => P(y)))");
            s2 = parser.parse("FORALL x,y ((P(x) AND P(A)) OR (P(B) => P(y)))");

            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("FORALL x,y ((P(x) AND P(A)) OR (P(A) => P(y)))");
            s2 = parser.parse("FORALL x,y ((P(A) AND P(A)) OR (P(A) => P(y)))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new Constant("A"), result[new Variable("x")]);

            //
            s1 = parser.parse("EXISTS x,y ((P(x) AND P(A)) OR (P(A) => P(y)))");
            s2 = parser.parse("EXISTS x,y ((P(x) AND P(A)) OR (P(A) => P(y)))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            s1 = parser.parse("EXISTS x,y ((P(x) AND P(A)) OR (P(A) => P(y)))");
            s2 = parser.parse("EXISTS x   ((P(x) AND P(A)) OR (P(A) => P(y)))");

            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("EXISTS x,y ((P(x) AND P(A)) OR (P(A) => P(y)))");
            s2 = parser.parse("EXISTS x,y ((P(x) AND P(A)) OR (P(B) => P(y)))");

            result = unifier.unify(s1, s2);

            Assert.IsNull(result);

            s1 = parser.parse("EXISTS x,y ((P(x) AND P(A)) OR (P(A) => P(y)))");
            s2 = parser.parse("EXISTS x,y ((P(A) AND P(A)) OR (P(A) => P(y)))");

            result = unifier.unify(s1, s2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new Constant("A"), result[new Variable("x")]);
        }
    }
}
