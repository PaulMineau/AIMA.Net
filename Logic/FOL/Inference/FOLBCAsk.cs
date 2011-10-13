namespace AIMA.Core.Logic.FOL.Inference
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AIMA.Core.Logic.FOL.Inference.Proof;
    using AIMA.Core.Logic.FOL.KB;
    using AIMA.Core.Logic.FOL.KB.Data;
    using AIMA.Core.Logic.FOL.Parsing.AST;

    /**
     * Artificial Intelligence A Modern Approach (2nd Edition): Figure 9.6, page 288.
     * 
     * <pre>
     * function FOL-BC-ASK(KB, goals, theta) returns a set of substitutions
     *   input: KB, a knowledge base
     *          goals, a list of conjuncts forming a query (theta already applied)
     *          theta, the current substitution, initially the empty substitution {}
     *   local variables: answers, a set of substitutions, initially empty
     *   
     *   if goals is empty then return {theta}
     *   qDelta <- SUBST(theta, FIRST(goals))
     *   for each sentence r in KB where STANDARDIZE-APART(r) = (p1 ^ ... ^ pn => q)
     *          and thetaDelta <- UNIFY(q, qDelta) succeeds
     *       new_goals <- [p1,...,pn|REST(goals)]
     *       answers <- FOL-BC-ASK(KB, new_goals, COMPOSE(thetaDelta, theta)) U answers
     *   return answers
     * </pre>
     * 
     * Figure 9.6 A simple backward-chaining algorithm.
     */

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class FOLBCAsk : InferenceProcedure
    {

        public FOLBCAsk()
        {

        }

        //
        // START-InferenceProcedure
        public InferenceResult ask(FOLKnowledgeBase KB, Sentence query)
        {
            // Assertions on the type queries this Inference procedure
            // supports
            if (!(query is AtomicSentence))
            {
                throw new ArgumentException(
                        "Only Atomic Queries are supported.");
            }

            List<Literal> goals = new List<Literal>();
            goals.Add(new Literal((AtomicSentence)query));

            BCAskAnswerHandler ansHandler = new BCAskAnswerHandler();

            List<List<ProofStepBwChGoal>> allProofSteps = folbcask(KB, ansHandler,
                    goals, new Dictionary<Variable, Term>());

            ansHandler.setAllProofSteps(allProofSteps);

            return ansHandler;
        }

        // END-InferenceProcedure
        //

        //
        // PRIVATE METHODS
        //

        /**
         * <code>
         * function FOL-BC-ASK(KB, goals, theta) returns a set of substitutions
         *   input: KB, a knowledge base
         *          goals, a list of conjuncts forming a query (theta already applied)
         *          theta, the current substitution, initially the empty substitution {}
         * </code>
         */
        private List<List<ProofStepBwChGoal>> folbcask(FOLKnowledgeBase KB,
                BCAskAnswerHandler ansHandler, List<Literal> goals,
                Dictionary<Variable, Term> theta)
        {
            List<List<ProofStepBwChGoal>> thisLevelProofSteps = new List<List<ProofStepBwChGoal>>();
            // local variables: answers, a set of substitutions, initially empty

            // if goals is empty then return {theta}
            if (goals.Count==0)
            {
                thisLevelProofSteps.Add(new List<ProofStepBwChGoal>());
                return thisLevelProofSteps;
            }

            // qDelta <- SUBST(theta, FIRST(goals))
            Literal qDelta = KB.subst(theta, goals[0]);

            // for each sentence r in KB where
            // STANDARDIZE-APART(r) = (p1 ^ ... ^ pn => q)
            foreach (Clause r in KB.getAllDefiniteClauses())
            {
                Clause r2 = KB.standardizeApart(r);
                // and thetaDelta <- UNIFY(q, qDelta) succeeds
                Dictionary<Variable, Term> thetaDelta = KB.unify(r2.getPositiveLiterals()
                        [0].getAtomicSentence(), qDelta.getAtomicSentence());
                if (null != thetaDelta)
                {
                    // new_goals <- [p1,...,pn|REST(goals)]
                    List<Literal> newGoals = new List<Literal>(r2
                            .getNegativeLiterals());
                    newGoals.AddRange(goals.Skip(1));
                    // answers <- FOL-BC-ASK(KB, new_goals, COMPOSE(thetaDelta,
                    // theta)) U answers
                    Dictionary<Variable, Term> composed = compose(KB, thetaDelta, theta);
                    List<List<ProofStepBwChGoal>> lowerLevelProofSteps = folbcask(
                            KB, ansHandler, newGoals, composed);

                    ansHandler.addProofStep(lowerLevelProofSteps, r2, qDelta,
                            composed);

                    thisLevelProofSteps.AddRange(lowerLevelProofSteps);
                }
            }

            // return answers
            return thisLevelProofSteps;
        }

        // Artificial Intelligence A Modern Approach (2nd Edition): page 288.
        // COMPOSE(delta, tau) is the substitution whose effect is identical to
        // the effect of applying each substitution in turn. That is,
        // SUBST(COMPOSE(theta1, theta2), p) = SUBST(theta2, SUBST(theta1, p))
        private Dictionary<Variable, Term> compose(FOLKnowledgeBase KB,
                Dictionary<Variable, Term> theta1, Dictionary<Variable, Term> theta2)
        {
            Dictionary<Variable, Term> composed = new Dictionary<Variable, Term>();

            // So that it behaves like:
            // SUBST(theta2, SUBST(theta1, p))
            // There are two steps involved here.
            // See: http://logic.stanford.edu/classes/cs157/2008/notes/chap09.pdf
            // for a detailed discussion:

            // 1. Apply theta2 to the range of theta1.
            foreach (Variable v in theta1.Keys)
            {
                composed.Add(v, KB.subst(theta2, theta1[v]));
            }

            // 2. Adjoin to delta all pairs from tau with different
            // domain variables.
            foreach (Variable v in theta2.Keys)
            {
                if (!theta1.ContainsKey(v))
                {
                    composed.Add(v, theta2[v]);
                }
            }

            return cascadeSubstitutions(KB, composed);
        }

        // See:
        // http://logic.stanford.edu/classes/cs157/2008/miscellaneous/faq.html#jump165
        // for need for this.
        private Dictionary<Variable, Term> cascadeSubstitutions(FOLKnowledgeBase KB,
                Dictionary<Variable, Term> theta)
        {
            foreach (Variable v in theta.Keys)
            {
                Term t = theta[v];
                theta.Add(v, KB.subst(theta, t));
            }

            return theta;
        }

        class BCAskAnswerHandler : InferenceResult
        {

            private List<Proof.Proof> proofs = new List<Proof.Proof>();

            public BCAskAnswerHandler()
            {

            }

            //
            // START-InferenceResult
            public bool isPossiblyFalse()
            {
                return proofs.Count == 0;
            }

            public bool isTrue()
            {
                return proofs.Count > 0;
            }

            public bool isUnknownDueToTimeout()
            {
                return false;
            }

            public bool isPartialResultDueToTimeout()
            {
                return false;
            }

            public List<Proof.Proof> getProofs()
            {
                return proofs;
            }

            // END-InferenceResult
            //

            public void setAllProofSteps(List<List<ProofStepBwChGoal>> allProofSteps)
            {
                foreach (List<ProofStepBwChGoal> steps in allProofSteps)
                {
                    ProofStepBwChGoal lastStep = steps[steps.Count - 1];
                    Dictionary<Variable, Term> theta = lastStep.getBindings();
                    proofs.Add(new ProofFinal(lastStep, theta));
                }
            }

            public void addProofStep(
                    List<List<ProofStepBwChGoal>> currentLevelProofSteps,
                    Clause toProve, Literal currentGoal,
                    Dictionary<Variable, Term> bindings)
            {

                if (currentLevelProofSteps.Count > 0)
                {
                    ProofStepBwChGoal predecessor = new ProofStepBwChGoal(toProve,
                            currentGoal, bindings);
                    foreach (List<ProofStepBwChGoal> steps in currentLevelProofSteps)
                    {
                        if (steps.Count > 0)
                        {
                            steps[0].setPredecessor(predecessor);
                        }
                        steps.Insert(0, predecessor);
                    }
                }
            }
        }
    }
}