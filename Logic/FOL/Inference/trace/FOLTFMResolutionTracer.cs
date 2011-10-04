namespace CosmicFlow.AIMA.Core.Logic.FOL.Inference.Trace
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.FOL.Inference;
    using CosmicFlow.AIMA.Core.Logic.FOL.KB.Data;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public interface FOLTFMResolutionTracer
    {
        void stepStartWhile(List<Clause> clauses, int totalNoClauses,
                int totalNoNewCandidateClauses);

        void stepOuterFor(Clause i);

        void stepInnerFor(Clause i, Clause j);

        void stepResolved(Clause iFactor, Clause jFactor, List<Clause> resolvents);

        void stepFinished(List<Clause> clauses, InferenceResult result);
    }
}