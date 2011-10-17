namespace AIMA.Core.Environment.Map
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core;
    using AIMA.Core.Util.DataStructure;
    using AIMA.Core.Agent;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class MapEnvironmentState : EnvironmentState
    {
        private Dictionary<IAgent, Pair<String, Double>> agentLocationAndTravelDistance = new Dictionary<IAgent, Pair<String, Double>>();

        public MapEnvironmentState()
        {

        }

        public String getAgentLocation(IAgent a)
        {
            Pair<String, Double> locAndTDistance = agentLocationAndTravelDistance
                    [a];
            if (null == locAndTDistance)
            {
                return null;
            }
            return locAndTDistance.getFirst();
        }

        public Double getAgentTravelDistance(IAgent a)
        {
            Pair<String, Double> locAndTDistance = agentLocationAndTravelDistance
                    [a];
            if (null == locAndTDistance)
            {
                return Double.MinValue;
            }
            return locAndTDistance.getSecond();
        }

        public void setAgentLocationAndTravelDistance(IAgent a, String location,
                Double travelDistance)
        {
            agentLocationAndTravelDistance.Add(a, new Pair<String, Double>(
                    location, travelDistance));
        }
    }
}
