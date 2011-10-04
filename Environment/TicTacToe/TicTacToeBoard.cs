namespace AIMA.Core.Environment.TicTacToe
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Util.DataStructure;

/**
 * @author Ravi Mohan
 * @author R. Lunde
 * 
 */
public class TicTacToeBoard {
	public const String O = "O";
	public const String X = "X";
	public const String EMPTY = "-";

	String[] state = new String[] { EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY,
			EMPTY, EMPTY, EMPTY };

	public bool isEmpty(int row, int col) {
		return state[getAbsPosition(row, col)] == EMPTY;
	}

	public bool isMarked(String s, int i, int j) {
		return getValue(i, j).Equals(s);
	}

	public void markX(int row, int col) {
		mark(row, col, X);
	}

	public void markO(int row, int col) {
		mark(row, col, O);
	}

	private void mark(int row, int col, String symbol) {
		state[getAbsPosition(row, col)] = symbol;
	}

	public bool isAnyRowComplete() {
		for (int i = 0; i < 3; i++) {
			String val = getValue(i, 0);
			if (val != EMPTY && val == getValue(i, 1) && val == getValue(i, 2))
				return true;
		}
		return false;
	}

	public bool isAnyColumnComplete() {
		for (int j = 0; j < 3; j++) {
			String val = getValue(0, j);
			if (val != EMPTY && val == getValue(1, j) && val == getValue(2, j))
				return true;
		}
		return false;
	}

	public bool isAnyDiagonalComplete() {
		bool retVal = false;
		String val = getValue(0, 0);
		if (val != EMPTY && val == getValue(1, 1) && val == getValue(2, 2))
			return true;
		val = getValue(0, 2);
		if (val != EMPTY && val == getValue(1, 1) && val == getValue(2, 0))
			return true;
		return retVal;
	}

	public bool lineThroughBoard() {
		return (isAnyRowComplete() || isAnyColumnComplete() || isAnyDiagonalComplete());
	}

	public String getValue(int row, int col) {
		return state[getAbsPosition(row, col)];
	}

	private void setValue(int row, int col, String val) {
		state[getAbsPosition(row, col)] = val;
	}

	public TicTacToeBoard cloneBoard() {
		return (TicTacToeBoard) clone();
	}

	public override Object clone() {
		TicTacToeBoard newBoard = new TicTacToeBoard();
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				String s = getValue(i, j);
				newBoard.setValue(i, j, s);
			}
		}
		return newBoard;
	}

	public int getNumberOfMarkedPositions() {
		int retVal = 0;
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (!(isEmpty(i, j))) {
					retVal++;
				}
			}
		}
		return retVal;
	}

	public List getUnMarkedPositions() {
		List<XYLocation> retVal = new List<XYLocation>();
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (isEmpty(i, j)) {
					retVal.Add(new XYLocation(i, j));
				}
			}

		}
		return retVal;
	}

	public String[] getState() {
		return state;
	}

	public void setState(String[] state) {
		this.state = state;
	}

	public override bool Equals(Object anObj) {
		TicTacToeBoard anotherBoard = (TicTacToeBoard) anObj;
		for (int i = 0; i < 9; i++)
			if (state[i] != anotherBoard.state[i])
				return false;
		return true;
	}

	public override String ToString() {
		StringBuffer buf = new StringBuffer();
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++)
				buf.append(getValue(i, j) + " ");
			buf.append("\n");
		}
		return buf.ToString();
	}

	public void print() {
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++)
				System.Console.Write(getValue(i, j) + " ");
			System.Console.WriteLine();
		}
	}

	//
	// PRIVATE METHODS
	//

	private int getAbsPosition(int row, int col) {
		return row * 3 + col;
	}
}
}