using System;
using System.Collections.Generic;

namespace AIMA.Probability.Bayes
{

    /**
     * General interface to be implemented by Bayesian Inference algorithms.
     * 
     * @author Ciaran O'Reilly
     */

    public interface BayesInference
    {
        /**
         * @param X
         *            the query variables.
         * @param observedEvidence
         *            observed values for variables E.
         * @param bn
         *            a Bayes net with variables {X} &cup; E &cup; Y /* Y = hidden
         *            variables
         * @return a distribution over the query variables.
         */

        CategoricalDistribution ask(RandomVariable[] X,
                                    AssignmentProposition[] observedEvidence,
                                    BayesianNetwork bn);
    }
}
