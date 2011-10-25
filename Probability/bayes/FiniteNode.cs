namespace AIMA.Probability.Bayes
{


    /**
     * A node over a Random Variable that has a finite countable domain.
     * 
     * @author Ciaran O'Reilly
     * 
     */

    public interface FiniteNode : DiscreteNode
    {

        /**
         * 
         * @return the Conditional Probability Table detailing the finite set of
         *         probabilities for this Node.
         */
        ConditionalProbabilityTable getCPT();
    }
}