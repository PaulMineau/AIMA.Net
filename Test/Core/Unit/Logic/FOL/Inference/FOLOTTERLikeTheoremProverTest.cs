namespace AIMA.Test.Core.Unit.Logic.Fol.Inference
{


    using AIMA.Core.Logic.FOL;
    using AIMA.Core.Logic.FOL.Domain;
    using AIMA.Core.Logic.FOL.Inference;
    using AIMA.Core.Logic.FOL.Inference.Otter.DefaultImpl;
    using AIMA.Core.Logic.FOL.KB;
    using AIMA.Core.Logic.FOL.KB.Data;
    using AIMA.Core.Logic.FOL.Parsing;
    using AIMA.Core.Logic.FOL.Parsing.AST;
    using AIMA.Test.Core.Unit.Logic.FOL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    [TestClass]
    public class FOLOTTERLikeTheoremProverTest :
            CommonFOLInferenceProcedureTests
    {

        [TestMethod]
        public void testDefaultClauseSimplifier()
        {
            FOLDomain domain = new FOLDomain();
            domain.addConstant("ZERO");
            domain.addConstant("ONE");
            domain.addPredicate("P");
            domain.addFunction("Plus");
            domain.addFunction("Power");

            FOLParser parser = new FOLParser(domain);

            List<TermEquality> rewrites = new List<TermEquality>();
            rewrites.Add((TermEquality)parser.parse("Plus(x, ZERO) = x"));
            rewrites.Add((TermEquality)parser.parse("Plus(ZERO, x) = x"));
            rewrites.Add((TermEquality)parser.parse("Power(x, ONE) = x"));
            rewrites.Add((TermEquality)parser.parse("Power(x, ZERO) = ONE"));
            DefaultClauseSimplifier simplifier = new DefaultClauseSimplifier(
                    rewrites);

            Sentence s1 = parser
                    .parse("((P(Plus(y,ZERO),Plus(ZERO,y)) OR P(Power(y, ONE),Power(y,ZERO))) OR P(Power(y,ZERO),Plus(y,ZERO)))");

            CNFConverter cnfConverter = new CNFConverter(parser);

            CNF cnf = cnfConverter.convertToCNF(s1);

            Assert.AreEqual(1, cnf.getNumberOfClauses());

            Clause simplified = simplifier.simplify(cnf.getConjunctionOfClauses()
                    [0]);

            Assert.AreEqual("[P(y,y), P(y,ONE), P(ONE,y)]", simplified
                    .ToString());
        }

        // This tests to ensure the OTTERLike theorem prover
        // uses subsumption correctly so that it exhausts
        // its search space.
        [TestMethod]
        public void testExhaustsSearchSpace()
        {
            // Taken from AIMA pg 679
            FOLDomain domain = new FOLDomain();
            domain.addPredicate("alternate");
            domain.addPredicate("bar");
            domain.addPredicate("fri_sat");
            domain.addPredicate("hungry");
            domain.addPredicate("patrons");
            domain.addPredicate("price");
            domain.addPredicate("raining");
            domain.addPredicate("reservation");
            domain.addPredicate("type");
            domain.addPredicate("wait_estimate");
            domain.addPredicate("will_wait");
            domain.addConstant("Some");
            domain.addConstant("Full");
            domain.addConstant("French");
            domain.addConstant("Thai");
            domain.addConstant("Burger");
            domain.addConstant("$");
            domain.addConstant("_30_60");
            domain.addConstant("X0");
            FOLParser parser = new FOLParser(domain);

            // The hypothesis
            String c1 = "patrons(v,Some)";
            String c2 = "patrons(v,Full) AND (hungry(v) AND type(v,French))";
            String c3 = "patrons(v,Full) AND (hungry(v) AND (type(v,Thai) AND fri_sat(v)))";
            String c4 = "patrons(v,Full) AND (hungry(v) AND type(v,Burger))";
            String sh = "FORALL v (will_wait(v) <=> (" + c1 + " OR (" + c2
                    + " OR (" + c3 + " OR (" + c4 + ")))))";

            Sentence hypothesis = parser.parse(sh);
            Sentence desc = parser
                    .parse("(((((((((alternate(X0) AND NOT(bar(X0))) AND NOT(fri_sat(X0))) AND hungry(X0)) AND patrons(X0,Full)) AND price(X0,$)) AND NOT(raining(X0))) AND NOT(reservation(X0))) AND type(X0,Thai)) AND wait_estimate(X0,_30_60))");
            Sentence classification = parser.parse("will_wait(X0)");

            FOLKnowledgeBase kb = new FOLKnowledgeBase(domain,
                    new FOLOTTERLikeTheoremProver(false));

            kb.tell(hypothesis);
            kb.tell(desc);

            InferenceResult ir = kb.ask(classification);

            Assert.IsFalse(ir.isTrue());
            Assert.IsTrue(ir.isPossiblyFalse());
            Assert.IsFalse(ir.isUnknownDueToTimeout());
            Assert.IsFalse(ir.isPartialResultDueToTimeout());
            Assert.AreEqual(0, ir.getProofs().Count);
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryCriminalXFalse()
        {
            testDefiniteClauseKBKingsQueryCriminalXFalse(new FOLOTTERLikeTheoremProver(
                    false));
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryRichardEvilFalse()
        {
            testDefiniteClauseKBKingsQueryRichardEvilFalse(new FOLOTTERLikeTheoremProver(
                    false));
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryJohnEvilSucceeds()
        {
            testDefiniteClauseKBKingsQueryJohnEvilSucceeds(new FOLOTTERLikeTheoremProver(
                    false));
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryEvilXReturnsJohnSucceeds()
        {
            testDefiniteClauseKBKingsQueryEvilXReturnsJohnSucceeds(new FOLOTTERLikeTheoremProver(
                    false));
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryKingXReturnsJohnAndRichardSucceeds()
        {
            testDefiniteClauseKBKingsQueryKingXReturnsJohnAndRichardSucceeds(new FOLOTTERLikeTheoremProver(
                    false));
        }

        [TestMethod]
        public void testDefiniteClauseKBWeaponsQueryCriminalXReturnsWestSucceeds()
        {
            testDefiniteClauseKBWeaponsQueryCriminalXReturnsWestSucceeds(new FOLOTTERLikeTheoremProver(
                    false));
        }

        [TestMethod]
        public void testHornClauseKBRingOfThievesQuerySkisXReturnsNancyRedBertDrew()
        {
            // This KB ends up being infinite when resolving, however 2
            // seconds is more than enough to extract the 4 answers
            // that are expected
            testHornClauseKBRingOfThievesQuerySkisXReturnsNancyRedBertDrew(new FOLOTTERLikeTheoremProver(
                    2 * 1000, false));
        }

        [TestMethod]
        public void testFullFOLKBLovesAnimalQueryKillsCuriosityTunaSucceeds()
        {
            testFullFOLKBLovesAnimalQueryKillsCuriosityTunaSucceeds(
                    new FOLOTTERLikeTheoremProver(false), false);
        }

        [TestMethod]
        public void testFullFOLKBLovesAnimalQueryNotKillsJackTunaSucceeds()
        {
            testFullFOLKBLovesAnimalQueryNotKillsJackTunaSucceeds(
                    new FOLOTTERLikeTheoremProver(false), false);
        }

        [TestMethod]
        public void testFullFOLKBLovesAnimalQueryKillsJackTunaFalse()
        {
            // This query will not return using OTTER Like resolution
            // as keep expanding clauses through resolution for this KB.
            testFullFOLKBLovesAnimalQueryKillsJackTunaFalse(
                    new FOLOTTERLikeTheoremProver(false), true);
        }

        [TestMethod]
        public void testEqualityAxiomsKBabcAEqualsCSucceeds()
        {
            testEqualityAxiomsKBabcAEqualsCSucceeds(new FOLOTTERLikeTheoremProver(
                    false));
        }

        [TestMethod]
        public void testEqualityAndSubstitutionAxiomsKBabcdFFASucceeds()
        {
            testEqualityAndSubstitutionAxiomsKBabcdFFASucceeds(new FOLOTTERLikeTheoremProver(
                    false));
        }

        [TestMethod]
        public void testEqualityAndSubstitutionAxiomsKBabcdPDSucceeds()
        {
            testEqualityAndSubstitutionAxiomsKBabcdPDSucceeds(new FOLOTTERLikeTheoremProver(
                    false));
        }

        [TestMethod]
        public void testEqualityAndSubstitutionAxiomsKBabcdPFFASucceeds()
        {
            testEqualityAndSubstitutionAxiomsKBabcdPFFASucceeds(
                    new FOLOTTERLikeTheoremProver(false), false);
        }

        [TestMethod]
        public void testEqualityNoAxiomsKBabcAEqualsCSucceeds()
        {
            testEqualityNoAxiomsKBabcAEqualsCSucceeds(
                    new FOLOTTERLikeTheoremProver(true), false);
        }

        [TestMethod]
        public void testEqualityAndSubstitutionNoAxiomsKBabcdFFASucceeds()
        {
            testEqualityAndSubstitutionNoAxiomsKBabcdFFASucceeds(
                    new FOLOTTERLikeTheoremProver(true), false);
        }

        [TestMethod]
        public void testEqualityAndSubstitutionNoAxiomsKBabcdPDSucceeds()
        {
            testEqualityAndSubstitutionNoAxiomsKBabcdPDSucceeds(
                    new FOLOTTERLikeTheoremProver(true), false);
        }

        [TestMethod]
        public void testEqualityAndSubstitutionNoAxiomsKBabcdPFFASucceeds()
        {
            testEqualityAndSubstitutionNoAxiomsKBabcdPFFASucceeds(
                    new FOLOTTERLikeTheoremProver(true), false);
        }
    }
}