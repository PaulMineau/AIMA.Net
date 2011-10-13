namespace AIMA.Core.Probability.Reasoning
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Util.DataStructure;
    using AIMA.Core.Util.Math;

    /**
     * @author Ravi Mohan
     * 
     */
    public class SensorModel
    {
        private Table<String, String, Double> table;

        private List<String> states;

        public SensorModel(List<String> states, List<String> perceptions)
        {
            this.states = states;
            table = new Table<String, String, Double>(states, perceptions);
        }

        public void setSensingProbability(String state, String perception,
                double probability)
        {
            table.set(state, perception, probability);
        }

        public Double get(String state, String perception)
        {
            return table.get(state, perception).Value;
        }

        public Matrix asMatrix(String perception) {
		List<Double> values = new List<Double>();
		// for (String state : aBelief.states()) {
		foreach (String state in states) {
			values.Add(get(state, perception));
		}
		Matrix OMatrix = Matrix.createDiagonalMatrix(values);
		return OMatrix;
	}
    }
}
