namespace AIMA.Test.Core.Unit.Probability.Reasoning
{

    using AIMA.Core.Probability;
    using AIMA.Core.Probability.Reasoning;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class ParticleFilterTest
    {
        private HiddenMarkovModel rainman, robot;

        ParticleSet particleSet;

        Randomizer randomizer;

        [TestInitialize]
        public void setUp()
        {
            rainman = HMMFactory.createRainmanHMM();
            robot = HMMFactory.createRobotHMM();

            randomizer = new MockRandomizer(new double[] { 0.1, 0.9 });
            particleSet = new ParticleSet(rainman);
            particleSet.add(new Particle(HmmConstants.RAINING));
            particleSet.add(new Particle(HmmConstants.RAINING));
            particleSet.add(new Particle(HmmConstants.RAINING));
            particleSet.add(new Particle(HmmConstants.NOT_RAINING));
        }

        [TestMethod]
        public void testFilteringWithParticleSetsWorksForRainmanHmm()
        {
            Randomizer r = new MockRandomizer(new double[] { 0.1, 0.2, 0.3, 0.4,
				0.5, 0.6, 0.7, 0.8, 0.9 });
            ParticleSet starting = rainman.prior().toParticleSet(rainman, r, 100);
            Assert.AreEqual(56, starting
                    .numberOfParticlesWithState(HmmConstants.RAINING));
            Assert.AreEqual(44, starting
                    .numberOfParticlesWithState(HmmConstants.NOT_RAINING));

            ParticleSet afterSeeingUmbrella = starting.filter(
                    HmmConstants.SEE_UMBRELLA, r);
            Assert.AreEqual(84, afterSeeingUmbrella
                    .numberOfParticlesWithState(HmmConstants.RAINING));
            Assert.AreEqual(16, afterSeeingUmbrella
                    .numberOfParticlesWithState(HmmConstants.NOT_RAINING));

            // or alternatively
            ParticleSet afterNotSeeingUmbrella = starting.filter(
                    HmmConstants.SEE_NO_UMBRELLA, r);
            Assert.AreEqual(12, afterNotSeeingUmbrella
                    .numberOfParticlesWithState(HmmConstants.RAINING));
            Assert.AreEqual(88, afterNotSeeingUmbrella
                    .numberOfParticlesWithState(HmmConstants.NOT_RAINING));
        }

        [TestMethod]
        public void testFilteringWithParticleSetsForRobotHmm()
        {
            Randomizer r = new MockRandomizer(new double[] { 0.1, 0.2, 0.3, 0.4,
				0.5, 0.6, 0.7, 0.8, 0.9 });
            ParticleSet starting = robot.prior().toParticleSet(robot, r, 100);
            Assert.AreEqual(56, starting
                    .numberOfParticlesWithState(HmmConstants.DOOR_OPEN));
            Assert.AreEqual(44, starting
                    .numberOfParticlesWithState(HmmConstants.DOOR_CLOSED));

            // step one = robot takes no action but senses open door
            ParticleSet afterStepOne = starting.filter(HmmConstants.SEE_DOOR_OPEN,
                    r);
            Assert.AreEqual(66, afterStepOne
                    .numberOfParticlesWithState(HmmConstants.DOOR_OPEN));
            Assert.AreEqual(34, afterStepOne
                    .numberOfParticlesWithState(HmmConstants.DOOR_CLOSED));

            // step two = robot pushes the door and then senses open door
            ParticleSet afterStepTwo = starting.filter(HmmConstants.PUSH_DOOR,
                    HmmConstants.SEE_DOOR_OPEN, r);
            Assert.AreEqual(100, afterStepTwo
                    .numberOfParticlesWithState(HmmConstants.DOOR_OPEN));
            Assert.AreEqual(0, afterStepTwo
                    .numberOfParticlesWithState(HmmConstants.DOOR_CLOSED));
        }

        [TestMethod]
        public void testRandomVariableConversionToParticleSet()
        {
            RandomVariable rv = rainman.prior();
            ParticleSet ps = rv.toParticleSet(rainman, randomizer, 10);
            Assert.AreEqual(5, ps
                    .numberOfParticlesWithState(HmmConstants.RAINING));
            Assert.AreEqual(5, ps
                    .numberOfParticlesWithState(HmmConstants.NOT_RAINING));
        }

        [TestMethod]
        public void testParticleSetConversionToRandomVariable()
        {

            RandomVariable rv = particleSet.toRandomVariable();
            Assert.AreEqual(0.75, rv.getProbabilityOf(HmmConstants.RAINING),
                    0.001);
            Assert.AreEqual(0.25,
                    rv.getProbabilityOf(HmmConstants.NOT_RAINING), 0.001);
        }

        [TestMethod]
        public void testRoundTripConversion()
        {
            RandomVariable rv = particleSet.toRandomVariable();
            Randomizer r = new MockRandomizer(new double[] { 0.1, 0.2, 0.3, 0.4,
				0.9 });
            ParticleSet ps2 = rv.toParticleSet(rainman, r, 10);
            Assert.AreEqual(8, ps2
                    .numberOfParticlesWithState(HmmConstants.RAINING));
            Assert.AreEqual(2, ps2
                    .numberOfParticlesWithState(HmmConstants.NOT_RAINING));
        }

        [TestMethod]
        public void testTransitionModelGeneratesNewStateWhenGivenOldStateAndProbability()
        {

            TransitionModel tm = rainman.transitionModel();
            String oldState = HmmConstants.RAINING;
            String state1 = tm.getStateForProbability(oldState, randomizer
                    .nextDouble());
            String state2 = tm.getStateForProbability(oldState, randomizer
                    .nextDouble());
            Assert.AreEqual(state1, HmmConstants.RAINING);
            Assert.AreEqual(state2, HmmConstants.NOT_RAINING);
        }

        [TestMethod]
        public void testParticleSetForPredictedStateGeneratedFromOldStateParticleSet()
        {
            Randomizer r = new MockRandomizer(new double[] { 0.1, 0.2, 0.3, 0.4,
				0.5, 0.6, 0.7, 0.8, 0.9 });
            ParticleSet ps = rainman.prior().toParticleSet(rainman, r, 10);
            Assert.AreEqual(6, ps
                    .numberOfParticlesWithState(HmmConstants.RAINING));
            Assert.AreEqual(4, ps
                    .numberOfParticlesWithState(HmmConstants.NOT_RAINING));

            ParticleSet nps = ps.generateParticleSetForPredictedState(r);
            Assert.AreEqual(7, nps
                    .numberOfParticlesWithState(HmmConstants.RAINING));
            Assert.AreEqual(3, nps
                    .numberOfParticlesWithState(HmmConstants.NOT_RAINING));
        }

        [TestMethod]
        public void testParticleSetForPerceptionUpdatedStateGeneratedFromPredictedStateParticleSetGivenPerception()
        {
            Randomizer r = new MockRandomizer(new double[] { 0.1, 0.2, 0.3, 0.4,
				0.5, 0.6, 0.7, 0.8, 0.9 });
            ParticleSet starting = rainman.prior().toParticleSet(rainman, r, 10);
            ParticleSet predicted = starting
                    .generateParticleSetForPredictedState(r);

            ParticleSet updatedWithPerceptionOfUmbrella = predicted
                    .perceptionUpdate(HmmConstants.SEE_UMBRELLA, r);
            Assert.AreEqual(9, updatedWithPerceptionOfUmbrella
                    .numberOfParticlesWithState(HmmConstants.RAINING));
            Assert.AreEqual(1, updatedWithPerceptionOfUmbrella
                    .numberOfParticlesWithState(HmmConstants.NOT_RAINING));

            ParticleSet updatedWithPerceptionOfNoUmbrella = predicted
                    .perceptionUpdate(HmmConstants.SEE_NO_UMBRELLA, r);
            Assert.AreEqual(2, updatedWithPerceptionOfNoUmbrella
                    .numberOfParticlesWithState(HmmConstants.RAINING));
            Assert.AreEqual(8, updatedWithPerceptionOfNoUmbrella
                    .numberOfParticlesWithState(HmmConstants.NOT_RAINING));
        }
    }
}