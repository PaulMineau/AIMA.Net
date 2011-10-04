namespace CosmicFlow.AIMA.Core.Probability.Reasoning
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Probability;
    using CosmicFlow.AIMA.Core.Util.Math;

    /**
     * @author Ravi Mohan
     * 
     */
    public class HiddenMarkovModel
    {

        SensorModel _sensorModel;

        TransitionModel _transitionModel;

        private RandomVariable priorDistribution;

        public HiddenMarkovModel(RandomVariable priorDistribution,
                TransitionModel tm, SensorModel sm)
        {
            this.priorDistribution = priorDistribution;
            this._transitionModel = tm;
            this._sensorModel = sm;
        }

        public RandomVariable prior()
        {
            return priorDistribution;
        }

        public RandomVariable predict(RandomVariable aBelief, String action)
        {
            RandomVariable newBelief = aBelief.duplicate();

            Matrix beliefMatrix = aBelief.asMatrix();
            Matrix transitionMatrix = _transitionModel.asMatrix(action);
            Matrix predicted = transitionMatrix.transpose().times(beliefMatrix);
            newBelief.updateFrom(predicted);
            return newBelief;
        }

        public RandomVariable perceptionUpdate(RandomVariable aBelief,
                String perception)
        {
            RandomVariable newBelief = aBelief.duplicate();

            // one way - use matrices
            Matrix beliefMatrix = aBelief.asMatrix();
            Matrix o_matrix = _sensorModel.asMatrix(perception);
            Matrix updated = o_matrix.times(beliefMatrix);
            newBelief.updateFrom(updated);
            newBelief.normalize();
            return newBelief;

            // alternate way of doing this. clearer in intent.
            // for (String state : aBelief.states()){
            // double probabilityOfPerception= sensorModel.get(state,perception);
            // newBelief.setProbabilityOf(state,probabilityOfPerception *
            // aBelief.getProbabilityOf(state));
            // }
        }

        public RandomVariable forward(RandomVariable aBelief, String action,
                String perception)
        {
            return perceptionUpdate(predict(aBelief, action), perception);
        }

        public RandomVariable forward(RandomVariable aBelief, String perception)
        {
            return forward(aBelief, HmmConstants.DO_NOTHING, perception);
        }

        public RandomVariable calculate_next_backward_message(
                RandomVariable forwardBelief,
                RandomVariable present_backward_message, String perception)
        {
            RandomVariable result = present_backward_message.duplicate();
            // System.Console.WriteLine("fb :-calculating new backward message");
            // System.Console.WriteLine("fb :-diagonal matrix from sens model = ");
            Matrix oMatrix = _sensorModel.asMatrix(perception);
            // System.Console.WriteLine(oMatrix);
            Matrix transitionMatrix = _transitionModel.asMatrix();// action
            // should
            // be
            // passed
            // in
            // here?
            // System.Console.WriteLine("fb :-present backward message = "
            // +present_backward_message);
            Matrix backwardMatrix = transitionMatrix.times(oMatrix
                    .times(present_backward_message.asMatrix()));
            Matrix resultMatrix = backwardMatrix.arrayTimes(forwardBelief
                    .asMatrix());
            result.updateFrom(resultMatrix);
            result.normalize();
            // System.Console.WriteLine("fb :-normalized new backward message = "
            // +result);
            return result;
        }

        public List<RandomVariable> forward_backward(List<String> perceptions)
        {
            RandomVariable[] forwardMessages = new RandomVariable[perceptions
                    .Count + 1];
            RandomVariable backwardMessage = priorDistribution.createUnitBelief();
            RandomVariable[] smoothedBeliefs = new RandomVariable[perceptions
                    .Count + 1];

            forwardMessages[0] = priorDistribution;
            smoothedBeliefs[0] = null;

            // populate forward messages
            for (int i = 0; i < perceptions.Count; i++)
            { // N.B i starts at 1,
                // not zero
                forwardMessages[i + 1] = forward(forwardMessages[i], perceptions[i]);
            }
            for (int i = perceptions.Count; i > 0; i--)
            {
                RandomVariable smoothed = priorDistribution.duplicate();
                smoothed.updateFrom(forwardMessages[i].asMatrix().arrayTimes(
                        backwardMessage.asMatrix()));
                smoothed.normalize();
                smoothedBeliefs[i] = smoothed;
                backwardMessage = calculate_next_backward_message(
                        forwardMessages[i], backwardMessage, perceptions[i - 1]);
            }

            return new List<RandomVariable>(smoothedBeliefs);
        }

        public SensorModel sensorModel()
        {
            return _sensorModel;
        }

        public TransitionModel transitionModel()
        {
            return _transitionModel;
        }

    }
}