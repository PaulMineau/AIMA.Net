namespace AIMA.Core.Environment.Vacuum
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AIMA.Core.Agent.Impl.DynamicPercept;

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public class VacuumEnvPercept : DynamicPercept
    {
        public const String ATTRIBUTE_AGENT_LOCATION = "agentLocation";
        public const String ATTRIBUTE_STATE = "state";

        public VacuumEnvPercept(String agentLocation,
                VacuumEnvironment.LocationState state)
        {
            setAttribute(ATTRIBUTE_AGENT_LOCATION, agentLocation);
            setAttribute(ATTRIBUTE_STATE, state);
        }

        public String getAgentLocation()
        {
            return (String)getAttribute(ATTRIBUTE_AGENT_LOCATION);
        }

        public VacuumEnvironment.LocationState getLocationState()
        {
            return (VacuumEnvironment.LocationState)getAttribute(ATTRIBUTE_STATE);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.append("[");
            sb.append(getAgentLocation());
            sb.append(", ");
            sb.append(getLocationState());
            sb.append("]");

            return sb.ToString();
        }
    }
}
