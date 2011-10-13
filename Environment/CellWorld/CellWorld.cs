namespace AIMA.Core.Environment.CellWorld
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Probability;
    using AIMA.Core.Probability.Decision;
    using AIMA.Core.Util.DataStructure;

    /**
     * @author Ravi Mohan
     * 
     */
    public class CellWorld : MDPSource<CellWorldPosition, String>
    {
        public const String LEFT = "left";

        public const String RIGHT = "right";

        public const String UP = "up";

        public const String DOWN = "down";

        public const String NO_OP = "no_op";

        List<Cell> blockedCells, allCells;

        private int numberOfRows;

        private int numberOfColumns;

        private List<Cell> terminalStates;

        private Cell initialState;

        public CellWorld(int numberOfRows, int numberOfColumns, double initialReward)
        {
            allCells = new List<Cell>();
            blockedCells = new List<Cell>();

            terminalStates = new List<Cell>();

            this.numberOfRows = numberOfRows;
            this.numberOfColumns = numberOfColumns;

            for (int row = 1; row <= numberOfRows; row++)
            {
                for (int col = 1; col <= numberOfColumns; col++)
                {
                    allCells.Add(new Cell(row, col, initialReward));
                }
            }

            initialState = getCellAt(1, 4);
        }

        public void markBlocked(int i, int j)
        {
            blockedCells.Add(getCellAt(i, j));

        }

        private bool isBlocked(int i, int j) {
		if ((i < 1) || (i > numberOfRows) || (j < 1) || (j > numberOfColumns)) {
			return true;
		}
		foreach (Cell c in blockedCells) {
			if ((c.getX() == i) && (c.getY() == j)) {
				return true;
			}
		}
		return false;
	}

        private Cell getCellAt(int i, int j) {
		foreach (Cell c in allCells) {
			if ((c.getX() == i) && (c.getY() == j)) {
				return c;
			}
		}
		throw new ApplicationException("No Cell found at " + i + " , " + j);
	}

        public CellWorldPosition moveProbabilisticallyFrom(int i, int j,
                String direction, Randomizer r)
        {
            Cell c = getCellAt(i, j);
            if (terminalStates.Contains(c))
            {
                return c.position();
            }
            return moveFrom(i, j, determineDirectionOfActualMovement(direction, r));

        }

        private CellWorldPosition moveFrom(int i, int j, String direction)
        {
            if (direction.Equals(LEFT))
            {
                return moveLeftFrom(i, j);
            }
            if (direction.Equals(RIGHT))
            {
                return moveRightFrom(i, j);
            }
            if (direction.Equals(UP))
            {
                return moveUpFrom(i, j);
            }
            if (direction.Equals(DOWN))
            {
                return moveDownFrom(i, j);
            }
            throw new ApplicationException("Unable to move " + direction + " from " + i
                    + " , " + j);
        }

        private CellWorldPosition moveFrom(CellWorldPosition startingPosition,
                String direction)
        {
            return moveFrom(startingPosition.getX(), startingPosition.getY(),
                    direction);
        }

        private String determineDirectionOfActualMovement(
                String commandedDirection, double prob)
        {
            if (prob < 0.8)
            {
                return commandedDirection;
            }
            else if ((prob > 0.8) && (prob < 0.9))
            {
                if ((commandedDirection.Equals(LEFT))
                        || (commandedDirection.Equals(RIGHT)))
                {
                    return UP;
                }
                if ((commandedDirection.Equals(UP))
                        || (commandedDirection.Equals(DOWN)))
                {
                    return LEFT;
                }
            }
            else
            { // 0.9 < prob < 1.0
                if ((commandedDirection.Equals(LEFT))
                        || (commandedDirection.Equals(RIGHT)))
                {
                    return DOWN;
                }
                if ((commandedDirection.Equals(UP))
                        || (commandedDirection.Equals(DOWN)))
                {
                    return RIGHT;
                }
            }
            throw new ApplicationException(
                    "Unable to determine direction when command =  "
                            + commandedDirection + " and probability = " + prob);

        }

        private String determineDirectionOfActualMovement(
                String commandedDirection, Randomizer r)
        {
            return determineDirectionOfActualMovement(commandedDirection, r
                    .nextDouble());

        }

        private CellWorldPosition moveLeftFrom(int i, int j)
        {
            if (isBlocked(i, j - 1))
            {
                return new CellWorldPosition(i, j);
            }
            return new CellWorldPosition(i, j - 1);
        }

        private CellWorldPosition moveRightFrom(int i, int j)
        {
            if (isBlocked(i, j + 1))
            {
                return new CellWorldPosition(i, j);
            }
            return new CellWorldPosition(i, j + 1);
        }

        private CellWorldPosition moveUpFrom(int i, int j)
        {
            if (isBlocked(i + 1, j))
            {
                return new CellWorldPosition(i, j);
            }
            return new CellWorldPosition(i + 1, j);
        }

        private CellWorldPosition moveDownFrom(int i, int j)
        {
            if (isBlocked(i - 1, j))
            {
                return new CellWorldPosition(i, j);
            }
            return new CellWorldPosition(i - 1, j);
        }

        public void setReward(int i, int j, double reward)
        {
            Cell c = getCellAt(i, j);
            c.setReward(reward);

        }

        public List<Cell> unblockedCells() {
		List<Cell> res = new List<Cell>();
		foreach (Cell c in allCells) {
			if (!(blockedCells.Contains(c))) {
				res.Add(c);
			}
		}
		return res;
	}

        public bool isBlocked(Pair<int, int> p)
        {
            return isBlocked(p.getFirst(), p.getSecond());
        }

        // what is the probability of starting from position p1 taking action a and
        // reaaching position p2
        // method is public ONLY for testing do not use in client code.
        public double getTransitionProbability(CellWorldPosition startingPosition,
                String actionDesired, CellWorldPosition endingPosition) {

		String firstRightAngledAction = determineDirectionOfActualMovement(
				actionDesired, 0.85);
		String secondRightAngledAction = determineDirectionOfActualMovement(
				actionDesired, 0.95);

		Dictionary<String, CellWorldPosition> actionsToPositions = new Dictionary<String, CellWorldPosition>();
		actionsToPositions.Add(actionDesired, moveFrom(startingPosition,
				actionDesired));
        actionsToPositions.Add(firstRightAngledAction, moveFrom(
				startingPosition, firstRightAngledAction));
        actionsToPositions.Add(secondRightAngledAction, moveFrom(
				startingPosition, secondRightAngledAction));

		Dictionary<CellWorldPosition, Double> positionsToProbability = new Dictionary<CellWorldPosition, Double>();
		foreach (CellWorldPosition p in actionsToPositions.Values) {
            positionsToProbability[p] = 0.0;
		}

		foreach (String action in actionsToPositions.Keys) {
			CellWorldPosition position = actionsToPositions[action];
			double value = positionsToProbability[position];
			if (action.Equals(actionDesired)) {
                positionsToProbability[position]+= 0.8;
			} else { // right angled steps
                positionsToProbability[position] += 0.1;
			}

		}

		if (positionsToProbability.ContainsKey(endingPosition)) {
			return positionsToProbability[endingPosition];
		} else {
			return 0.0;
		}

	}

        public MDPTransitionModel<CellWorldPosition, String> getTransitionModel() {
		List<CellWorldPosition> terminalPositions = new List<CellWorldPosition>();
		foreach (Cell tc in terminalStates) {
			terminalPositions.Add(tc.position());
		}
		MDPTransitionModel<CellWorldPosition, String> mtm = new MDPTransitionModel<CellWorldPosition, String>(
				terminalPositions);

		List<String> actions = new List<string>(){ UP, DOWN, LEFT,
				RIGHT };

		foreach (CellWorldPosition startingPosition in getNonFinalStates()) {
			foreach (String actionDesired in actions) {
				foreach (Cell target in unblockedCells()) { // too much work? should
					// just cycle through
					// neighbouring cells
					// instead of all cells.
					CellWorldPosition endingPosition = target.position();
					double transitionProbability = getTransitionProbability(
							startingPosition, actionDesired, endingPosition);
					if (!(transitionProbability == 0.0)) {

						mtm.setTransitionProbability(startingPosition,
								actionDesired, endingPosition,
								transitionProbability);
					}
				}
			}
		}
		return mtm;
	}

        public MDPRewardFunction<CellWorldPosition> getRewardFunction() {

		MDPRewardFunction<CellWorldPosition> result = new MDPRewardFunction<CellWorldPosition>();
		foreach (Cell c in unblockedCells()) {
			CellWorldPosition pos = c.position();
			double reward = c.getReward();
			result.setReward(pos, reward);
		}

		return result;
	}

        public List<CellWorldPosition> unblockedPositions() {
		List<CellWorldPosition> result = new List<CellWorldPosition>();
		foreach (Cell c in unblockedCells()) {
			result.Add(c.position());
		}
		return result;
	}

        public MDP<CellWorldPosition, String> asMdp()
        {

            return new MDP<CellWorldPosition, String>(this);
        }

        public List<CellWorldPosition> getNonFinalStates()
        {
            List<CellWorldPosition> nonFinalPositions = unblockedPositions();
            nonFinalPositions.Remove(getCellAt(2, 4).position());
            nonFinalPositions.Remove(getCellAt(3, 4).position());
            return nonFinalPositions;
        }

        public List<CellWorldPosition> getFinalStates()
        {
            List<CellWorldPosition> finalPositions = new List<CellWorldPosition>();
            finalPositions.Add(getCellAt(2, 4).position());
            finalPositions.Add(getCellAt(3, 4).position());
            return finalPositions;
        }

        public void setTerminalState(int i, int j)
        {
            setTerminalState(new CellWorldPosition(i, j));

        }

        public void setTerminalState(CellWorldPosition position)
        {
            terminalStates.Add(getCellAt(position.getX(), position.getY()));

        }

        public CellWorldPosition getInitialState()
        {
            return initialState.position();
        }

        public MDPPerception<CellWorldPosition> execute(CellWorldPosition position,
                String action, Randomizer r)
        {
            CellWorldPosition pos = moveProbabilisticallyFrom(position.getX(),
                    position.getY(), action, r);
            double reward = getCellAt(pos.getX(), pos.getY()).getReward();
            return new MDPPerception<CellWorldPosition>(pos, reward);
        }

        public List<String> getAllActions()
        {
            return new List<string>(){ LEFT, RIGHT, UP, DOWN };
        }
    }
}