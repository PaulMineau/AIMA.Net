namespace CosmicFlow.AIMA.Core.Learning.Neural
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class LogSigActivationFunction : ActivationFunction
    {

        public double activation(double parameter)
        {

            return 1.0 / (1.0 + Math.Pow(Math.E, (-1.0 * parameter)));
        }

        public double deriv(double parameter)
        {
            // parameter = induced field
            // e == activation
            double e = 1.0 / (1.0 + Math.Pow(Math.E, (-1.0 * parameter)));
            return e * (1.0 - e);
        }
    }
}
