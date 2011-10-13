namespace AIMA.Test.Core.Unit.Logic.Propositional.Algorithms
{

    using AIMA.Core.Logic.Propositional.Algorithms;
    using AIMA.Core.Logic.Propositional.Parsing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class PLFCEntailsTest
    {
        PLFCEntails plfce;

        [TestInitialize]
        public void setUp()
        {
            plfce = new PLFCEntails();
        }

        [TestMethod]
        public void testAIMAExample()
        {
            KnowledgeBase kb = new KnowledgeBase();
            kb.tell(" (P => Q)");
            kb.tell("((L AND M) => P)");
            kb.tell("((B AND L) => M)");
            kb.tell("( (A AND P) => L)");
            kb.tell("((A AND B) => L)");
            kb.tell("(A)");
            kb.tell("(B)");

            Assert.AreEqual(true, plfce.plfcEntails(kb, "Q"));
        }
    }
}