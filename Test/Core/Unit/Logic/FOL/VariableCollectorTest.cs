namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.FOL
{

    using CosmicFlow.AIMA.Core.Logic.FOL;
    using CosmicFlow.AIMA.Core.Logic.FOL.Domain;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing.AST;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class VariableCollectorTest
    {

        FOLParser parser;

        VariableCollector vc;

        [TestInitialize]
        public void setUp()
        {
            parser = new FOLParser(DomainFactory.crusadesDomain());
            vc = new VariableCollector();
        }

        [TestMethod]
        public void testSimplepredicate()
        {
            List<Variable> variables = vc.collectAllVariables(parser
                    .parse("King(x)"));
            Assert.AreEqual(1, variables.Count);
            Assert.IsTrue(variables.Contains(new Variable("x")));
        }

        [TestMethod]
        public void testMultipleVariables()
        {
            List<Variable> variables = vc.collectAllVariables(parser
                    .parse("BrotherOf(x) = EnemyOf(y)"));
            Assert.AreEqual(2, variables.Count);
            Assert.IsTrue(variables.Contains(new Variable("x")));
            Assert.IsTrue(variables.Contains(new Variable("y")));
        }

        [TestMethod]
        public void testQuantifiedVariables()
        {
            // Note: Should collect quantified variables
            // even if not mentioned in clause.
            List<Variable> variables = vc.collectAllVariables(parser
                    .parse("FORALL x,y,z (BrotherOf(x) = EnemyOf(y))"));
            Assert.AreEqual(3, variables.Count);
            Assert.IsTrue(variables.Contains(new Variable("x")));
            Assert.IsTrue(variables.Contains(new Variable("y")));
            Assert.IsTrue(variables.Contains(new Variable("z")));
        }
    }
}
