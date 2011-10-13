namespace AIMA.Test.Core.Unit.Logic.Fol.Inference
{

    using AIMA.Core.Logic.FOL.Inference;
    using AIMA.Test.Core.Unit.Logic.FOL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    [TestClass]
    public class FOLModelEliminationTest : CommonFOLInferenceProcedureTests
    {

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryCriminalXFalse()
        {
            testDefiniteClauseKBKingsQueryCriminalXFalse(new FOLModelElimination());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryRichardEvilFalse()
        {
            testDefiniteClauseKBKingsQueryRichardEvilFalse(new FOLModelElimination());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryJohnEvilSucceeds()
        {
            testDefiniteClauseKBKingsQueryJohnEvilSucceeds(new FOLModelElimination());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryEvilXReturnsJohnSucceeds()
        {
            testDefiniteClauseKBKingsQueryEvilXReturnsJohnSucceeds(new FOLModelElimination());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryKingXReturnsJohnAndRichardSucceeds()
        {
            testDefiniteClauseKBKingsQueryKingXReturnsJohnAndRichardSucceeds(new FOLModelElimination());
        }

        [TestMethod]
        public void testDefiniteClauseKBWeaponsQueryCriminalXReturnsWestSucceeds()
        {
            testDefiniteClauseKBWeaponsQueryCriminalXReturnsWestSucceeds(new FOLModelElimination());
        }

        [TestMethod]
        public void testHornClauseKBRingOfThievesQuerySkisXReturnsNancyRedBertDrew()
        {
            // This KB ends up being infinite when resolving, however 2
            // seconds is more than enough to extract the 4 answers
            // that are expected
            testHornClauseKBRingOfThievesQuerySkisXReturnsNancyRedBertDrew(new FOLModelElimination(
                    2 * 1000));
        }

        [TestMethod]
        public void testFullFOLKBLovesAnimalQueryKillsCuriosityTunaSucceeds()
        {
            testFullFOLKBLovesAnimalQueryKillsCuriosityTunaSucceeds(
                    new FOLModelElimination(), false);
        }

        [TestMethod]
        public void testFullFOLKBLovesAnimalQueryNotKillsJackTunaSucceeds()
        {
            testFullFOLKBLovesAnimalQueryNotKillsJackTunaSucceeds(
                    new FOLModelElimination(), false);
        }

        [TestMethod]
        public void testFullFOLKBLovesAnimalQueryKillsJackTunaFalse()
        {
            // Note: While the KB expands infinitely, the answer
            // search for this bottoms out indicating the
            // KB does not entail the fact.
            testFullFOLKBLovesAnimalQueryKillsJackTunaFalse(
                    new FOLModelElimination(), false);
        }

        [TestMethod]
        public void testEqualityAxiomsKBabcAEqualsCSucceeds()
        {
            testEqualityAxiomsKBabcAEqualsCSucceeds(new FOLModelElimination());
        }

        [TestMethod]
        public void testEqualityAndSubstitutionAxiomsKBabcdFFASucceeds()
        {
            testEqualityAndSubstitutionAxiomsKBabcdFFASucceeds(new FOLModelElimination());
        }

        [TestMethod]
        public void testEqualityAndSubstitutionAxiomsKBabcdPDSucceeds()
        {
            testEqualityAndSubstitutionAxiomsKBabcdPDSucceeds(new FOLModelElimination());
        }

        [TestMethod]
        public void testEqualityAndSubstitutionAxiomsKBabcdPFFASucceeds()
        {
            testEqualityAndSubstitutionAxiomsKBabcdPFFASucceeds(
                    new FOLModelElimination(), false);
        }

        [TestMethod]
        public void testEqualityNoAxiomsKBabcAEqualsCSucceeds()
        {
            testEqualityNoAxiomsKBabcAEqualsCSucceeds(new FOLModelElimination(),
                    true);
        }

        [TestMethod]
        public void testEqualityAndSubstitutionNoAxiomsKBabcdFFASucceeds()
        {
            testEqualityAndSubstitutionNoAxiomsKBabcdFFASucceeds(
                    new FOLModelElimination(), true);
        }

        [TestMethod]
        public void testEqualityAndSubstitutionNoAxiomsKBabcdPDSucceeds()
        {
            testEqualityAndSubstitutionNoAxiomsKBabcdPDSucceeds(
                    new FOLModelElimination(), true);
        }

        [TestMethod]
        public void testEqualityAndSubstitutionNoAxiomsKBabcdPFFASucceeds()
        {
            testEqualityAndSubstitutionNoAxiomsKBabcdPFFASucceeds(
                    new FOLModelElimination(), true);
        }
    }
}