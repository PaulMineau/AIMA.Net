namespace AIMA.Core.Environment.Vacuum
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Agent.Impl;


    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class ModelBasedReflexVacuumAgent : AbstractAgent
    {

        private const String ATTRIBUTE_CURRENT_LOCATION = "currentLocation";
        private const String ATTRIBUTE_CURRENT_STATE = "currentState";
        private const String ATTRIBUTE_STATE_LOCATION_A = "stateLocationA";
        private const String ATTRIBUTE_STATE_LOCATION_B = "stateLocationB";

        /* TODO: looks like the following are function delegates, replace
        public ModelBasedReflexVacuumAgent() {
            super(new ModelBasedReflexAgentProgram() {
                protected override void init() {
                    setState(new DynamicState());
                    setRules(getRuleSet());
                }

                protected DynamicState updateState(DynamicState state,
                        Action anAction, Percept percept, Model model) {

                    VacuumEnvPercept vep = (VacuumEnvPercept) percept;

                    state.setAttribute(ATTRIBUTE_CURRENT_LOCATION, vep
                            .getAgentLocation());
                    state.setAttribute(ATTRIBUTE_CURRENT_STATE, vep
                            .getLocationState());
                    // Keep track of the state of the different locations
                    if (VacuumEnvironment.LOCATION_A == vep.getAgentLocation()) {
                        state.setAttribute(ATTRIBUTE_STATE_LOCATION_A, vep
                                .getLocationState());
                    } else {
                        state.setAttribute(ATTRIBUTE_STATE_LOCATION_B, vep
                                .getLocationState());
                    }
                    return state;
                }
            });
         * */


        //
        // PRIVATE METHODS
        //
        private static HashSet<Rule> getRuleSet()
        {
            // Note: Using a LinkedHashSet so that the iteration order (i.e. implied
            // precedence) of rules can be guaranteed.
            HashSet<Rule> rules = new LinkedHashSet<Rule>();

            rules.Add(new Rule(new ANDCondition(new EQUALCondition(
                    ATTRIBUTE_STATE_LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new EQUALCondition(
                    ATTRIBUTE_STATE_LOCATION_B,
                    VacuumEnvironment.LocationState.Clean)), NoOpAction.NO_OP));
            rules.Add(new Rule(new EQUALCondition(ATTRIBUTE_CURRENT_STATE,
                    VacuumEnvironment.LocationState.Dirty),
                    VacuumEnvironment.ACTION_SUCK));
            rules.Add(new Rule(new EQUALCondition(ATTRIBUTE_CURRENT_LOCATION,
                    VacuumEnvironment.LOCATION_A),
                    VacuumEnvironment.ACTION_MOVE_RIGHT));
            rules.Add(new Rule(new EQUALCondition(ATTRIBUTE_CURRENT_LOCATION,
                    VacuumEnvironment.LOCATION_B),
                    VacuumEnvironment.ACTION_MOVE_LEFT));

            return rules;
        }
    }
}
