namespace aima.test.core.unit.learning;

using org.junit.runner.RunWith;
using org.junit.runners.Suite;

using aima.test.core.unit.learning.framework.DataSetTest;
using aima.test.core.unit.learning.framework.InformationAndGainTest;
using aima.test.core.unit.learning.inductive.DLTestTest;
using aima.test.core.unit.learning.inductive.DecisionListTest;
using aima.test.core.unit.learning.learners.DecisionTreeTest;
using aima.test.core.unit.learning.learners.EnsembleLearningTest;
using aima.test.core.unit.learning.learners.LearnerTests;
using aima.test.core.unit.learning.neural.BackPropagationTests;
using aima.test.core.unit.learning.neural.LayerTests;
using aima.test.core.unit.learning.reinforcement.QTableTest;
using aima.test.core.unit.learning.reinforcement.ReinforcementLearningTest;

@RunWith(Suite.class)
@Suite.SuiteClasses( { DataSetTest.class, InformationAndGainTest.class,
		DecisionListTest.class, DLTestTest.class, DecisionTreeTest.class,
		EnsembleLearningTest.class, LearnerTests.class,
		BackPropagationTests.class, LayerTests.class, QTableTest.class,
		ReinforcementLearningTest.class })
public class LearningTestSuite {

}
