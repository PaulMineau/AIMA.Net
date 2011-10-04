namespace CosmicFlow.AIMA.Test.Core.Unit.Logic.Fol.Inference
{

    using CosmicFlow.AIMA.Core.Logic.FOL.Inference;
    using CosmicFlow.AIMA.Test.Core.Unit.Logic.FOL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    [TestClass]
    public class FOLFCAskTest : CommonFOLInferenceProcedureTests
    {

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryCriminalXFalse()
        {
            testDefiniteClauseKBKingsQueryCriminalXFalse(new FOLFCAsk());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryRichardEvilFalse()
        {
            testDefiniteClauseKBKingsQueryRichardEvilFalse(new FOLFCAsk());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryJohnEvilSucceeds()
        {
            testDefiniteClauseKBKingsQueryJohnEvilSucceeds(new FOLFCAsk());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryEvilXReturnsJohnSucceeds()
        {
            testDefiniteClauseKBKingsQueryEvilXReturnsJohnSucceeds(new FOLFCAsk());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryKingXReturnsJohnAndRichardSucceeds()
        {
            testDefiniteClauseKBKingsQueryKingXReturnsJohnAndRichardSucceeds(new FOLFCAsk());
        }

        [TestMethod]
        public void testDefiniteClauseKBWeaponsQueryCriminalXReturnsWestSucceeds()
        {
            testDefiniteClauseKBWeaponsQueryCriminalXReturnsWestSucceeds(new FOLFCAsk());
        }
    }
}