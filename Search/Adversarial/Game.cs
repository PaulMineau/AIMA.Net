namespace AIMA.Core.Search.Adversarial
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public abstract class Game
    {
        protected GameState initialState = new GameState();

        protected GameState presentState = new GameState();

        protected int level;

        public abstract List<GameState> getSuccessorStates(GameState state);

        public abstract GameState makeMove(GameState state, Object o);

        public abstract int getMiniMaxValue(GameState state);

        public abstract int getAlphaBetaValue(GameState state);

        public bool hasEnded()
        {
            return (terminalTest(getState()));
        }

        public int getLevel(GameState g)
        {
            return (((int)g.get("level")).intValue());
        }

        public List getMoves(GameState state)
        {
            return (List)state.get("moves");
        }

        public String getPlayerToMove(GameState state)
        {
            return (String)state.get("player");
        }

        public int getUtility(GameState h)
        {
            return ((int)h.get("utility")).intValue();
        }

        public GameState getState()
        {
            return presentState;
        }

        public int maxValue(GameState state)
        {
            int v = int.MIN_VALUE;
            if (terminalTest(state))
            {
                return computeUtility(state);
            }
            else
            {
                List<GameState> successorList = getSuccessorStates(state);
                for (int i = 0; i < successorList.Count; i++)
                {
                    GameState successor = successorList.get(i);
                    int minimumValueOfSuccessor = minValue(successor);
                    if (minimumValueOfSuccessor > v)
                    {
                        v = minimumValueOfSuccessor;
                        state.put("next", successor);
                    }
                }
                return v;
            }

        }

        public int minValue(GameState state)
        {

            int v = int.MAX_VALUE;

            if (terminalTest(state))
            {
                return computeUtility(state);

            }
            else
            {
                List<GameState> successorList = getSuccessorStates(state);
                for (int i = 0; i < successorList.Count; i++)
                {
                    GameState successor = successorList.get(i);
                    int maximumValueOfSuccessors = maxValue(successor);
                    if (maximumValueOfSuccessors < v)
                    {
                        v = maximumValueOfSuccessors;
                        state.put("next", successor);
                    }
                }
                return v;
            }

        }

        public int minValue(GameState state, AlphaBeta ab)
        {
            int v = int.MAX_VALUE;

            if (terminalTest(state))
            {
                return (computeUtility(state));

            }
            else
            {
                List<GameState> successorList = getSuccessorStates(state);
                for (int i = 0; i < successorList.Count; i++)
                {
                    GameState successor = successorList.get(i);
                    int maximumValueOfSuccessor = maxValue(successor, ab.copy());
                    if (maximumValueOfSuccessor < v)
                    {
                        v = maximumValueOfSuccessor;
                        state.put("next", successor);
                    }
                    if (v <= ab.alpha())
                    {
                        // System.Console.WriteLine("pruning from min");
                        return v;
                    }
                    ab.setBeta(Util.min(ab.beta(), v));

                }
                return v;
            }

        }

        public void makeMiniMaxMove()
        {
            getMiniMaxValue(presentState);
            GameState nextState = (GameState)presentState.get("next");
            if (nextState == null)
            {
                throw new RuntimeException("Mini Max Move failed");

            }
            makeMove(presentState, nextState.get("moveMade"));

        }

        public void makeAlphaBetaMove()
        {
            getAlphaBetaValue(presentState);

            GameState nextState = (GameState)presentState.get("next");
            if (nextState == null)
            {
                throw new RuntimeException("Alpha Beta Move failed");
            }
            makeMove(presentState, nextState.get("moveMade"));

        }

        //
        // PROTECTED METHODS
        //
        protected abstract int computeUtility(GameState state);

        protected abstract bool terminalTest(GameState state);

        protected int maxValue(GameState state, AlphaBeta ab)
        {
            int v = int.MIN_VALUE;
            if (terminalTest(state))
            {
                return computeUtility(state);
            }
            else
            {
                List<GameState> successorList = getSuccessorStates(state);
                for (int i = 0; i < successorList.Count; i++)
                {
                    GameState successor = (GameState)successorList.get(i);
                    int minimumValueOfSuccessor = minValue(successor, ab.copy());
                    if (minimumValueOfSuccessor > v)
                    {
                        v = minimumValueOfSuccessor;
                        state.put("next", successor);
                    }
                    if (v >= ab.beta())
                    {
                        // System.Console.WriteLine("pruning from max");
                        return v;
                    }
                    ab.setAlpha(Util.max(ab.alpha(), v));
                }
                return v;
            }
        }
    }
}