namespace AIMA.Core.Search.Framework
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;

    /**
     * Returns one for every action.
     * 
     * @author Ravi Mohan
     */
    public class DefaultStepCostFunction : StepCostFunction
    {

        public double c(Object stateFrom, Action action, Object stateTo)
        {
            return 1;
        }
    }
}