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
    public class ProofStepClauseBinaryResolvent : AbstractProofStep
    {
        private List<ProofStep> predecessors = new List<ProofStep>();
        private Clause resolvent = null;
        private Clause parent1, parent2 = null;
        private Dictionary<Variable, Term> subst = null;
        private Dictionary<Variable, Term> renameSubst = null;

        public ProofStepClauseBinaryResolvent(Clause resolvent, Clause parent1,
                Clause parent2, Dictionary<Variable, Term> subst,
                Dictionary<Variable, Term> renameSubst)
        {
            this.resolvent = resolvent;
            this.parent1 = parent1;
            this.parent2 = parent2;
            this.subst = subst;
            this.renameSubst = renameSubst;
            this.predecessors.Add(parent1.getProofStep());
            this.predecessors.Add(parent2.getProofStep());
        }

        //
        // START-ProofStep
        public override List<ProofStep> getPredecessorSteps()
        {
            return new ReadOnlyCollection<ProofStep>(predecessors).ToList<ProofStep>();
        }

        public override String getProof()
        {
            return resolvent.ToString();
        }

        public override String getJustification()
        {
            int lowStep = parent1.getProofStep().getStepNumber();
            int highStep = parent2.getProofStep().getStepNumber();

            if (lowStep > highStep)
            {
                lowStep = highStep;
                highStep = parent1.getProofStep().getStepNumber();
            }

            return "Resolution: " + lowStep + "," + highStep + " " + subst + ", "
                    + renameSubst;
        }
        // END-ProofStep
        //
    }
}