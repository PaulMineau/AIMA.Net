namespace AIMA.Core.Logic.FOL.Inference.Proof
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AIMA.Core.Logic.FOL.KB.Data;
    using AIMA.Core.Logic.FOL.Parsing.AST;
    using System.Collections.ObjectModel;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class ProofStepClauseDemodulation : AbstractProofStep
    {
        private List<ProofStep> predecessors = new List<ProofStep>();
        private Clause demodulated = null;
        private Clause origClause = null;
        private TermEquality assertion = null;

        public ProofStepClauseDemodulation(Clause demodulated, Clause origClause,
                TermEquality assertion)
        {
            this.demodulated = demodulated;
            this.origClause = origClause;
            this.assertion = assertion;
            this.predecessors.Add(origClause.getProofStep());
        }

        //
        // START-ProofStep
        public override List<ProofStep> getPredecessorSteps()
        {
            return new ReadOnlyCollection<ProofStep>(predecessors).ToList<ProofStep>();
        }

        public override String getProof()
        {
            return demodulated.ToString();
        }

        public override String getJustification()
        {
            return "Demodulation: " + origClause.getProofStep().getStepNumber()
                    + ", [" + assertion + "]";
        }
        // END-ProofStep
        //
    }
}