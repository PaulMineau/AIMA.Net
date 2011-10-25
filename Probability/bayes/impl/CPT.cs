using System;
using System.Collections.Generic;
using AIMA;
using AIMA.Probability;
using AIMA.Probability.Bayes;
using AIMA.Probability.Domain;
using AIMA.Probability.Util;

namespace AIMA.Probability.Bayes.Impl
{

/**
 * Default implementation of the ConditionalProbabilityTable interface.
 * 
 * @author Ciaran O'Reilly
 * 
 */
public class CPT : ConditionalProbabilityTable {
	private RandomVariable on = null;
	private Set<RandomVariable> parents = new Set<RandomVariable>();
	private ProbabilityTable table = null;
	private List<Object> onDomain = new List<Object>();

	public CPT(RandomVariable on, double[] values,
			params RandomVariable [] conditionedOn) {
		this.on = on;
		if (null == conditionedOn) {
			conditionedOn = new RandomVariable[0];
		}
		RandomVariable[] tableVars = new RandomVariable[conditionedOn.Length + 1];
		for (int i = 0; i < conditionedOn.Length; i++) {
			tableVars[i] = conditionedOn[i];
			parents.add(conditionedOn[i]);
		}
		tableVars[conditionedOn.Length] = on;
		table = new ProbabilityTable(values, tableVars);
		onDomain.AddRange(((FiniteDomain) on.getDomain()).getPossibleValues());

		checkEachRowTotalsOne();
	}

	public double probabilityFor(params Object [] values) {
		return table.getValue(values);
	}

	//
	// START-ConditionalProbabilityDistribution


	public RandomVariable getOn() {
		return on;
	}


	public Set<RandomVariable> getParents() {
		return parents;
	}


	public Set<RandomVariable> getFor() {
		return table.getFor();
	}


	public bool contains(RandomVariable rv) {
		return table.contains(rv);
	}

	public double getValue(params Object[] eventValues) {
		return table.getValue(eventValues);
	}


	public double getValue(params AssignmentProposition[] eventValues) {
		return table.getValue(eventValues);
	}

	public Object getSample(double probabilityChoice, params  Object[] parentValues) {
		return ProbUtil.sample(probabilityChoice, on,
				getConditioningCase(parentValues).getValues());
	}

	public Object getSample(double probabilityChoice,
			params AssignmentProposition[] parentValues) {
		return ProbUtil.sample(probabilityChoice, on,
				getConditioningCase(parentValues).getValues());
	}

	// END-ConditionalProbabilityDistribution
	//

	//
	// START-ConditionalProbabilityTable

	public CategoricalDistribution getConditioningCase(params Object[] parentValues) {
		if (parentValues.Length != parents.Count) {
			throw new ArgumentException(
					"The number of parent value arguments ["
							+ parentValues.Length
							+ "] is not equal to the number of parents ["
							+ parents.Count + "] for this CPT.");
		}
		AssignmentProposition[] aps = new AssignmentProposition[parentValues.Length];
		int idx = 0;
		foreach (RandomVariable parentRV in parents) {
			aps[idx] = new AssignmentProposition(parentRV, parentValues[idx]);
			idx++;
		}

		return getConditioningCase(aps);
	}


	public CategoricalDistribution getConditioningCase(
			params AssignmentProposition[] parentValues) {
		if (parentValues.Length != parents.Count) {
			throw new ArgumentException(
					"The number of parent value arguments ["
							+ parentValues.Length
							+ "] is not equal to the number of parents ["
							+ parents.Count + "] for this CPT.");
		}
		 ProbabilityTable cc = new ProbabilityTable(getOn());
        //ProbabilityTable.Iterator  pti = new ProbabilityTable.Iterator() {
        //    private int idx = 0;


        //    public void iterate(Map<RandomVariable, Object> possibleAssignment,
        //            double probability) {
        //        cc.getValues()[idx] = probability;
        //        idx++;
        //    }
        //};
        //table.iterateOverTable(pti, parentValues);
        // TODO: this is screwed up
		return cc;
	}

	public Factor getFactorFor(params AssignmentProposition[] evidence) {
		Set<RandomVariable> fofVars = new Set<RandomVariable>(
				table.getFor());
		foreach (AssignmentProposition ap in evidence) {
			fofVars.Remove(ap.getTermVariable());
		}
		 ProbabilityTable fof = new ProbabilityTable(new List<RandomVariable>(fofVars));
		// Otherwise need to iterate through the table for the
		// non evidence variables.
		 Object[] termValues = new Object[fofVars.Count];
        //ProbabilityTable.Iterator di = new ProbabilityTable.Iterator() {
        //    public void iterate(Map<RandomVariable, Object> possibleWorld,
        //            double probability) {
        //        if (0 == termValues.length) {
        //            fof.getValues()[0] += probability;
        //        } else {
        //            int i = 0;
        //            for (RandomVariable rv : fof.getFor()) {
        //                termValues[i] = possibleWorld.get(rv);
        //                i++;
        //            }
        //            fof.getValues()[fof.getIndex(termValues)] += probability;
        //        }
        //    }
        //};
		//table.iterateOverTable(di, evidence);
        // TODO: another screwed up iterator
		return fof;
	}

	// END-ConditionalProbabilityTable
	//

	//
	// PRIVATE METHODS
	//
	private void checkEachRowTotalsOne() {
        //ProbabilityTable.Iterator di = new ProbabilityTable.Iterator() {
        //    private int rowSize = onDomain.size();
        //    private int iterateCnt = 0;
        //    private double rowProb = 0;

        //    public void iterate(Map<RandomVariable, Object> possibleWorld,
        //            double probability) {
        //        iterateCnt++;
        //        rowProb += probability;
        //        if (iterateCnt % rowSize == 0) {
        //            if (Math.abs(1 - rowProb) > ProbabilityModel.DEFAULT_ROUNDING_THRESHOLD) {
        //                throw new IllegalArgumentException("Row "
        //                        + (iterateCnt / rowSize)
        //                        + " of CPT does not sum to 1.0.");
        //            }
        //            rowProb = 0;
        //        }
        //    }
        //};

        //table.iterateOverTable(di);
        // TODO:
	}
}
}
