namespace CosmicFlow.AIMA.Core.Probability
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
/**
 * @author Ravi Mohan
 * 
 */
public class Query {

	private String queryVariable;

	private Dictionary<String, bool> evidenceVariables;

	public Query(String queryVariable, String[] evidenceVariables,
			bool[] evidenceValues) {
		this.queryVariable = queryVariable;
		this.evidenceVariables = new Dictionary<String, bool>();
		for (int i = 0; i < evidenceVariables.Length; i++) {
			this.evidenceVariables.Add(evidenceVariables[i], 
					evidenceValues[i]);
		}
	}

	public Dictionary<String, bool> getEvidenceVariables() {
		return evidenceVariables;
	}

	public String getQueryVariable() {
		return queryVariable;
	}
}
}