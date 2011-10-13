namespace AIMA.Test.Core.Unit.Logic.Propositional.Parsing
{


    using AIMA.Core.Logic.Propositional.Parsing.Ast;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class ListTest
    {

        [TestMethod]
        public void testListOfSymbolsClone()
        {
            List<Symbol> l = new List<Symbol>();
            l.Add(new Symbol("A"));
            l.Add(new Symbol("B"));
            l.Add(new Symbol("C"));
            Symbol[] copy = new Symbol[l.Count];
            l.CopyTo(copy);
            List<Symbol> l2 = copy.ToList<Symbol>();
            l2.Remove(new Symbol("B"));
            Assert.AreEqual(3, l.Count);
            Assert.AreEqual(2, l2.Count);
        }

        [TestMethod]
        public void testListRemove()
        {
            List<int> one = new List<int>();
            one.Add(1);
            Assert.AreEqual(1, one.Count);
            one.Remove(0);
            Assert.AreEqual(0, one.Count);
        }
    }
}