namespace CosmicFlow.AIMA.Core.Learning.Inductive
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Learning.Framework;
    using CosmicFlow.AIMA.Core.Util;
    using System.Text;

    /**
     * @author Ravi Mohan
     * 
     */
    public class ConstantDecisonTree : DecisionTree
    {
        // represents leaf nodes like "Yes" or "No"
        private String value;

        public ConstantDecisonTree(String value)
        {
            this.value = value;
        }

        public override void addLeaf(String attributeValue, String decision)
        {
            throw new ApplicationException("cannot add Leaf to ConstantDecisonTree");
        }

        public override void addNode(String attributeValue, DecisionTree tree)
        {
            throw new ApplicationException("cannot add Node to ConstantDecisonTree");
        }

        public override Object predict(Example e)
        {
            return value;
        }

        public override String ToString()
        {
            return "DECISION -> " + value;
        }

        public override String ToString(int depth, StringBuilder buf)
        {
            buf.Append(Util.ntimes("\t", depth + 1));
            buf.Append("DECISION -> " + value + "\n");
            return buf.ToString();
        }
    }
}