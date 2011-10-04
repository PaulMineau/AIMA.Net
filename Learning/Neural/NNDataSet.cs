namespace CosmicFlow.AIMA.Core.Learning.Neural
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Learning;
    using CosmicFlow.AIMA.Core.Learning.Framework;
    using CosmicFlow.AIMA.Core.Util;
    using CosmicFlow.AIMA.Core.Util.DataStructure;
    using System.IO;

    /**
     * @author Ravi Mohan
     * 
     */
    public abstract class NNDataSet
    {
        /*
         * This class represents a source of examples to the rest of the nn
         * framework. Assumes only one function approximator works on an instance at
         * a given point in time
         */
        /*
         * the parsed and preprocessed form of the dataset.
         */
        private List<NNExample> dataset;
        /*
         * a copy from which examples are drawn.
         */
        private List<NNExample> presentlyProcessed = new List<NNExample>();

        /*
         * list of mean Values for all components of raw data set
         */
        private List<Double> means;

        /*
         * list of stdev Values for all components of raw data set
         */
        private List<Double> stdevs;
        /*
         * the normalized data set
         */
        protected List<List<Double>> nds;

        /*
         * the column numbers of the "target"
         */

        protected List<int> targetColumnNumbers;

        /*
         * population delegated to subclass because only subclass knows which
         * column(s) is target
         */
        public abstract void setTargetColumns();

        /*
         * create a normalized data "table" from the data in the file. At this
         * stage, the data isnot split into input pattern and tragets
         */
        public void createNormalizedDataFromFile(String filename)
        {

            List<List<Double>> rds = new List<List<Double>>();

            // create raw data set
            using (StreamReader reader = new StreamReader(typeof(NNDataSet).Assembly.GetManifestResourceStream("AIMA.Resource." + filename)))
        {
            String line;
            while ((line = reader.ReadLine()) != null)
            {
                                rds.Add(exampleFromString(line, ','));
            }
        }

            // normalize raw dataset
            nds = normalize(rds);
        }

        /*
         * create a normalized data "table" from the DataSet using numerizer. At
         * this stage, the data isnot split into input pattern and targets TODO
         * remove redundancy of recreating the target columns. the numerizer has
         * already isolated the targets
         */
        public void createNormalizedDataFromDataSet(DataSet ds, Numerizer numerizer)
        {

            List<List<Double>> rds = rawExamplesFromDataSet(ds, numerizer);
            // normalize raw dataset
            nds = normalize(rds);
        }

        /*
         * Gets (and removes) a random example from the 'presentlyProcessed'
         */
        public NNExample getExampleAtRandom()
        {

            int i = Util.randomNumberBetween(0, (presentlyProcessed.Count - 1));
            NNExample result = presentlyProcessed[i];
            presentlyProcessed.RemoveAt(i);
            return result;
        }

        /*
         * Gets (and removes) a random example from the 'presentlyProcessed'
         */
        public NNExample getExample(int index)
        {
            NNExample result = presentlyProcessed[index];
            presentlyProcessed.RemoveAt(index);
            return result;
        }

        /*
         * check if any more examples remain to be processed
         */
        public bool hasMoreExamples()
        {
            return presentlyProcessed.Count > 0;
        }

        /*
         * check how many examples remain to be processed
         */
        public int howManyExamplesLeft()
        {
            return presentlyProcessed.Count;
        }

        /*
         * refreshes the presentlyProcessed dataset so it can be used for a new
         * epoch of training.
         */
        public void refreshDataset() {
		presentlyProcessed = new List<NNExample>();
		foreach (NNExample e in dataset) {
			presentlyProcessed.Add(e.copyExample());
		}
	}

        /*
         * method called by clients to set up data set and make it ready for
         * processing
         */
        public void createExamplesFromFile(String filename)
        {
            createNormalizedDataFromFile(filename);
            setTargetColumns();
            createExamples();

        }

        /*
         * method called by clients to set up data set and make it ready for
         * processing
         */
        public void createExamplesFromDataSet(DataSet ds, Numerizer numerizer)
        {
            createNormalizedDataFromDataSet(ds, numerizer);
            setTargetColumns();
            createExamples();

        }

        public List<List<Double>> getNormalizedData()
        {
            return nds;
        }

        public List<Double> getMeans()
        {
            return means;
        }

        public List<Double> getStdevs()
        {
            return stdevs;
        }

        //
        // PRIVATE METHODS
        //

        /*
         * create Example instances from a normalized data "table".
         */
        private void createExamples() {
		dataset = new List<NNExample>();
		foreach (List<Double> dataLine in nds) {
			List<Double> input = new List<Double>();
			List<Double> target = new List<Double>();
			for (int i = 0; i < dataLine.Count; i++) {
				if (targetColumnNumbers.Contains(i)) {
					target.Add(dataLine[i]);
				} else {
					input.Add(dataLine[i]);
				}
			}
			dataset.Add(new NNExample(input, target));
		}
		refreshDataset();// to populate the preentlyProcessed dataset
	}

        private List<List<Double>> normalize(List<List<Double>> rds) {
		int rawDataLength = rds[0].Count;
		List<List<Double>> nds = new List<List<Double>>();

		means = new List<Double>();
		stdevs = new List<Double>();

		List<List<Double>> normalizedColumns = new List<List<Double>>();
		// clculate means for each coponent of example data
		for (int i = 0; i < rawDataLength; i++) {
			List<Double> columnValues = new List<Double>();
			foreach (List<Double> rawDatum in rds) {
				columnValues.Add(rawDatum[i]);
			}
			double mean = Util.calculateMean(columnValues);
			means.Add(mean);

			double stdev = Util.calculateStDev(columnValues, mean);
			stdevs.Add(stdev);

			normalizedColumns.Add(Util.normalizeFromMeanAndStdev(columnValues,
					mean, stdev));

		}
		// re arrange data from columns
		// TODO Assert normalized columns have same size etc

		int columnLength = normalizedColumns[0].Count;
		int numberOfColumns = normalizedColumns.Count;
		for (int i = 0; i < columnLength; i++) {
			List<Double> lst = new List<Double>();
			for (int j = 0; j < numberOfColumns; j++) {
				lst.Add(normalizedColumns[j][i]);
			}
			nds.Add(lst);
		}
		return nds;
	}

        private List<Double> exampleFromString(String line, Char separator) {
		// assumes all values for inout and target are doubles
		List<Double> rexample = new List<Double>();
		List<String> attributeValues = new List<string>( line.Split(separator));
		foreach (String valString in attributeValues) {
			rexample.Add(Double.Parse(valString));
		}
		return rexample;
	}

        private List<List<Double>> rawExamplesFromDataSet(DataSet ds,
                Numerizer numerizer) {
		// assumes all values for inout and target are doubles
		List<List<Double>> rds = new List<List<Double>>();
		for (int i = 0; i < ds.size(); i++) {
			List<Double> rexample = new List<Double>();
			Example e = ds.getExample(i);
			Pair<List<Double>, List<Double>> p = numerizer.numerize(e);
			List<Double> attributes = p.getFirst();
			foreach (Double d in attributes) {
				rexample.Add(d);
			}
			List<Double> targets = p.getSecond();
			foreach (Double d in targets) {
				rexample.Add(d);
			}
			rds.Add(rexample);
		}
		return rds;
	}
    }
}