namespace AIMA.Core.Search.Framework
{
    using System.Collections.Generic;
    using AIMA.Core.Agent;

    /**
     * Returns one for every action.
     * 
     * @author Ravi Mohan
     */
    public class DefaultStepCostFunction : StepCostFunction
    {

        public double c(object stateFrom, Action action, object stateTo)
        {
            return 1;
        }
    }
}