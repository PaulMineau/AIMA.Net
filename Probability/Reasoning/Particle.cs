namespace CosmicFlow.AIMA.Core.Probability.Reasoning
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class Particle
    {

        private String state;

        private double weight;

        public Particle(String state, double weight)
        {
            this.state = state;
            this.weight = weight;
        }

        public Particle(String state) : this(state, 0)
        {
            
        }

        public bool hasState(String aState)
        {
            return state.Equals(aState);
        }

        public String getState()
        {
            return state;
        }

        public double getWeight()
        {
            return weight;
        }

        public void setWeight(double particleWeight)
        {
            weight = particleWeight;
        }
    }
}