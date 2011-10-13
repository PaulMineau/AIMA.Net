namespace AIMA.Core.Learning.Learners
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Learning.Framework;
    using AIMA.Core.Util;
    using AIMA.Core.Util.DataStructure;

    /**
     * @author Ravi Mohan
     * 
     */
    public class AdaBoostLearner : Learner
    {

        private List<Learner> learners;

        private DataSet dataSet;

        private double[] exampleWeights;

        private Dictionary<Learner, Double> learnerWeights;

        public AdaBoostLearner(List<Learner> learners, DataSet ds)
        {
            this.learners = learners;
            this.dataSet = ds;

            initializeExampleWeights(ds.examples.Count);
            initializeHypothesisWeights(learners.Count);
        }

        public void train(DataSet ds) {
		initializeExampleWeights(ds.examples.Count);

		foreach (Learner learner in learners) {
			learner.train(ds);

			double error = calculateError(ds, learner);
			if (error < 0.0001) {
				break;
			}

			adjustExampleWeights(ds, learner, error);

			double newHypothesisWeight = learnerWeights[learner]
					* Math.Log((1.0 - error) / error);
            if (learnerWeights.ContainsKey(learner))
            {
                learnerWeights[learner] = newHypothesisWeight;
            }
            else
            {
                learnerWeights.Add(learner, newHypothesisWeight);
            }
		}
	}

        public String predict(Example e)
        {
            return weightedMajority(e);
        }

        public int[] test(DataSet ds) {
		int[] results = new int[] { 0, 0 };

		foreach (Example e in ds.examples) {
			if (e.targetValue().Equals(predict(e))) {
				results[0] = results[0] + 1;
			} else {
				results[1] = results[1] + 1;
			}
		}
		return results;
	}

        //
        // PRIVATE METHODS
        //

        private String weightedMajority(Example e)
        {
            List<String> targetValues = dataSet.getPossibleAttributeValues(dataSet
                    .getTargetAttributeName());

            Table<String, Learner, Double> table = createTargetValueLearnerTable(
                    targetValues, e);
            return getTargetValueWithTheMaximumVotes(targetValues, table);
        }

        private Table<String, Learner, Double> createTargetValueLearnerTable(
                List<String> targetValues, Example e) {
		// create a table with target-attribute values as rows and learners as
		// columns and cells containing the weighted votes of each Learner for a
		// target value
		// Learner1 Learner2 Laerner3 .......
		// Yes 0.83 0.5 0
		// No 0 0 0.6

		Table<String, Learner, Double> table = new Table<String, Learner, Double>(
				targetValues, learners);
		// initialize table
		foreach (Learner l in learners) {
			foreach (String s in targetValues) {
				table.set(s, l, 0.0);
			}
		}
		foreach (Learner learner in learners) {
			String predictedValue = learner.predict(e);
			foreach (String v in targetValues) {
				if (predictedValue.Equals(v)) {
					table.set(v, learner, table.get(v, learner).Value
							+ learnerWeights[learner] * 1);
				}
			}
		}
		return table;
	}

        private String getTargetValueWithTheMaximumVotes(List<String> targetValues,
                Table<String, Learner, Double> table) {
		String targetValueWithMaxScore = targetValues[0];
		double score = scoreOfValue(targetValueWithMaxScore, table, learners);
		foreach (String value in targetValues) {
			double _scoreOfValue = scoreOfValue(value, table, learners);
            if (_scoreOfValue > score)
            {
				targetValueWithMaxScore = value;
                score = _scoreOfValue;
			}
		}
		return targetValueWithMaxScore;
	}

        private void initializeExampleWeights(int size)
        {
            if (size == 0)
            {
                throw new ApplicationException(
                        "cannot initialize Ensemble learning with Empty Dataset");
            }
            double value = 1.0 / (1.0 * size);
            exampleWeights = new double[size];
            for (int i = 0; i < size; i++)
            {
                exampleWeights[i] = value;
            }
        }

        private void initializeHypothesisWeights(int size) {
		if (size == 0) {
			throw new ApplicationException(
					"cannot initialize Ensemble learning with Zero Learners");
		}

		learnerWeights = new Dictionary<Learner, Double>();
		foreach (Learner le in learners) {
			learnerWeights.Add(le, 1.0);
		}
	}

        private double calculateError(DataSet ds, Learner l)
        {
            double error = 0.0;
            for (int i = 0; i < ds.examples.Count; i++)
            {
                Example e = ds.getExample(i);
                if (!(l.predict(e).Equals(e.targetValue())))
                {
                    error = error + exampleWeights[i];
                }
            }
            return error;
        }

        private void adjustExampleWeights(DataSet ds, Learner l, double error)
        {
            double epsilon = error / (1.0 - error);
            for (int j = 0; j < ds.examples.Count; j++)
            {
                Example e = ds.getExample(j);
                if ((l.predict(e).Equals(e.targetValue())))
                {
                    exampleWeights[j] = exampleWeights[j] * epsilon;
                }
            }
            exampleWeights = Util.normalize(exampleWeights);
        }

        private double scoreOfValue(String targetValue,
                Table<String, Learner, Double> table, List<Learner> learners) {
		double score = 0.0;
		foreach (Learner l in learners) {
			score += table.get(targetValue, l).Value;
		}
		return score;
	}
    }
}