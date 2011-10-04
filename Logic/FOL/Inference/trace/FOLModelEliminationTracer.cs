namespace CosmicFlow.AIMA.Core.Logic.FOL.Inference.Trace
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ciaran O'Reilly
     * 
     */
    public interface FOLModelEliminationTracer
    {
        void reset();

        void increment(int depth, int noFarParents);
    }
}