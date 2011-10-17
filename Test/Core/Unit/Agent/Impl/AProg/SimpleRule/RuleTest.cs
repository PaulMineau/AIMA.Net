namespace aima.test.core.unit.agent.impl.aprog.simplerule
{



    using AIMA.Core.Agent;
    using AIMA.Core.Agent.Impl;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AIMA.Core.Agent.Impl.AProg.SimpleRule;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    [TestClass]
    public class RuleTest
    {

        private readonly static Action ACTION_INITIATE_BRAKING = new DynamicAction(
                "initiate-braking");
        private readonly static Action ACTION_EMERGENCY_BRAKING = new DynamicAction(
                "emergency-braking");
        //
        private const System.String ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING = "car-in-front-is-braking";
        private const System.String ATTRIBUTE_CAR_IN_FRONT_IS_INDICATING = "car-in-front-is-indicating";
        private const System.String ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING = "car-in-front-tires-smoking";

        [TestMethod]
        public void testEQUALRule()
        {
            Rule r = new Rule(new EQUALCondition(ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING,
                    true), ACTION_INITIATE_BRAKING);

            Assert.Equals(ACTION_INITIATE_BRAKING, r.getAction());

            Assert
                    .Equals(
                            "if car-in-front-is-braking==true then Action[name==initiate-braking].",
                            r.ToString());

            Assert.Equals(true, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true)));

            Assert.Equals(false, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, false)));

            Assert.Equals(false, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_INDICATING, true)));
        }

        [TestMethod]
        public void testNOTRule()
        {
            Rule r = new Rule(new NOTCondition(new EQUALCondition(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true)),
                    ACTION_INITIATE_BRAKING);

            Assert.Equals(ACTION_INITIATE_BRAKING, r.getAction());

            Assert.Equals(
                            "if ![car-in-front-is-braking==true] then Action[name==initiate-braking].",
                            r.ToString());

            Assert.Equals(false, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true)));

            Assert.Equals(true, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, false)));

            Assert.Equals(true, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_INDICATING, true)));
        }

        [TestMethod]
        public void testANDRule()
        {
            Rule r = new Rule(new ANDCondition(new EQUALCondition(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true), new EQUALCondition(
                    ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)),
                    ACTION_EMERGENCY_BRAKING);

            Assert.Equals(ACTION_EMERGENCY_BRAKING, r.getAction());

            Assert
                    .Equals(
                            "if [car-in-front-is-braking==true && car-in-front-tires-smoking==true] then Action[name==emergency-braking].",
                            r.ToString());

            Assert.Equals(false, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true)));

            Assert.Equals(false, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

            Assert.Equals(true, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true,
                    ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

            Assert.Equals(false, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, false,
                    ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

            Assert.Equals(false, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true,
                    ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, false)));
        }

        [TestMethod]
        public void testORRule()
        {
            Rule r = new Rule(new ORCondition(new EQUALCondition(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true), new EQUALCondition(
                    ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)),
                    ACTION_EMERGENCY_BRAKING);

            Assert.Equals(ACTION_EMERGENCY_BRAKING, r.getAction());

            Assert
                    .Equals(
                            "if [car-in-front-is-braking==true || car-in-front-tires-smoking==true] then Action[name==emergency-braking].",
                            r.ToString());

            Assert.Equals(true, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true)));

            Assert.Equals(true, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

            Assert.Equals(true, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true,
                    ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

            Assert.Equals(true, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, false,
                    ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

            Assert.Equals(true, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true,
                    ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, false)));

            Assert.Equals(false, r.evaluate(new DynamicPercept(
                    ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, false,
                    ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, false)));
        }
    }
}