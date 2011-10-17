namespace AIMA.Core.Agent.Impl.AProg
{
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Agent.Impl;
    using AIMA.Core.Util.DataStructure;

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

    private Table<List<Percept>, System.String, Action> table;

	private const System.String ACTION = "action";

	// persistent: percepts, a sequence, initially empty
	// table, a table of actions, indexed by percept sequences, initially fully
	// specified
	public TableDrivenAgentProgram(
			Map<List<Percept>, Action> perceptSequenceActions) {

		List<List<Percept>> rowHeaders = new List<List<Percept>>(
				perceptSequenceActions.Keys);

        List<System.String> colHeaders = new List<System.String>();
		colHeaders.Add(ACTION);

        table = new Table<List<Percept>, System.String, Action>(rowHeaders, colHeaders);

		foreach (List<Percept> row in rowHeaders) {
			table.set(row, ACTION, perceptSequenceActions[row]);
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
        if (table.get(percepts, ACTION) != null)
        {
            action = table.get(percepts, ACTION);
        }
        else
        {
			action = NoOpAction.NO_OP;
		}

		return action;
	}
}
}