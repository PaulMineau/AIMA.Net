using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Windows.Markup;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AIMA.Core.Probability.Reasoning;
using AIMA.Core.Util.DataStructure;
using AIMA.Core.Probability;

namespace AIMA.Probability.Reasoning
{
    /// <summary>
    /// An activity that executes child activities in order and supports a pre/post activity
    /// </summary>
    [ContentProperty("Activities")]
    [Designer(typeof(HMMFactoryDesigner))]
    public sealed class HMMFactoryActivity : CodeActivity<HiddenMarkovModel>
    {
        #region Private Members
        
        Collection<String> states;
        Collection<String> perceptions;
        Collection<Triplet<String, String, Double>> transitionProbabilities;
        Collection<Triplet<String, String, Double>> sensingProbabilities;

        #endregion

        public Collection<String> States
        {
            get
            {
                if (states == null)
                {
                    states = new Collection<String>();
                }
                return states;
            }
            set
            {
                states = value;
            }
        }

        public Collection<String> Perceptions
        {
            get
            {
                if (perceptions == null)
                {
                    perceptions = new Collection<String>();
                }
                return perceptions;
            }
            set
            {
                perceptions = value;
            }
        }

        public Collection<Triplet<String, String, double>> TransitionProbabilities
        {
            get
            {
                if (transitionProbabilities == null)
                {
                    transitionProbabilities = new Collection<Triplet<string, string, double>>();
                }
                return transitionProbabilities;
            }
            set
            {
                transitionProbabilities = value;
            }
        }

        public Collection<Triplet<String, String, double>> SensingProbabilities
        {
            get
            {
                if (sensingProbabilities == null)
                {
                    sensingProbabilities = new Collection<Triplet<string, string, double>>();
                }
                return sensingProbabilities;
            }
            set
            {
                sensingProbabilities = value;
            }
        }


        /// <summary>
        /// Implementation of the activity
        /// </summary>
        /// <param name="context">The context used to schedule</param>
        protected override HiddenMarkovModel Execute(CodeActivityContext context)
        {
            List<String> statesList = States.ToList<String>();
            RandomVariable prior = new RandomVariable(statesList);
            TransitionModel tm = new TransitionModel(States.ToList<String>());
            foreach (Triplet<string, string, double> transitionProbability in TransitionProbabilities)
            {
                tm.setTransitionProbability(transitionProbability.First,transitionProbability.Second,transitionProbability.Third);
            }

            SensorModel sm = new SensorModel(States.ToList<String>(),Perceptions.ToList<String>());
            foreach (Triplet<string, string, double> sensingProbability in SensingProbabilities)
            {
                sm.setSensingProbability(sensingProbability.First, sensingProbability.Second, sensingProbability.Third);
            }
            HiddenMarkovModel result = new HiddenMarkovModel(prior, tm, sm);

            return result;
        }




    }
}