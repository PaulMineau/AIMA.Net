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
    public class FIFOQueueTest
    {

        [TestMethod]
        public void testFIFOQueue()
        {
            FIFOQueue<String> queue = new FIFOQueue<String>();
            Assert.IsTrue(queue.isEmpty());

            queue.push("Hello");
            Assert.AreEqual(1, queue.Count);
            Assert.IsFalse(queue.isEmpty());

            queue.push("Hi");
            Assert.AreEqual(2, queue.Count);
            Assert.IsFalse(queue.isEmpty());

            String s = queue.pop();
            Assert.AreEqual("Hello", s);
            Assert.AreEqual(1, queue.Count);
            Assert.AreEqual("Hi", queue.Peek());

            queue.push("bonjour");
            queue.push("salaam alaikum");
            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual("Hi", queue.pop());
            Assert.AreEqual("bonjour", queue.pop());
            Assert.AreEqual("salaam alaikum", queue.pop());

            Assert.AreEqual(0, queue.Count);
        }
    }
}