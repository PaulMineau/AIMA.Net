namespace AIMA.Test.Core.Unit
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using AIMA.Core.Util;

/**
 * @author Ravi Mohan
 * 
 */
[TestClass]
public class SetOpsTest {
	List<int> s1, s2;

	[TestInitialize]
	public void setUp() {
		s1 = new List<int>();
		s1.Add(1);
		s1.Add(2);
		s1.Add(3);
		s1.Add(4);

		s2 = new List<int>();
		s2.Add(4);
		s2.Add(5);
		s2.Add(6);
	}

	[TestMethod]
	public void testUnion() {
		List<int> union = SetOps.union(s1, s2);
        Assert.AreEqual(6, union.Count);
        Assert.AreEqual(4, s1.Count);
        Assert.AreEqual(3, s2.Count);

		s1.Remove(1);
        Assert.AreEqual(6, union.Count);
        Assert.AreEqual(3, s1.Count);
        Assert.AreEqual(3, s2.Count);
	}

	[TestMethod]
	public void testIntersection() {
		List<int> intersection = SetOps.intersection(s1, s2);
        Assert.AreEqual(1, intersection.Count);
        Assert.AreEqual(4, s1.Count);
        Assert.AreEqual(3, s2.Count);

		s1.Remove(1);
        Assert.AreEqual(1, intersection.Count);
        Assert.AreEqual(3, s1.Count);
        Assert.AreEqual(3, s2.Count);
	}

	[TestMethod]
	public void testDifference() {
		List<int> difference = SetOps.difference(s1, s2);
		Assert.AreEqual(3, difference.Count);
        Assert.IsTrue(difference.Contains(1));
        Assert.IsTrue(difference.Contains(2));
        Assert.IsTrue(difference.Contains(3));
	}

    [TestMethod]
	public void testDifference2() {
        List<int> one = new List<int>();
        List<int> two = new List<int>();
		one.Add(1);
		two.Add(1);
        List<int> difference = SetOps.difference(one, two);
		Assert.AreEqual(0,difference.Count);
	}
}
}