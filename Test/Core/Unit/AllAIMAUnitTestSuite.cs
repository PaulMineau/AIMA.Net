namespace aima.test.core.unit;

using org.junit.runner.RunWith;
using org.junit.runners.Suite;

using aima.test.core.unit.agent.AgentTestSuite;
using aima.test.core.unit.environment.EnvironmentTestSuite;
using aima.test.core.unit.learning.LearningTestSuite;
using aima.test.core.unit.logic.LogicTestSuite;
using aima.test.core.unit.probability.ProbabilityTestSuite;
using aima.test.core.unit.search.SearchTestSuite;
using aima.test.core.unit.util.UtilTestSuite;

@RunWith(Suite.class)
@Suite.SuiteClasses( { AgentTestSuite.class, EnvironmentTestSuite.class,
		LearningTestSuite.class, LogicTestSuite.class,
		ProbabilityTestSuite.class, SearchTestSuite.class, UtilTestSuite.class })
public class AllAIMAUnitTestSuite {
}
