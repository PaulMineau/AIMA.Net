namespace CosmicFlow.AIMA.Core.Learning.Neural
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class PureLinearActivationFunction : ActivationFunction
    {

        public double activation(double parameter)
        {
            return parameter;
        }

        public double deriv(double parameter)
        {

            return 1;
        }
    }
}