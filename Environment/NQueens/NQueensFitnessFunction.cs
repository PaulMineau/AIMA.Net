namespace AIMA.Core.Environment.NQueens
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Search.Framework;
    using AIMA.Core.Search.Local;
    using AIMA.Core.Util.DataStructure;

    /**
     * A class whose purpose is to evaluate the fitness of NQueen individuals
     * and to provide utility methods for translating between an NQueensBoard
     * representation and the String representation used by the GeneticAlgorithm.
     */

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class NQueensFitnessFunction : FitnessFunction, GoalTest
    {

        private NQueensGoalTest goalTest = new NQueensGoalTest();

        public NQueensFitnessFunction()
        {

        }

        //
        // START - Interface FitnessFunction
        public double getValue(String individual)
        {
            double fitness = 0;

            NQueensBoard board = getBoardForIndividual(individual);
            int boardSize = board.getSize();

            // Calculate the number of non-attacking pairs of queens (refer to AIMA
            // page 117).
            List<XYLocation> qPositions = board.getQueenPositions();
            for (int fromX = 0; fromX < (boardSize - 1); fromX++)
            {
                for (int toX = fromX + 1; toX < boardSize; toX++)
                {
                    int fromY = qPositions.get(fromX).getYCoOrdinate();
                    bool nonAttackingPair = true;
                    // Check right beside
                    int toY = fromY;
                    if (board.queenExistsAt(new XYLocation(toX, toY)))
                    {
                        nonAttackingPair = false;
                    }
                    // Check right and above
                    toY = fromY - (toX - fromX);
                    if (toY >= 0)
                    {
                        if (board.queenExistsAt(new XYLocation(toX, toY)))
                        {
                            nonAttackingPair = false;
                        }
                    }
                    // Check right and below
                    toY = fromY + (toX - fromX);
                    if (toY < boardSize)
                    {
                        if (board.queenExistsAt(new XYLocation(toX, toY)))
                        {
                            nonAttackingPair = false;
                        }
                    }

                    if (nonAttackingPair)
                    {
                        fitness += 1.0;
                    }
                }
            }

            return fitness;
        }

        // END - Interface FitnessFunction
        //

        //
        // START - Interface GoalTest
        public bool isGoalState(Object state)
        {
            return goalTest.isGoalState(getBoardForIndividual((String)state));
        }

        // END - Interface GoalTest
        //

        public NQueensBoard getBoardForIndividual(String individual)
        {
            int boardSize = individual.length();
            NQueensBoard board = new NQueensBoard(boardSize);
            for (int i = 0; i < boardSize; i++)
            {
                int pos = Character
                        .digit(individual.charAt(i), individual.length());
                board.AddQueenAt(new XYLocation(i, pos));
            }

            return board;
        }

        public String generateRandomIndividual(int boardSize)
        {
            StringBuffer ind = new StringBuffer();

            assert(boardSize >= Character.MIN_RADIX && boardSize <= Character.MAX_RADIX);

            for (int i = 0; i < boardSize; i++)
            {
                ind.append(Character.forDigit(new Random().nextInt(boardSize),
                        boardSize));
            }

            return ind.ToString();
        }

        public HashSet<Character> getFiniteAlphabetForBoardOfSize(int size)
        {
            HashSet<Character> fab = new HashSet<Character>();

            assert(size >= Character.MIN_RADIX && size <= Character.MAX_RADIX);

            for (int i = 0; i < size; i++)
            {
                fab.Add(Character.forDigit(i, size));
            }

            return fab;
        }
    }
}