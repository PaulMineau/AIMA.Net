namespace aima.test.core.unit.environment.nqueens;

using java.util.ArrayList;
using java.util.List;

using org.junit.Assert;
using org.junit.Before;
using org.junit.Test;

using AIMA.Core.Agent.Action;
using AIMA.Core.Environment.NQueens.NQueensBoard;
using AIMA.Core.Environment.NQueens.NQueensFunctionFactory;
using AIMA.Core.Search.Framework.ActionsFunction;
using AIMA.Core.Search.Framework.ResultFunction;

/**
 * @author Ravi Mohan
 * @author Ciaran O'Reilly
 */
public class NQueensFunctionFactoryTest {
	ActionsFunction af;
	ResultFunction rf;
	NQueensBoard oneBoard;
	NQueensBoard eightBoard;

	@Before
	public void setUp() {
		oneBoard = new NQueensBoard(1);
		eightBoard = new NQueensBoard(8);

		af = NQueensFunctionFactory.getIActionsFunction();
		rf = NQueensFunctionFactory.getResultFunction();
	}

	@Test
	public void testSimpleBoardSuccessorGenerator() {
		List<Action> actions = new ArrayList<Action>(af.actions(oneBoard));
		Assert.assertEquals(1, actions.size());
		NQueensBoard next = (NQueensBoard) rf.result(oneBoard, actions.get(0));
		Assert.assertEquals(1, next.getNumberOfQueensOnBoard());
	}

	@Test
	public void testComplexBoardSuccessorGenerator() {
		List<Action> actions = new ArrayList<Action>(af.actions(eightBoard));
		Assert.assertEquals(8, actions.size());
		NQueensBoard next = (NQueensBoard) rf
				.result(eightBoard, actions.get(0));
		Assert.assertEquals(1, next.getNumberOfQueensOnBoard());

		actions = new ArrayList<Action>(af.actions(next));
		Assert.assertEquals(6, actions.size());
	}
}
