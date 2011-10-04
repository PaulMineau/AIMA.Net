namespace aima.test.core.unit.search.csp;

using java.util.ArrayList;
using java.util.List;

using org.junit.Assert;
using org.junit.Before;
using org.junit.Test;

using AIMA.Core.Search.CSP.Assignment;
using AIMA.Core.Search.CSP.Variable;

/**
 * @author Ravi Mohan
 * 
 */
public class AssignmentTest {
	private const Variable X = new Variable("x");
	private const Variable Y = new Variable("y");

	private List<Variable> variables;
	private Assignment assignment;

	@Before
	public void setUp() {
		variables = new ArrayList<Variable>();
		variables.add(X);
		variables.add(Y);
		assignment = new Assignment();
	}

	@Test
	public void testAssignmentCompletion() {
		Assert.assertFalse(assignment.isComplete(variables));
		assignment.setAssignment(X, "Ravi");
		Assert.assertFalse(assignment.isComplete(variables));
		assignment.setAssignment(Y, "AIMA");
		Assert.assertTrue(assignment.isComplete(variables));
		assignment.removeAssignment(X);
		Assert.assertFalse(assignment.isComplete(variables));
	}

	// @Test
	// public void testAssignmentDefaultVariableSelection() {
	// Assert.assertEquals(X, assignment.selectFirstUnassignedVariable(csp));
	// assignment.setAssignment(X, "Ravi");
	// Assert.assertEquals(Y, assignment.selectFirstUnassignedVariable(csp));
	// assignment.setAssignment(Y, "AIMA");
	// Assert.assertEquals(null, assignment.selectFirstUnassignedVariable(csp));
	// }
}