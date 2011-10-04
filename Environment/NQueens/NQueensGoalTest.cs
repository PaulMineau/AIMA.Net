namespace AIMA.Core.Environment.NQueens
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Search.Framework;

    /**
     * @author R. Lunde
     */
    public class NQueensGoalTest : GoalTest
    {

        public bool isGoalState(Object state)
        {
            NQueensBoard board = (NQueensBoard)state;
            return board.getNumberOfQueensOnBoard() == board.getSize()
                    && board.getNumberOfAttackingPairs() == 0;
        }
    }
}