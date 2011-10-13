namespace AIMA.Test.Core.Unit.Logic.FOL.KB.Data
{

    using AIMA.Core.Logic.FOL;
    using AIMA.Core.Logic.FOL.Domain;
    using AIMA.Core.Logic.FOL.KB;
    using AIMA.Core.Logic.FOL.KB.Data;
    using AIMA.Core.Logic.FOL.Parsing;
    using AIMA.Core.Logic.FOL.Parsing.AST;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;


    /**
     * @author Ciaran O'Reilly
     * 
     */
    [TestClass]
    public class ClauseTest
    {

        [TestInitialize]
        public void setUp()
        {
            StandardizeApartIndexicalFactory.flush();
        }

        [TestMethod]
        public void testImmutable()
        {
            Clause c = new Clause();

            Assert.IsFalse(c.isImmutable());

            c.addNegativeLiteral(new Predicate("Pred1", new List<Term>()));
            c.addPositiveLiteral(new Predicate("Pred2", new List<Term>()));

            c.setImmutable();

            Assert.IsTrue(c.isImmutable());

            //try {
            c.addNegativeLiteral(new Predicate("Pred3", new List<Term>()));

            Assert.Fail("Should have thrown an IllegalStateException");
            //} catch (IllegalStateException ise) {
            //    // Ok, Expected
            //}

            //	try {
            c.addPositiveLiteral(new Predicate("Pred3", new List<Term>()));

            Assert.Fail("Should have thrown an IllegalStateException");
            //} catch (IllegalStateException ise) {
            // Ok, Expected
            //}
        }

        [TestMethod]
        public void testIsEmpty()
        {
            Clause c1 = new Clause();
            Assert.IsTrue(c1.isEmpty());

            c1.addNegativeLiteral(new Predicate("Pred1", new List<Term>()));
            Assert.IsFalse(c1.isEmpty());

            Clause c2 = new Clause();
            Assert.IsTrue(c2.isEmpty());

            c2.addPositiveLiteral(new Predicate("Pred1", new List<Term>()));
            Assert.IsFalse(c2.isEmpty());

            Clause c3 = new Clause();
            Assert.IsTrue(c3.isEmpty());

            c3.addNegativeLiteral(new Predicate("Pred1", new List<Term>()));
            c3.addPositiveLiteral(new Predicate("Pred1", new List<Term>()));
            // Should be empty as they resolved with each other
            Assert.IsFalse(c3.isEmpty());

            c3.addNegativeLiteral(new Predicate("Pred1", new List<Term>()));
            c3.addPositiveLiteral(new Predicate("Pred2", new List<Term>()));
            Assert.IsFalse(c3.isEmpty());
        }

        [TestMethod]
        public void testIsHornClause()
        {
            Clause c1 = new Clause();
            Assert.IsFalse(c1.isHornClause());

            c1.addNegativeLiteral(new Predicate("Pred1", new List<Term>()));
            Assert.IsTrue(c1.isHornClause());

            c1.addPositiveLiteral(new Predicate("Pred2", new List<Term>()));
            Assert.IsTrue(c1.isHornClause());

            c1.addNegativeLiteral(new Predicate("Pred3", new List<Term>()));
            Assert.IsTrue(c1.isHornClause());
            c1.addNegativeLiteral(new Predicate("Pred4", new List<Term>()));
            Assert.IsTrue(c1.isHornClause());

            c1.addPositiveLiteral(new Predicate("Pred5", new List<Term>()));
            Assert.IsFalse(c1.isHornClause());
        }

        [TestMethod]
        public void testIsDefiniteClause()
        {
            Clause c1 = new Clause();
            Assert.IsFalse(c1.isDefiniteClause());

            c1.addNegativeLiteral(new Predicate("Pred1", new List<Term>()));
            Assert.IsFalse(c1.isDefiniteClause());

            c1.addPositiveLiteral(new Predicate("Pred2", new List<Term>()));
            Assert.IsTrue(c1.isDefiniteClause());

            c1.addNegativeLiteral(new Predicate("Pred3", new List<Term>()));
            Assert.IsTrue(c1.isDefiniteClause());
            c1.addNegativeLiteral(new Predicate("Pred4", new List<Term>()));
            Assert.IsTrue(c1.isDefiniteClause());

            c1.addPositiveLiteral(new Predicate("Pred5", new List<Term>()));
            Assert.IsFalse(c1.isDefiniteClause());
        }

        [TestMethod]
        public void testIsUnitClause()
        {
            Clause c1 = new Clause();
            Assert.IsFalse(c1.isUnitClause());

            c1.addPositiveLiteral(new Predicate("Pred1", new List<Term>()));
            Assert.IsTrue(c1.isUnitClause());

            c1.addPositiveLiteral(new Predicate("Pred2", new List<Term>()));
            Assert.IsFalse(c1.isUnitClause());

            c1 = new Clause();
            Assert.IsFalse(c1.isUnitClause());

            c1.addPositiveLiteral(new Predicate("Pred1", new List<Term>()));
            Assert.IsTrue(c1.isUnitClause());

            c1.addNegativeLiteral(new Predicate("Pred2", new List<Term>()));
            Assert.IsFalse(c1.isUnitClause());

            c1 = new Clause();
            Assert.IsFalse(c1.isUnitClause());

            c1.addNegativeLiteral(new Predicate("Pred1", new List<Term>()));
            Assert.IsTrue(c1.isUnitClause());

            c1.addPositiveLiteral(new Predicate("Pred2", new List<Term>()));
            Assert.IsFalse(c1.isUnitClause());
        }

        [TestMethod]
        public void testIsImplicationDefiniteClause()
        {
            Clause c1 = new Clause();
            Assert.IsFalse(c1.isImplicationDefiniteClause());

            c1.addPositiveLiteral(new Predicate("Pred1", new List<Term>()));
            Assert.IsFalse(c1.isImplicationDefiniteClause());

            c1.addNegativeLiteral(new Predicate("Pred2", new List<Term>()));
            Assert.IsTrue(c1.isImplicationDefiniteClause());
            c1.addNegativeLiteral(new Predicate("Pred3", new List<Term>()));
            Assert.IsTrue(c1.isImplicationDefiniteClause());

            c1.addPositiveLiteral(new Predicate("Pred4", new List<Term>()));
            Assert.IsFalse(c1.isImplicationDefiniteClause());
        }

        [TestMethod]
        public void testBinaryResolvents()
        {
            FOLDomain domain = new FOLDomain();
            domain.addPredicate("Pred1");
            domain.addPredicate("Pred2");
            domain.addPredicate("Pred3");
            domain.addPredicate("Pred4");

            Clause c1 = new Clause();

            // Ensure that resolving to self when empty returns an empty clause
            Assert.IsNotNull(c1.binaryResolvents(c1));
            Assert.AreEqual(1, c1.binaryResolvents(c1).Count);
            Assert.IsTrue(c1.binaryResolvents(c1)[0].isEmpty());

            // Check if resolve with self to an empty clause
            c1.addPositiveLiteral(new Predicate("Pred1", new List<Term>()));
            c1.addNegativeLiteral(new Predicate("Pred1", new List<Term>()));
            Assert.IsNotNull(c1.binaryResolvents(c1));
            Assert.AreEqual(1, c1.binaryResolvents(c1).Count);
            // i.e. resolving a tautology with a tautology gives you
            // back a tautology.
            Assert.AreEqual("[~Pred1(), Pred1()]", c1.binaryResolvents(c1)
                    [0].ToString());

            // Check if try to resolve with self and no resolvents
            c1 = new Clause();
            c1.addPositiveLiteral(new Predicate("Pred1", new List<Term>()));
            Assert.AreEqual(0, c1.binaryResolvents(c1).Count);

            c1 = new Clause();
            Clause c2 = new Clause();
            // Ensure that two empty clauses resolve to an empty clause
            Assert.IsNotNull(c1.binaryResolvents(c2));
            Assert.AreEqual(1, c1.binaryResolvents(c2).Count);
            Assert.IsTrue(c1.binaryResolvents(c2).First().isEmpty());
            Assert.IsNotNull(c2.binaryResolvents(c1));
            Assert.AreEqual(1, c2.binaryResolvents(c1).Count);
            Assert.IsTrue(c2.binaryResolvents(c1).First().isEmpty());

            // Enusre the two complementary clauses resolve
            // to the empty clause
            c1.addPositiveLiteral(new Predicate("Pred1", new List<Term>()));
            c2.addNegativeLiteral(new Predicate("Pred1", new List<Term>()));
            Assert.IsNotNull(c1.binaryResolvents(c2));
            Assert.AreEqual(1, c1.binaryResolvents(c2).Count);
            Assert.IsTrue(c1.binaryResolvents(c2).First().isEmpty());
            Assert.IsNotNull(c2.binaryResolvents(c1));
            Assert.AreEqual(1, c2.binaryResolvents(c1).Count);
            Assert.IsTrue(c2.binaryResolvents(c1).First().isEmpty());

            // Ensure that two clauses that have two complementaries
            // resolve with two resolvents
            c1.addPositiveLiteral(new Predicate("Pred1", new List<Term>()));
            c2.addNegativeLiteral(new Predicate("Pred1", new List<Term>()));
            c1.addPositiveLiteral(new Predicate("Pred2", new List<Term>()));
            c2.addNegativeLiteral(new Predicate("Pred2", new List<Term>()));
            Assert.IsNotNull(c1.binaryResolvents(c2));
            Assert.AreEqual(2, c1.binaryResolvents(c2).Count);
            Assert.IsNotNull(c2.binaryResolvents(c1));
            Assert.AreEqual(2, c2.binaryResolvents(c1).Count);

            // Ensure two clauses that factor are not
            // considered resolved
            c1 = new Clause();
            c2 = new Clause();
            c1.addPositiveLiteral(new Predicate("Pred1", new List<Term>()));
            c1.addPositiveLiteral(new Predicate("Pred2", new List<Term>()));
            c1.addNegativeLiteral(new Predicate("Pred3", new List<Term>()));
            c1.addNegativeLiteral(new Predicate("Pred4", new List<Term>()));
            c2.addPositiveLiteral(new Predicate("Pred2", new List<Term>()));
            c2.addNegativeLiteral(new Predicate("Pred4", new List<Term>()));
            Assert.IsNotNull(c1.binaryResolvents(c2));
            Assert.AreEqual(0, c1.binaryResolvents(c2).Count);
            Assert.IsNotNull(c2.binaryResolvents(c1));
            Assert.AreEqual(0, c2.binaryResolvents(c1).Count);

            // Ensure the resolvent is a subset of the originals
            c1 = new Clause();
            c2 = new Clause();
            c1.addPositiveLiteral(new Predicate("Pred1", new List<Term>()));
            c1.addNegativeLiteral(new Predicate("Pred2", new List<Term>()));
            c1.addNegativeLiteral(new Predicate("Pred3", new List<Term>()));
            c2.addPositiveLiteral(new Predicate("Pred2", new List<Term>()));
            Assert.IsNotNull(c1.binaryResolvents(c2));
            Assert.IsNotNull(c2.binaryResolvents(c1));
            Assert.AreEqual(1, c1.binaryResolvents(c2).First()
                    .getNumberPositiveLiterals());
            Assert.AreEqual(1, c1.binaryResolvents(c2).First()
                    .getNumberNegativeLiterals());
            Assert.AreEqual(1, c2.binaryResolvents(c1).First()
                    .getNumberPositiveLiterals());
            Assert.AreEqual(1, c2.binaryResolvents(c1).First()
                    .getNumberNegativeLiterals());
        }

        [TestMethod]
        public void testBinaryResolventsOrderDoesNotMatter()
        {
            // This is a regression test, to ensure
            // the ordering of resolvents does not matter.
            // If the order ends up mattering, then likely
            // a problem was introduced in the Clause class
            // unifier, or related class.

            // Set up the initial set of clauses based on the
            // loves animal domain as it contains functions
            // new clauses will always be created (i.e. is an
            // infinite universe of discourse).
            FOLKnowledgeBase kb = new FOLKnowledgeBase(DomainFactory
                    .lovesAnimalDomain());

            kb
                    .tell("FORALL x (FORALL y (Animal(y) => Loves(x, y)) => EXISTS y Loves(y, x))");
            kb
                    .tell("FORALL x (EXISTS y (Animal(y) AND Kills(x, y)) => FORALL z NOT(Loves(z, x)))");
            kb.tell("FORALL x (Animal(x) => Loves(Jack, x))");
            kb.tell("(Kills(Jack, Tuna) OR Kills(Curiosity, Tuna))");
            kb.tell("Cat(Tuna)");
            kb.tell("FORALL x (Cat(x) => Animal(x))");

            List<Clause> clauses = new List<Clause>();
            clauses.AddRange(kb.getAllClauses());

            List<Clause> newClauses = new List<Clause>();
            long maxRunTime = 30 * 1000; // 30 seconds
            long finishTime = System.DateTime.UtcNow.Ticks + maxRunTime;
            do
            {
                clauses.AddRange(newClauses);
                newClauses.Clear();
                Clause[] clausesA = new Clause[clauses.Count];
                clauses.CopyTo(clausesA);
                for (int i = 0; i < clausesA.Length; i++)
                {
                    Clause cI = clausesA[i];
                    for (int j = 0; j < clausesA.Length; j++)
                    {
                        Clause cJ = clausesA[j];

                        newClauses.AddRange(cI.getFactors());
                        newClauses.AddRange(cJ.getFactors());

                        List<Clause> cIresolvents = cI.binaryResolvents(cJ);
                        List<Clause> cJresolvents = cJ.binaryResolvents(cI);
                        if (!cIresolvents.Equals(cJresolvents))
                        {
                            System.Console.WriteLine("cI=" + cI);
                            System.Console.WriteLine("cJ=" + cJ);
                            System.Console.WriteLine("cIR=" + cIresolvents);
                            System.Console.WriteLine("cJR=" + cJresolvents);
                            Assert
                                    .Fail("Ordering of binary resolvents has become usingant, which should not be the case");
                        }

                        foreach (Clause r in cIresolvents)
                        {
                            newClauses.AddRange(r.getFactors());
                        }

                        if (System.DateTime.UtcNow.Ticks > finishTime)
                        {
                            break;
                        }
                    }
                    if (System.DateTime.UtcNow.Ticks > finishTime)
                    {
                        break;
                    }
                }
            } while (System.DateTime.UtcNow.Ticks < finishTime);
        }

        [TestMethod]
        public void testEqualityBinaryResolvents()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");

            FOLParser parser = new FOLParser(domain);

            // B = A
            Clause c1 = new Clause();
            c1.addPositiveLiteral((AtomicSentence)parser.parse("B = A"));

            Clause c2 = new Clause();
            c2.addNegativeLiteral((AtomicSentence)parser.parse("B = A"));
            c2.addPositiveLiteral((AtomicSentence)parser.parse("B = A"));

            List<Clause> resolvents = c1.binaryResolvents(c2);

            Assert.AreEqual(1, resolvents.Count);
            Assert.AreEqual("[[B = A]]", resolvents.ToString());
        }

        [TestMethod]
        public void testHashCode()
        {
            Term cons1 = new Constant("C1");
            Term cons2 = new Constant("C2");
            Term var1 = new Variable("v1");
            List<Term> pts1 = new List<Term>();
            List<Term> pts2 = new List<Term>();
            pts1.Add(cons1);
            pts1.Add(cons2);
            pts1.Add(var1);
            pts2.Add(cons2);
            pts2.Add(cons1);
            pts2.Add(var1);

            Clause c1 = new Clause();
            Clause c2 = new Clause();
            Assert.AreEqual(c1.GetHashCode(), c2.GetHashCode());

            c1.addNegativeLiteral(new Predicate("Pred1", pts1));
            Assert.AreNotSame(c1.GetHashCode(), c2.GetHashCode());
            c2.addNegativeLiteral(new Predicate("Pred1", pts1));
            Assert.AreEqual(c1.GetHashCode(), c2.GetHashCode());

            c1.addPositiveLiteral(new Predicate("Pred1", pts1));
            Assert.AreNotSame(c1.GetHashCode(), c2.GetHashCode());
            c2.addPositiveLiteral(new Predicate("Pred1", pts1));
            Assert.AreEqual(c1.GetHashCode(), c2.GetHashCode());
        }

        [TestMethod]
        public void testSimpleEquals()
        {
            Term cons1 = new Constant("C1");
            Term cons2 = new Constant("C2");
            Term var1 = new Variable("v1");
            List<Term> pts1 = new List<Term>();
            List<Term> pts2 = new List<Term>();
            pts1.Add(cons1);
            pts1.Add(cons2);
            pts1.Add(var1);
            pts2.Add(cons2);
            pts2.Add(cons1);
            pts2.Add(var1);

            Clause c1 = new Clause();
            Clause c2 = new Clause();
            Assert.IsTrue(c1.Equals(c1));
            Assert.IsTrue(c2.Equals(c2));
            Assert.IsTrue(c1.Equals(c2));
            Assert.IsTrue(c2.Equals(c1));

            // Check negatives
            c1.addNegativeLiteral(new Predicate("Pred1", pts1));
            Assert.IsFalse(c1.Equals(c2));
            Assert.IsFalse(c2.Equals(c1));
            c2.addNegativeLiteral(new Predicate("Pred1", pts1));
            Assert.IsTrue(c1.Equals(c2));
            Assert.IsTrue(c2.Equals(c1));

            c1.addNegativeLiteral(new Predicate("Pred2", pts2));
            Assert.IsFalse(c1.Equals(c2));
            Assert.IsFalse(c2.Equals(c1));
            c2.addNegativeLiteral(new Predicate("Pred2", pts2));
            Assert.IsTrue(c1.Equals(c2));
            Assert.IsTrue(c2.Equals(c1));
            // Check same but added in different order
            c1.addNegativeLiteral(new Predicate("Pred3", pts1));
            Assert.IsFalse(c1.Equals(c2));
            Assert.IsFalse(c2.Equals(c1));
            c1.addNegativeLiteral(new Predicate("Pred4", pts1));
            Assert.IsFalse(c1.Equals(c2));
            Assert.IsFalse(c2.Equals(c1));
            c2.addNegativeLiteral(new Predicate("Pred4", pts1));
            Assert.IsFalse(c1.Equals(c2));
            Assert.IsFalse(c2.Equals(c1));
            c2.addNegativeLiteral(new Predicate("Pred3", pts1));
            Assert.IsTrue(c1.Equals(c2));
            Assert.IsTrue(c2.Equals(c1));

            // Check positives
            c1.addPositiveLiteral(new Predicate("Pred1", pts1));
            Assert.IsFalse(c1.Equals(c2));
            Assert.IsFalse(c2.Equals(c1));
            c2.addPositiveLiteral(new Predicate("Pred1", pts1));
            Assert.IsTrue(c1.Equals(c2));
            Assert.IsTrue(c2.Equals(c1));

            c1.addPositiveLiteral(new Predicate("Pred2", pts2));
            Assert.IsFalse(c1.Equals(c2));
            Assert.IsFalse(c2.Equals(c1));
            c2.addPositiveLiteral(new Predicate("Pred2", pts2));
            Assert.IsTrue(c1.Equals(c2));
            Assert.IsTrue(c2.Equals(c1));
            // Check same but added in different order
            c1.addPositiveLiteral(new Predicate("Pred3", pts1));
            Assert.IsFalse(c1.Equals(c2));
            Assert.IsFalse(c2.Equals(c1));
            c1.addPositiveLiteral(new Predicate("Pred4", pts1));
            Assert.IsFalse(c1.Equals(c2));
            Assert.IsFalse(c2.Equals(c1));
            c2.addPositiveLiteral(new Predicate("Pred4", pts1));
            Assert.IsFalse(c1.Equals(c2));
            Assert.IsFalse(c2.Equals(c1));
            c2.addPositiveLiteral(new Predicate("Pred3", pts1));
            Assert.IsTrue(c1.Equals(c2));
            Assert.IsTrue(c2.Equals(c1));
        }

        [TestMethod]
        public void testComplexEquals()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addConstant("C");
            domain.addConstant("D");
            domain.addPredicate("P");
            domain.addPredicate("Animal");
            domain.addPredicate("Kills");
            domain.addFunction("F");
            domain.addFunction("SF0");

            FOLParser parser = new FOLParser(domain);

            CNFConverter cnfConverter = new CNFConverter(parser);
            Sentence s1 = parser.parse("((x1 = y1 AND y1 = z1) => x1 = z1)");
            Sentence s2 = parser.parse("((x2 = y2 AND F(y2) = z2) => F(x2) = z2)");
            CNF cnf1 = cnfConverter.convertToCNF(s1);
            CNF cnf2 = cnfConverter.convertToCNF(s2);

            Clause c1 = cnf1.getConjunctionOfClauses()[0];
            Clause c2 = cnf2.getConjunctionOfClauses()[0];

            Assert.IsFalse(c1.Equals(c2));

            s1 = parser.parse("((x1 = y1 AND y1 = z1) => x1 = z1)");
            s2 = parser.parse("((x2 = y2 AND y2 = z2) => x2 = z2)");
            cnf1 = cnfConverter.convertToCNF(s1);
            cnf2 = cnfConverter.convertToCNF(s2);

            c1 = cnf1.getConjunctionOfClauses()[0];
            c2 = cnf2.getConjunctionOfClauses()[0];

            Assert.IsTrue(c1.Equals(c2));

            s1 = parser.parse("((x1 = y1 AND y1 = z1) => x1 = z1)");
            s2 = parser.parse("((y2 = z2 AND x2 = y2) => x2 = z2)");
            cnf1 = cnfConverter.convertToCNF(s1);
            cnf2 = cnfConverter.convertToCNF(s2);

            c1 = cnf1.getConjunctionOfClauses()[0];
            c2 = cnf2.getConjunctionOfClauses()[0];

            Assert.IsTrue(c1.Equals(c2));

            s1 = parser.parse("(((x1 = y1 AND y1 = z1) AND z1 = r1) => x1 = r1)");
            s2 = parser.parse("(((x2 = y2 AND y2 = z2) AND z2 = r2) => x2 = r2)");
            cnf1 = cnfConverter.convertToCNF(s1);
            cnf2 = cnfConverter.convertToCNF(s2);

            c1 = cnf1.getConjunctionOfClauses()[0];
            c2 = cnf2.getConjunctionOfClauses()[0];

            Assert.IsTrue(c1.Equals(c2));

            s1 = parser.parse("(((x1 = y1 AND y1 = z1) AND z1 = r1) => x1 = r1)");
            s2 = parser.parse("(((z2 = r2 AND y2 = z2) AND x2 = y2) => x2 = r2)");
            cnf1 = cnfConverter.convertToCNF(s1);
            cnf2 = cnfConverter.convertToCNF(s2);

            c1 = cnf1.getConjunctionOfClauses()[0];
            c2 = cnf2.getConjunctionOfClauses()[0];

            Assert.IsTrue(c1.Equals(c2));

            s1 = parser.parse("(((x1 = y1 AND y1 = z1) AND z1 = r1) => x1 = r1)");
            s2 = parser.parse("(((x2 = y2 AND y2 = z2) AND z2 = y2) => x2 = r2)");
            cnf1 = cnfConverter.convertToCNF(s1);
            cnf2 = cnfConverter.convertToCNF(s2);

            c1 = cnf1.getConjunctionOfClauses()[0];
            c2 = cnf2.getConjunctionOfClauses()[0];

            Assert.IsFalse(c1.Equals(c2));

            s1 = parser
                    .parse("(((((x1 = y1 AND y1 = z1) AND z1 = r1) AND r1 = q1) AND q1 = s1) => x1 = r1)");
            s2 = parser
                    .parse("(((((x2 = y2 AND y2 = z2) AND z2 = r2) AND r2 = q2) AND q2 = s2) => x2 = r2)");
            cnf1 = cnfConverter.convertToCNF(s1);
            cnf2 = cnfConverter.convertToCNF(s2);

            c1 = cnf1.getConjunctionOfClauses()[0];
            c2 = cnf2.getConjunctionOfClauses()[0];

            Assert.IsTrue(c1.Equals(c2));

            s1 = parser
                    .parse("((((NOT(Animal(c1920)) OR NOT(Animal(c1921))) OR NOT(Kills(c1922,c1920))) OR NOT(Kills(c1919,c1921))) OR NOT(Kills(SF0(c1922),SF0(c1919))))");
            s2 = parser
                    .parse("((((NOT(Animal(c1929)) OR NOT(Animal(c1928))) OR NOT(Kills(c1927,c1929))) OR NOT(Kills(c1930,c1928))) OR NOT(Kills(SF0(c1930),SF0(c1927))))");
            cnf1 = cnfConverter.convertToCNF(s1);
            cnf2 = cnfConverter.convertToCNF(s2);

            c1 = cnf1.getConjunctionOfClauses()[0];
            c2 = cnf2.getConjunctionOfClauses()[0];

            Assert.IsTrue(c1.Equals(c2));
        }

        [TestMethod]
        public void testNonTrivialFactors()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addFunction("F");
            domain.addFunction("G");
            domain.addFunction("H");
            domain.addPredicate("P");
            domain.addPredicate("Q");

            FOLParser parser = new FOLParser(domain);

            // p(x,y), q(a,b), p(b,a), q(y,x)
            Clause c = new Clause();
            c.addPositiveLiteral((Predicate)parser.parse("P(x,y)"));
            c.addPositiveLiteral((Predicate)parser.parse("Q(A,B)"));
            c.addNegativeLiteral((Predicate)parser.parse("P(B,A)"));
            c.addPositiveLiteral((Predicate)parser.parse("Q(y,x)"));

            Assert.AreEqual("[[~P(B,A), P(B,A), Q(A,B)]]", c
                    .getNonTrivialFactors().ToString());

            // p(x,y), q(a,b), p(b,a), q(y,x)
            c = new Clause();
            c.addPositiveLiteral((Predicate)parser.parse("P(x,y)"));
            c.addPositiveLiteral((Predicate)parser.parse("Q(A,B)"));
            c.addNegativeLiteral((Predicate)parser.parse("P(B,A)"));
            c.addNegativeLiteral((Predicate)parser.parse("Q(y,x)"));

            Assert.AreEqual("[]", c.getNonTrivialFactors().ToString());

            // p(x,f(y)), p(g(u),x), p(f(y),u)
            c = new Clause();
            c.addPositiveLiteral((Predicate)parser.parse("P(x,F(y))"));
            c.addPositiveLiteral((Predicate)parser.parse("P(G(u),x)"));
            c.addPositiveLiteral((Predicate)parser.parse("P(F(y),u)"));

            // Should be: [{P(F(c#),F(c#)),P(G(F(c#)),F(c#))}]
            c = c.getNonTrivialFactors().First();
            Literal p = c.getPositiveLiterals()[0];
            Assert.AreEqual("P", p.getAtomicSentence().getSymbolicName());
            Function f = (Function)p.getAtomicSentence().getArgs()[0];
            Assert.AreEqual("F", f.getFunctionName());
            Variable v = (Variable)f.getTerms()[0];
            f = (Function)p.getAtomicSentence().getArgs()[1];
            Assert.AreEqual("F", f.getFunctionName());
            Assert.AreEqual(v, f.getTerms()[0]);

            //
            p = c.getPositiveLiterals()[1];
            f = (Function)p.getAtomicSentence().getArgs()[0];
            Assert.AreEqual("G", f.getFunctionName());
            f = (Function)f.getTerms()[0];
            Assert.AreEqual("F", f.getFunctionName());
            Assert.AreEqual(v, f.getTerms()[0]);
            f = (Function)p.getAtomicSentence().getArgs()[1];
            Assert.AreEqual("F", f.getFunctionName());
            Assert.AreEqual(v, f.getTerms()[0]);

            // p(g(x)), q(x), p(f(a)), p(x), p(g(f(x))), q(f(a))
            c = new Clause();
            c.addPositiveLiteral((Predicate)parser.parse("P(G(x))"));
            c.addPositiveLiteral((Predicate)parser.parse("Q(x)"));
            c.addPositiveLiteral((Predicate)parser.parse("P(F(A))"));
            c.addPositiveLiteral((Predicate)parser.parse("P(x)"));
            c.addPositiveLiteral((Predicate)parser.parse("P(G(F(x)))"));
            c.addPositiveLiteral((Predicate)parser.parse("Q(F(A))"));

            Assert.AreEqual("[[P(F(A)), P(G(F(F(A)))), P(G(F(A))), Q(F(A))]]",
                    c.getNonTrivialFactors().ToString());
        }

        // Note: Tests derived from:
        // http://logic.stanford.edu/classes/cs157/2008/notes/chap09.pdf
        // page 16.
        [TestMethod]
        public void testIsTautology()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addPredicate("P");
            domain.addPredicate("Q");
            domain.addPredicate("R");
            domain.addFunction("F");

            FOLParser parser = new FOLParser(domain);

            // {p(f(a)),~p(f(a))}
            Clause c = new Clause();
            c.addPositiveLiteral((Predicate)parser.parse("P(F(A))"));
            Assert.IsFalse(c.isTautology());
            c.addNegativeLiteral((Predicate)parser.parse("P(F(A))"));
            Assert.IsTrue(c.isTautology());

            // {p(x),q(y),~q(y),r(z)}
            c = new Clause();
            c.addPositiveLiteral((Predicate)parser.parse("P(x)"));
            Assert.IsFalse(c.isTautology());
            c.addPositiveLiteral((Predicate)parser.parse("Q(y)"));
            Assert.IsFalse(c.isTautology());
            c.addNegativeLiteral((Predicate)parser.parse("Q(y)"));
            Assert.IsTrue(c.isTautology());
            c.addPositiveLiteral((Predicate)parser.parse("R(z)"));
            Assert.IsTrue(c.isTautology());

            // {~p(a),p(x)}
            c = new Clause();
            c.addNegativeLiteral((Predicate)parser.parse("P(A)"));
            Assert.IsFalse(c.isTautology());
            c.addPositiveLiteral((Predicate)parser.parse("P(x)"));
            Assert.IsFalse(c.isTautology());
        }

        // Note: Tests derived from:
        // http://logic.stanford.edu/classes/cs157/2008/lectures/lecture12.pdf
        // slides 17 and 18.
        [TestMethod]
        public void testSubsumes()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("A");
            domain.addConstant("B");
            domain.addConstant("C");
            domain.addConstant("D");
            domain.addConstant("E");
            domain.addConstant("F");
            domain.addConstant("G");
            domain.addConstant("H");
            domain.addConstant("I");
            domain.addConstant("J");
            domain.addPredicate("P");
            domain.addPredicate("Q");

            FOLParser parser = new FOLParser(domain);

            // Example
            // {~p(a,b),q(c)}
            Clause psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(C)"));
            // {~p(x,y)}
            Clause phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(x,y)"));

            Assert.IsTrue(phi.subsumes(psi));
            // Non-Example
            // {~p(x,b),q(x)}
            psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(x,B)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(x)"));
            // {~p(a,y)}
            phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(A,y)"));
            // Reason for Non-Example:
            // {p(b,b)}
            // {~q(b)}
            Assert.IsFalse(phi.subsumes(psi));

            //
            // Additional Examples

            // Non-Example
            // {~p(x,b),q(z)}
            psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(x,B)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(z)"));
            // {~p(a,y)}
            phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(A,y)"));

            Assert.IsFalse(phi.subsumes(psi));

            // Example
            // {~p(a,b),~p(w,z),q(c)}
            psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(w,z)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(C)"));
            // {~p(x,y),~p(a,b)}
            phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(x,y)"));
            phi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));

            Assert.IsTrue(phi.subsumes(psi));

            // Non-Example
            // {~p(v,b),~p(w,z),q(c)}
            psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(v,B)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(w,z)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(C)"));
            // {~p(x,y),~p(a,b)}
            phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(x,y)"));
            phi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));

            Assert.IsFalse(phi.subsumes(psi));

            // Example
            // {~p(a,b),~p(c,d),~p(e,f),~p(g,h),~p(i,j),q(c)}
            psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(C,D)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(E,F)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(G,H)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(C)"));
            // {~p(i,j)}
            phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));

            Assert.IsTrue(phi.subsumes(psi));

            // Example
            // {~p(a,b),~p(c,d),~p(e,f),q(c)}
            psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(C,D)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(E,F)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(C)"));
            // {~p(e,f),~p(a,b),~p(c,d)}
            phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(E,F)"));
            phi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            phi.addNegativeLiteral((Predicate)parser.parse("P(C,D)"));

            Assert.IsTrue(phi.subsumes(psi));

            // Example
            // {~p(a,b),~p(c,d),~p(e,f),~p(g,h),~p(i,j),q(c)}
            psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(C,D)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(E,F)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(G,H)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(C)"));
            // {~p(i,j),~p(c,d)}
            phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));
            phi.addNegativeLiteral((Predicate)parser.parse("P(C,D)"));

            Assert.IsTrue(phi.subsumes(psi));

            // Non-Example
            // {~p(a,b),~p(x,d),~p(e,f),~p(g,h),~p(i,j),q(c)}
            psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(x,D)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(E,F)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(G,H)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(C)"));
            // {~p(i,j),~p(c,d)}
            phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));
            phi.addNegativeLiteral((Predicate)parser.parse("P(C,D)"));

            Assert.IsFalse(phi.subsumes(psi));

            // Example
            // {~p(a,b),~p(c,d),~p(e,f),~p(g,h),~p(i,j),q(c)}
            psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(C,D)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(E,F)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(G,H)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(C)"));
            // {~p(i,j),~p(a,x)}
            phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));
            phi.addNegativeLiteral((Predicate)parser.parse("P(A,x)"));

            Assert.IsTrue(phi.subsumes(psi));

            // Example
            // {~p(a,b),~p(c,d),~p(e,f),~p(g,h),~p(i,j),q(a,b),q(c,d),q(e,f)}
            psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(C,D)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(E,F)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(G,H)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(A,B)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(C,D)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(E,F)"));
            // {~p(i,j),~p(a,b),q(e,f),q(a,b)}
            phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));
            phi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            phi.addPositiveLiteral((Predicate)parser.parse("Q(E,F)"));
            phi.addPositiveLiteral((Predicate)parser.parse("Q(A,B)"));

            Assert.IsTrue(phi.subsumes(psi));

            // Non-Example
            // {~p(a,b),~p(c,d),~p(e,f),~p(g,h),~p(i,j),q(a,b),q(c,d),q(e,f)}
            psi = new Clause();
            psi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(C,D)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(E,F)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(G,H)"));
            psi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(A,B)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(C,D)"));
            psi.addPositiveLiteral((Predicate)parser.parse("Q(E,F)"));
            // {~p(i,j),~p(a,b),q(e,f),q(a,b)}
            phi = new Clause();
            phi.addNegativeLiteral((Predicate)parser.parse("P(I,J)"));
            phi.addNegativeLiteral((Predicate)parser.parse("P(A,B)"));
            phi.addPositiveLiteral((Predicate)parser.parse("Q(E,A)"));
            phi.addPositiveLiteral((Predicate)parser.parse("Q(A,B)"));

            Assert.IsFalse(phi.subsumes(psi));
        }
    }
}