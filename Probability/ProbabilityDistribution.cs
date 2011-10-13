namespace AIMA.Core.Probability
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    /**
     * @author Ravi Mohan
     * 
     */
    public class ProbabilityDistribution
    {
        private List<Row> rows = new List<Row>();
        // <VariableName:DistributionIndex>
        public Dictionary<String, int> variableNames = new Dictionary<String, int>();

        public ProbabilityDistribution(params String [] vNames)
        {
            for (int i = 0; i < vNames.Length; i++)
            {
                variableNames.Add(vNames[i], i);
            }
        }

        public void set(double probability, params bool [] values)
        {
            if (values.Length != variableNames.Count)
            {
                throw new ArgumentOutOfRangeException(
                        "Invalid number of values, must = # of Random Variables in distribution:"
                                + variableNames.Count);
            }
            
            rows.Add(new Row(probability, this, values));
        }

        public double probabilityOf(String variableName, bool b)
        {
            Dictionary<String, bool> h = new Dictionary<String, bool>();
            h.Add(variableName, b);
            return probabilityOf(h);
        }

        public double probabilityOf(Dictionary<String, bool> conditions)
        {
            double prob = 0.0;
            foreach (Row row in rows)
            {
                bool rowMeetsAllConditions = true;
                foreach (string key in conditions.Keys)
                {
                    if (!(row.matches(key, conditions[key])))
                    {
                        rowMeetsAllConditions = false;
                        break;
                    }
                }
                if (rowMeetsAllConditions)
                {
                    prob += row.probability;
                }
            }

            return prob;
        }

        public override String ToString()
        {
            StringBuilder b = new StringBuilder();
            foreach (Row row in rows)
            {
                b.Append(row.ToString() + "\n");
            }

            return b.ToString();
        }

        //
        // INNER CLASSES
        //

        internal class Row
        {
            public double probability;
            private bool[] values;
            private ProbabilityDistribution probabilityDistribution;

            internal Row(double probability, ProbabilityDistribution probabilityDistribution, params bool [] vals)
            {
                this.probabilityDistribution = probabilityDistribution;
                this.probability = probability;
                values = new bool[vals.Length];
                System.Array.Copy(vals, 0, values, 0, vals.Length);
            }

            public bool matches(String vName, bool value)
            {
                bool rVal = false;
                
                if (probabilityDistribution.variableNames.ContainsKey(vName))
                {
                    int idx = probabilityDistribution.variableNames[vName];
                    rVal = values[idx] == value;
                }
                return rVal;
            }

            public override String ToString() {
			StringBuilder b = new StringBuilder();
			b.Append("[");
			bool first = true;
			foreach (string key in probabilityDistribution.variableNames.Keys) {
				if (first) {
					first = false;
				} else {
					b.Append(", ");
				}
				b.Append(key);
				b.Append("=");
                b.Append(values[probabilityDistribution.variableNames[key]]);
			}
			b.Append("]");
			b.Append(" => ");
			b.Append(probability);

			return b.ToString();
		}
        }
    }
}