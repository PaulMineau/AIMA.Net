namespace CosmicFlow.AIMA.Core.Probability
{

    using CosmicFlow.AIMA.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.Collections;
    /**
     * @author Ravi Mohan
     * 
     */
    public class BayesNet
    {
        private List<BayesNetNode> roots = new List<BayesNetNode>();

        private List<BayesNetNode> variableNodes;

        public BayesNet(BayesNetNode root)
        {
            roots.Add(root);
        }

        public BayesNet(BayesNetNode root1, BayesNetNode root2) : this(root1)
        {
            roots.Add(root2);
        }

        public BayesNet(BayesNetNode root1, BayesNetNode root2, BayesNetNode root3) : this(root1, root2)
        {
            
            roots.Add(root3);
        }

        public BayesNet(List<BayesNetNode> rootNodes)
        {
            roots = rootNodes;
        }

        public List<String> getVariables()
        {
            variableNodes = getVariableNodes();
            List<String> variables = new List<String>();
            foreach (BayesNetNode variableNode in variableNodes)
            {
                variables.Add(variableNode.getVariable());
            }
            return variables;
        }

        public double probabilityOf(String Y, bool value,
                Dictionary<String, bool> evidence)
        {
            BayesNetNode y = getNodeOf(Y);
            if (y == null)
            {
                throw new ApplicationException("Unable to find a node with variable "
                        + Y);
            }
            else
            {
                List<BayesNetNode> parentNodes = y.getParents();
                if (parentNodes.Count == 0)
                {// root nodes
                    Dictionary<String, bool> YTable = new Dictionary<String, bool>();
                    YTable.Add(Y, value);

                    double prob = y.probabilityOf(YTable);
                    return prob;

                }
                else
                {// non rootnodes
                    Dictionary<String, bool> parentValues = new Dictionary<String, bool>();
                    foreach (BayesNetNode parent in parentNodes)
                    {
                        parentValues.Add(parent.getVariable(), evidence[parent
                                .getVariable()]);
                    }
                    double prob = y.probabilityOf(parentValues);
                    if (value.Equals(true))
                    {
                        return prob;
                    }
                    else
                    {
                        return (1.0 - prob);
                    }

                }

            }
        }

        public Dictionary<String, bool> getPriorSample(Randomizer r)
        {
            Dictionary<String, bool> h = new Dictionary<String, bool>();
            List<BayesNetNode> variableNodes = getVariableNodes();
            foreach (BayesNetNode node in variableNodes)
            {
                h.Add(node.getVariable(), node.isTrueFor(r.nextDouble(), h));
            }
            return h;
        }

        public Dictionary<String, bool> getPriorSample()
        {
            return getPriorSample(new JavaRandomizer());
        }

        public double[] rejectionSample(String X, Dictionary<String,bool> evidence,
                int numberOfSamples, Randomizer r)
        {
            double[] retval = new double[2];
            for (int i = 0; i < numberOfSamples; i++)
            {
                Dictionary<String, bool> sample = getPriorSample(r);
                if (consistent(sample, evidence))
                {
                    bool queryValue = sample[X];
                    if (queryValue)
                    {
                        retval[0] += 1;
                    }
                    else
                    {
                        retval[1] += 1;
                    }
                }
            }
            return Util.normalize(retval);
        }

        public double[] likelihoodWeighting(String X,
                Dictionary<String, bool> evidence, int numberOfSamples,
                Randomizer r)
        {
            double[] retval = new double[2];
            for (int i = 0; i < numberOfSamples; i++)
            {
                Dictionary<String, bool> x = new Dictionary<String, bool>();
                double w = 1.0;
                List<BayesNetNode> variableNodes = getVariableNodes();
                foreach (BayesNetNode node in variableNodes)
                {
                    if ( evidence.ContainsKey(node.getVariable()))
                    {
                        w *= node.probabilityOf(x);
                        x.Add(node.getVariable(), evidence[node.getVariable()]);
                    }
                    else
                    {
                        x
                                .Add(node.getVariable(), node.isTrueFor(r
                                        .nextDouble(), x));
                    }
                }
                bool queryValue = x[X];
                if (queryValue)
                {
                    retval[0] += w;
                }
                else
                {
                    retval[1] += w;
                }

            }
            return Util.normalize(retval);
        }

        public double[] mcmcAsk(String X, Dictionary<String, bool> evidence,
                int numberOfVariables, Randomizer r)
        {
            double[] retval = new double[2];
            List<String> _nonEvidenceVariables = nonEvidenceVariables(evidence, X);
            Dictionary<String, bool> evt = createRandomEvent(
                    _nonEvidenceVariables, evidence, r);
            for (int j = 0; j < numberOfVariables; j++)
            {
                foreach(string variable in _nonEvidenceVariables)
                {
                    BayesNetNode node = getNodeOf(variable);
                    List<BayesNetNode> blanket = markovBlanket(node);
                    Dictionary<string,bool> mb = createMBValues(blanket, evt);
                    // event.put(node.getVariable(), node.isTrueFor(
                    // r.getProbability(), mb));
                    evt[node.getVariable()] = truthValue(rejectionSample(node
                            .getVariable(), mb, 100, r), r);
                    bool queryValue = evt[X];
                    if (queryValue)
                    {
                        retval[0] += 1;
                    }
                    else
                    {
                        retval[1] += 1;
                    }
                }
            }
            return Util.normalize(retval);
        }

        public double[] mcmcAsk(String X, Dictionary<String, bool> evidence,
                int numberOfVariables)
        {
            return mcmcAsk(X, evidence, numberOfVariables, new JavaRandomizer());
        }

        public double[] likelihoodWeighting(String X,
                Dictionary<String, bool> evidence, int numberOfSamples)
        {
            return likelihoodWeighting(X, evidence, numberOfSamples,
                    new JavaRandomizer());
        }

        public double[] rejectionSample(String X,
                Dictionary<String, bool> evidence, int numberOfSamples)
        {
            return rejectionSample(X, evidence, numberOfSamples,
                    new JavaRandomizer());
        }

        //
        // PRIVATE METHODS
        //

        private List<BayesNetNode> getVariableNodes()
        {
            // TODO dicey initalisation works fine but unclear . clarify
            if (variableNodes == null)
            {
                List<BayesNetNode> newVariableNodes = new List<BayesNetNode>();
                List<BayesNetNode> parents = roots;
                List<BayesNetNode> traversedParents = new List<BayesNetNode>();

                while (parents.Count != 0)
                {
                    List<BayesNetNode> newParents = new List<BayesNetNode>();
                    foreach (BayesNetNode parent in parents)
                    {
                        // if parent unseen till now
                        if (!(traversedParents.Contains(parent)))
                        {
                            newVariableNodes.Add(parent);
                            // add any unseen children to next generation of parents
                            List<BayesNetNode> children = parent.getChildren();
                            foreach (BayesNetNode child in children)
                            {
                                if (!newParents.Contains(child))
                                {
                                    newParents.Add(child);
                                }
                            }
                            traversedParents.Add(parent);
                        }
                    }

                    parents = newParents;
                }
                variableNodes = newVariableNodes;
            }

            return variableNodes;
        }

        private BayesNetNode getNodeOf(String y)
        {
            List<BayesNetNode> variableNodes = getVariableNodes();
            foreach (BayesNetNode node in variableNodes)
            {
                if (node.getVariable().Equals(y))
                {
                    return node;
                }
            }
            return null;
        }

        private bool consistent(Dictionary<String, bool> sample, Dictionary<String, bool> evidence)
        {
            foreach (string key in evidence.Keys)
            {
                bool value = (bool)evidence[key];
                if (!(value.Equals(sample[key])))
                {
                    return false;
                }
            }
            return true;
        }

        private bool truthValue(double[] ds, Randomizer r)
        {
            double value = r.nextDouble();
            if (value < ds[0])
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private Dictionary<String, bool> createRandomEvent(
                List<string> nonEvidenceVariables, Dictionary<String, bool> evidence,
                Randomizer r)
        {
            Dictionary<String, bool> table = new Dictionary<String, bool>();
            List<String> variables = getVariables();
            foreach (String variable in variables)
            {

                if (nonEvidenceVariables.Contains(variable))
                {
                    bool value = r.nextDouble() <= 0.5 ? true
                            : false;
                    table.Add(variable, value);
                }
                else
                {
                    table.Add(variable, evidence[variable]);
                }
            }
            return table;
        }

        private List<String> nonEvidenceVariables(Dictionary<String, bool> evidence,
                String query)
        {
            List<String> nonEvidenceVariables = new List<String>();
            List<String> variables = getVariables();
            foreach (String variable in variables)
            {

                if (!(evidence.ContainsKey(variable)))
                {
                    nonEvidenceVariables.Add(variable);
                }
            }
            return nonEvidenceVariables;
        }

        private List<BayesNetNode> markovBlanket(BayesNetNode node)
        {
            return markovBlanket(node, new List<BayesNetNode>());
        }

        private List<BayesNetNode> markovBlanket(BayesNetNode node,
                List<BayesNetNode> soFar)
        {
            // parents
            List<BayesNetNode> parents = node.getParents();
            foreach (BayesNetNode parent in parents)
            {
                if (!soFar.Contains(parent))
                {
                    soFar.Add(parent);
                }
            }
            // children
            List<BayesNetNode> children = node.getChildren();
            foreach (BayesNetNode child in children)
            {
                if (!soFar.Contains(child))
                {
                    soFar.Add(child);
                    List<BayesNetNode> childsParents = child.getParents();
                    foreach (BayesNetNode childsParent in childsParents)
                    {
                        ;
                        if ((!soFar.Contains(childsParent))
                                && (!(childsParent.Equals(node))))
                        {
                            soFar.Add(childsParent);
                        }
                    }// childsParents
                }// end contains child

            }// end child

            return soFar;
        }

        private Dictionary<String, bool> createMBValues(List<BayesNetNode> markovBlanket,
                Dictionary<String, bool> evt)
        {
            Dictionary<String, bool> table = new Dictionary<String, bool>();
            foreach (BayesNetNode node in markovBlanket)
            {
                table.Add(node.getVariable(), evt[node.getVariable()]);
            }
            return table;
        }
    }
}