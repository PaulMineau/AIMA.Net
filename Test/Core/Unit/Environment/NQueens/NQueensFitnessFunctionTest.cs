namespace aima.test.core.unit.environment.nqueens;

using java.util.Set;

using org.junit.Assert;
using org.junit.Before;
using org.junit.Test;

using AIMA.Core.Environment.NQueens.NQueensBoard;
using AIMA.Core.Environment.NQueens.NQueensFitnessFunction;

/**
 * @author Ciaran O'Reilly
 * 
 */
public class NQueensFitnessFunctionTest {

	NQueensFitnessFunction fitnessFunction;

	@Before
	public void setUp() {
		fitnessFunction = new NQueensFitnessFunction();
	}

	@Test
	public void test_getValue() {
		Assert.assertTrue(0.0 == fitnessFunction.getValue("00000000"));
		Assert.assertTrue(0.0 == fitnessFunction.getValue("01234567"));
		Assert.assertTrue(0.0 == fitnessFunction.getValue("76543210"));

		Assert.assertTrue(23.0 == fitnessFunction.getValue("56136477"));
		Assert.assertTrue(28.0 == fitnessFunction.getValue("04752613"));
	}

	@Test
	public void test_isGoalState() {
		Assert.assertTrue(fitnessFunction.isGoalState("04752613"));
		Assert.assertFalse(fitnessFunction.isGoalState("00000000"));
		Assert.assertFalse(fitnessFunction.isGoalState("56136477"));
	}

	@Test
	public void test_getBoardForIndividual() {
		NQueensBoard board = fitnessFunction.getBoardForIndividual("56136477");
		Assert.assertEquals(" -  -  -  -  -  -  -  - \n"
				+ " -  -  Q  -  -  -  -  - \n" + " -  -  -  -  -  -  -  - \n"
				+ " -  -  -  Q  -  -  -  - \n" + " -  -  -  -  -  Q  -  - \n"
				+ " Q  -  -  -  -  -  -  - \n" + " -  Q  -  -  Q  -  -  - \n"
				+ " -  -  -  -  -  -  Q  Q \n", board.getBoardPic());

		Assert.assertEquals("--------\n" + "--Q-----\n" + "--------\n"
				+ "---Q----\n" + "-----Q--\n" + "Q-------\n" + "-Q--Q---\n"
				+ "------QQ\n", board.ToString());
	}

	@Test
	public void test_generateRandomIndividual() {
		for (int i = Character.MIN_RADIX; i <= Character.MAX_RADIX; i++) {
			String individual = fitnessFunction.generateRandomIndividual(i);
			Assert.assertEquals(i, individual.length());
		}
	}

	@Test
	public void test_getFiniteAlphabet() {
		for (int i = Character.MIN_RADIX; i <= Character.MAX_RADIX; i++) {
			Set<Character> fab = fitnessFunction
					.getFiniteAlphabetForBoardOfSize(i);
			Assert.assertEquals(i, fab.size());
		}
	}
}
