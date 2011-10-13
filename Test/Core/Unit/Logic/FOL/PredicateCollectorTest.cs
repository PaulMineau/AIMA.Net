namespace AIMA.Test.Core.Unit.Logic.FOL
{

    using AIMA.Core.Logic.FOL;
    using AIMA.Core.Logic.FOL.Domain;
    using AIMA.Core.Logic.FOL.Parsing;
    using AIMA.Core.Logic.FOL.Parsing.AST;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;


    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class PredicateCollectorTest
    {
        PredicateCollector collector;

        FOLParser parser;

        [TestInitialize]
        public void setUp()
        {
            collector = new PredicateCollector();
            parser = new FOLParser(DomainFactory.weaponsDomain());
        }

        [TestMethod]
        public void testSimpleSentence()
        {
            Sentence s = parser.parse("(Missile(x) => Weapon(x))");
            List<Predicate> predicates = collector.getPredicates(s);
            Assert.IsNotNull(predicates);
        }
    }
}
