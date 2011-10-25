using System;
using System.Collections.Generic;

namespace AIMA.Probability.Bayes
{
    /**
     * A conditional probability distribution on a RandomVariable X<sub>i</sub>:<br>
     * <br>
     * P(X<sub>i</sub> | Parents(X<sub>i</sub>)) that quantifies the effect of the
     * parents on X<sub>i</sub>.
     * 
     * @author Ciaran O'Reilly
     * 
     */

    public interface ConditionalProbabilityDistribution
    {
        /**
         * @return the Random Variable this conditional probability distribution is
         *         on.
         */
        RandomVariable getOn();

        /**
         * @return a consistent ordered Set (e.g. LinkedHashSet) of the parent
         *         Random Variables for this conditional distribution.
         */
        Set<RandomVariable> getParents();

        /**
         * A convenience method for merging the elements of getParents() and getOn()
         * into a consistent ordered set (i.e. getOn() should always be the last
         * Random Variable returned when iterating over the set).
         * 
         * @return a consistent ordered Set (e.g. LinkedHashSet) of the random
         *         variables this conditional probability distribution is for.
         */
        Set<RandomVariable> getFor();

        /**
         * 
         * @param rv
         *            the Random Variable to be checked.
         * @return true if the conditional distribution is for the passed in Random
         *         Variable, false otherwise.
         */
        bool contains(RandomVariable rv);

        /**
         * Get the value for the provided set of values for the random variables
         * comprising the Conditional Distribution (ordering and size of each must
         * equal getFor() and their domains must match).
         * 
         * @param eventValues
         *            the values for the random variables comprising the Conditional
         *            Distribution
         * @return the value for the possible worlds associated with the assignments
         *         for the random variables comprising the Conditional Distribution.
         */
        double getValue(params Object[] eventValues);

        /**
         * Get the value for the provided set of AssignmentPropositions for the
         * random variables comprising the Conditional Distribution (size of each
         * must equal and their random variables must match).
         * 
         * @param eventValues
         *            the assignment propositions for the random variables
         *            comprising the Conditional Distribution
         * @return the value for the possible worlds associated with the assignments
         *         for the random variables comprising the Conditional Distribution.
         */
        double getValue(params AssignmentProposition[] eventValues);

        /**
         * A conditioning case is just a possible combination of values for the
         * parent nodes - a miniature possible world, if you like. The returned
         * distribution must sum to 1, because the entries represent an exhaustive
         * set of cases for the random variable.
         * 
         * @param parentValues
         *            for the conditioning case. The ordering and size of
         *            parentValues must equal getParents() and their domains must
         *            match.
         * @return the Probability Distribution for the Random Variable the
         *         Conditional Probability Distribution is On.
         * @see ConditionalProbabilityDistribution#getOn()
         * @see ConditionalProbabilityDistribution#getParents()
         */
        ProbabilityDistribution getConditioningCase(params Object[] parentValues);

        /**
         * A conditioning case is just a possible combination of values for the
         * parent nodes - a miniature possible world, if you like. The returned
         * distribution must sum to 1, because the entries represent an exhaustive
         * set of cases for the random variable.
         * 
         * @param parentValues
         *            for the conditioning case. The size of parentValues must equal
         *            getParents() and their Random Variables must match.
         * @return the Probability Distribution for the Random Variable the
         *         Conditional Probability Distribution is On.
         * @see ConditionalProbabilityDistribution#getOn()
         * @see ConditionalProbabilityDistribution#getParents()
         */

        ProbabilityDistribution getConditioningCase(
            params AssignmentProposition[] parentValues);

        /**
         * Retrieve a specific example for the Random Variable this conditional
         * distribution is on.
         * 
         * @param probabilityChoice
         *            a double value, from the range [0.0d, 1.0d), i.e. 0.0d
         *            (inclusive) to 1.0d (exclusive).
         * @param parentValues
         *            for the conditioning case. The ordering and size of
         *            parentValues must equal getParents() and their domains must
         *            match.
         * @return a sample value from the domain of the Random Variable this
         *         distribution is on, based on the probability argument passed in.
         * @see ConditionalProbabilityDistribution#getOn()
         */
        Object getSample(double probabilityChoice, params Object[] parentValues);

        /**
         * Retrieve a specific example for the Random Variable this conditional
         * distribution is on.
         * 
         * @param probabilityChoice
         *            a double value, from the range [0.0d, 1.0d), i.e. 0.0d
         *            (inclusive) to 1.0d (exclusive).
         * @param parentValues
         *            for the conditioning case. The size of parentValues must equal
         *            getParents() and their Random Variables must match.
         * @return a sample value from the domain of the Random Variable this
         *         distribution is on, based on the probability argument passed in.
         * @see ConditionalProbabilityDistribution#getOn()
         */

        Object getSample(double probabilityChoice,
                         params AssignmentProposition[] parentValues);
    }
}