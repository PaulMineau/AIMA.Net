namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.FOL.KB.Data
{


    using CosmicFlow.AIMA.Core.Logic.FOL.KB.Data;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing.AST;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    [TestClass]
    public class ChainTest
    {

        [TestMethod]
        public void testIsEmpty()
        {
            Chain c = new Chain();

            Assert.IsTrue(c.isEmpty());

            c.addLiteral(new Literal(new Predicate("P", new List<Term>())));

            Assert.IsFalse(c.isEmpty());

            List<Literal> lits = new List<Literal>();

            lits.Add(new Literal(new Predicate("P", new List<Term>())));

            c = new Chain(lits);

            Assert.IsFalse(c.isEmpty());
        }

        [TestMethod]
        public void testContrapositives()
        {
            List<Chain> conts;
            Literal p = new Literal(new Predicate("P", new List<Term>()));
            Literal notq = new Literal(new Predicate("Q", new List<Term>()),
                    true);
            Literal notr = new Literal(new Predicate("R", new List<Term>()),
                    true);

            Chain c = new Chain();

            conts = c.getContrapositives();
            Assert.AreEqual(0, conts.Count);

            c.addLiteral(p);
            conts = c.getContrapositives();
            Assert.AreEqual(0, conts.Count);

            c.addLiteral(notq);
            conts = c.getContrapositives();
            Assert.AreEqual(1, conts.Count);
            Assert.AreEqual("<~Q(),P()>", conts[0].ToString());

            c.addLiteral(notr);
            conts = c.getContrapositives();
            Assert.AreEqual(2, conts.Count);
            Assert.AreEqual("<~Q(),P(),~R()>", conts[0].ToString());
            Assert.AreEqual("<~R(),P(),~Q()>", conts[1].ToString());
        }
    }
}