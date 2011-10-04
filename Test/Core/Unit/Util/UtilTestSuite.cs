namespace aima.test.core.unit.util;

using org.junit.runner.RunWith;
using org.junit.runners.Suite;

using aima.test.core.unit.util.datastructure.FIFOQueueTest;
using aima.test.core.unit.util.datastructure.LIFOQueueTest;
using aima.test.core.unit.util.datastructure.TableTest;
using aima.test.core.unit.util.datastructure.XYLocationTest;
using aima.test.core.unit.util.math.MixedRadixNumberTest;

@RunWith(Suite.class)
@Suite.SuiteClasses( { FIFOQueueTest.class, LIFOQueueTest.class,
		TableTest.class, XYLocationTest.class, MixedRadixNumberTest.class,
		SetOpsTest.class, UtilTest.class })
public class UtilTestSuite {

}
