namespace CosmicFlow.AIMA.Core.Learning.Framework
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public class DataSet
    {
        protected DataSet()
        {

        }

        public List<Example> examples;

        public DataSetSpecification specification;

        public DataSet(DataSetSpecification spec)
        {
            examples = new List<Example>();
            this.specification = spec;
        }

        public void add(Example e)
        {
            examples.Add(e);
        }

        public int size()
        {
            return examples.Count;
        }

        public Example getExample(int number)
        {
            return examples[number];
        }

        public DataSet removeExample(Example e)
        {
            DataSet ds = new DataSet(specification);
            foreach (Example eg in examples)
            {
                if (!(e.Equals(eg)))
                {
                    ds.add(eg);
                }
            }
            return ds;
        }

        public double getInformationFor()
        {
            String attributeName = specification.getTarget();
            Dictionary<String, int> counts = new Dictionary<String, int>();
            foreach (Example e in examples)
            {

                String val = e.getAttributeValueAsString(attributeName);
                if (counts.ContainsKey(val))
                {
                    counts[val]++;
                }
                else
                {
                    counts.Add(val, 1);
                }
            }

            double[] data = new double[counts.Keys.Count];
            int i = 0;
            foreach(string key in counts.Keys)
            {
                data[i] = counts[key];
                i++;
            }

            data = Util.normalize(data);

            return Util.information(data);
        }

        public Dictionary<String, DataSet> splitByAttribute(String attributeName)
        {
            Dictionary<String, DataSet> results = new Dictionary<String, DataSet>();
            foreach (Example e in examples)
            {
                String val = e.getAttributeValueAsString(attributeName);
                if (results.ContainsKey(val))
                {
                    results[val].add(e);
                }
                else
                {
                    DataSet ds = new DataSet(specification);
                    ds.add(e);
                    results.Add(val, ds);
                }
            }
            return results;
        }

        public double calculateGainFor(String parameterName)
        {
            Dictionary<String, DataSet> hash = splitByAttribute(parameterName);
            double totalSize = examples.Count;
            double remainder = 0.0;
            foreach (String parameterValue in hash.Keys)
            {
                double reducedDataSetSize = hash[parameterValue].examples
                        .Count;
                remainder += (reducedDataSetSize / totalSize)
                        * hash[parameterValue].getInformationFor();
            }
            return getInformationFor() - remainder;
        }

        public override bool Equals(Object o)
        {
            if (this == o)
            {
                return true;
            }
            if ((o == null) || !(o is DataSet))
            {
                return false;
            }
            DataSet other = (DataSet)o;
            return examples.Equals(other.examples);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public List<Example>.Enumerator iterator()
        {
            return examples.GetEnumerator();
        }

        public DataSet copy()
        {
            DataSet ds = new DataSet(specification);
            foreach (Example e in examples)
            {
                ds.add(e);
            }
            return ds;
        }

        public List<String> getAttributeNames()
        {
            return specification.getAttributeNames();
        }

        public String getTargetAttributeName()
        {
            return specification.getTarget();
        }

        public DataSet emptyDataSet()
        {
            return new DataSet(specification);
        }

        /**
         * @param specification
         *            The specification to set. USE SPARINGLY for testing etc ..
         *            makes no semantic sense
         */
        public void setSpecification(DataSetSpecification specification)
        {
            this.specification = specification;
        }

        public List<String> getPossibleAttributeValues(String attributeName)
        {
            return specification.getPossibleAttributeValues(attributeName);
        }

        public DataSet matchingDataSet(String attributeName, String attributeValue)
        {
            DataSet ds = new DataSet(specification);
            foreach (Example e in examples)
            {
                if (e.getAttributeValueAsString(attributeName).Equals(
                        attributeValue))
                {
                    ds.add(e);
                }
            }
            return ds;
        }

        public List<String> getNonTargetAttributes()
        {
            return Util.removeFrom(getAttributeNames(), getTargetAttributeName());
        }
    }
}
