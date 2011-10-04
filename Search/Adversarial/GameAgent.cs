namespace AIMA.Core.Search.Adversarial
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent.Impl;

    /**
     * @author Ravi Mohan
     * 
     */
    public class GameAgent : AbstractAgent
    {
        private Game game;

        public GameAgent(Game g)
        {
            this.game = g;
        }

        public void makeMiniMaxMove()
        {
            game.makeMiniMaxMove();
        }

        public void makeAlphaBetaMove()
        {
            game.makeAlphaBetaMove();
        }

    }
}