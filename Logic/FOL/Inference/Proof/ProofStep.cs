namespace CosmicFlow.AIMA.Core.Logic.FOL.Inference.Proof
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ciaran O'Reilly
     * 
     */
    public interface ProofStep
    {
        int getStepNumber();

        void setStepNumber(int step);

        List<ProofStep> getPredecessorSteps();

        String getProof();

        String getJustification();
    }
}