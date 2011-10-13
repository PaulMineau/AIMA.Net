namespace AIMA.Core.Probability.Decision
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Environment;
    using AIMA.Core.Environment.CellWorld;

    /**
     * @author Ravi Mohan
     * 
     */
    public class MDPFactory
    {

        public static MDP<CellWorldPosition, String> createFourByThreeMDP()
        {

            CellWorld cw = new CellWorld(3, 4, 0.4);
            cw = new CellWorld(3, 4, -0.04);

            cw.markBlocked(2, 2);

            cw.setTerminalState(2, 4);
            cw.setReward(2, 4, -1);

            cw.setTerminalState(3, 4);
            cw.setReward(3, 4, 1);
            return cw.asMdp();
        }
    }
}