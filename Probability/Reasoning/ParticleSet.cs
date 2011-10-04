namespace CosmicFlow.AIMA.Core.Probability.Reasoning
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Probability;

    /**
     * @author Ravi Mohan
     * 
     */
    public class ParticleSet
    {

        private List<Particle> particles;

        private HiddenMarkovModel hmm;

        public ParticleSet(HiddenMarkovModel hmm)
        {
            particles = new List<Particle>();
            this.hmm = hmm;
        }

        // Use these two to get the filtered set directly. This is the only method a
        // user class needs to call.
        // The other methods are public only to be accessible to tests.

        public ParticleSet filter(String perception, Randomizer r)
        {
            return generateParticleSetForPredictedState(r).perceptionUpdate(
                    perception, r);
        }

        public ParticleSet filter(String action, String perception, Randomizer r)
        {
            return generateParticleSetForPredictedState(action, r)
                    .perceptionUpdate(perception, r);
        }

        // these are internal methods. public only to facilitate testing
        public int numberOfParticlesWithState(String state) {
		int total = 0;
		foreach (Particle p in particles) {
			if (p.hasState(state)) {
				total += 1;
			}
		}
		return total;
	}

        public void add(Particle particle)
        {
            particles.Add(particle);

        }

        public int size()
        {
            return particles.Count;
        }

        public RandomVariable toRandomVariable() {
		List<String> states = new List<String>();
		Dictionary<String, int> stateCount = new Dictionary<String, int>();
		foreach (Particle p in particles) {
			String state = p.getState();
			if (!(states.Contains(state))) {
				states.Add(state);
				stateCount.Add(state, 0);
			}

			stateCount[state]++;

		}

		RandomVariable result = new RandomVariable(states);
		foreach (String state in stateCount.Keys) {
			result.setProbabilityOf(state,
					((double) stateCount[state] / particles.Count));
		}
		return result;
	}

        public ParticleSet generateParticleSetForPredictedState(
                Randomizer randomizer)
        {
            return generateParticleSetForPredictedState(HmmConstants.DO_NOTHING,
                    randomizer);
        }

        public ParticleSet generateParticleSetForPredictedState(String action,
                Randomizer randomizer)
        {
            ParticleSet predictedParticleSet = new ParticleSet(this.hmm);
            foreach (Particle p in particles)
            {
                String newState = hmm.transitionModel().getStateForProbability(
                        p.getState(), action, randomizer.nextDouble());

                Particle generatedParticle = new Particle(newState);
                predictedParticleSet.add(generatedParticle);
            }
            return predictedParticleSet;
        }

        public ParticleSet perceptionUpdate(String perception, Randomizer r)
        {
            // compute Particle Weight
            foreach (Particle p in particles)
            {
                double particleWeight = hmm.sensorModel().get(p.getState(),
                        perception);
                p.setWeight(particleWeight);
            }

            // weighted sample to create new ParticleSet
            ParticleSet result = new ParticleSet(hmm);
            while (result.size() != size())
            {
                foreach (Particle p in particles)
                {
                    double probability = r.nextDouble();
                    if (probability <= p.getWeight())
                    {
                        if (result.size() < size())
                        {
                            result.add(new Particle(p.getState(), p.getWeight()));
                        }
                    }
                }

            }
            return result;
        }

        public Particle getParticle(int i)
        {
            return particles[i];
        }
    }
}