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
    public class FOLTFMResolutionTest : CommonFOLInferenceProcedureTests
    {

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryCriminalXFalse()
        {
            testDefiniteClauseKBKingsQueryCriminalXFalse(new FOLTFMResolution());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryRichardEvilFalse()
        {
            testDefiniteClauseKBKingsQueryRichardEvilFalse(new FOLTFMResolution());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryJohnEvilSucceeds()
        {
            testDefiniteClauseKBKingsQueryJohnEvilSucceeds(new FOLTFMResolution());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryEvilXReturnsJohnSucceeds()
        {
            testDefiniteClauseKBKingsQueryEvilXReturnsJohnSucceeds(new FOLTFMResolution());
        }

        [TestMethod]
        public void testDefiniteClauseKBKingsQueryKingXReturnsJohnAndRichardSucceeds()
        {
            testDefiniteClauseKBKingsQueryKingXReturnsJohnAndRichardSucceeds(new FOLTFMResolution());
        }

        [TestMethod]
        public void testDefiniteClauseKBWeaponsQueryCriminalXReturnsWestSucceeds()
        {
            testDefiniteClauseKBWeaponsQueryCriminalXReturnsWestSucceeds(new FOLTFMResolution());
        }

        [TestMethod]
        public void testHornClauseKBRingOfThievesQuerySkisXReturnsNancyRedBertDrew()
        {
            // The clauses in this KB can keep creating resolvents infinitely,
            // therefore give it 20 seconds to find the 4 answers to this, should
            // be more than enough.
            testHornClauseKBRingOfThievesQuerySkisXReturnsNancyRedBertDrew(new FOLTFMResolution(
                    40 * 1000));
        }

        [TestMethod]
        public void testFullFOLKBLovesAnimalQueryKillsCuriosityTunaSucceeds()
        {
            // 10 seconds should be more than plenty for this query to finish.
            testFullFOLKBLovesAnimalQueryKillsCuriosityTunaSucceeds(
                    new FOLTFMResolution(10 * 1000), false);
        }

        [TestMethod]
        public void testFullFOLKBLovesAnimalQueryNotKillsJackTunaSucceeds()
        {
            // 10 seconds should be more than plenty for this query to finish.
            testFullFOLKBLovesAnimalQueryNotKillsJackTunaSucceeds(
                    new FOLTFMResolution(10 * 1000), false);
        }

        [TestMethod]
        public void testFullFOLKBLovesAnimalQueryKillsJackTunaFalse()
        {
            // This query will not return using TFM as keep expanding
            // clauses through resolution for this KB.
            testFullFOLKBLovesAnimalQueryKillsJackTunaFalse(new FOLTFMResolution(
                    10 * 1000), true);
        }

        [TestMethod]
        public void testEqualityAxiomsKBabcAEqualsCSucceeds()
        {
            testEqualityAxiomsKBabcAEqualsCSucceeds(new FOLTFMResolution(10 * 1000));
        }

        [TestMethod]
        public void testEqualityAndSubstitutionAxiomsKBabcdFFASucceeds()
        {
            testEqualityAndSubstitutionAxiomsKBabcdFFASucceeds(new FOLTFMResolution(
                    40 * 1000));
        }

        // Note: Requires VM arguments to be:
        // -Xms256m -Xmx1024m
        // due to the amount of memory it uses.
        // Therefore, ignore by default as people don't normally set this.
        [TestMethod]
        public void testEqualityAndSubstitutionAxiomsKBabcdPDSucceeds()
        {
            testEqualityAndSubstitutionAxiomsKBabcdPDSucceeds(new FOLTFMResolution(
                    10 * 1000));
        }

        [TestMethod]
        public void testEqualityAndSubstitutionAxiomsKBabcdPFFASucceeds()
        {
            // TFM is unable to find the correct answer to this in a reasonable
            // amount of time for a JUnit test.
            testEqualityAndSubstitutionAxiomsKBabcdPFFASucceeds(
                    new FOLTFMResolution(10 * 1000), true);
        }

        [TestMethod]
        public void testEqualityNoAxiomsKBabcAEqualsCSucceeds()
        {
            testEqualityNoAxiomsKBabcAEqualsCSucceeds(new FOLTFMResolution(
                    10 * 1000), true);
        }

        [TestMethod]
        public void testEqualityAndSubstitutionNoAxiomsKBabcdFFASucceeds()
        {
            testEqualityAndSubstitutionNoAxiomsKBabcdFFASucceeds(
                    new FOLTFMResolution(10 * 1000), true);
        }

        [TestMethod]
        public void testEqualityAndSubstitutionNoAxiomsKBabcdPDSucceeds()
        {
            testEqualityAndSubstitutionNoAxiomsKBabcdPDSucceeds(
                    new FOLTFMResolution(10 * 1000), true);
        }

        [TestMethod]
        public void testEqualityAndSubstitutionNoAxiomsKBabcdPFFASucceeds()
        {
            testEqualityAndSubstitutionNoAxiomsKBabcdPFFASucceeds(
                    new FOLTFMResolution(10 * 1000), true);
        }
    }
}