namespace CosmicFlow.AIMA.Core.Learning.Neural
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Util.Math;

    /**
     * @author Ravi Mohan
     * 
     */
    public class NNExample
    {
        private List<Double> normalizedInput, normalizedTarget;

        public NNExample(List<Double> normalizedInput, List<Double> normalizedTarget)
        {
            this.normalizedInput = normalizedInput;
            this.normalizedTarget = normalizedTarget;
        }

        public NNExample copyExample() {
		List<Double> newInput = new List<Double>();
		List<Double> newTarget = new List<Double>();
		foreach (Double d in normalizedInput) {
			newInput.Add(d);
		}
		foreach (Double d in normalizedTarget) {
			newTarget.Add(d);
		}
		return new NNExample(newInput, newTarget);
	}

        public Vector getInput()
        {
            Vector v = new Vector(normalizedInput);
            return v;

        }

        public Vector getTarget()
        {
            Vector v = new Vector(normalizedTarget);
            return v;

        }

        public bool isCorrect(Vector prediction)
        {
            /*
             * compares the index having greatest value in target to indec having
             * greatest value in prediction. Ifidentical, correct
             */
            return getTarget().indexHavingMaxValue() == prediction
                    .indexHavingMaxValue();
        }
    }
}
