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
    public class ProofStepFoChAlreadyAFact : AbstractProofStep
    {
        //
        private readonly List<ProofStep> _noPredecessors = new List<ProofStep>();
        //
        private Literal fact = null;

        public ProofStepFoChAlreadyAFact(Literal fact)
        {
            this.fact = fact;
        }

        //
        // START-ProofStep
        public override List<ProofStep> getPredecessorSteps()
        {
            return new ReadOnlyCollection<ProofStep>(_noPredecessors).ToList<ProofStep>();
        }

        public override String getProof()
        {
            return fact.ToString();
        }

        public override String getJustification()
        {
            return "Already a known fact in the KB.";
        }
        // END-ProofStep
        //
    }
}