namespace CosmicFlow.AIMA.Test.Core.Unit.Probability
{

    using CosmicFlow.AIMA.Core.Probability;

    /**
     * @author Ravi Mohan
     * 
     */
    public class MockRandomizer : Randomizer
    {

        private double[] values;

        private int index;

        public MockRandomizer(double[] values)
        {
            this.values = values;
            this.index = 0;
        }

        public double nextDouble()
        {
            if (index == values.Length)
            {
                index = 0;
            }

            double value = values[index];
            index++;
            return value;
        }
    }
}