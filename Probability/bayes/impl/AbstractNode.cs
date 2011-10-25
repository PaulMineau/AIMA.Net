using System;

namespace AIMA.Probability.Bayes.Impl
{

    /**
     * Abstract base implementation of the Node interface.
     * 
     * @author Ciaran O'Reilly
     * @author Ravi Mohan
     */

    public abstract class AbstractNode : Node
    {
        private RandomVariable variable = null;
        private Set<Node> parents = null;
        private Set<Node> children = null;

        public AbstractNode(RandomVariable var)
            : this(var, null)
        {

        }

        public AbstractNode(RandomVariable var, params Node[] parents)
        {
            if (null == var)
            {
                throw new ArgumentException(
                    "Random Variable for Node must be specified.");
            }
            this.variable = var;
            this.parents = new Set<Node>();
            if (null != parents)
            {
                foreach (Node p in parents)
                {
                    ((AbstractNode) p).addChild(this);
                    this.parents.add(p);
                }
            }

            this.children = new Set<Node>();
        }

        //
        // START-Node
        public RandomVariable getRandomVariable()
        {
            return variable;
        }

        public bool isRoot()
        {
            return 0 == getParents().Count;
        }

        public Set<Node> getParents()
        {
            return parents;
        }

        public Set<Node> getChildren()
        {
            return children;
        }

        public Set<Node> getMarkovBlanket()
        {
            Set<Node> mb = new Set<Node>();
            // Given its parents,
            mb.addAll(getParents());
            // children,
            mb.addAll(getChildren());
            // and children's parents
            foreach (Node cn in getChildren())
            {
                mb.addAll(cn.getParents());
            }

            return mb;
        }

        public abstract ConditionalProbabilityDistribution getCPD();

        // END-Node
        //


        public String toString()
        {
            return getRandomVariable().getName();
        }


        public bool equals(Object o)
        {
            if (null == o)
            {
                return false;
            }
            if (o == this)
            {
                return true;
            }

            if (o is Node)
            {
                Node n = (Node) o;

                return getRandomVariable().Equals(n.getRandomVariable());
            }

            return false;
        }

        public int hashCode()
        {
            return variable.GetHashCode();
        }

        //
        // PROTECTED METHODS
        //
        protected void addChild(Node childNode)
        {
            children = new LinkedHashSet<Node>(children);

            children.add(childNode);


        }
    }
}