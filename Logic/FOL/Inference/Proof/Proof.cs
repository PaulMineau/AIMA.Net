namespace AIMA.Core.Logic.FOL.Inference.Proof
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.FOL.Parsing.AST;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public interface Proof
    {
        /**
         * 
         * @return A list of proof steps that show how an answer was derived.
         */
        List<ProofStep> getSteps();

        /**
         * 
         * @return a Dictionary of bindings for any variables that were in the original
         *         query. Will be an empty Dictionary if no variables existed in the
         *         original query.
         */
        Dictionary<Variable, Term> getAnswerBindings();

        /**
         * 
         * @param updatedBindings
         *            allows for the bindings to be renamed. Note: should not be
         *            used for any other reason.
         */
        void replaceAnswerBindings(Dictionary<Variable, Term> updatedBindings);
    }
}