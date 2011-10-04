namespace CosmicFlow.AIMA.Core.Learning.Learners
{
    using System;
    using System.Collections.Generic;
using CosmicFlow.AIMA.Core.Learning.Framework;
using CosmicFlow.AIMA.Core.Learning.Inductive;

/**
 * @author Ravi Mohan
 * 
 */
public class StumpLearner : DecisionTreeLearner {

	public StumpLearner(DecisionTree sl, String unable_to_classify) : base(sl, unable_to_classify) {
		
	}

	public override void train(DataSet ds) {
		// System.Console.WriteLine("Stump learner training");
		// do nothing the stump is not inferred from the dataset
	}
}
}
