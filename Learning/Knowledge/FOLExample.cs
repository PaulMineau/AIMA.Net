namespace CosmicFlow.AIMA.Core.Learning.Knowledge
{
    using System;
    using System.Collections.Generic;
using CosmicFlow.AIMA.Core.Learning.Framework;
using CosmicFlow.AIMA.Core.Logic.FOL;
using CosmicFlow.AIMA.Core.Logic.FOL.Parsing.AST;

/**
 * @author Ciaran O'Reilly
 * 
 */
public class FOLExample {
	private FOLDataSetDomain folDSDomain = null;
	private Example example = null;
	private int egNo = 0;
	//
	private Constant ithExampleConstant = null;
	private Sentence classification = null;
	private Sentence description = null;

	//
	// PUBLIC METHODS
	//
	public FOLExample(FOLDataSetDomain folDSDomain, Example example, int egNo) {
		this.folDSDomain = folDSDomain;
		this.example = example;
		this.egNo = egNo;
		constructFOLEg();
	}

	public int getExampleNumber() {
		return egNo;
	}

	public Sentence getClassification() {
		return classification;
	}

	public Sentence getDescription() {
		return description;
	}

	public override String ToString() {
		return classification.ToString() + " " + Connectors.AND + " "
				+ description.ToString();
	}

	//
	// PRIVATE METHODS
	//
	private void constructFOLEg() {
		ithExampleConstant = new Constant(folDSDomain.getExampleConstant(egNo));

		List<Term> terms = new List<Term>();
		terms.Add(ithExampleConstant);
		// Create the classification sentence
		classification = new Predicate(folDSDomain.getGoalPredicateName(),
				terms);
		if (!example.getAttributeValueAsString(
				folDSDomain.getDataSetTargetName()).Equals(
				folDSDomain.getTrueGoalValue())) {
			// if not true then needs to be a Not sentence
			classification = new NotSentence(classification);
		}

		// Create the description sentence
		List<Sentence> descParts = new List<Sentence>();
		foreach (String dname in folDSDomain.getDescriptionDataSetNames()) {
			String foldDName = folDSDomain.getFOLName(dname);
			terms = new List<Term>();
			terms.Add(ithExampleConstant);
			// If multivalued becomes a two place predicate
			// e.g: Patrons(X1, Some)
			// otherwise: Hungry(X1) or ~ Hungry(X1)
			// see pg 769 of AIMA
			Sentence part = null;
			if (folDSDomain.isMultivalued(dname)) {
				terms.Add(new Constant(folDSDomain.getFOLName(example
						.getAttributeValueAsString(dname))));
				part = new Predicate(foldDName, terms);
			} else {
				part = new Predicate(foldDName, terms);
				// Need to determine if false
				if (!folDSDomain.getTrueGoalValue().Equals(
						example.getAttributeValueAsString(dname))) {
					part = new NotSentence(part);
				}
			}
			descParts.Add(part);
		}
		if (descParts.Count == 1) {
			description = descParts[0];
		} else if (descParts.Count > 1) {
			description = new ConnectedSentence(Connectors.AND, descParts
					[0], descParts[1]);
			for (int i = 2; i < descParts.Count; i++) {
				description = new ConnectedSentence(Connectors.AND,
						description, descParts[i]);
			}
		}
	}
}
}
