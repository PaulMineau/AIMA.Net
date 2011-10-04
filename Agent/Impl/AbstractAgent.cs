namespace AIMA.Core.Agent.Impl
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Agent;

/**
 * @author Ravi Mohan
 * @author Ciaran O'Reilly
 */
public abstract class AbstractAgent : Agent {

	protected AgentProgram program;
	private bool alive = true;

	public AbstractAgent() {

	}

	public AbstractAgent(AgentProgram aProgram) {
		program = aProgram;
	}

	//
	// START-Agent
	public Action execute(Percept p) {
		if (null != program) {
			return program.execute(p);
		}
		return NoOpAction.NO_OP;
	}

	public bool isAlive() {
		return alive;
	}

	public void setAlive(bool alive) {
		this.alive = alive;
	}

	// END-Agent
	//
}
}