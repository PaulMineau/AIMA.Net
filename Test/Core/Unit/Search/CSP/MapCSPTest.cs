namespace aima.test.core.unit.search.csp;

using org.junit.Assert;
using org.junit.Before;
using org.junit.Test;

using AIMA.Core.Search.CSP.Assignment;
using AIMA.Core.Search.CSP.BacktrackingStrategy;
using AIMA.Core.Search.CSP.CSP;
using AIMA.Core.Search.CSP.MapCSP;
using AIMA.Core.Search.CSP.MinConflictsStrategy;

/**
 * @author Ravi Mohan
 * 
 */
public class MapCSPTest {
	private CSP csp;

	@Before
	public void setUp() {
		csp = new MapCSP();
	}

	@Test
	public void testBackTrackingSearch() {
		Assignment results = new BacktrackingStrategy().solve(csp);
		Assert.assertNotNull(results);
		Assert.assertEquals(MapCSP.GREEN, results.getAssignment(MapCSP.WA));
		Assert.assertEquals(MapCSP.RED, results.getAssignment(MapCSP.NT));
		Assert.assertEquals(MapCSP.BLUE, results.getAssignment(MapCSP.SA));
		Assert.assertEquals(MapCSP.GREEN, results.getAssignment(MapCSP.Q));
		Assert.assertEquals(MapCSP.RED, results.getAssignment(MapCSP.NSW));
		Assert.assertEquals(MapCSP.GREEN, results.getAssignment(MapCSP.V));
		Assert.assertEquals(MapCSP.RED, results.getAssignment(MapCSP.T));
	}

	@Test
	public void testMCSearch() {
		new MinConflictsStrategy(100).solve(csp);
	}
}
