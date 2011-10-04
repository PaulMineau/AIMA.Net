namespace AIMA.Core.Environment.Vacuum
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Agent;
using AIMA.Core.Agent.Impl;

/**
 * Artificial Intelligence A Modern Approach (3rd Edition): Figure 2.8, page 48.
 * <code>
 * function REFLEX-VACUUM-AGENT([location, status]) returns an action
 *   
 *   if status = Dirty then return Suck
 *   else if location = A then return Right
 *   else if location = B then return Left
 * </code>
 * Figure 2.8 The agent program for a simple reflex agent in the two-state vacuum environment.
 * This program : the action function tabulated in Figure 2.3.
 */

/**
 * @author Ciaran O'Reilly
 * 
 */
public class ReflexVacuumAgent : AbstractAgent {

    /* TODO: replace the dynamic class
	public ReflexVacuumAgent() {
		super(new AgentProgram() {
			// function REFLEX-VACUUM-AGENT([location, status]) returns an
			// action
			public Action execute(Percept percept) {
				VacuumEnvPercept vep = (VacuumEnvPercept) percept;

				// if status = Dirty then return Suck
				if (VacuumEnvironment.LocationState.Dirty == vep
						.getLocationState()) {
					return VacuumEnvironment.ACTION_SUCK;
					// else if location = A then return Right
				} else if (VacuumEnvironment.LOCATION_A == vep
						.getAgentLocation()) {
					return VacuumEnvironment.ACTION_MOVE_RIGHT;
				} else if (VacuumEnvironment.LOCATION_B == vep
						.getAgentLocation()) {
					// else if location = B then return Left
					return VacuumEnvironment.ACTION_MOVE_LEFT;
				}

				// Note: This should not be returned if the
				// environment is correct
				return NoOpAction.NO_OP;
			}
		});
     * */
	}
}
