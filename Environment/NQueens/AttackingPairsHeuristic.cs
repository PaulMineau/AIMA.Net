namespace AIMA.Core.Environment.NQueens
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Search.Framework;

    /**
     * Estimates the distance to goal by the number of attacking pairs of queens on
     * the board.
     * 
     * @author R. Lunde
     */
    public class AttackingPairsHeuristic : HeuristicFunction
    {

        public double h(Object state)
        {
            NQueensBoard board = (NQueensBoard)state;
            return board.getNumberOfAttackingPairs();
        }
    }
}