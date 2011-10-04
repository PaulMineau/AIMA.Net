namespace CosmicFlow.AIMA.Core.Probability
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Probability.Reasoning;
    using CosmicFlow.AIMA.Core.Util;
    using CosmicFlow.AIMA.Core.Util.Math;

    /**
     * @author Ravi Mohan
     * 
     */
    public class RandomVariable
    {
        public string Name { get { return name; }  set { name = value;} }

        private String name;

        private Dictionary<String, Double> distribution;

        private List<String> _states;

        public RandomVariable(List<String> states) : this("HiddenState", states)
        {
        }

        public RandomVariable(String name, List<String> states)
        {
            this.name = name;
            this._states = states;
            this.distribution = new Dictionary<String, Double>();
            int numberOfStates = states.Count;
            double initialProbability = 1.0 / numberOfStates;
            foreach (String s in states)
            {
                distribution.Add(s, initialProbability);
            }
        }

        private RandomVariable(String name, List<String> states,
                Dictionary<String, Double> distribution)
        {
            this.name = name;
            this._states = states;
            this.distribution = distribution;
        }

        public void setProbabilityOf(String state, Double probability)
        {
            if (_states.Contains(state))
            {
                if (distribution.ContainsKey(state))
                {
                    distribution[state] = probability;
                }
                else
                {
                    distribution.Add(state, probability);
                }
            }
            else
            {
                throw new ApplicationException(state + "  is an invalid state");
            }
        }

        public double getProbabilityOf(String state)
        {
            if (_states.Contains(state))
            {
                return distribution[state];
            }
            else
            {
                throw new ApplicationException(state + "  is an invalid state");
            }
        }

        public List<String> states()
        {
            return _states;
        }

        public RandomVariable duplicate()
        {
            Dictionary<String, Double> probs = new Dictionary<String, Double>();
            foreach (String key in distribution.Keys)
            {
                probs.Add(key, distribution[key]);
            }
            return new RandomVariable(name, _states, probs);

        }

        public void normalize()
        {
            List<Double> probs = new List<Double>();
            foreach (String s in _states)
            {
                probs.Add(distribution[s]);
            }
            List<Double> newProbs = Util.normalize(probs);
            for (int i = 0; i < _states.Count; i++)
            {
                distribution[_states[i]] = newProbs[i];
            }
        }

        public Matrix asMatrix()
        {
            Matrix m = new Matrix(_states.Count, 1);
            for (int i = 0; i < _states.Count; i++)
            {
                m.set(i, 0, distribution[_states[i]]);
            }
            return m;

        }

        public void updateFrom(Matrix aMatrix)
        {
            for (int i = 0; i < _states.Count; i++)
            {
                distribution[_states[i]] = aMatrix.get(i, 0);
            }

        }

        public RandomVariable createUnitBelief()
        {
            RandomVariable result = duplicate();
            foreach (String s in states())
            {
                result.setProbabilityOf(s, 1.0);
            }
            return result;
        }

        public override String ToString()
        {
            return asMatrix().ToString();
        }

        public ParticleSet toParticleSet(HiddenMarkovModel hmm,
                Randomizer randomizer, int numberOfParticles)
        {
            ParticleSet result = new ParticleSet(hmm);
            for (int i = 0; i < numberOfParticles; i++)
            {
                double rvalue = randomizer.nextDouble();
                String state = getStateForRandomNumber(rvalue);
                result.add(new Particle(state, 0));
            }
            return result;
        }

        private String getStateForRandomNumber(double rvalue)
        {
            double total = 0.0;
            foreach (String s in _states)
            {
                total = total + distribution[s];
                if (total >= rvalue)
                {
                    return s;
                }
            }
            throw new ApplicationException("cannot handle " + rvalue);
        }
    }
}