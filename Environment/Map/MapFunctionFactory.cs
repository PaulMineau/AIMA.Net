namespace AIMA.Core.Environment.Map
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Agent.Impl;
    using AIMA.Core.Search.Framework;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class MapFunctionFactory
    {
        private static ResultFunction _resultFunction = null;
        private static PerceptToStateFunction _perceptToStateFunction = null;

        public static ActionsFunction getActionsFunction(Map aMap)
        {
            return new MapActionsFunction(aMap);
        }

        public static ResultFunction getResultFunction()
        {
            if (null == _resultFunction)
            {
                _resultFunction = new MapResultFunction();
            }
            return _resultFunction;
        }

        private static class MapActionsFunction : ActionsFunction
        {
            private Map map = null;

            public MapActionsFunction(Map aMap)
            {
                map = aMap;
            }

            public HashSet<Action> actions(Object state)
            {
                HashSet<Action> actions = new LinkedHashSet<Action>();
                String location = state.ToString();

                List<String> linkedLocations = map.getLocationsLinkedTo(location);
                foreach (String linkLoc in linkedLocations)
                {
                    actions.Add(new MoveToAction(linkLoc));
                }

                return actions;
            }
        }

        public static PerceptToStateFunction getPerceptToStateFunction()
        {
            if (null == _perceptToStateFunction)
            {
                _perceptToStateFunction = new MapPerceptToStateFunction();
            }
            return _perceptToStateFunction;
        }

        private static class MapResultFunction : ResultFunction
        {
            public MapResultFunction()
            {
            }

            public Object result(Object s, Action a)
            {

                if (a is MoveToAction)
                {
                    MoveToAction mta = (MoveToAction)a;

                    return mta.getToLocation();
                }

                // The Action is not understood or is a NoOp
                // the result will be the current state.
                return s;
            }
        }

        private static class MapPerceptToStateFunction :
                PerceptToStateFunction
        {
            public Object getState(Percept p)
            {
                return ((DynamicPercept)p)
                        .getAttribute(DynAttributeNames.PERCEPT_IN);
            }
        }
    }
}
