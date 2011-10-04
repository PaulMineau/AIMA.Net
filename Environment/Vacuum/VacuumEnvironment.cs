namespace AIMA.Core.Environment.Vacuum
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Agent.Impl;

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public class VacuumEnvironment : AbstractEnvironment
    {
        // Allowable Actions within the Vaccum Environment
        public const Action ACTION_MOVE_LEFT = new DynamicAction("Left");
        public const Action ACTION_MOVE_RIGHT = new DynamicAction("Right");
        public const Action ACTION_SUCK = new DynamicAction("Suck");
        public const String LOCATION_A = "A";
        public const String LOCATION_B = "B";

        public enum LocationState
        {
            Clean, Dirty
        };

        //
        protected VacuumEnvironmentState envState = null;
        protected bool isDone = false;

        public VacuumEnvironment()
        {
            Random r = new Random();
            envState = new VacuumEnvironmentState(
                    0 == r.nextInt(2) ? LocationState.Clean : LocationState.Dirty,
                    0 == r.nextInt(2) ? LocationState.Clean : LocationState.Dirty);
        }

        public VacuumEnvironment(LocationState locAState, LocationState locBState)
        {
            envState = new VacuumEnvironmentState(locAState, locBState);
        }

        public override EnvironmentState getCurrentState()
        {
            return envState;
        }

        public override EnvironmentState executeAction(Agent a, Action agentAction)
        {

            if (ACTION_MOVE_RIGHT == agentAction)
            {
                envState.setAgentLocation(a, LOCATION_B);
                updatePerformanceMeasure(a, -1);
            }
            else if (ACTION_MOVE_LEFT == agentAction)
            {
                envState.setAgentLocation(a, LOCATION_A);
                updatePerformanceMeasure(a, -1);
            }
            else if (ACTION_SUCK == agentAction)
            {
                if (LocationState.Dirty == envState.getLocationState(envState
                        .getAgentLocation(a)))
                {
                    envState.setLocationState(envState.getAgentLocation(a),
                            LocationState.Clean);
                    updatePerformanceMeasure(a, 10);
                }
            }
            else if (agentAction.isNoOp())
            {
                // In the Vacuum Environment we consider things done if
                // the agent generates a NoOp.
                isDone = true;
            }

            return envState;
        }


        public override Percept getPerceptSeenBy(Agent anAgent)
        {
            String agentLocation = envState.getAgentLocation(anAgent);
            return new VacuumEnvPercept(agentLocation, envState
                    .getLocationState(agentLocation));
        }

        public override bool isDone()
        {
            return super.isDone() || isDone;
        }

        public override void addAgent(Agent a)
        {
            int idx = new Random().nextInt(2);
            envState.setAgentLocation(a, idx == 0 ? LOCATION_A : LOCATION_B);
            super.AddAgent(a);
        }

        public void addAgent(Agent a, String location)
        {
            // Ensure the agent state information is tracked before
            // adding to super, as super will notify the registered
            // EnvironmentViews that is was added.
            envState.setAgentLocation(a, location);
            super.AddAgent(a);
        }

        public LocationState getLocationState(String location)
        {
            return envState.getLocationState(location);
        }

        public String getAgentLocation(Agent a)
        {
            return envState.getAgentLocation(a);
        }
    }
}