namespace aima.test.core.unit.search.framework
{


    using AIMA.Core.Search.Framework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class NodeTest
    {

        [TestMethod]
        public void testRootNode()
        {
            Node node1 = new Node("state1");
            Assert.IsTrue(node1.isRootNode());
            Node node2 = new Node("state2", node1, null, 1.0);
            Assert.IsTrue(node1.isRootNode());
            Assert.IsFalse(node2.isRootNode());
            Assert.AreEqual(node1, node2.getParent());
        }

        [TestMethod]
        public void testGetPathFromRoot()
        {
            Node node1 = new Node("state1");
            Node node2 = new Node("state2", node1, null, 1.0);
            Node node3 = new Node("state3", node2, null, 1.0);
            List<Node> path = node3.getPathFromRoot();
            Assert.Equals(node1, path[0]);
            Assert.Equals(node2, path[1]);
            Assert.Equals(node3, path[2]);
        }
    }
}
