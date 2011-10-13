namespace AIMA.Core.Learning.Neural
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Util.Math;

    /**
     * @author Ravi Mohan
     * 
     */
    public interface NNTrainingScheme
    {
        Vector processInput(FeedForwardNeuralNetwork network, Vector input);

        void processError(FeedForwardNeuralNetwork network, Vector error);

        void setNeuralNetwork(FunctionApproximator ffnn);
    }
}