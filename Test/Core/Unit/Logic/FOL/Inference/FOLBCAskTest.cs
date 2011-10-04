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
    public class FOLBCAskTest : CommonFOLInferenceProcedureTests
    {

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryCriminalXFalse()
        {
            testDefiniteClauseKBKingsQueryCriminalXFalse(new FOLBCAsk());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryRichardEvilFalse()
        {
            testDefiniteClauseKBKingsQueryRichardEvilFalse(new FOLBCAsk());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryJohnEvilSucceeds()
        {
            testDefiniteClauseKBKingsQueryJohnEvilSucceeds(new FOLBCAsk());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryEvilXReturnsJohnSucceeds()
        {
            testDefiniteClauseKBKingsQueryEvilXReturnsJohnSucceeds(new FOLBCAsk());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryKingXReturnsJohnAndRichardSucceeds()
        {
            testDefiniteClauseKBKingsQueryKingXReturnsJohnAndRichardSucceeds(new FOLBCAsk());
        }

        [TestMethod]
        public void testDefiniteClauseKBWeaponsQueryCriminalXReturnsWestSucceeds()
        {
            testDefiniteClauseKBWeaponsQueryCriminalXReturnsWestSucceeds(new FOLBCAsk());
        }
    }
}