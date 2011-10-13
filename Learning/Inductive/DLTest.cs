namespace AIMA.Core.Learning.Inductive
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Learning.Framework;
    using System.Text;

    /**
     * @author Ravi Mohan
     * 
     */
    public class DLTest
    {

        // represents a single test in the Decision List
        private Dictionary<String, String> attrValues;

        public DLTest()
        {
            attrValues = new Dictionary<String, String>();
        }

        public void add(String nta, String ntaValue)
        {
            if (!attrValues.ContainsKey(nta))
            {
                attrValues.Add(nta, ntaValue);
            }
            else
            {
                attrValues[nta] = ntaValue;
            }

        }

        public bool matches(Example e)
        {
            foreach (String key in attrValues.Keys)
            {
                if (!(attrValues[key].Equals(e.getAttributeValueAsString(key))))
                {
                    return false;
                }
            }
            return true;
            // return e.targetValue().Equals(targetValue);
        }

        public DataSet matchedExamples(DataSet ds)
        {
            DataSet matched = ds.emptyDataSet();
            foreach (Example e in ds.examples)
            {
                if (matches(e))
                {
                    matched.add(e);
                }
            }
            return matched;
        }

        public DataSet unmatchedExamples(DataSet ds)
        {
            DataSet unmatched = ds.emptyDataSet();
            foreach (Example e in ds.examples)
            {
                if (!(matches(e)))
                {
                    unmatched.add(e);
                }
            }
            return unmatched;
        }

        public override String ToString()
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("IF  ");
            foreach (String key in attrValues.Keys)
            {
                buf.Append(key + " = ");
                buf.Append(attrValues[key] + " ");
            }
            buf.Append(" DECISION ");
            return buf.ToString();
        }
    }
}