namespace AIMA.Core.Environment.Map
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Search.Framework;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class BidirectionalMapProblem : Problem,
            BidirectionalProblem
    {

        Map map;

        Problem reverseProblem;

        public BidirectionalMapProblem(Map aMap, String initialState,
                String goalState)
        {
            super(initialState, MapFunctionFactory.getActionsFunction(aMap),
                    MapFunctionFactory.getResultFunction(), new DefaultGoalTest(
                            goalState), new MapStepCostFunction(aMap));

            map = aMap;

            reverseProblem = new Problem(goalState, MapFunctionFactory
                    .getActionsFunction(aMap), MapFunctionFactory
                    .getResultFunction(), new DefaultGoalTest(initialState),
                    new MapStepCostFunction(aMap));
        }

        //
        // START Interface BidrectionalProblem
        public Problem getOriginalProblem()
        {
            return this;
        }

        public Problem getReverseProblem()
        {
            return reverseProblem;
        }
        // END Interface BirectionalProblem
        //
    }
}