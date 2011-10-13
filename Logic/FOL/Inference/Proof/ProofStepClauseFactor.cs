namespace AIMA.Core.Logic.FOL.Inference.Proof
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AIMA.Core.Logic.FOL.KB.Data;
    using System.Collections.ObjectModel;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class ProofStepClauseFactor : AbstractProofStep
    {
        private List<ProofStep> predecessors = new List<ProofStep>();
        private Clause factor = null;
        private Clause factorOf = null;

        public ProofStepClauseFactor(Clause factor, Clause factorOf)
        {
            this.factor = factor;
            this.factorOf = factorOf;
            this.predecessors.Add(factorOf.getProofStep());
        }

        //
        // START-ProofStep
        public override List<ProofStep> getPredecessorSteps()
        {
            return new ReadOnlyCollection<ProofStep>(predecessors).ToList<ProofStep>();
        }

        public override String getProof()
        {
            return factor.ToString();
        }

        public override String getJustification()
        {
            return "Factor of " + factorOf.getProofStep().getStepNumber();
        }
        // END-ProofStep
        //
    }
}