using System;
using AIMA.Core.Util.DataStructure;
using AIMA.Probability.Util;

namespace AIMA.Probability.Bayes.Approx
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 534.<br>
     * <br>
     * 
     * <pre>
     * function LIKELIHOOD-WEIGHTING(X, e, bn, N) returns an estimate of <b>P</b>(X|e)
     *   inputs: X, the query variable
     *           e, observed values for variables E
     *           bn, a Bayesian network specifying joint distribution <b>P</b>(X<sub>1</sub>,...,X<sub>n</sub>)
     *           N, the total number of samples to be generated
     *   local variables: W, a vector of weighted counts for each value of X, initially zero
     *   
     *   for j = 1 to N do
     *       <b>x</b>,w <- WEIGHTED-SAMPLE(bn,e)
     *       W[x] <- W[x] + w where x is the value of X in <b>x</b>
     *   return NORMALIZE(W)
     * --------------------------------------------------------------------------------------
     * function WEIGHTED-SAMPLE(bn, e) returns an event and a weight
     *   
     *    w <- 1; <b>x</b> <- an event with n elements initialized from e
     *    foreach variable X<sub>i</sub> in X<sub>1</sub>,...,X<sub>n</sub> do
     *        if X<sub>i</sub> is an evidence variable with value x<sub>i</sub> in e
     *            then w <- w * P(X<sub>i</sub> = x<sub>i</sub> | parents(X<sub>i</sub>))
     *            else <b>x</b>[i] <- a random sample from <b>P</b>(X<sub>i</sub> | parents(X<sub>i</sub>))
     *    return <b>x</b>, w
     * </pre>
     * 
     * Figure 14.15 The likelihood-weighting algorithm for inference in Bayesian
     * networks. In WEIGHTED-SAMPLE, each nonevidence variable is sampled according
     * to the conditional distribution given the values already sampled for the
     * variable's parents, while a weight is accumulated based on the likelihood for
     * each evidence variable.<br>
     * <br>
     * <b>Note:</b> The implementation has been extended to handle queries with
     * multiple variables. <br>
     * 
     * @author Ciaran O'Reilly
     * @author Ravi Mohan
     */

    public class LikelihoodWeighting : BayesSampleInference
    {
        private Randomizer randomizer = null;

        public LikelihoodWeighting()
            : this(new JavaRandomizer())
        {

        }

        public LikelihoodWeighting(Randomizer r)
        {
            this.randomizer = r;
        }

        // function LIKELIHOOD-WEIGHTING(X, e, bn, N) returns an estimate of
        // <b>P</b>(X|e)
        /**
         * The LIKELIHOOD-WEIGHTING algorithm in Figure 14.15. For answering queries
         * given evidence in a Bayesian Network.
         * 
         * @param X
         *            the query variables
         * @param e
         *            observed values for variables E
         * @param bn
         *            a Bayesian network specifying joint distribution
         *            <b>P</b>(X<sub>1</sub>,...,X<sub>n</sub>)
         * @param N
         *            the total number of samples to be generated
         * @return an estimate of <b>P</b>(X|e)
         */

        public CategoricalDistribution likelihoodWeighting(RandomVariable[] X,
                                                           AssignmentProposition[] e, BayesianNetwork bn, int N)
        {
            // local variables: W, a vector of weighted counts for each value of X,
            // initially zero
            double[] W = new double[ProbUtil
                .expectedSizeOfCategoricalDistribution(X)];

            // for j = 1 to N do
            for (int j = 0; j < N; j++)
            {
                // <b>x</b>,w <- WEIGHTED-SAMPLE(bn,e)
                Pair<Map<RandomVariable, Object>, Double> x_w = weightedSample(bn,
                                                                               e);
                // W[x] <- W[x] + w where x is the value of X in <b>x</b>
                W[ProbUtil.indexOf(X, x_w.getFirst())] += x_w.getSecond();
            }
            // return NORMALIZE(W)
            return new ProbabilityTable(W, X).normalize();
        }

        // function WEIGHTED-SAMPLE(bn, e) returns an event and a weight
        /**
         * The WEIGHTED-SAMPLE function in Figure 14.15.
         * 
         * @param e
         *            observed values for variables E
         * @param bn
         *            a Bayesian network specifying joint distribution
         *            <b>P</b>(X<sub>1</sub>,...,X<sub>n</sub>)
         * @return return <b>x</b>, w - an event with its associated weight.
         */

        public Pair<Map<RandomVariable, Object>, Double> weightedSample(
            BayesianNetwork bn, AssignmentProposition[] e)
        {
            // w <- 1;
            double w = 1.0;
            // <b>x</b> <- an event with n elements initialized from e
            Map<RandomVariable, Object> x = new LinkedHashMap<RandomVariable, Object>();
            foreach (AssignmentProposition ap in
            e)
            {
                x.Add(ap.getTermVariable(), ap.getValue());
            }

            // foreach variable X<sub>i</sub> in X<sub>1</sub>,...,X<sub>n</sub> do
            foreach (RandomVariable Xi in
            bn.getVariablesInTopologicalOrder())
            {
                // if X<sub>i</sub> is an evidence variable with value x<sub>i</sub>
                // in e
                if (x.ContainsKey(Xi))
                {
                    // then w <- w * P(X<sub>i</sub> = x<sub>i</sub> |
                    // parents(X<sub>i</sub>))
                    w *= bn.getNode(Xi)
                        .getCPD()
                        .getValue(
                            ProbUtil.getEventValuesForXiGivenParents(
                                bn.getNode(Xi), x));
                }
                else
                {
                    // else <b>x</b>[i] <- a random sample from
                    // <b>P</b>(X<sub>i</sub> | parents(X<sub>i</sub>))
                    x.Add(Xi, ProbUtil.randomSample(bn.getNode(Xi), x, randomizer));
                }
            }
            // return <b>x</b>, w
            return new Pair<Map<RandomVariable, Object>, Double>(x, w);
        }

        //
        // START-BayesSampleInference

        public CategoricalDistribution ask(RandomVariable[] X,
                                           AssignmentProposition[] observedEvidence,
                                           BayesianNetwork bn, int N)
        {
            return likelihoodWeighting(X, observedEvidence, bn, N);
        }

        // END-BayesSampleInference
        //
    }
}
