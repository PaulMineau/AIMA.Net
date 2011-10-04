namespace CosmicFlow.AIMA.Core.Learning.Neural
{
    using System;
    using System.Collections.Generic;
/**
 * @author Ravi Mohan
 * 
 */
public class IrisNNDataSet : NNDataSet {

	public override void setTargetColumns() {
		// assumed that data from file has been pre processed
		// TODO this should be
		// somewhere else,in the
		// super class.
		// Type != class Aargh! I want more
		// powerful type systems
		targetColumnNumbers = new List<int>();
		int size = nds[0].Count;
		targetColumnNumbers.Add(size - 1); // last column
		targetColumnNumbers.Add(size - 2); // last but one column
		targetColumnNumbers.Add(size - 3); // and the one before that
	}
}
}