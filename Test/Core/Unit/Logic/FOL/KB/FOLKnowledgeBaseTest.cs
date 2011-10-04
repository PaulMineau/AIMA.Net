namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.FOL.KB
{

    using CosmicFlow.AIMA.Core.Logic.FOL;
    using CosmicFlow.AIMA.Core.Logic.FOL.Domain;
    using CosmicFlow.AIMA.Core.Logic.FOL.KB;
    using CosmicFlow.AIMA.Core.Logic.FOL.KB.Data;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing.AST;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     * 
     */
    [TestClass]
    public class FOLKnowledgeBaseTest
    {

        private FOLKnowledgeBase weaponsKB, kingsKB;

        [TestInitialize]
        public void setUp()
        {
            StandardizeApartIndexicalFactory.flush();

            weaponsKB = new FOLKnowledgeBase(DomainFactory.weaponsDomain());

            kingsKB = new FOLKnowledgeBase(DomainFactory.kingsDomain());
        }

        [TestMethod]
        public void testAddRuleAndFact()
        {
            weaponsKB.tell("(Missile(x) => Weapon(x))");
            Assert.AreEqual(1, weaponsKB.getNumberRules());
            weaponsKB.tell("American(West)");
            Assert.AreEqual(1, weaponsKB.getNumberRules());
            Assert.AreEqual(1, weaponsKB.getNumberFacts());
        }

        [TestMethod]
        public void testAddComplexRule()
        {
            weaponsKB
                    .tell("( (((American(x) AND Weapon(y)) AND Sells(x,y,z)) AND Hostile(z)) => Criminal(x))");
            Assert.AreEqual(1, weaponsKB.getNumberRules());
            weaponsKB.tell("American(West)");
            Assert.AreEqual(1, weaponsKB.getNumberRules());

            List<Term> terms = new List<Term>();
            terms.Add(new Variable("v0"));

            Clause dcRule = weaponsKB.getAllDefiniteClauseImplications()[0];
            Assert.IsNotNull(dcRule);
            Assert.AreEqual(true, dcRule.isImplicationDefiniteClause());
            Assert.AreEqual(new Literal(new Predicate("Criminal", terms)),
                    dcRule.getPositiveLiterals()[0]);
        }

        [TestMethod]
        public void testFactNotAddedWhenAlreadyPresent()
        {
            kingsKB.tell("((King(x) AND Greedy(x)) => Evil(x))");
            kingsKB.tell("King(John)");
            kingsKB.tell("King(Richard)");
            kingsKB.tell("Greedy(John)");

            Assert.AreEqual(1, kingsKB.getNumberRules());
            Assert.AreEqual(3, kingsKB.getNumberFacts());

            kingsKB.tell("King(John)");
            kingsKB.tell("King(Richard)");
            kingsKB.tell("Greedy(John)");

            Assert.AreEqual(1, kingsKB.getNumberRules());
            Assert.AreEqual(3, kingsKB.getNumberFacts());

            kingsKB.tell("(((King(John))))");
            kingsKB.tell("(((King(Richard))))");
            kingsKB.tell("(((Greedy(John))))");

            Assert.AreEqual(1, kingsKB.getNumberRules());
            Assert.AreEqual(3, kingsKB.getNumberFacts());
        }
    }
}