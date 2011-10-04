namespace CosmicFlow.AIMA.Core.Learning.Neural
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public interface ActivationFunction
    {
        double activation(double parameter);

        double deriv(double parameter);
    }
}
