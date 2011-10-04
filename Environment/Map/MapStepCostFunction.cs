namespace AIMA.Core.Environment.Map
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Search.Framework;

    /**
     * Implementation of StepCostFunction interface that uses the distance between locations
     * to calculate the cost in addition to a constant cost, so that it may be used
     * in conjunction with a Uniform-cost search.
     */

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class MapStepCostFunction : StepCostFunction
    {
        private Map map = null;

        //
        // Used by Uniform-cost search to ensure every step is greater than or equal
        // to some small positive constant
        private static double constantCost = 1.0;

        public MapStepCostFunction(Map aMap)
        {
            this.map = aMap;
        }

        //
        // START-StepCostFunction
        public double c(Object fromCurrentState, Action action, Object toNextState)
        {

            String fromLoc = fromCurrentState.ToString();
            String toLoc = toNextState.ToString();

            Double distance = map.getDistance(fromLoc, toLoc);

            if (distance == null || distance <= 0)
            {
                return constantCost;
            }

            return new Double(distance);
        }

        // END-StepCostFunction
        //
    }
}