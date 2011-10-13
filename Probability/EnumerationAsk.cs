namespace AIMA.Core.Probability
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public class EnumerationAsk
    {

        public static double[] ask(Query q, BayesNet net)
        {
            Dictionary<String, bool> evidenceVariables = q.getEvidenceVariables();

            double[] probDist = new double[2];
            // true probability
            evidenceVariables[q.getQueryVariable()] = true;
            probDist[0] = enumerateAll(net, net.getVariables(), evidenceVariables);
            // false probability
            evidenceVariables[q.getQueryVariable()] = false;
            probDist[1] = enumerateAll(net, net.getVariables(), evidenceVariables);
            // System.Console.WriteLine( probDist[0] + " " + probDist[1]);
            // return probDist;
            double[] normalized = Util.normalize(probDist);
            // System.Console.WriteLine( normalized[0] + " " + normalized[1]);
            return normalized;
        }

        private static double enumerateAll(BayesNet net, List<string> unprocessedVariables,
                Dictionary<String, bool> evidenceVariables)
        {
            if (unprocessedVariables.Count == 0)
            {

                return 1.0;
            }
            else
            {
                String Y = (String)unprocessedVariables[0];

                if (evidenceVariables.ContainsKey(Y))
                {

                    double probYGivenParents = net.probabilityOf(Y,
                            evidenceVariables[Y], evidenceVariables);

                    double secondTerm = enumerateAll(net, Util
                            .rest(unprocessedVariables), evidenceVariables);

                    return probYGivenParents * secondTerm;
                }
                else
                {
                    double sigma = 0.0;
                    Dictionary<String, bool> clone1 = cloneEvidenceVariables(evidenceVariables);
                    clone1.Add(Y, true);
                    double probYTrueGivenParents = net.probabilityOf(Y,
                            true, clone1);

                    double secondTerm = enumerateAll(net, Util
                            .rest(unprocessedVariables), clone1);

                    double trueProbabilityY = probYTrueGivenParents * secondTerm;

                    Dictionary<String, bool> clone2 = cloneEvidenceVariables(evidenceVariables);
                    clone2.Add(Y, false);
                    double probYFalseGivenParents = net.probabilityOf(Y,
                            false, clone2);

                    secondTerm = enumerateAll(net, Util.rest(unprocessedVariables),
                            clone2);
                    double falseProbabilityY = probYFalseGivenParents * secondTerm;
                    // System.Console.Write(secondTerm + " ) )");
                    sigma = trueProbabilityY + falseProbabilityY;
                    return sigma;

                }
            }
        }

        private static Dictionary<String, bool> cloneEvidenceVariables(
                Dictionary<String, bool> evidence)
        {
            Dictionary<String, bool> cloned = new Dictionary<String, bool>();
            foreach(string key in evidence.Keys)
            {
                bool b = evidence[key];
                if (b == true)
                {
                    cloned.Add(key, true);
                }
                else if ((evidence[key]) == false)
                {
                    cloned.Add(key, false);
                }
            }
            return cloned;
        }
    }
}