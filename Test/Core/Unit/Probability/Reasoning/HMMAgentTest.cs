namespace AIMA.Test.Core.Unit.Probability.Reasoning
{

    using AIMA.Core.Probability.Reasoning;
    using Microsoft.VisualStudio.TestTools.UnitTesting;


    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class HMMAgentTest
    {
        private const double TOLERANCE = 0.001;

        private HMMAgent robot, rainman;

        [TestInitialize]
        public void setUp()
        {

            robot = new HMMAgent(HMMFactory.createRobotHMM());
            rainman = new HMMAgent(HMMFactory.createRainmanHMM());
        }

        [TestMethod]
        public void testRobotInitialization()
        {
            Assert.AreEqual(0.5, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_OPEN), 0.001);
            Assert.AreEqual(0.5, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_CLOSED), 0.001);
        }

        [TestMethod]
        public void testRobotHMMPredictionAndMeasurementUpdateStepsModifyBeliefCorrectly()
        {

            Assert.AreEqual(0.5, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_OPEN), 0.001);
            Assert.AreEqual(0.5, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_CLOSED), 0.001);

            robot.act(HmmConstants.DO_NOTHING);
            Assert.AreEqual(0.5, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_OPEN), 0.001);
            Assert.AreEqual(0.5, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_CLOSED), 0.001);

            robot.perceive(HmmConstants.SEE_DOOR_OPEN);
            Assert.AreEqual(0.75, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_OPEN), TOLERANCE);
            Assert.AreEqual(0.25, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_CLOSED), TOLERANCE);

            robot.act(HmmConstants.PUSH_DOOR);
            Assert.AreEqual(0.95, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_OPEN), 0.001);
            Assert.AreEqual(0.05, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_CLOSED), 0.001);

            robot.perceive(HmmConstants.SEE_DOOR_OPEN);
            Assert.AreEqual(0.983, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_OPEN), TOLERANCE);
            Assert.AreEqual(0.017, robot.belief().getProbabilityOf(
                    HmmConstants.DOOR_CLOSED), TOLERANCE);
        }

        [TestMethod]
        public void testRainmanInitialization()
        {
            Assert.AreEqual(0.5, rainman.belief().getProbabilityOf(
                    HmmConstants.RAINING), 0.001);
            Assert.AreEqual(0.5, rainman.belief().getProbabilityOf(
                    HmmConstants.NOT_RAINING), 0.001);
        }

        [TestMethod]
        public void testRainmanHMMPredictionAndMeasurementUpdateStepsModifyBeliefCorrectly()
        {
            Assert.AreEqual(0.5, rainman.belief().getProbabilityOf(
                    HmmConstants.RAINING), 0.001);
            Assert.AreEqual(0.5, rainman.belief().getProbabilityOf(
                    HmmConstants.NOT_RAINING), 0.001);

            rainman.waitWithoutActing();
            Assert.AreEqual(0.5, rainman.belief().getProbabilityOf(
                    HmmConstants.RAINING), 0.001);
            Assert.AreEqual(0.5, rainman.belief().getProbabilityOf(
                    HmmConstants.NOT_RAINING), 0.001);

            rainman.perceive(HmmConstants.SEE_UMBRELLA);
            Assert.AreEqual(0.818, rainman.belief().getProbabilityOf(
                    HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.182, rainman.belief().getProbabilityOf(
                    HmmConstants.NOT_RAINING), TOLERANCE);

            rainman.waitWithoutActing();
            Assert.AreEqual(0.627, rainman.belief().getProbabilityOf(
                    HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.373, rainman.belief().getProbabilityOf(
                    HmmConstants.NOT_RAINING), TOLERANCE);

            rainman.perceive(HmmConstants.SEE_UMBRELLA);
            Assert.AreEqual(0.883, rainman.belief().getProbabilityOf(
                    HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.117, rainman.belief().getProbabilityOf(
                    HmmConstants.NOT_RAINING), TOLERANCE);
        }
    }
}