using System;
using System.Collections.Generic;

namespace AIMA.Probability.Bayes.Impl
{

    /**
     * Default implementation of the BayesianNetwork interface.
     * 
     * @author Ciaran O'Reilly
     * @author Ravi Mohan
     */

    public class BayesNet : BayesianNetwork
    {
        protected Set<Node> rootNodes = new LinkedHashSet<Node>();
        protected List<RandomVariable> variables = new List<RandomVariable>();
        protected Map<RandomVariable, Node> varToNodeMap = new Map<RandomVariable, Node>();

        public BayesNet(params Node[] rootNodes)
        {
            if (null == rootNodes)
            {
                throw new IllegalArgumentException(
                    "Root Nodes need to be specified.");
            }
            foreach (Node n in rootNodes)
            {
                this.rootNodes.add(n);
            }
            if (this.rootNodes.size() != rootNodes.Length)
            {
                throw new IllegalArgumentException(
                    "Duplicate Root Nodes Passed in.");
            }
            // Ensure is a DAG
            checkIsDAGAndCollectVariablesInTopologicalOrder();
            //variables = Collections.unmodifiableList(variables);
        }

        //
        // START-BayesianNetwork

        public List<RandomVariable> getVariablesInTopologicalOrder()
        {
            return variables;
        }


        public Node getNode(RandomVariable rv)
        {
            return varToNodeMap.get(rv);
        }

        // END-BayesianNetwork
        //

        //
        // PRIVATE METHODS
        //
        private void checkIsDAGAndCollectVariablesInTopologicalOrder()
        {

            // Topological sort based on logic described at:
            // http://en.wikipedia.org/wiki/Topoligical_sorting
            Set<Node> seenAlready = new Set<Node>();
            Map<Node, List<Node>> incomingEdges = new Map<Node, List<Node>>();
            Set<Node> s = new Set<Node>();
            foreach (Node n in this.rootNodes)
            {
                walkNode(n, seenAlready, incomingEdges, s);
            }
            while (!(s.Count == 0))
            {
                HashSet<Node>.Enumerator enumerator = s.GetEnumerator();
                enumerator.MoveNext();
                Node n = enumerator.Current;
                s.remove(n);
                variables.Add(n.getRandomVariable());
                varToNodeMap.put(n.getRandomVariable(), n);
                foreach (Node m in n.getChildren())
                {
                    List<Node> edges = incomingEdges.get(m);
                    edges.Remove(n);
                    if (edges.Count == 0)
                    {
                        s.add(m);
                    }
                }
            }

            foreach (List<Node> edges in incomingEdges.values())
            {
                if (!(edges.Count == 0))
                {
                    throw new IllegalArgumentException(
                        "Network contains at least one cycle in it, must be a DAG.");
                }
            }
        }

        private void walkNode(Node n, Set<Node> seenAlready,
                              Map<Node, List<Node>> incomingEdges, Set<Node> rootNodes)
        {
            if (!seenAlready.Contains(n))
            {
                seenAlready.add(n);
                // Check if has no incoming edges
                if (n.isRoot())
                {
                    rootNodes.add(n);
                }
                incomingEdges.put(n, new List<Node>(n.getParents()));
                foreach (Node c in n.getChildren())
                {
                    walkNode(c, seenAlready, incomingEdges, rootNodes);
                }
            }
        }
    }
}