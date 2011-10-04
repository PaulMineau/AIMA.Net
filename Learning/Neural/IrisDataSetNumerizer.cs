namespace CosmicFlow.AIMA.Core.Learning.Neural
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Learning.Framework;
    using CosmicFlow.AIMA.Core.Util.DataStructure;

    /**
     * @author Ravi Mohan
     * 
     */
    public class IrisDataSetNumerizer : Numerizer
    {

        public Pair<List<Double>, List<Double>> numerize(Example e)
        {
            List<Double> input = new List<Double>();
            List<Double> desiredOutput = new List<Double>();

            double sepal_length = e.getAttributeValueAsDouble("sepal_length");
            double sepal_width = e.getAttributeValueAsDouble("sepal_width");
            double petal_length = e.getAttributeValueAsDouble("petal_length");
            double petal_width = e.getAttributeValueAsDouble("petal_width");

            input.Add(sepal_length);
            input.Add(sepal_width);
            input.Add(petal_length);
            input.Add(petal_width);

            String plant_category_string = e
                    .getAttributeValueAsString("plant_category");

            desiredOutput = convertCategoryToListOfDoubles(plant_category_string);

            Pair<List<Double>, List<Double>> io = new Pair<List<Double>, List<Double>>(
                    input, desiredOutput);

            return io;
        }

        public String denumerize(List<Double> outputValue)
        {
            List<Double> rounded = new List<Double>();
            foreach (Double d in outputValue)
            {
                rounded.Add(round(d));
            }
            if (rounded.Equals(new List<double>(){0.0, 0.0, 1.0}))
            {
                return "setosa";
            }
            else if (rounded.Equals(new List<double>(){0.0, 1.0, 0.0}))
            {
                return "versicolor";
            }
            else if (rounded.Equals(new List<double>(){1.0, 0.0, 0.0}))
            {
                return "virginica";
            }
            else
            {
                return "unknown";
            }
        }

        //
        // PRIVATE METHODS
        //
        private double round(Double d)
        {
            if (d < 0)
            {
                return 0.0;
            }
            if (d > 1)
            {
                return 1.0;
            }
            else
            {
                return Math.Round(d);
            }
        }

        private List<Double> convertCategoryToListOfDoubles(
                String plant_category_string)
        {
            if (plant_category_string.Equals("setosa"))
            {
                return new List<double>(){0.0, 0.0, 1.0};
            }
            else if (plant_category_string.Equals("versicolor"))
            {
                return new List<double>(){0.0, 1.0, 0.0};
            }
            else if (plant_category_string.Equals("virginica"))
            {
                return new List<double>(){1.0, 0.0, 0.0};
            }
            else
            {
                throw new ApplicationException("invalid plant category");
            }
        }
    }
}