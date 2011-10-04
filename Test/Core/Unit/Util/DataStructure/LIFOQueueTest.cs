namespace CosmicFlow.AIMA.Test.Core.Unit
{

using CosmicFlow.AIMA.Core.Util.DataStructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

/**
 * @author Ravi Mohan
 * 
 */
[TestClass]
public class LIFOQueueTest {

	[TestMethod]
	public void testLIFOQueue() {
		LIFOQueue<String> queue = new LIFOQueue<String>();
		Assert.IsTrue(queue.isEmpty());

		queue.add("Hello");
        Assert.AreEqual(1, queue.Count);
		Assert.IsFalse(queue.isEmpty());

		queue.add("Hi");
        Assert.AreEqual(2, queue.Count);
        Assert.IsFalse(queue.isEmpty());

		String s = (String) queue.pop();
        Assert.AreEqual("Hi", s);
        Assert.AreEqual(1, queue.Count);
        Assert.AreEqual("Hello", queue.Peek());

        List<String> l = new List<String>();
		l.Add("salaam alaikum");
		l.Add("bonjour");
		queue.addAll(l);
        Assert.AreEqual(3, queue.Count);

        Assert.AreEqual("bonjour", queue.pop());
        Assert.AreEqual("salaam alaikum", queue.pop());
        Assert.AreEqual("Hello", queue.pop());

        Assert.AreEqual(0, queue.Count);
	}
}
}