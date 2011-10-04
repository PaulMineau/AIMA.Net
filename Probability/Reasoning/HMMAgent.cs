using NC3A.SI.Rowlex;
[assembly: Ontology("AIMA", "http://www.test.com/AIMA")]

namespace CosmicFlow.AIMA.Core.Probability.Reasoning
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Probability;


    /**
     * @author Ravi Mohan
     * 
     */
    [RdfSerializable(HasResourceUri=false)]
    public class HMMAgent
    {
        private HiddenMarkovModel hmm;

        [RdfProperty(true,
            Name = "belief",
            DomainsAsType = new Type[] { typeof(HMMAgent), typeof(RandomVariable) },
            Domains = new string[] { "http://extradomain.com/10#type3" },
            //RangesAsType = new Type[] {typeof(Item)},
            //Ranges = new string[] { "http://extradomain2.com/10#type4" },
            UseLocalRestrictionInsteadOfDomain = false)]
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