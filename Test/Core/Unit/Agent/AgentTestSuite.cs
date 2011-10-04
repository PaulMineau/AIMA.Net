namespace aima.test.core.unit.agent;

using org.junit.runner.RunWith;
using org.junit.runners.Suite;

using aima.test.core.unit.agent.impl.DynamicPerceptTest;
using aima.test.core.unit.agent.impl.PerceptSequenceTest;
using aima.test.core.unit.agent.impl.aprog.TableDrivenAgentProgramTest;
using aima.test.core.unit.agent.impl.aprog.simplerule.RuleTest;

@RunWith(Suite.class)
@Suite.SuiteClasses( { RuleTest.class, TableDrivenAgentProgramTest.class,
		DynamicPerceptTest.class, PerceptSequenceTest.class })
public class AgentTestSuite {

}
