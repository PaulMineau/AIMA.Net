namespace CosmicFlow.AIMA.Core.Probability.Decision
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Util.DataStructure;

    /**
     * @author Ravi Mohan
     * 
     */
    public class MDPTransition<STATE_TYPE, ACTION_TYPE>
    {
        private Triplet<STATE_TYPE, ACTION_TYPE, STATE_TYPE> triplet;

        public MDPTransition(STATE_TYPE initial, ACTION_TYPE action,
                STATE_TYPE destination)
        {
            this.triplet = new Triplet<STATE_TYPE, ACTION_TYPE, STATE_TYPE>(
                    initial, action, destination);
        }

        public STATE_TYPE getInitialState()
        {
            return triplet.getFirst();
        }

        public ACTION_TYPE getAction()
        {
            return triplet.getSecond();
        }

        public STATE_TYPE getDestinationState()
        {
            return triplet.getThird();
        }

        public override bool Equals(Object o)
        {
            if (o == this)
            {
                return true;
            }
            if (!(o is MDPTransition<STATE_TYPE, ACTION_TYPE>))
            {
                return false;
            }
            MDPTransition<STATE_TYPE, ACTION_TYPE> other = (MDPTransition<STATE_TYPE, ACTION_TYPE>)(o);// weird
            // typing
            // issue
            // work
            // out
            // later
            return triplet.Equals(other.triplet);
        }

        public override int GetHashCode()
        {
            return triplet.GetHashCode();
        }

        public override String ToString()
        {
            return triplet.ToString();
        }
    }
}