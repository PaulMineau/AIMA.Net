namespace CosmicFlow.AIMA.Core.Probability.Reasoning
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Probability;


    /**
     * @author Ravi Mohan
     * 
     */
    public class HMMAgent
    {
        private HiddenMarkovModel hmm;

        public RandomVariable Belief { get; set; }

        private RandomVariable _belief;

        public HMMAgent(HiddenMarkovModel hmm)
        {
            this.hmm = hmm;
            this._belief = hmm.prior().duplicate();
        }

        public RandomVariable belief()
        {
            return _belief;
        }

        public void act(String action)
        {
            _belief = hmm.predict(_belief, action);
        }

        public void waitWithoutActing()
        {
            act(HmmConstants.DO_NOTHING);
        }

        public void perceive(String perception)
        {
            _belief = hmm.perceptionUpdate(_belief, perception);
        }
    }
}