namespace AIMA.Probability.MDP
{

    /**
     * An interface for MDP transition probability functions.
     * 
     * @param <S>
     *            the state type.
     * @param <A>
     *            the action type.
     * 
     * @author Ciaran O'Reilly
     * @author Ravi Mohan
     */

    public class TransitionProbabilityFunction<S, A>
    {

        /**
         * Return the probability of going from state s using action a to s' based
         * on the underlying transition model P(s' | s, a).
         * 
         * @param sDelta
         *            the state s' being transitioned to.
         * @param s
         *            the state s being transitions from.
         * @param a
         *            the action used to move from state s to s'.
         * @return the probability of going from state s using action a to s'.
         */
       public virtual double probability(S sDelta, S s, A a)
       {
           // TODO
           return double.MinValue;
       }
    }
}
