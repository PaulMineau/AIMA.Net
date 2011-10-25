using System;
using AIMA.Probability.Util;

namespace AIMA.Probability
{

    /**
     * An implementation of the FiniteProbabilityModel API using a full joint
     * distribution as the underlying model.
     * 
     * @author Ciaran O'Reilly
     */

    public class FullJointDistributionModel : FiniteProbabilityModel
    {

        private ProbabilityTable distribution = null;
        private Set<RandomVariable> representation = null;

        public FullJointDistributionModel(double[] values, params RandomVariable[] vars)
        {
            if (null == vars)
            {
                throw new ArgumentException(
                    "Random Variables describing the model's representation of the World need to be specified.");
            }

            distribution = new ProbabilityTable(values, vars);

            representation = new Set<RandomVariable>();
            for (int i = 0; i < vars.Length; i++)
            {
                representation.add(vars[i]);
            }
            //representation = Collections.unmodifiableSet(representation);
        }

        //
        // START-ProbabilityModel
        public bool isValid()
        {
            // Handle rounding
            return Math.Abs(1 - distribution.getSum()) <= ProbabilityModel.DEFAULT_ROUNDING_THRESHOLD;
        }

        public double prior(params IProposition[] phi)
        {
            return probabilityOf(ProbUtil.constructConjunction(phi));
        }

        public double posterior(IProposition phi, params IProposition[] evidence)
        {

            IProposition conjEvidence = ProbUtil.constructConjunction(evidence);

            // P(A | B) = P(A AND B)/P(B) - (13.3 AIMA3e)
            IProposition aAndB = new ConjunctiveProposition(phi, conjEvidence);
            double probabilityOfEvidence = prior(conjEvidence);
            if (0 != probabilityOfEvidence)
            {
                return prior(aAndB)/probabilityOfEvidence;
            }

            return 0;
        }

        public Set<RandomVariable> getRepresentation()
        {
            return representation;
        }

        // END-ProbabilityModel
        //

        //
        // START-FiniteProbabilityModel
        public CategoricalDistribution priorDistribution(params IProposition[] phi)
        {
            return jointDistribution(phi);
        }

        public CategoricalDistribution posteriorDistribution(IProposition phi,
                                                             params IProposition[] evidence)
        {

            IProposition conjEvidence = ProbUtil.constructConjunction(evidence);

            // P(A | B) = P(A AND B)/P(B) - (13.3 AIMA3e)
            CategoricalDistribution dAandB = jointDistribution(phi, conjEvidence);
            CategoricalDistribution dEvidence = jointDistribution(conjEvidence);

            return dAandB.divideBy(dEvidence);
        }

        public CategoricalDistribution jointDistribution(
            params IProposition[] propositions)
        {
            ProbabilityTable d = null;
            IProposition conjProp = ProbUtil
                .constructConjunction(propositions);
            LinkedHashSet<RandomVariable> vars = new LinkedHashSet<RandomVariable>(
                conjProp.getUnboundScope());

            if (vars.Count > 0)
            {
                RandomVariable[] distVars = new RandomVariable[vars.Count];
                vars.CopyTo(distVars);

                ProbabilityTable ud = new ProbabilityTable(distVars);
                Object[] values = new Object[vars.Count];

                //ProbabilityTable.Iterator di = new ProbabilityTable.Iterator() {

                //    public void iterate(Map<RandomVariable, Object> possibleWorld,
                //            double probability) {
                //        if (conjProp.holds(possibleWorld)) {
                //            int i = 0;
                //            for (RandomVariable rv : vars) {
                //                values[i] = possibleWorld.get(rv);
                //                i++;
                //            }
                //            int dIdx = ud.getIndex(values);
                //            ud.setValue(dIdx, ud.getValues()[dIdx] + probability);
                //        }
                //    }
                //};

                //distribution.iterateOverTable(di);
                // TODO:
                d = ud;
            }
            else
            {
                // No Unbound Variables, therefore just return
                // the singular probability related to the proposition.
                d = new ProbabilityTable();
                d.setValue(0, prior(propositions));
            }
            return d;
        }

        // END-FiniteProbabilityModel
        //

        //
        // PRIVATE METHODS
        //
        private double probabilityOf(IProposition phi)
        {
            double[] probSum = new double[1];
            //ProbabilityTable.Iterator di = new ProbabilityTable.Iterator() {
            //    public void iterate(Map<RandomVariable, Object> possibleWorld,
            //            double probability) {
            //        if (phi.holds(possibleWorld)) {
            //            probSum[0] += probability;
            //        }
            //    }
            //};

            //distribution.iterateOverTable(di);
            // TODO
            return probSum[0];
        }
    }
}