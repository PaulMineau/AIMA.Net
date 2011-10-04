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
    public class ProofStepClauseParamodulation : AbstractProofStep
    {
        private List<ProofStep> predecessors = new List<ProofStep>();
        private Clause paramodulated = null;
        private Clause topClause = null;
        private Clause equalityClause = null;
        private TermEquality assertion = null;

        public ProofStepClauseParamodulation(Clause paramodulated,
                Clause topClause, Clause equalityClause, TermEquality assertion)
        {
            this.paramodulated = paramodulated;
            this.topClause = topClause;
            this.equalityClause = equalityClause;
            this.assertion = assertion;
            this.predecessors.Add(topClause.getProofStep());
            this.predecessors.Add(equalityClause.getProofStep());
        }

        //
        // START-ProofStep
        public override List<ProofStep> getPredecessorSteps()
        {
            return new ReadOnlyCollection<ProofStep>(predecessors).ToList<ProofStep>();
        }

        public override String getProof()
        {
            return paramodulated.ToString();
        }

        public override String getJustification()
        {
            return "Paramodulation: " + topClause.getProofStep().getStepNumber()
                    + ", " + equalityClause.getProofStep().getStepNumber() + ", ["
                    + assertion + "]";

        }
        // END-ProofStep
        //
    }
}
