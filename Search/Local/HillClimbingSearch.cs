namespace AIMA.Core.Search.Local
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Agent;
using AIMA.Core.Search.Framework;
using AIMA.Core.Util;

/**
 * Artificial Intelligence A Modern Approach (3rd Edition): Figure 4.2, page 122.
 * 
 * <code>
 * function HILL-CLIMBING(problem) returns a state that is a local maximum
 *                    
 *   current <- MAKE-NODE(problem.INITIAL-STATE)
 *   loop do
 *     neighbor <- a highest-valued successor of current
 *     if neighbor.VALUE <= current.VALUE then return current.STATE
 *     current <- neighbor
 * </code>
 * Figure 4.2 The hill-climbing search algorithm, which is the most basic local search technique. 
 * At each step the current node is replaced by the best neighbor; in this version, that means 
 * the neighbor with the highest VALUE, but if a heuristic cost estimate h is used, we would find 
 * the neighbor with the lowest h.
 */

/**
 * @author Ravi Mohan
 * 
 */
public class HillClimbingSearch : NodeExpander , Search {

	public enum SearchOutcome {
		FAILURE, SOLUTION_FOUND
	};

	private HeuristicFunction hf = null;

	private SearchOutcome outcome = SearchOutcome.FAILURE;

	private Object lastState = null;

	public HillClimbingSearch(HeuristicFunction hf) {
		this.hf = hf;
	}

	// function HILL-CLIMBING(problem) returns a state that is a local maximum
	public List<Action> search(Problem p) {
		clearInstrumentation();
		outcome = SearchOutcome.FAILURE;
		lastState = null;
		// current <- MAKE-NODE(problem.INITIAL-STATE)
		Node current = new Node(p.getInitialState());
		Node neighbor = null;
		// loop do
		while (!CancelableThread.currIsCanceled()) {
			List<Node> children = expandNode(current, p);
			// neighbor <- a highest-valued successor of current
			neighbor = getHighestValuedNodeFrom(children, p);

			// if neighbor.VALUE <= current.VALUE then return current.STATE
			if ((neighbor == null) || (getValue(neighbor) <= getValue(current))) {
				if (SearchUtils.isGoalState(p, current)) {
					outcome = SearchOutcome.SOLUTION_FOUND;
				}
				lastState = current.getState();
				return SearchUtils.actionsFromNodes(current.getPathFromRoot());
			}
			// current <- neighbor
			current = neighbor;
		}
		return new List<Action>();
	}

	public SearchOutcome getOutcome() {
		return outcome;
	}

	public Object getLastSearchState() {
		return lastState;
	}

	//
	// PRIVATE METHODS
	//

	private Node getHighestValuedNodeFrom(List<Node> children, Problem p) {
		double highestValue = Double.NEGATIVE_INFINITY;
		Node nodeWithHighestValue = null;
		for (int i = 0; i < children.Count; i++) {
			Node child = (Node) children.get(i);
			double value = getValue(child);
			if (value > highestValue) {
				highestValue = value;
				nodeWithHighestValue = child;
			}
		}
		return nodeWithHighestValue;
	}

	private double getValue(Node n) {
		// assumption greater heuristic value =>
		// HIGHER on hill; 0 == goal state;
		return -1 * hf.h(n.getState());
	}
}
}