namespace aima.test.core.unit.search;

using org.junit.runner.RunWith;
using org.junit.runners.Suite;

using aima.test.core.unit.search.csp.AssignmentTest;
using aima.test.core.unit.search.csp.CSPTest;
using aima.test.core.unit.search.csp.MapCSPTest;
using aima.test.core.unit.search.framework.NodeTest;
using aima.test.core.unit.search.framework.SolutionCheckerTest;
using aima.test.core.unit.search.informed.AStarSearchTest;
using aima.test.core.unit.search.informed.GreedyBestFirstSearchTest;
using aima.test.core.unit.search.informed.RecursiveBestFirstSearchTest;
using aima.test.core.unit.search.local.SimulatedAnnealingSearchTest;
using aima.test.core.unit.search.online.LRTAStarAgentTest;
using aima.test.core.unit.search.online.OnlineDFSAgentTest;
using AIMA.Test.Core.Unit.Search.Uninformed.BidirectionalSearchTest;
using AIMA.Test.Core.Unit.Search.Uninformed.BreadthFirstSearchTest;
using AIMA.Test.Core.Unit.Search.Uninformed.DepthFirstSearchTest;
using AIMA.Test.Core.Unit.Search.Uninformed.DepthLimitedSearchTest;
using AIMA.Test.Core.Unit.Search.Uninformed.IterativeDeepeningSearchTest;
using AIMA.Test.Core.Unit.Search.Uninformed.UniformCostSearchTest;

@RunWith(Suite.class)
@Suite.SuiteClasses( { AssignmentTest.class, CSPTest.class, MapCSPTest.class,
		AStarSearchTest.class, GreedyBestFirstSearchTest.class,
		RecursiveBestFirstSearchTest.class, SimulatedAnnealingSearchTest.class,
		LRTAStarAgentTest.class, OnlineDFSAgentTest.class,
		BidirectionalSearchTest.class, BreadthFirstSearchTest.class,
		DepthFirstSearchTest.class, DepthLimitedSearchTest.class,
		IterativeDeepeningSearchTest.class, UniformCostSearchTest.class,
		NodeTest.class, SolutionCheckerTest.class })
public class SearchTestSuite {
}
