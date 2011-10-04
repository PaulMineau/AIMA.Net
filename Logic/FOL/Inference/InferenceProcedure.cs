namespace CosmicFlow.AIMA.Core.Logic.FOL.Inference
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.FOL.KB;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing.AST;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public interface InferenceProcedure
    {
        /**
         * 
         * @param kb
         *            the knowledge base against which the query is to be made.
         * @param aQuery
         *            to be answered.
         * @return an InferenceResult.
         */
        InferenceResult ask(FOLKnowledgeBase kb, Sentence aQuery);
    }
}