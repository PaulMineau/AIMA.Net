namespace aima.test.core.unit.agent.impl.aprog.simplerule;

using org.junit.Assert;
using org.junit.Test;

using AIMA.Core.Agent.Action;
using AIMA.Core.Agent.Impl.DynamicAction;
using AIMA.Core.Agent.Impl.DynamicPercept;
using AIMA.Core.Agent.Impl.AProg.SimpleRule.ANDCondition;
using AIMA.Core.Agent.Impl.AProg.SimpleRule.EQUALCondition;
using AIMA.Core.Agent.Impl.AProg.SimpleRule.NOTCondition;
using AIMA.Core.Agent.Impl.AProg.SimpleRule.ORCondition;
using AIMA.Core.Agent.Impl.AProg.SimpleRule.Rule;

/**
 * @author Ciaran O'Reilly
 * 
 */
public class RuleTest {

	private const Action ACTION_INITIATE_BRAKING = new DynamicAction(
			"initiate-braking");
	private const Action ACTION_EMERGENCY_BRAKING = new DynamicAction(
			"emergency-braking");
	//
	private const String ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING = "car-in-front-is-braking";
	private const String ATTRIBUTE_CAR_IN_FRONT_IS_INDICATING = "car-in-front-is-indicating";
	private const String ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING = "car-in-front-tires-smoking";

	@Test
	public void testEQUALRule() {
		Rule r = new Rule(new EQUALCondition(ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING,
				true), ACTION_INITIATE_BRAKING);

		Assert.assertEquals(ACTION_INITIATE_BRAKING, r.getAction());

		Assert
				.assertEquals(
						"if car-in-front-is-braking==true then Action[name==initiate-braking].",
						r.ToString());

		Assert.assertEquals(true, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true)));

		Assert.assertEquals(false, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, false)));

		Assert.assertEquals(false, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_INDICATING, true)));
	}

	@Test
	public void testNOTRule() {
		Rule r = new Rule(new NOTCondition(new EQUALCondition(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true)),
				ACTION_INITIATE_BRAKING);

		Assert.assertEquals(ACTION_INITIATE_BRAKING, r.getAction());

		Assert
				.assertEquals(
						"if ![car-in-front-is-braking==true] then Action[name==initiate-braking].",
						r.ToString());

		Assert.assertEquals(false, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true)));

		Assert.assertEquals(true, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, false)));

		Assert.assertEquals(true, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_INDICATING, true)));
	}

	@Test
	public void testANDRule() {
		Rule r = new Rule(new ANDCondition(new EQUALCondition(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true), new EQUALCondition(
				ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)),
				ACTION_EMERGENCY_BRAKING);

		Assert.assertEquals(ACTION_EMERGENCY_BRAKING, r.getAction());

		Assert
				.assertEquals(
						"if [car-in-front-is-braking==true && car-in-front-tires-smoking==true] then Action[name==emergency-braking].",
						r.ToString());

		Assert.assertEquals(false, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true)));

		Assert.assertEquals(false, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

		Assert.assertEquals(true, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true,
				ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

		Assert.assertEquals(false, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, false,
				ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

		Assert.assertEquals(false, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true,
				ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, false)));
	}

	@Test
	public void testORRule() {
		Rule r = new Rule(new ORCondition(new EQUALCondition(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true), new EQUALCondition(
				ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)),
				ACTION_EMERGENCY_BRAKING);

		Assert.assertEquals(ACTION_EMERGENCY_BRAKING, r.getAction());

		Assert
				.assertEquals(
						"if [car-in-front-is-braking==true || car-in-front-tires-smoking==true] then Action[name==emergency-braking].",
						r.ToString());

		Assert.assertEquals(true, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true)));

		Assert.assertEquals(true, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

		Assert.assertEquals(true, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true,
				ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

		Assert.assertEquals(true, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, false,
				ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, true)));

		Assert.assertEquals(true, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, true,
				ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, false)));

		Assert.assertEquals(false, r.evaluate(new DynamicPercept(
				ATTRIBUTE_CAR_IN_FRONT_IS_BRAKING, false,
				ATTRIBUTE_CAR_IN_FRONT_TIRES_SMOKING, false)));
	}
}