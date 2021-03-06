using System;
using System.Collections.Generic;
using AIMA.Core.Agent;
using AIMA.Core.Agent.Impl;
using AIMA.Core.Agent.Impl.AProg.SimpleRule;

namespace AIMA.Core.Agent.Impl.AProg
{
/**
 * Artificial Intelligence A Modern Approach (3rd Edition): Figure 2.10, page 49.
 * <code>
 * function SIMPLE-RELEX-AGENT(percept) returns an action
 *   persistent: rules, a set of condition-action rules
 *   
 *   state  <- INTERPRET-INPUT(percept);
 *   rule   <- RULE-MATCH(state, rules);
 *   action <- rule.ACTION;
 *   return action
 * </code>
 * Figure 2.10 A simple reflex agent. It acts according to a rule whose condition matches
 * the current state, as defined by the percept.
 */

/**
 * @author Ciaran O'Reilly
 * 
 */
public class SimpleReflexAgentProgram : AgentProgram {
	//
	// persistent: rules, a set of condition-action rules
    private Set<Rule> rules;

	public SimpleReflexAgentProgram(Set<Rule> aRuleSet) {
		rules = aRuleSet;
	}

	//
	// START-AgentProgram

	// function SIMPLE-RELEX-AGENT(percept) returns an action
	public Action execute(Percept percept) {

		// state <- INTERPRET-INPUT(percept);
		ObjectWithDynamicAttributes state = interpretInput(percept);
		// rule <- RULE-MATCH(state, rules);
		Rule rule = ruleMatch(state, rules);
		// action <- rule.ACTION;
		// return action
		return ruleAction(rule);
	}

	// END-AgentProgram
	//

	//
	// PROTECTED METHODS
	//
	protected ObjectWithDynamicAttributes interpretInput(Percept p) {
		return (DynamicPercept) p;
	}

	protected Rule ruleMatch(ObjectWithDynamicAttributes state,
			Set<Rule> rulesSet) {
		foreach (Rule r in rulesSet) {
			if (r.evaluate(state)) {
				return r;
			}
		}
		return null;
	}

	protected Action ruleAction(Rule r) {
		return null == r ? NoOpAction.NO_OP : r.getAction();
	}
}
}