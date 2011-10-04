namespace CosmicFlow.AIMA.Core.Logic.FOL.Inference.Proof
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CosmicFlow.AIMA.Core.Logic.FOL.KB.Data;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing.AST;
    using System.Collections.ObjectModel;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class ProofStepChainCancellation : AbstractProofStep
    {
        private List<ProofStep> predecessors = new List<ProofStep>();
        private Chain cancellation = null;
        private Chain cancellationOf = null;
        private Dictionary<Variable, Term> subst = null;

        public ProofStepChainCancellation(Chain cancellation, Chain cancellationOf,
                Dictionary<Variable, Term> subst)
        {
            this.cancellation = cancellation;
            this.cancellationOf = cancellationOf;
            this.subst = subst;
            this.predecessors.Add(cancellationOf.getProofStep());
        }

        //
        // START-ProofStep
        public override List<ProofStep> getPredecessorSteps()
        {
            return new ReadOnlyCollection<ProofStep>(predecessors).ToList<ProofStep>();
        }

        public override String getProof()
        {
            return cancellation.ToString();
        }

        public override String getJustification()
        {
            return "Cancellation: " + cancellationOf.getProofStep().getStepNumber()
                    + " " + subst;
        }
        // END-ProofStep
        //
    }
}