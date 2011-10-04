namespace aima.test.core.unit.environment;

using org.junit.runner.RunWith;
using org.junit.runners.Suite;

using aima.test.core.unit.environment.cellworld.CellWorldTest;
using aima.test.core.unit.environment.eightpuzzle.EightPuzzleBoardMoveTest;
using aima.test.core.unit.environment.eightpuzzle.EightPuzzleBoardTest;
using aima.test.core.unit.environment.eightpuzzle.EightPuzzleFunctionFactoryTest;
using aima.test.core.unit.environment.eightpuzzle.MisplacedTileHeuristicFunctionTest;
using aima.test.core.unit.environment.map.MapAgentTest;
using aima.test.core.unit.environment.map.MapEnvironmentTest;
using aima.test.core.unit.environment.map.MapFunctionFactoryTest;
using aima.test.core.unit.environment.map.MapStepCostFunctionTest;
using aima.test.core.unit.environment.map.MapTest;
using aima.test.core.unit.environment.nqueens.NQueensBoardTest;
using aima.test.core.unit.environment.nqueens.NQueensFitnessFunctionTest;
using aima.test.core.unit.environment.nqueens.NQueensFunctionFactoryTest;
using aima.test.core.unit.environment.nqueens.NQueensGoalTestTest;
using aima.test.core.unit.environment.tictactoe.TicTacToeTest;
using aima.test.core.unit.environment.vacuum.ModelBasedReflexVacuumAgentTest;
using aima.test.core.unit.environment.vacuum.ReflexVacuumAgentTest;
using aima.test.core.unit.environment.vacuum.SimpleReflexVacuumAgentTest;
using aima.test.core.unit.environment.vacuum.TableDrivenVacuumAgentTest;
using aima.test.core.unit.environment.vacuum.VacuumEnvironmentTest;
using aima.test.core.unit.environment.xyenv.XYEnvironmentTest;

@RunWith(Suite.class)
@Suite.SuiteClasses( { CellWorldTest.class, EightPuzzleBoardMoveTest.class,
		EightPuzzleBoardTest.class, EightPuzzleFunctionFactoryTest.class,
		MisplacedTileHeuristicFunctionTest.class, TicTacToeTest.class,
		MapAgentTest.class, MapEnvironmentTest.class,
		MapStepCostFunctionTest.class, MapFunctionFactoryTest.class,
		MapTest.class, NQueensBoardTest.class,
		NQueensFitnessFunctionTest.class, NQueensGoalTestTest.class,
		NQueensFunctionFactoryTest.class,
		ModelBasedReflexVacuumAgentTest.class, ReflexVacuumAgentTest.class,
		SimpleReflexVacuumAgentTest.class, TableDrivenVacuumAgentTest.class,
		VacuumEnvironmentTest.class, XYEnvironmentTest.class })
public class EnvironmentTestSuite {

}
