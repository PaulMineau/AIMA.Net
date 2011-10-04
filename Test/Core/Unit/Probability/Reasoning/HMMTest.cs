namespace CosmicFlow.AIMA.Test.Core.Unit.Probability.Reasoning
{

    using CosmicFlow.AIMA.Core.Probability;
    using CosmicFlow.AIMA.Core.Probability.Reasoning;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class HMMTest
    {
        private HiddenMarkovModel robotHmm, rainmanHmm;

        private const double TOLERANCE = 0.001;

        [TestInitialize]
        public void setUp()
        {
            robotHmm = HMMFactory.createRobotHMM();
            rainmanHmm = HMMFactory.createRainmanHMM();
        }

        [TestMethod]
        public void testRobotHMMInitialization()
        {

            Assert.AreEqual(0.5, robotHmm.prior().getProbabilityOf(
                    HmmConstants.DOOR_OPEN), 0.001);
            Assert.AreEqual(0.5, robotHmm.prior().getProbabilityOf(
                    HmmConstants.DOOR_CLOSED), 0.001);
        }

        [TestMethod]
        public void testRainmanHmmInitialization()
        {

            Assert.AreEqual(0.5, rainmanHmm.prior().getProbabilityOf(
                    HmmConstants.RAINING), 0.001);
            Assert.AreEqual(0.5, rainmanHmm.prior().getProbabilityOf(
                    HmmConstants.NOT_RAINING), 0.001);
        }

        [TestMethod]
        public void testForwardMessagingWorksForFiltering()
        {
            RandomVariable afterOneStep = robotHmm.forward(robotHmm.prior(),
                    HmmConstants.DO_NOTHING, HmmConstants.SEE_DOOR_OPEN);
            Assert.AreEqual(0.75, afterOneStep
                    .getProbabilityOf(HmmConstants.DOOR_OPEN), TOLERANCE);
            Assert.AreEqual(0.25, afterOneStep
                    .getProbabilityOf(HmmConstants.DOOR_CLOSED), TOLERANCE);

            RandomVariable afterTwoSteps = robotHmm.forward(afterOneStep,
                    HmmConstants.PUSH_DOOR, HmmConstants.SEE_DOOR_OPEN);
            Assert.AreEqual(0.983, afterTwoSteps
                    .getProbabilityOf(HmmConstants.DOOR_OPEN), TOLERANCE);
            Assert.AreEqual(0.017, afterTwoSteps
                    .getProbabilityOf(HmmConstants.DOOR_CLOSED), TOLERANCE);
        }

        [TestMethod]
        public void testRecursiveBackwardMessageCalculationIsCorrect()
        {
            RandomVariable afterOneStep = rainmanHmm.forward(rainmanHmm.prior(),
                    HmmConstants.DO_NOTHING, HmmConstants.SEE_UMBRELLA);
            RandomVariable afterTwoSteps = rainmanHmm.forward(afterOneStep,
                    HmmConstants.DO_NOTHING, HmmConstants.SEE_UMBRELLA);

            RandomVariable postSequence = afterTwoSteps.duplicate()
                    .createUnitBelief();

            RandomVariable smoothed = rainmanHmm.calculate_next_backward_message(
                    afterOneStep, postSequence, HmmConstants.SEE_UMBRELLA);
            Assert.AreEqual(0.883, smoothed
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.117, smoothed
                    .getProbabilityOf(HmmConstants.NOT_RAINING), TOLERANCE);
        }

        [TestMethod]
        public void testForwardBackwardOnRainmanHmm()
        {
            List<String> perceptions = new List<String>();
            perceptions.Add(HmmConstants.SEE_UMBRELLA);
            perceptions.Add(HmmConstants.SEE_UMBRELLA);

            List<RandomVariable> results = rainmanHmm.forward_backward(perceptions);
            Assert.AreEqual(3, results.Count);

            Assert.IsNull(results[0]);
            RandomVariable smoothedDayOne = results[1];
            Assert.AreEqual(0.982, smoothedDayOne
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.018, smoothedDayOne
                    .getProbabilityOf(HmmConstants.NOT_RAINING), TOLERANCE);

            RandomVariable smoothedDayTwo = results[2];
            Assert.AreEqual(0.883, smoothedDayTwo
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.117, smoothedDayTwo
                    .getProbabilityOf(HmmConstants.NOT_RAINING), TOLERANCE);
        }

        [TestMethod]
        public void testForwardBackwardOnRainmanHmmFor3daysData()
        {
            List<String> perceptions = new List<String>();
            perceptions.Add(HmmConstants.SEE_UMBRELLA);
            perceptions.Add(HmmConstants.SEE_UMBRELLA);
            perceptions.Add(HmmConstants.SEE_NO_UMBRELLA);

            List<RandomVariable> results = rainmanHmm.forward_backward(perceptions);
            Assert.AreEqual(4, results.Count);
            Assert.IsNull(results[0]);

            RandomVariable smoothedDayOne = results[1];
            Assert.AreEqual(0.964, smoothedDayOne
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.036, smoothedDayOne
                    .getProbabilityOf(HmmConstants.NOT_RAINING), TOLERANCE);

            RandomVariable smoothedDayTwo = results[2];
            Assert.AreEqual(0.484, smoothedDayTwo
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.516, smoothedDayTwo
                    .getProbabilityOf(HmmConstants.NOT_RAINING), TOLERANCE);

            RandomVariable smoothedDayThree = results[3];
            Assert.AreEqual(0.190, smoothedDayThree
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.810, smoothedDayThree
                    .getProbabilityOf(HmmConstants.NOT_RAINING), TOLERANCE);
        }

        [TestMethod]
        public void testForwardBackwardAndFixedLagSmoothingGiveSameResults()
        {
            List<String> perceptions = new List<String>();

            String dayOnePerception = HmmConstants.SEE_UMBRELLA;
            String dayTwoPerception = HmmConstants.SEE_UMBRELLA;
            String dayThreePerception = HmmConstants.SEE_NO_UMBRELLA;

            perceptions.Add(dayOnePerception);
            perceptions.Add(dayTwoPerception);
            perceptions.Add(dayThreePerception);

            List<RandomVariable> fbResults = rainmanHmm
                    .forward_backward(perceptions);
            Assert.AreEqual(4, fbResults.Count);

            // RandomVariable fbDayOneResult = fbResults.get(1);
            // System.Console.WriteLine(fbDayOneResult);

            FixedLagSmoothing fls = new FixedLagSmoothing(rainmanHmm, 2);

            Assert.IsNull(fls.smooth(dayOnePerception));
            // System.Console.WriteLine(fls.smooth(dayTwoPerception));
            // RandomVariable flsDayoneResult = fls.smooth(dayThreePerception);
            // System.Console.WriteLine(flsDayoneResult);
        }

        [TestMethod]
        public void testOneStepFixedLagSmoothingOnRainManHmm()
        {
            FixedLagSmoothing fls = new FixedLagSmoothing(rainmanHmm, 1);

            RandomVariable smoothedDayZero = fls.smooth(HmmConstants.SEE_UMBRELLA); // see
            // umbrella on day one
            Assert.AreEqual(0.627, smoothedDayZero
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);

            RandomVariable smoothedDayOne = fls.smooth(HmmConstants.SEE_UMBRELLA); // see
            // umbrella on day two
            Assert.AreEqual(0.883, smoothedDayOne
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.117, smoothedDayOne
                    .getProbabilityOf(HmmConstants.NOT_RAINING), TOLERANCE);

            RandomVariable smoothedDayTwo = fls
                    .smooth(HmmConstants.SEE_NO_UMBRELLA); // see no umbrella on
            // day three
            Assert.AreEqual(0.799, smoothedDayTwo
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.201, smoothedDayTwo
                    .getProbabilityOf(HmmConstants.NOT_RAINING), TOLERANCE);
        }

        [TestMethod]
        public void testOneStepFixedLagSmoothingOnRainManHmmWithDifferingEvidence()
        {
            FixedLagSmoothing fls = new FixedLagSmoothing(rainmanHmm, 1);

            RandomVariable smoothedDayZero = fls.smooth(HmmConstants.SEE_UMBRELLA);// see
            // umbrella on day one
            Assert.AreEqual(0.627, smoothedDayZero
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);

            RandomVariable smoothedDayOne = fls
                    .smooth(HmmConstants.SEE_NO_UMBRELLA);// no umbrella on day
            // two
            Assert.AreEqual(0.702, smoothedDayOne
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.297, smoothedDayOne
                    .getProbabilityOf(HmmConstants.NOT_RAINING), TOLERANCE);
        }

        [TestMethod]
        public void testTwoStepFixedLagSmoothingOnRainManHmm()
        {
            FixedLagSmoothing fls = new FixedLagSmoothing(rainmanHmm, 2);

            RandomVariable smoothedOne = fls.smooth(HmmConstants.SEE_UMBRELLA); // see
            // umbrella on day one
            Assert.IsNull(smoothedOne);

            smoothedOne = fls.smooth(HmmConstants.SEE_UMBRELLA); // see
            // umbrella on day two
            Assert.AreEqual(0.653, smoothedOne
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.346, smoothedOne
                    .getProbabilityOf(HmmConstants.NOT_RAINING), TOLERANCE);

            RandomVariable smoothedTwo = fls.smooth(HmmConstants.SEE_UMBRELLA);// see
            // umbrella on day 3
            Assert.AreEqual(0.894, smoothedTwo
                    .getProbabilityOf(HmmConstants.RAINING), TOLERANCE);
            Assert.AreEqual(0.105, smoothedTwo
                    .getProbabilityOf(HmmConstants.NOT_RAINING), TOLERANCE);
        }
    }
}