namespace AIMA.Core.Agent.Impl.AProg
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Agent.Action;
using AIMA.Core.Agent.AgentProgram;
using AIMA.Core.Agent.Percept;
using AIMA.Core.Agent.Impl.NoOpAction;
using AIMA.Core.Util.DataStructure.Table;

/**
 * Artificial Intelligence A Modern Approach (3rd Edition): Figure 2.7, page 47.
 * <code>
 * function TABLE-DRIVEN-AGENT(percept) returns an action
 *   persistent: percepts, a sequence, initially empty
 *               table, a table of actions, indexed by percept sequences, initially fully specified
 *           
 *   append percept to end of percepts
 *   action <- LOOKUP(percepts, table)
 *   return action
 * </code>
 * Figure 2.7 The TABLE-DRIVEN-AGENT program is invoked for each new percept and 
 * returns an action each time. It retains the complete percept sequence in memory.
 */

/**
 * @author Ciaran O'Reilly
 * 
 */
public class TableDrivenAgentProgram : AgentProgram {
	private List<Percept> percepts = new List<Percept>();

	private Table<List<Percept>, String, Action> table;

	private const String ACTION = "action";

	// persistent: percepts, a sequence, initially empty
	// table, a table of actions, indexed by percept sequences, initially fully
	// specified
	public TableDrivenAgentProgram(
			Map<List<Percept>, Action> perceptSequenceActions) {

		List<List<Percept>> rowHeaders = new List<List<Percept>>(
				perceptSequenceActions.keySet());

		List<String> colHeaders = new List<String>();
		colHeaders.Add(ACTION);

		table = new Table<List<Percept>, String, Action>(rowHeaders, colHeaders);

		foreach (List<Percept> row in rowHeaders) {
			table.set(row, ACTION, perceptSequenceActions.get(row));
		}
	}

	//
	// START-AgentProgram

	// function TABLE-DRIVEN-AGENT(percept) returns an action
	public Action execute(Percept percept) {
		// append percept to end of percepts
		percepts.Add(percept);

		// action <- LOOKUP(percepts, table)
		// return action
		return lookupCurrentAction();
	}

	// END-AgentProgram
	//

	//
	// PRIVATE METHODS
	//
	private Action lookupCurrentAction() {
		Action action = null;

		action = table.get(percepts, ACTION);
		if (null == action) {
			action = NoOpAction.NO_OP;
		}

		return action;
	}
}
}