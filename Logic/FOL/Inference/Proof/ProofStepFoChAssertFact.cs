namespace CosmicFlow.AIMA.Core.Logic.FOL.Inference.Proof
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using CosmicFlow.AIMA.Core.Logic.FOL.KB.Data;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing.AST;
    using System.Collections.ObjectModel;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class ProofStepFoChAssertFact : AbstractProofStep
    {
        //
        private List<ProofStep> predecessors = new List<ProofStep>();
        //
        private Clause implication = null;
        private Literal fact = null;
        private Dictionary<Variable, Term> bindings = null;

        public ProofStepFoChAssertFact(Clause implication, Literal fact,
                Dictionary<Variable, Term> bindings, ProofStep predecessor)
        {
            this.implication = implication;
            this.fact = fact;
            this.bindings = bindings;
            if (null != predecessor)
            {
                predecessors.Add(predecessor);
            }
        }

        //
        // START-ProofStep
        public override List<ProofStep> getPredecessorSteps()
        {
            return new ReadOnlyCollection<ProofStep>(predecessors).ToList<ProofStep>();
        }

        public override String getProof()
        {
            StringBuilder sb = new StringBuilder();
            List<Literal> nLits = implication.getNegativeLiterals();
            for (int i = 0; i < implication.getNumberNegativeLiterals(); i++)
            {
                sb.Append(nLits[i].getAtomicSentence());
                if (i != (implication.getNumberNegativeLiterals() - 1))
                {
                    sb.Append(" AND ");
                }
            }
            sb.Append(" => ");
            sb.Append(implication.getPositiveLiterals()[0]);
            return sb.ToString();
        }

        public override String getJustification()
        {
            return "Assert fact " + fact.ToString() + ", " + bindings;
        }
        // END-ProofStep
        //
    }
}