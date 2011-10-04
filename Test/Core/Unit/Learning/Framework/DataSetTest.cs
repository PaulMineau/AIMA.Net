namespace CosmicFlow.AIMA.Test.Core.Unit.Learning.Framework
{

    using CosmicFlow.AIMA.Core.Learning.Framework;
    using CosmicFlow.AIMA.Core.Learning.Neural;
    using CosmicFlow.AIMA.Core.Util.DataStructure;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class DataSetTest
    {
        private const String YES = "Yes";

        DataSetSpecification spec;

        [TestMethod]
        public void testNormalizationOfFileBasedDataProducesCorrectMeanStdDevAndNormalizedValues()
        {
            RabbitEyeDataSet reds = new RabbitEyeDataSet();
            reds.createNormalizedDataFromFile("rabbiteyes.csv");

            List<Double> means = reds.getMeans();
            Assert.AreEqual(2, means.Count);
            Assert.AreEqual(244.771, means[0], 0.001);
            Assert.AreEqual(145.505, means[1], 0.001);

            List<Double> stdev = reds.getStdevs();
            Assert.AreEqual(2, stdev.Count);
            Assert.AreEqual(213.554, stdev[0], 0.001);
            Assert.AreEqual(65.776, stdev[1], 0.001);

            List<List<Double>> normalized = reds.getNormalizedData();
            Assert.AreEqual(70, normalized.Count);

            // check first value
            Assert.AreEqual(-1.0759, normalized[0][0], 0.001);
            Assert.AreEqual(-1.882, normalized[0][1], 0.001);

            // check last Value
            Assert.AreEqual(2.880, normalized[69][0], 0.001);
            Assert.AreEqual(1.538, normalized[69][1], 0.001);
        }

        [TestMethod]
        public void testExampleFormation()
        {
            RabbitEyeDataSet reds = new RabbitEyeDataSet();
            reds.createExamplesFromFile("rabbiteyes.csv");
            Assert.AreEqual(70, reds.howManyExamplesLeft());
            reds.getExampleAtRandom();
            Assert.AreEqual(69, reds.howManyExamplesLeft());
            reds.getExampleAtRandom();
            Assert.AreEqual(68, reds.howManyExamplesLeft());
        }

        [TestMethod]
        public void testLoadsDatasetFile()
        {

            DataSet ds = DataSetFactory.getRestaurantDataSet();
            Assert.AreEqual(12, ds.size());

            Example first = ds.getExample(0);
            Assert.AreEqual(YES, first.getAttributeValueAsString("alternate"));
            Assert.AreEqual("$$$", first.getAttributeValueAsString("price"));
            Assert.AreEqual("0-10", first
                    .getAttributeValueAsString("wait_estimate"));
            Assert.AreEqual(YES, first.getAttributeValueAsString("will_wait"));
            Assert.AreEqual(YES, first.targetValue());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testThrowsExceptionForNonExistentFile()
        {
            new DataSetFactory().fromFile("nonexistent", null, ' ');
        }

        [TestMethod]
        public void testLoadsIrisDataSetWithNumericAndStringAttributes()
        {
            DataSet ds = DataSetFactory.getIrisDataSet();
            Example first = ds.getExample(0);
            Assert.AreEqual("5.1", first
                    .getAttributeValueAsString("sepal_length"));
        }

        [TestMethod]
        public void testNonDestructiveRemoveExample()
        {
            DataSet ds1 = DataSetFactory.getRestaurantDataSet();
            DataSet ds2 = ds1.removeExample(ds1.getExample(0));
            Assert.AreEqual(12, ds1.size());
            Assert.AreEqual(11, ds2.size());
        }

        [TestMethod]
        public void testNumerizesAndDeNumerizesIrisDataSetExample1()
        {
            DataSet ds = DataSetFactory.getIrisDataSet();
            Example first = ds.getExample(0);
            Numerizer n = new IrisDataSetNumerizer();
            Pair<List<Double>, List<Double>> io = n.numerize(first);

            AssertListsEqual<double>(new List<double>() { 5.1, 3.5, 1.4, 0.2 }, io.getFirst());
            AssertListsEqual<double>(new List<double>() { 0.0, 0.0, 1.0 }, io.getSecond());

            String plant_category = n.denumerize(new List<double>() { 0.0, 0.0, 1.0 });
            Assert.AreEqual("setosa", plant_category);
        }

        [TestMethod]
        public void testNumerizesAndDeNumerizesIrisDataSetExample2()
        {
            DataSet ds = DataSetFactory.getIrisDataSet();
            Example first = ds.getExample(51);
            Numerizer n = new IrisDataSetNumerizer();
            Pair<List<Double>, List<Double>> io = n.numerize(first);

            AssertListsEqual<double>(new List<double>() { 6.4, 3.2, 4.5, 1.5 }, io.getFirst());
            AssertListsEqual<double>(new List<double>() { 0.0, 1.0, 0.0 }, io.getSecond());

            String plant_category = n.denumerize(new List<double>() { 0.0, 1.0, 0.0 });
            Assert.AreEqual("versicolor", plant_category);
        }

        private void AssertListsEqual<T>(List<T> l1, List<T> l2)
        {
            if (l1.Count != l2.Count)
            {
                Assert.Fail("Lists were not the same length");
            }
            for (int i = 0; i < l1.Count; i++)
            {
                if (!l1[i].Equals(l2[i]))
                {
                    Assert.Fail("Lists are not equal");
                }
            }
        }

        [TestMethod]
        public void testNumerizesAndDeNumerizesIrisDataSetExample3()
        {
            DataSet ds = DataSetFactory.getIrisDataSet();
            Example first = ds.getExample(100);
            Numerizer n = new IrisDataSetNumerizer();
            Pair<List<Double>, List<Double>> io = n.numerize(first);

            AssertListsEqual<double>(new List<double>() { 6.3, 3.3, 6.0, 2.5 }, io.getFirst());
            AssertListsEqual<double>(new List<double>() { 1.0, 0.0, 0.0 }, io.getSecond());

            String plant_category = n.denumerize(new List<double>() { 1.0, 0.0, 0.0 });
            Assert.AreEqual("virginica", plant_category);
        }
    }
}
