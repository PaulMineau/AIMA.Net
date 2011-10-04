namespace AIMA.Core.Environment.Map
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Util.DataStructure;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class MapEnvironmentState : EnvironmentState
    {
        private Dictionary<Agent, Pair<String, Double>> agentLocationAndTravelDistance = new Dictionary<Agent, Pair<String, Double>>();

        public MapEnvironmentState()
        {

        }

        public String getAgentLocation(Agent a)
        {
            Pair<String, Double> locAndTDistance = agentLocationAndTravelDistance
                    .get(a);
            if (null == locAndTDistance)
            {
                return null;
            }
            return locAndTDistance.getFirst();
        }

        public Double getAgentTravelDistance(Agent a)
        {
            Pair<String, Double> locAndTDistance = agentLocationAndTravelDistance
                    .get(a);
            if (null == locAndTDistance)
            {
                return null;
            }
            return locAndTDistance.getSecond();
        }

        public void setAgentLocationAndTravelDistance(Agent a, String location,
                Double travelDistance)
        {
            agentLocationAndTravelDistance.put(a, new Pair<String, Double>(
                    location, travelDistance));
        }
    }
}
