namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.FOL
{

    using CosmicFlow.AIMA.Core.Logic.FOL.Inference;
    using CosmicFlow.AIMA.Core.Logic.FOL.Inference.Proof;
    using CosmicFlow.AIMA.Core.Logic.FOL.KB;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing.AST;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public abstract class CommonFOLInferenceProcedureTests
    {

        //
        // Protected Methods
        //
        protected void testDefiniteClauseKBKingsQueryCriminalXFalse(
                InferenceProcedure infp)
        {
            FOLKnowledgeBase kkb = FOLKnowledgeBaseFactory
                    .createKingsKnowledgeBase(infp);
            List<Term> terms = new List<Term>();
            terms.Add(new Variable("x"));
            Predicate query = new Predicate("Criminal", terms);
            InferenceResult answer = kkb.ask(query);
            Assert.IsTrue(null != answer);
            Assert.IsTrue(answer.isPossiblyFalse());
            Assert.IsFalse(answer.isTrue());
            Assert.IsFalse(answer.isUnknownDueToTimeout());
            Assert.IsFalse(answer.isPartialResultDueToTimeout());
            Assert.IsTrue(0 == answer.getProofs().Count);
        }

        protected void testDefiniteClauseKBKingsQueryRichardEvilFalse(
                InferenceProcedure infp)
        {
            FOLKnowledgeBase kkb = FOLKnowledgeBaseFactory
                    .createKingsKnowledgeBase(infp);
            List<Term> terms = new List<Term>();
            terms.Add(new Constant("Richard"));
            Predicate query = new Predicate("Evil", terms);
            InferenceResult answer = kkb.ask(query);
            Assert.IsTrue(null != answer);
            Assert.IsTrue(answer.isPossiblyFalse());
            Assert.IsFalse(answer.isTrue());
            Assert.IsFalse(answer.isUnknownDueToTimeout());
            Assert.IsFalse(answer.isPartialResultDueToTimeout());
            Assert.IsTrue(0 == answer.getProofs().Count);
        }

        protected void testDefiniteClauseKBKingsQueryJohnEvilSucceeds(
                InferenceProcedure infp)
        {
            FOLKnowledgeBase kkb = FOLKnowledgeBaseFactory
                    .createKingsKnowledgeBase(infp);
            List<Term> terms = new List<Term>();
            terms.Add(new Constant("John"));
            Predicate query = new Predicate("Evil", terms);
            InferenceResult answer = kkb.ask(query);

            Assert.IsTrue(null != answer);
            Assert.IsFalse(answer.isPossiblyFalse());
            Assert.IsTrue(answer.isTrue());
            Assert.IsFalse(answer.isUnknownDueToTimeout());
            Assert.IsFalse(answer.isPartialResultDueToTimeout());
            Assert.IsTrue(1 == answer.getProofs().Count);
            Assert.IsTrue(0 == answer.getProofs()[0].getAnswerBindings()
                    .Count);
        }

        protected void testDefiniteClauseKBKingsQueryEvilXReturnsJohnSucceeds(
                InferenceProcedure infp)
        {
            FOLKnowledgeBase kkb = FOLKnowledgeBaseFactory
                    .createKingsKnowledgeBase(infp);
            List<Term> terms = new List<Term>();
            terms.Add(new Variable("x"));
            Predicate query = new Predicate("Evil", terms);
            InferenceResult answer = kkb.ask(query);

            Assert.IsTrue(null != answer);
            Assert.IsFalse(answer.isPossiblyFalse());
            Assert.IsTrue(answer.isTrue());
            Assert.IsFalse(answer.isUnknownDueToTimeout());
            Assert.IsFalse(answer.isPartialResultDueToTimeout());
            Assert.IsTrue(1 == answer.getProofs().Count);
            Assert.IsTrue(1 == answer.getProofs()[0].getAnswerBindings()
                    .Count);
            Assert.AreEqual(new Constant("John"), answer.getProofs()[0]
                    .getAnswerBindings()[new Variable("x")]);
        }

        protected void testDefiniteClauseKBKingsQueryKingXReturnsJohnAndRichardSucceeds(
                InferenceProcedure infp)
        {
            FOLKnowledgeBase kkb = FOLKnowledgeBaseFactory
                    .createKingsKnowledgeBase(infp);
            List<Term> terms = new List<Term>();
            terms.Add(new Variable("x"));
            Predicate query = new Predicate("King", terms);
            InferenceResult answer = kkb.ask(query);

            Assert.IsTrue(null != answer);
            Assert.IsFalse(answer.isPossiblyFalse());
            Assert.IsTrue(answer.isTrue());
            Assert.IsFalse(answer.isUnknownDueToTimeout());
            Assert.IsFalse(answer.isPartialResultDueToTimeout());
            Assert.IsTrue(2 == answer.getProofs().Count);
            Assert.IsTrue(1 == answer.getProofs()[0].getAnswerBindings()
                    .Count);
            Assert.IsTrue(1 == answer.getProofs()[1].getAnswerBindings()
                    .Count);

            bool gotJohn, gotRichard;
            gotJohn = gotRichard = false;
            Constant cJohn = new Constant("John");
            Constant cRichard = new Constant("Richard");
            foreach (Proof p in answer.getProofs())
            {
                Dictionary<Variable, Term> ans = p.getAnswerBindings();
                Assert.AreEqual(1, ans.Count);
                if (cJohn.Equals(ans[new Variable("x")]))
                {
                    gotJohn = true;
                }
                if (cRichard.Equals(ans[new Variable("x")]))
                {
                    gotRichard = true;
                }
            }
            Assert.IsTrue(gotJohn);
            Assert.IsTrue(gotRichard);
        }

        protected void testDefiniteClauseKBWeaponsQueryCriminalXReturnsWestSucceeds(
                InferenceProcedure infp)
        {
            FOLKnowledgeBase wkb = FOLKnowledgeBaseFactory
                    .createWeaponsKnowledgeBase(infp);
            List<Term> terms = new List<Term>();
            terms.Add(new Variable("x"));
            Predicate query = new Predicate("Criminal", terms);

            InferenceResult answer = wkb.ask(query);

            Assert.IsTrue(null != answer);
            Assert.IsFalse(answer.isPossiblyFalse());
            Assert.IsTrue(answer.isTrue());
            Assert.IsFalse(answer.isUnknownDueToTimeout());
            Assert.IsFalse(answer.isPartialResultDueToTimeout());
            Assert.IsTrue(1 == answer.getProofs().Count);
            Assert.IsTrue(1 == answer.getProofs()[0].getAnswerBindings()
                    .Count);
            Assert.AreEqual(new Constant("West"), answer.getProofs()[0]
                    .getAnswerBindings()[new Variable("x")]);
        }

        protected void testHornClauseKBRingOfThievesQuerySkisXReturnsNancyRedBertDrew(
                InferenceProcedure infp)
        {
            FOLKnowledgeBase rotkb = FOLKnowledgeBaseFactory
                    .createRingOfThievesKnowledgeBase(infp);
            List<Term> terms = new List<Term>();
            terms.Add(new Variable("x"));
            Predicate query = new Predicate("Skis", terms);

            InferenceResult answer = rotkb.ask(query);

            Assert.IsTrue(null != answer);
            Assert.IsFalse(answer.isPossiblyFalse());
            Assert.IsTrue(answer.isTrue());
            Assert.IsFalse(answer.isUnknownDueToTimeout());
            // DB can expand infinitely so is only partial.
            Assert.IsTrue(answer.isPartialResultDueToTimeout());
            Assert.AreEqual(4, answer.getProofs().Count);
            Assert.AreEqual(1, answer.getProofs()[0].getAnswerBindings()
                    .Count);
            Assert.AreEqual(1, answer.getProofs()[1].getAnswerBindings()
                    .Count);
            Assert.AreEqual(1, answer.getProofs()[2].getAnswerBindings()
                    .Count);
            Assert.AreEqual(1, answer.getProofs()[3].getAnswerBindings()
                    .Count);

            List<Constant> expected = new List<Constant>();
            expected.Add(new Constant("Nancy"));
            expected.Add(new Constant("Red"));
            expected.Add(new Constant("Bert"));
            expected.Add(new Constant("Drew"));
            foreach (Proof p in answer.getProofs())
            {
                expected.Remove((Constant)p.getAnswerBindings()[new Variable("x")]);
            }
            Assert.AreEqual(0, expected.Count);
        }

        protected void testFullFOLKBLovesAnimalQueryKillsCuriosityTunaSucceeds(
                InferenceProcedure infp, bool expectedToTimeOut)
        {
            FOLKnowledgeBase akb = FOLKnowledgeBaseFactory
                    .createLovesAnimalKnowledgeBase(infp);
            List<Term> terms = new List<Term>();
            terms.Add(new Constant("Curiosity"));
            terms.Add(new Constant("Tuna"));
            Predicate query = new Predicate("Kills", terms);

            InferenceResult answer = akb.ask(query);
            Assert.IsTrue(null != answer);
            if (expectedToTimeOut)
            {
                Assert.IsFalse(answer.isPossiblyFalse());
                Assert.IsFalse(answer.isTrue());
                Assert.IsTrue(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(0 == answer.getProofs().Count);
            }
            else
            {
                Assert.IsFalse(answer.isPossiblyFalse());
                Assert.IsTrue(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(1 == answer.getProofs().Count);
                Assert.IsTrue(0 == answer.getProofs()[0]
                        .getAnswerBindings().Count);
            }
        }

        protected void testFullFOLKBLovesAnimalQueryNotKillsJackTunaSucceeds(
                InferenceProcedure infp, bool expectedToTimeOut)
        {
            FOLKnowledgeBase akb = FOLKnowledgeBaseFactory
                    .createLovesAnimalKnowledgeBase(infp);
            List<Term> terms = new List<Term>();
            terms.Add(new Constant("Jack"));
            terms.Add(new Constant("Tuna"));
            NotSentence query = new NotSentence(new Predicate("Kills", terms));

            InferenceResult answer = akb.ask(query);

            Assert.IsTrue(null != answer);
            if (expectedToTimeOut)
            {
                Assert.IsFalse(answer.isPossiblyFalse());
                Assert.IsFalse(answer.isTrue());
                Assert.IsTrue(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(0 == answer.getProofs().Count);
            }
            else
            {
                Assert.IsFalse(answer.isPossiblyFalse());
                Assert.IsTrue(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(1 == answer.getProofs().Count);
                Assert.IsTrue(0 == answer.getProofs()[0]
                        .getAnswerBindings().Count);
            }
        }

        protected void testFullFOLKBLovesAnimalQueryKillsJackTunaFalse(
                InferenceProcedure infp, bool expectedToTimeOut)
        {
            FOLKnowledgeBase akb = FOLKnowledgeBaseFactory
                    .createLovesAnimalKnowledgeBase(infp);
            List<Term> terms = new List<Term>();
            terms.Add(new Constant("Jack"));
            terms.Add(new Constant("Tuna"));
            Predicate query = new Predicate("Kills", terms);

            InferenceResult answer = akb.ask(query);

            Assert.IsTrue(null != answer);
            if (expectedToTimeOut)
            {
                Assert.IsFalse(answer.isPossiblyFalse());
                Assert.IsFalse(answer.isTrue());
                Assert.IsTrue(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(0 == answer.getProofs().Count);
            }
            else
            {
                Assert.IsTrue(answer.isPossiblyFalse());
                Assert.IsFalse(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(0 == answer.getProofs().Count);
            }
        }

        protected void testEqualityAxiomsKBabcAEqualsCSucceeds(
                InferenceProcedure infp)
        {
            FOLKnowledgeBase akb = FOLKnowledgeBaseFactory
                    .createABCEqualityKnowledgeBase(infp, true);

            TermEquality query = new TermEquality(new Constant("A"), new Constant(
                    "C"));

            InferenceResult answer = akb.ask(query);

            Assert.IsTrue(null != answer);
            Assert.IsFalse(answer.isPossiblyFalse());
            Assert.IsTrue(answer.isTrue());
            Assert.IsFalse(answer.isUnknownDueToTimeout());
            Assert.IsFalse(answer.isPartialResultDueToTimeout());
            Assert.IsTrue(1 == answer.getProofs().Count);
            Assert.IsTrue(0 == answer.getProofs()[0].getAnswerBindings()
                    .Count);
        }

        protected void testEqualityAndSubstitutionAxiomsKBabcdFFASucceeds(
                InferenceProcedure infp)
        {
            FOLKnowledgeBase akb = FOLKnowledgeBaseFactory
                    .createABCDEqualityAndSubstitutionKnowledgeBase(infp, true);

            List<Term> terms = new List<Term>();
            terms.Add(new Constant("A"));
            Function fa = new Function("F", terms);
            terms = new List<Term>();
            terms.Add(fa);
            TermEquality query = new TermEquality(new Function("F", terms),
                    new Constant("A"));

            InferenceResult answer = akb.ask(query);

            Assert.IsTrue(null != answer);
            Assert.IsFalse(answer.isPossiblyFalse());
            Assert.IsTrue(answer.isTrue());
            Assert.IsFalse(answer.isUnknownDueToTimeout());
            Assert.IsFalse(answer.isPartialResultDueToTimeout());
            Assert.IsTrue(1 == answer.getProofs().Count);
            Assert.IsTrue(0 == answer.getProofs()[0].getAnswerBindings()
                    .Count);
        }

        protected void testEqualityAndSubstitutionAxiomsKBabcdPDSucceeds(
                InferenceProcedure infp)
        {
            FOLKnowledgeBase akb = FOLKnowledgeBaseFactory
                    .createABCDEqualityAndSubstitutionKnowledgeBase(infp, true);

            List<Term> terms = new List<Term>();
            terms.Add(new Constant("D"));
            Predicate query = new Predicate("P", terms);

            InferenceResult answer = akb.ask(query);

            Assert.IsTrue(null != answer);
            Assert.IsFalse(answer.isPossiblyFalse());
            Assert.IsTrue(answer.isTrue());
            Assert.IsFalse(answer.isUnknownDueToTimeout());
            Assert.IsFalse(answer.isPartialResultDueToTimeout());
            Assert.IsTrue(1 == answer.getProofs().Count);
            Assert.IsTrue(0 == answer.getProofs()[0].getAnswerBindings()
                    .Count);
        }

        protected void testEqualityAndSubstitutionAxiomsKBabcdPFFASucceeds(
                InferenceProcedure infp, bool expectedToTimeOut)
        {
            FOLKnowledgeBase akb = FOLKnowledgeBaseFactory
                    .createABCDEqualityAndSubstitutionKnowledgeBase(infp, true);

            List<Term> terms = new List<Term>();
            terms.Add(new Constant("A"));
            Function fa = new Function("F", terms);
            terms = new List<Term>();
            terms.Add(fa);
            Function ffa = new Function("F", terms);
            terms = new List<Term>();
            terms.Add(ffa);
            Predicate query = new Predicate("P", terms);

            InferenceResult answer = akb.ask(query);

            if (expectedToTimeOut)
            {
                Assert.IsFalse(answer.isPossiblyFalse());
                Assert.IsFalse(answer.isTrue());
                Assert.IsTrue(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(0 == answer.getProofs().Count);
            }
            else
            {
                Assert.IsTrue(null != answer);
                Assert.IsFalse(answer.isPossiblyFalse());
                Assert.IsTrue(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(1 == answer.getProofs().Count);
                Assert.IsTrue(0 == answer.getProofs()[0]
                        .getAnswerBindings().Count);
            }
        }

        protected void testEqualityNoAxiomsKBabcAEqualsCSucceeds(
                InferenceProcedure infp, bool expectedToFail)
        {
            FOLKnowledgeBase akb = FOLKnowledgeBaseFactory
                    .createABCEqualityKnowledgeBase(infp, false);

            TermEquality query = new TermEquality(new Constant("A"), new Constant(
                    "C"));

            InferenceResult answer = akb.ask(query);

            Assert.IsTrue(null != answer);
            if (expectedToFail)
            {
                Assert.IsTrue(answer.isPossiblyFalse());
                Assert.IsFalse(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(0 == answer.getProofs().Count);
            }
            else
            {
                Assert.IsFalse(answer.isPossiblyFalse());
                Assert.IsTrue(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(1 == answer.getProofs().Count);
                Assert.IsTrue(0 == answer.getProofs()[0]
                        .getAnswerBindings().Count);
            }
        }

        protected void testEqualityAndSubstitutionNoAxiomsKBabcdFFASucceeds(
                InferenceProcedure infp, bool expectedToFail)
        {
            FOLKnowledgeBase akb = FOLKnowledgeBaseFactory
                    .createABCDEqualityAndSubstitutionKnowledgeBase(infp, false);

            List<Term> terms = new List<Term>();
            terms.Add(new Constant("A"));
            Function fa = new Function("F", terms);
            terms = new List<Term>();
            terms.Add(fa);
            TermEquality query = new TermEquality(new Function("F", terms),
                    new Constant("A"));

            InferenceResult answer = akb.ask(query);

            Assert.IsTrue(null != answer);
            if (expectedToFail)
            {
                Assert.IsTrue(answer.isPossiblyFalse());
                Assert.IsFalse(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(0 == answer.getProofs().Count);
            }
            else
            {
                Assert.IsFalse(answer.isPossiblyFalse());
                Assert.IsTrue(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(1 == answer.getProofs().Count);
                Assert.IsTrue(0 == answer.getProofs()[0]
                        .getAnswerBindings().Count);
            }
        }

        protected void testEqualityAndSubstitutionNoAxiomsKBabcdPDSucceeds(
                InferenceProcedure infp, bool expectedToFail)
        {
            FOLKnowledgeBase akb = FOLKnowledgeBaseFactory
                    .createABCDEqualityAndSubstitutionKnowledgeBase(infp, false);

            List<Term> terms = new List<Term>();
            terms.Add(new Constant("D"));
            Predicate query = new Predicate("P", terms);

            InferenceResult answer = akb.ask(query);

            Assert.IsTrue(null != answer);
            if (expectedToFail)
            {
                Assert.IsTrue(answer.isPossiblyFalse());
                Assert.IsFalse(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(0 == answer.getProofs().Count);
            }
            else
            {
                Assert.IsFalse(answer.isPossiblyFalse());
                Assert.IsTrue(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(1 == answer.getProofs().Count);
                Assert.IsTrue(0 == answer.getProofs()[0]
                        .getAnswerBindings().Count);
            }
        }

        protected void testEqualityAndSubstitutionNoAxiomsKBabcdPFFASucceeds(
                InferenceProcedure infp, bool expectedToFail)
        {
            FOLKnowledgeBase akb = FOLKnowledgeBaseFactory
                    .createABCDEqualityAndSubstitutionKnowledgeBase(infp, false);

            List<Term> terms = new List<Term>();
            terms.Add(new Constant("A"));
            Function fa = new Function("F", terms);
            terms = new List<Term>();
            terms.Add(fa);
            Function ffa = new Function("F", terms);
            terms = new List<Term>();
            terms.Add(ffa);
            Predicate query = new Predicate("P", terms);

            InferenceResult answer = akb.ask(query);

            Assert.IsTrue(null != answer);
            if (expectedToFail)
            {
                Assert.IsTrue(answer.isPossiblyFalse());
                Assert.IsFalse(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(0 == answer.getProofs().Count);
            }
            else
            {
                Assert.IsFalse(answer.isPossiblyFalse());
                Assert.IsTrue(answer.isTrue());
                Assert.IsFalse(answer.isUnknownDueToTimeout());
                Assert.IsFalse(answer.isPartialResultDueToTimeout());
                Assert.IsTrue(1 == answer.getProofs().Count);
                Assert.IsTrue(0 == answer.getProofs()[0]
                        .getAnswerBindings().Count);
            }
        }
    }
}