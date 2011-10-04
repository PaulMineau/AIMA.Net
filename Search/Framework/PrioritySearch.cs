namespace AIMA.Core.Search.Framework
{
    using System;
    using System.Collections.Generic;
using AIMA.Core.Agent;
using AIMA.Core.Util.DataStructure;

/**
 * @author Ravi Mohan
 * 
 */
public abstract class PrioritySearch : Search {
	protected QueueSearch search;

	public List<Action> search(Problem p) {
		return search.search(p, new PriorityQueue<Node>(5, getComparator()));
	}

	public Metrics getMetrics() {
		return search.getMetrics();
	}

	//
	// PROTECTED METHODS
	//
	protected abstract Comparator<Node> getComparator();
}
}