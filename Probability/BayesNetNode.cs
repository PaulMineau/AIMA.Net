namespace AIMA.Core.Probability
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class BayesNetNode
    {
        private String variable;

        List<BayesNetNode> parents, children;

        ProbabilityDistribution distribution;

        public BayesNetNode(String variable)
        {
            this.variable = variable;
            parents = new List<BayesNetNode>();
            children = new List<BayesNetNode>();
            distribution = new ProbabilityDistribution(variable);
        }

        public void influencedBy(BayesNetNode parent1)
        {
            addParent(parent1);
            parent1.addChild(this);
            distribution = new ProbabilityDistribution(parent1.getVariable());
        }

        public void influencedBy(BayesNetNode parent1, BayesNetNode parent2)
        {
            influencedBy(parent1);
            influencedBy(parent2);
            distribution = new ProbabilityDistribution(parent1.getVariable(),
                    parent2.getVariable());
        }

        public void setProbability(bool b, double d)
        {
            distribution.set(d, b);
            if (isRoot())
            {
                distribution.set(1.0 - d, !b);
            }
        }

        public void setProbability(bool b, bool c, double d)
        {
            distribution.set(d, b, c);

        }

        public String getVariable()
        {
            return variable;
        }

        public List<BayesNetNode> getChildren()
        {
            return children;
        }

        public List<BayesNetNode> getParents()
        {
            return parents;
        }

        public override String ToString()
        {
            return variable;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public double probabilityOf(Dictionary<String, bool> conditions)
        {
            return distribution.probabilityOf(conditions);
        }

        public bool isTrueFor(double probability,
                Dictionary<String, bool> modelBuiltUpSoFar)
        {
            Dictionary<String, bool> conditions = new Dictionary<String, bool>();
            if (isRoot())
            {
                conditions.Add(getVariable(), true);
            }
            else
            {
                for (int i = 0; i < parents.Count; i++)
                {
                    BayesNetNode parent = parents[i];
                    conditions.Add(parent.getVariable(), modelBuiltUpSoFar[parent.getVariable()]);
                }
            }
            double trueProbability = probabilityOf(conditions);
            if (probability <= trueProbability)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(Object o)
        {

            if (this == o)
            {
                return true;
            }
            if ((o == null) || (this.GetType() != o.GetType()))
            {
                return false;
            }
            BayesNetNode another = (BayesNetNode)o;
            return variable.Equals(another.variable);
        }

        //
        // PRIVATE METHODS
        //
        private void addParent(BayesNetNode node)
        {
            if (!(parents.Contains(node)))
            {
                parents.Add(node);
            }
        }

        private void addChild(BayesNetNode node)
        {
            if (!(children.Contains(node)))
            {
                children.Add(node);
            }
        }

        private bool isRoot()
        {
            return (parents.Count == 0);
        }
    }
}