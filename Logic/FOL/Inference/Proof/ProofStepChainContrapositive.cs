namespace CosmicFlow.AIMA.Core.Logic.FOL.Inference.Proof
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CosmicFlow.AIMA.Core.Logic.FOL.KB.Data;
    using System.Collections.ObjectModel;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class ProofStepChainContrapositive : AbstractProofStep
    {
        private List<ProofStep> predecessors = new List<ProofStep>();
        private Chain contrapositive = null;
        private Chain contrapositiveOf = null;

        public ProofStepChainContrapositive(Chain contrapositive,
                Chain contrapositiveOf)
        {
            this.contrapositive = contrapositive;
            this.contrapositiveOf = contrapositiveOf;
            this.predecessors.Add(contrapositiveOf.getProofStep());
        }

        //
        // START-ProofStep
        public override List<ProofStep> getPredecessorSteps()
        {
            return new ReadOnlyCollection<ProofStep>(predecessors).ToList<ProofStep>();
        }

        public override String getProof()
        {
            return contrapositive.ToString();
        }

        public override String getJustification()
        {
            return "Contrapositive: "
                    + contrapositiveOf.getProofStep().getStepNumber();
        }
        // END-ProofStep
        //
    }
}