namespace AIMA.Core.Environment.TicTacToe
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Search.Adversarial;
using AIMA.Core.Util.DataStructure;

/**
 * @author Ravi Mohan
 * 
 */
public class TicTacToe : Game {
	public TicTacToe() {
		List<XYLocation> moves = new List<XYLocation>();
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				XYLocation loc = new XYLocation(i, j);
				moves.Add(loc);
			}
		}
		initialState.put("moves", moves);
		initialState.put("player", "X");
		initialState.put("utility", new int(0));
		initialState.put("board", new TicTacToeBoard());
		initialState.put("level", new int(0));
		presentState = initialState;
	}

	public TicTacToeBoard getBoard(GameState state) {

		return (TicTacToeBoard) state.get("board");
	}

	public override List<GameState> getSuccessorStates(GameState state) {
		GameState temp = presentState;
		List<GameState> retVal = new List<GameState>();
		int parentLevel = getLevel(state);
		for (int i = 0; i < getMoves(state).Count; i++) {
			XYLocation loc = (XYLocation) getMoves(state).get(i);

			GameState aState = makeMove(state, loc);
			aState.put("moveMade", loc);
			aState.put("level", new int(parentLevel + 1));
			retVal.Add(aState);

		}
		presentState = temp;
		return retVal;
	}

	public override GameState makeMove(GameState state, Object o) {
		XYLocation loc = (XYLocation) o;
		return makeMove(state, loc.getXCoOrdinate(), loc.getYCoOrdinate());
	}

	public GameState makeMove(GameState state, int x, int y) {
		GameState temp = getMove(state, x, y);
		if (temp != null) {
			presentState = temp;
		}
		return presentState;
	}

	public GameState makeMove(int x, int y) {
		GameState state = presentState;
		GameState temp = getMove(state, x, y);
		if (temp != null) {
			presentState = temp;
		}
		return presentState;
	}

	public GameState getMove(GameState state, int x, int y) {
		GameState retVal = null;
		XYLocation loc = new XYLocation(x, y);
		List moves = getMoves(state);
		List newMoves = (List) moves.clone();
		if (moves.contains(loc)) {
			int index = newMoves.indexOf(loc);
			newMoves.remove(index);

			retVal = new GameState();

			retVal.put("moves", newMoves);
			TicTacToeBoard newBoard = getBoard(state).cloneBoard();
			if (getPlayerToMove(state) == "X") {
				newBoard.markX(x, y);
				retVal.put("player", "O");

			} else {
				newBoard.markO(x, y);
				retVal.put("player", "X");

			}
			retVal.put("board", newBoard);
			retVal.put("utility", new int(computeUtility(newBoard,
					getPlayerToMove(getState()))));
			retVal.put("level", new int(getLevel(state) + 1));
			// presentState = retVal;
		}
		return retVal;
	}

	public override int computeUtility(GameState state) {
		int utility = computeUtility((TicTacToeBoard) state.get("board"),
				(getPlayerToMove(state)));
		return utility;
	}

	public override bool terminalTest(GameState state) {
		TicTacToeBoard board = (TicTacToeBoard) state.get("board");
		bool line = board.lineThroughBoard();
		bool filled = board.getNumberOfMarkedPositions() == 9;
		return (line || filled);
	}

	public void printPossibleMoves() {
		System.Console.WriteLine("Possible moves");

		List moves = getMoves(presentState);
		for (int i = 0; i < moves.Count; i++) {
			XYLocation moveLoc = (XYLocation) moves.get(i);
			GameState newState = getMove(presentState,
					moveLoc.getXCoOrdinate(), moveLoc.getYCoOrdinate());
			TicTacToeBoard board = (TicTacToeBoard) newState.get("board");
			System.Console.WriteLine("utility = " + computeUtility(newState));
			System.Console.WriteLine("");
		}

	}

	public override int getMiniMaxValue(GameState state) {
		// statesSeen = new List();
		// System.Console.WriteLine("In get Minimax Value");
		// System.Console.WriteLine("Received state ");
		// ((TicTacToeBoard)state.get("board")).print();
		if (getPlayerToMove(state).equalsIgnoreCase("X")) {
			return maxValue(state);

		} else {
			return minValue(state);
		}
	}

	public override int getAlphaBetaValue(GameState state) {

		if (getPlayerToMove(state).equalsIgnoreCase("X")) {
			AlphaBeta initial = new AlphaBeta(int.MIN_VALUE,
					int.MAX_VALUE);
			int max = maxValue(state, initial);
			return max;

		} else {
			// invert?
			AlphaBeta initial = new AlphaBeta(int.MIN_VALUE,
					int.MAX_VALUE);
			return minValue(state, initial);
		}
	}

	//
	// PRIVATE METHODS
	//
	private int computeUtility(TicTacToeBoard aBoard, String playerToMove) {
		int retVal = 0;
		if (aBoard.lineThroughBoard()) {
			if (playerToMove.Equals("X")) {
				retVal = -1;
			} else {
				retVal = 1;
			}

		}
		return retVal;
	}
}
}