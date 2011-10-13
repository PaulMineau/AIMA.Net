namespace AIMA.Test.Core.Unit
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AIMA.Core.Util;

/**
 * @author Ravi Mohan
 * 
 */
[TestClass]
public class UtilTest {
	private List<Double> values;

	[TestInitialize]
	public void setUp() {
		values = new List<Double>();
		values.Add(1.0);
		values.Add(2.0);
		values.Add(3.0);
		values.Add(4.0);
		values.Add(5.0);
	}

	[TestMethod]
	public void testModeFunction() {
		List<int> l = new List<int>();
		l.Add(1);
		l.Add(2);
		l.Add(2);
		l.Add(3);
		int i = Util.mode(l);
        Assert.AreEqual(2, i);

		List<int> l2 = new List<int>();
		l2.Add(1);
		i = Util.mode(l2);
        Assert.AreEqual(1, i);
	}

	[TestMethod]
	public void testMeanCalculation() {
        Assert.AreEqual(3.0, Util.calculateMean(values), 0.001);
	}

	[TestMethod]
	public void testStDevCalculation() {
        Assert.AreEqual(1.5811, Util.calculateStDev(values, 3.0), 0.0001);
	}

	[TestMethod]
	public void testNormalization() {
		List<Double> nrm = Util.normalizeFromMeanAndStdev(values, 3.0, 1.5811);
		Assert.AreEqual(-1.264, nrm[0], 0.001);
        Assert.AreEqual(-0.632, nrm[1], 0.001);
        Assert.AreEqual(0.0, nrm[2], 0.001);
        Assert.AreEqual(0.632, nrm[3], 0.001);
        Assert.AreEqual(1.264, nrm[4], 0.001);

	}

    [TestMethod]
	public void testRandomNumberGenrationWhenStartAndEndNumbersAreSame() {
		int i = Util.randomNumberBetween(0, 0);
		int j = Util.randomNumberBetween(23, 23);
        Assert.AreEqual(0, i);
        Assert.AreEqual(23, j);
	}
}
}