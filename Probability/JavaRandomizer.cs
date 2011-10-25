namespace AIMA.Probability
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class JavaRandomizer : Randomizer
    {
        static Random r = new Random();

        public double nextDouble()
        {
            return r.NextDouble();
        }
    }
}