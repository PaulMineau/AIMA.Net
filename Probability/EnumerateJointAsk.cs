namespace CosmicFlow.AIMA.Core.Probability
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public class EnumerateJointAsk
    {

        public static double[] ask(Query q, ProbabilityDistribution pd)
        {
            double[] probDist = new double[2];
            Dictionary<String, bool> h = q.getEvidenceVariables();

            // true probability
            h[q.getQueryVariable()] = true;
            probDist[0] = pd.probabilityOf(h);
            // false probability
            h[q.getQueryVariable()] = false;
            probDist[1] = pd.probabilityOf(h);
            return Util.normalize(probDist);
        }
    }
}