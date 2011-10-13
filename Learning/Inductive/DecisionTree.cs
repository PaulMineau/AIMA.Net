namespace AIMA.Core.Learning.Inductive
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Learning.Framework;
    using AIMA.Core.Util;
    using System.Text;

    /**
     * @author Ravi Mohan
     * 
     */
    public class DecisionTree
    {
        private String attributeName;

        // each node modelled as a hash of attribute_value/decisiontree
        private Dictionary<String, DecisionTree> nodes;

        protected DecisionTree()
        {

        }

        public DecisionTree(String attributeName)
        {
            this.attributeName = attributeName;
            nodes = new Dictionary<String, DecisionTree>();

        }

        public virtual void addLeaf(String attributeValue, String decision)
        {
            if (nodes.ContainsKey(attributeValue))
            {
                nodes[attributeValue] = new ConstantDecisonTree(decision);
            }
            else
            {
                nodes.Add(attributeValue, new ConstantDecisonTree(decision));
            }
        }

        public virtual void addNode(String attributeValue, DecisionTree tree)
        {
            if (nodes.ContainsKey(attributeValue))
            {
                nodes[attributeValue] = tree;
            }
            else
            {
                nodes.Add(attributeValue, tree);
            }
        }

        public virtual Object predict(Example e)
        {
            String attrValue = e.getAttributeValueAsString(attributeName);
            if (nodes.ContainsKey(attrValue))
            {
                return nodes[attrValue].predict(e);
            }
            else
            {
                throw new ApplicationException("no node exists for attribute value "
                        + attrValue);
            }
        }

        public static DecisionTree getStumpFor(DataSet ds, String attributeName,
                String attributeValue, String returnValueIfMatched,
                List<String> unmatchedValues, String returnValueIfUnmatched)
        {
            DecisionTree dt = new DecisionTree(attributeName);
            dt.addLeaf(attributeValue, returnValueIfMatched);
            foreach (String unmatchedValue in unmatchedValues)
            {
                dt.addLeaf(unmatchedValue, returnValueIfUnmatched);
            }
            return dt;
        }

        public static List<DecisionTree> getStumpsFor(DataSet ds,
                String returnValueIfMatched, String returnValueIfUnmatched)
        {
            List<String> attributes = ds.getNonTargetAttributes();
            List<DecisionTree> trees = new List<DecisionTree>();
            foreach (String attribute in attributes)
            {
                List<String> values = ds.getPossibleAttributeValues(attribute);
                foreach (String value in values)
                {
                    List<String> unmatchedValues = Util.removeFrom(ds
                            .getPossibleAttributeValues(attribute), value);

                    DecisionTree tree = getStumpFor(ds, attribute, value,
                            returnValueIfMatched, unmatchedValues,
                            returnValueIfUnmatched);
                    trees.Add(tree);

                }
            }
            return trees;
        }

        /**
         * @return Returns the attributeName.
         */
        public String getAttributeName()
        {
            return attributeName;
        }

        public override String ToString()
        {
            return ToString(1, new StringBuilder());
        }

        public virtual String ToString(int depth, StringBuilder buf)
        {

            if (attributeName != null)
            {
                buf.Append(Util.ntimes("\t", depth));
                buf.Append(Util.ntimes("***", 1));
                buf.Append(attributeName + " \n");
                foreach (String attributeValue in nodes.Keys)
                {
                    buf.Append(Util.ntimes("\t", depth + 1));
                    buf.Append("+" + attributeValue);
                    buf.Append("\n");
                    DecisionTree child = nodes[attributeValue];
                    buf.Append(child.ToString(depth + 1, new StringBuilder()));
                }
            }

            return buf.ToString();
        }
    }
}