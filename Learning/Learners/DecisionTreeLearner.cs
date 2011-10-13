namespace AIMA.Core.Learning.Learners
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Learning.Framework;
    using AIMA.Core.Learning.Inductive;
    using AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public class DecisionTreeLearner : Learner
    {
        private DecisionTree tree;

        private String defaultValue;

        public DecisionTreeLearner()
        {
            this.defaultValue = "Unable To Classify";

        }

        // used when you have to test a non induced tree (eg: for testing)
        public DecisionTreeLearner(DecisionTree tree, String defaultValue)
        {
            this.tree = tree;
            this.defaultValue = defaultValue;
        }

        public virtual void train(DataSet ds)
        {
            List<String> attributes = ds.getNonTargetAttributes();
            this.tree = decisionTreeLearning(ds, attributes,
                    new ConstantDecisonTree(defaultValue));
        }

        public String predict(Example e)
        {
            return (String)tree.predict(e);
        }

        public int[] test(DataSet ds)
        {
            int[] results = new int[] { 0, 0 };

            foreach (Example e in ds.examples)
            {
                if (e.targetValue().Equals(tree.predict(e)))
                {
                    results[0] = results[0] + 1;
                }
                else
                {
                    results[1] = results[1] + 1;
                }
            }
            return results;
        }

        public DecisionTree getDecisionTree()
        {
            return tree;
        }

        //
        // PRIVATE METHODS
        //

        private DecisionTree decisionTreeLearning(DataSet ds,
                List<String> attributeNames, ConstantDecisonTree defaultTree)
        {
            if (ds.size() == 0)
            {
                return defaultTree;
            }
            if (allExamplesHaveSameClassification(ds))
            {
                return new ConstantDecisonTree(ds.getExample(0).targetValue());
            }
            if (attributeNames.Count == 0)
            {
                return majorityValue(ds);
            }
            String chosenAttribute = chooseAttribute(ds, attributeNames);

            DecisionTree tree = new DecisionTree(chosenAttribute);
            ConstantDecisonTree m = majorityValue(ds);

            List<String> values = ds.getPossibleAttributeValues(chosenAttribute);
            foreach (String v in values)
            {
                DataSet filtered = ds.matchingDataSet(chosenAttribute, v);
                List<String> newAttribs = Util.removeFrom(attributeNames,
                        chosenAttribute);
                DecisionTree subTree = decisionTreeLearning(filtered, newAttribs, m);
                tree.addNode(v, subTree);

            }

            return tree;
        }

        private ConstantDecisonTree majorityValue(DataSet ds)
        {
            Learner learner = new MajorityLearner();
            learner.train(ds);
            return new ConstantDecisonTree(learner.predict(ds.getExample(0)));
        }

        private String chooseAttribute(DataSet ds, List<String> attributeNames)
        {
            double greatestGain = 0.0;
            String attributeWithGreatestGain = attributeNames[0];
            foreach (String attr in attributeNames)
            {
                double gain = ds.calculateGainFor(attr);
                if (gain > greatestGain)
                {
                    greatestGain = gain;
                    attributeWithGreatestGain = attr;
                }
            }

            return attributeWithGreatestGain;
        }

        private bool allExamplesHaveSameClassification(DataSet ds)
        {
            String classification = ds.getExample(0).targetValue();
            List<Example>.Enumerator iter = ds.iterator();
            while (iter.MoveNext())
            {
                Example element = iter.Current;
                if (!(element.targetValue().Equals(classification)))
                {
                    return false;
                }

            }
            return true;
        }
    }
}
