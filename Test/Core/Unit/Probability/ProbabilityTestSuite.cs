namespace aima.test.core.unit.probability;

using org.junit.runner.RunWith;
using org.junit.runners.Suite;

using AIMA.Test.Core.Unit.Probability.Decision.PolicyIterationTest;
using AIMA.Test.Core.Unit.Probability.Decision.ValueIterationTest;
using aima.test.core.unit.probability.reasoning.HMMAgentTest;
using aima.test.core.unit.probability.reasoning.HMMTest;
using aima.test.core.unit.probability.reasoning.ParticleFilterTest;
using aima.test.core.unit.probability.reasoning.RandomVariableTest;

@RunWith(Suite.class)
@Suite.SuiteClasses( { PolicyIterationTest.class, ValueIterationTest.class,
		HMMAgentTest.class, HMMTest.class, ParticleFilterTest.class,
		RandomVariableTest.class, BayesNetNodeTest.class, BayesNetTest.class,
		EnumerationAskTest.class, EnumerationJointAskTest.class,
		ProbabilitySamplingTest.class })
public class ProbabilityTestSuite {

}
