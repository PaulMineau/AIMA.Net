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
    public class ProofStepClauseClausifySentence : AbstractProofStep
    {
        private List<ProofStep> predecessors = new List<ProofStep>();
        private Clause clausified = null;

        public ProofStepClauseClausifySentence(Clause clausified,
                Sentence origSentence)
        {
            this.clausified = clausified;
            this.predecessors.Add(new ProofStepPremise(origSentence));
        }

        //
        // START-ProofStep
        public override List<ProofStep> getPredecessorSteps()
        {
            return new ReadOnlyCollection<ProofStep>(predecessors).ToList<ProofStep>();
        }

        public override String getProof()
        {
            return clausified.ToString();
        }

        public override String getJustification()
        {
            return "Clausified " + predecessors[0].getStepNumber();
        }
        // END-ProofStep
        //
    }
}