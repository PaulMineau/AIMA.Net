using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AIMA.Core.Util;
using AIMA.Core.Util.Math;
using AIMA.Probability;
using AIMA.Probability.Domain;

namespace AIMA.Probability.Util
{

    /**
     * A Utility Class for associating values with a set of finite Random Variables.
     * This is also the default implementation of the CategoricalDistribution and
     * Factor interfaces (as they are essentially dependent on the same underlying
     * data structures).
     * 
     * @author Ciaran O'Reilly
     */

    public class ProbabilityTable : CategoricalDistribution
    {
        // PAUL: adding this factor, had to remove the interface implementation as in C# we're forced to make the Factor Interface a class
        // 
        private Factor factor;

        private double[] values = null;
        //
        private Map<RandomVariable, RVInfo> randomVarInfo = new LinkedHashMap<RandomVariable, RVInfo>();
        private int[] radices = null;
        private MixedRadixNumber queryMRN = null;
        //
        private String _toString = null;
        private double sum = -1;

        /**
         * Interface to be implemented by an object/algorithm that wishes to iterate
         * over the possible assignments for the random variables comprising this
         * table.
         * 
         * @see ProbabilityTable#iterateOverTable(Iterator)
         * @see ProbabilityTable#iterateOverTable(Iterator,
         *      AssignmentProposition...)
         */

        public interface ProbabilityTableIterator
        {
            /**
             * Called for each possible assignment for the Random Variables
             * comprising this ProbabilityTable.
             * 
             * @param possibleAssignment
             *            a possible assignment, &omega;, of variable/value pairs.
             * @param probability
             *            the probability associated with &omega;
             */

            void iterate(Map<RandomVariable, Object> possibleAssignment,
                         double probability);
        }

        public ProbabilityTable(List<RandomVariable> vars)
            : this(vars.ToArray())
        {

        }

        public ProbabilityTable(params RandomVariable[] vars)
            : this(new double[ProbUtil.expectedSizeOfProbabilityTable(vars)], vars)
        {
            ;
        }

        public ProbabilityTable(double[] vals, params RandomVariable[] vars)
        {
            if (null == vals)
            {
                throw new ArgumentException("Values must be specified");
            }
            if (vals.Length != ProbUtil.expectedSizeOfProbabilityTable(vars))
            {
                throw new ArgumentException("ProbabilityTable of Length "
                                            + values.Length + " is not the correct size, should be "
                                            + ProbUtil.expectedSizeOfProbabilityTable(vars)
                                            + " in order to represent all possible combinations.");
            }
            if (null != vars)
            {
                foreach (RandomVariable rv in vars)
                {
                    // Track index information relevant to each variable.
                    randomVarInfo.Add(rv, new RVInfo(rv));
                }
            }

            values = new double[vals.Length];
            Array.Copy(vals, 0, values, 0, vals.Length);

            radices = createRadixs(randomVarInfo);

            if (radices.Length > 0)
            {
                queryMRN = new MixedRadixNumber(0, radices);
            }
        }

        public int size()
        {
            return values.Length;
        }

        //
        // START-ProbabilityDistribution
        public Set<RandomVariable> getFor()
        {
            RandomVariable[] arr = new RandomVariable[randomVarInfo.Keys.Count];
            randomVarInfo.Keys.CopyTo(arr, 0);
            return new Set<RandomVariable>(new List<RandomVariable>(arr));
        }

        public bool contains(RandomVariable rv)
        {
            return randomVarInfo.ContainsKey(rv);
        }


        public double getValue(params Object[] assignments)
        {
            return values[getIndex(assignments)];
        }

        public double getValue(params AssignmentProposition[] assignments)
        {
            if (assignments.Length != randomVarInfo.Count)
            {
                throw new ArgumentException(
                    "Assignments passed in is not the same size as variables making up probability table.");
            }
            int[] radixValues = new int[assignments.Length];
            foreach (AssignmentProposition ap in assignments)
            {
                RVInfo rvInfo = randomVarInfo.get(ap.getTermVariable());
                if (null == rvInfo)
                {
                    throw new ArgumentException(
                        "Assignment passed for a variable that is not part of this probability table:"
                        + ap.getTermVariable());
                }
                radixValues[rvInfo.getRadixIdx()] = rvInfo.getIdxForDomain(ap
                                                                               .getValue());
            }
            return values[(int) queryMRN.getCurrentValueFor(radixValues)];
        }

        // END-ProbabilityDistribution
        //

        //
        // START-CategoricalDistribution
        public override double[] getValues()
        {
            return values;
        }


        public override void setValue(int idx, double value)
        {
            values[idx] = value;
            reinitLazyValues();
        }

        public override double getSum()
        {
            if (-1 == sum)
            {
                sum = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    sum += values[i];
                }
            }
            return sum;
        }

        public override CategoricalDistribution normalize()
        {
            double s = getSum();
            if (s != 0 && s != 1.0)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i]/s;
                }
                reinitLazyValues();
            }
            return this;
        }

        public override int getIndex(params Object[] assignments)
        {
            if (assignments.Length != randomVarInfo.size())
            {
                throw new IllegalArgumentException(
                    "Assignments passed in is not the same size as variables making up the table.");
            }
            int[] radixValues = new int[assignments.Length];
            int i = 0;
            foreach (RVInfo rvInfo in randomVarInfo.values())
            {
                radixValues[rvInfo.getRadixIdx()] = rvInfo
                    .getIdxForDomain(assignments[i]);
                i++;
            }

            return (int) queryMRN.getCurrentValueFor(radixValues);
        }

        public override CategoricalDistribution marginal(params RandomVariable[] vars)
        {
            return sumOut(vars);
        }

        public override CategoricalDistribution divideBy(CategoricalDistribution divisor)
        {
            return divideBy((ProbabilityTable) divisor);
        }

        public override CategoricalDistribution multiplyBy(CategoricalDistribution multiplier)
        {
            return pointwiseProduct((ProbabilityTable) multiplier);
        }

        public override CategoricalDistribution multiplyByPOS(
            CategoricalDistribution multiplier, params RandomVariable[] prodVarOrder)
        {
            return pointwiseProductPOS((ProbabilityTable) multiplier, prodVarOrder);
        }

        public override void iterateOver(CategoricalDistribution.Iterator cdi)
        {
            iterateOverTable(new CategoricalDistributionIteratorAdapter(cdi));
        }

        public void iterateOver(CategoricalDistribution.Iterator cdi,
                                params AssignmentProposition[] fixedValues)
        {
            iterateOverTable(new CategoricalDistributionIteratorAdapter(cdi),
                             fixedValues);
        }

        // END-CategoricalDistribution
        //

        //
        // START-Factor
        public Set<RandomVariable> getArgumentVariables()
        {
            return randomVarInfo.keySet();
        }

        public ProbabilityTable sumOut(params RandomVariable[] vars)
        {
            Set<RandomVariable> soutVars = new Set<RandomVariable>(
                this.randomVarInfo.keySet());
            foreach (RandomVariable rv in vars)
            {
                soutVars.remove(rv);
            }
            ProbabilityTable summedOut = new ProbabilityTable(soutVars);
            if (1 == summedOut.getValues().Length)
            {
                summedOut.getValues()[0] = getSum();
            }
            else
            {
                // Otherwise need to iterate through this distribution
                // to calculate the summed out distribution.
                Object[] termValues = new Object[summedOut.randomVarInfo
                    .size()];
                //ProbabilityTable.Iterator di = new ProbabilityTable.Iterator() {
                //    public void iterate(Map<RandomVariable, Object> possibleWorld,
                //            double probability) {

                //        int i = 0;
                //        foreach (RandomVariable rv in summedOut.randomVarInfo.keySet()) {
                //            termValues[i] = possibleWorld.get(rv);
                //            i++;
                //        }
                //        summedOut.getValues()[summedOut.getIndex(termValues)] += probability;
                //    }
                //};
                //iterateOverTable(di);
                //TODO:
            }

            return summedOut;
        }

        public Factor pointwiseProduct(Factor multiplier)
        {
            return pointwiseProduct((ProbabilityTable) multiplier);
        }

        public Factor pointwiseProductPOS(Factor multiplier,
                                          params RandomVariable[] prodVarOrder)
        {
            return pointwiseProductPOS((ProbabilityTable) multiplier, prodVarOrder);
        }

        public void iterateOver(Factor.Iterator fi)
        {
            iterateOverTable(new FactorIteratorAdapter(fi));
        }

        public void iterateOver(Factor.Iterator fi,
                                params AssignmentProposition[] fixedValues)
        {
            iterateOverTable(new FactorIteratorAdapter(fi), fixedValues);
        }

        // END-Factor
        //

        /**
         * Iterate over all the possible value assignments for the Random Variables
         * comprising this ProbabilityTable.
         * 
         * @param pti
         *            the ProbabilityTable Iterator to iterate.
         */

        public void iterateOverTable(Factor.Iterator pti)
        {
            Map<RandomVariable, Object> possibleWorld = new LinkedHashMap<RandomVariable, Object>();
            MixedRadixNumber mrn = new MixedRadixNumber(0, radices);
            do
            {
                foreach (RVInfo rvInfo in randomVarInfo.Values)
                {
                    possibleWorld.put(rvInfo.getVariable(), rvInfo
                                                                .getDomainValueAt(mrn.getCurrentNumeralValue(rvInfo
                                                                                                                 .
                                                                                                                 getRadixIdx
                                                                                                                 ())));
                }
                pti.iterate(possibleWorld, values[mrn.intValue()]);

            } while (mrn.increment());
        }

        /**
         * Iterate over all possible values assignments for the Random Variables
         * comprising this ProbabilityTable that are not in the fixed set of values.
         * This allows you to iterate over a subset of possible combinations.
         * 
         * @param pti
         *            the ProbabilityTable Iterator to iterate
         * @param fixedValues
         *            Fixed values for a subset of the Random Variables comprising
         *            this Probability Table.
         */

        public void iterateOverTable(Factor.Iterator pti,
                                     params AssignmentProposition[] fixedValues)
        {
            Map<RandomVariable, Object> possibleWorld = new LinkedHashMap<RandomVariable, Object>();
            MixedRadixNumber tableMRN = new MixedRadixNumber(0, radices);
            int[] tableRadixValues = new int[radices.Length];

            // Assert that the Random Variables for the fixed values
            // are part of this probability table and assign
            // all the fixed values to the possible world.
            foreach (AssignmentProposition ap in fixedValues)
            {
                if (!randomVarInfo.ContainsKey(ap.getTermVariable()))
                {
                    throw new ArgumentException("Assignment proposition ["
                                                + ap + "] does not belong to this probability table.");
                }
                possibleWorld.Add(ap.getTermVariable(), ap.getValue());
                RVInfo fixedRVI = randomVarInfo.get(ap.getTermVariable());
                tableRadixValues[fixedRVI.getRadixIdx()] = fixedRVI
                    .getIdxForDomain(ap.getValue());
            }
            // If have assignments for all the random variables
            // in this probability table
            if (fixedValues.Length == randomVarInfo.Count)
            {
                // Then only 1 iteration call is required.
                pti.iterate(possibleWorld, getValue(fixedValues));
            }
            else
            {
                // Else iterate over the non-fixed values
                List<RandomVariable> freeVariables = SetOps.difference(
                    new List<RandomVariable>(randomVarInfo.Keys), new List<RandomVariable>(possibleWorld.Keys));
                Map<RandomVariable, RVInfo> freeVarInfo = new LinkedHashMap<RandomVariable, RVInfo>();
                // Remove the fixed Variables
                foreach (RandomVariable fv in freeVariables)
                {
                    freeVarInfo.put(fv, new RVInfo(fv));
                }
                int[] freeRadixValues = createRadixs(freeVarInfo);
                MixedRadixNumber freeMRN = new MixedRadixNumber(0, freeRadixValues);
                Object fval = null;
                // Iterate through all combinations of the free variables
                do
                {
                    // Put the current assignments for the free variables
                    // into the possible world and update
                    // the current index in the table MRN
                    foreach (RVInfo freeRVI in freeVarInfo.values())
                    {
                        fval = freeRVI.getDomainValueAt(freeMRN
                                                            .getCurrentNumeralValue(freeRVI.getRadixIdx()));
                        possibleWorld.put(freeRVI.getVariable(), fval);

                        tableRadixValues[randomVarInfo.get(freeRVI.getVariable())
                                             .getRadixIdx()] = freeRVI.getIdxForDomain(fval);
                    }
                    pti.iterate(possibleWorld, values[(int) tableMRN
                                                                .getCurrentValueFor(tableRadixValues)]);

                } while (freeMRN.increment());
            }
        }

        public ProbabilityTable divideBy(ProbabilityTable divisor)
        {
            if (!randomVarInfo.keySet().containsAll(divisor.randomVarInfo.keySet()))
            {
                throw new IllegalArgumentException(
                    "Divisor must be a subset of the dividend.");
            }

            ProbabilityTable quotient = new ProbabilityTable(new List<RandomVariable>(randomVarInfo.Keys));

            if (1 == divisor.getValues().Length)
            {
                double d = divisor.getValues()[0];
                for (int i = 0; i < quotient.getValues().Length; i++)
                {
                    if (0 == d)
                    {
                        quotient.getValues()[i] = 0;
                    }
                    else
                    {
                        quotient.getValues()[i] = getValues()[i]/d;
                    }
                }
            }
            else
            {
                Set<RandomVariable> dividendDivisorDiff = SetOps
                    .difference(new List<RVInfo>(randomVarInfo.keySet()),
                                new List<RVInfo>(randomVarInfo.keySet()));
                Map<RandomVariable, RVInfo> tdiff = null;
                MixedRadixNumber tdMRN = null;
                if (dividendDivisorDiff.size() > 0)
                {
                    tdiff = new LinkedHashMap<RandomVariable, RVInfo>();
                    foreach (RandomVariable rv in dividendDivisorDiff)
                    {
                        tdiff.put(rv, new RVInfo(rv));
                    }
                    tdMRN = new MixedRadixNumber(0, createRadixs(tdiff));
                }
                Map<RandomVariable, RVInfo> diff = tdiff;
                MixedRadixNumber dMRN = tdMRN;
                int[] qRVs = new int[quotient.radices.Length];
                MixedRadixNumber qMRN = new MixedRadixNumber(0,
                                                             quotient.radices);
                //ProbabilityTable.Iterator divisorIterator = new ProbabilityTable.Iterator() {
                //    public void iterate(Map<RandomVariable, Object> possibleWorld,
                //            double probability) {
                //        foreach (RandomVariable rv in possibleWorld.keySet()) {
                //            RVInfo rvInfo = quotient.randomVarInfo.get(rv);
                //            qRVs[rvInfo.getRadixIdx()] = rvInfo
                //                    .getIdxForDomain(possibleWorld.get(rv));
                //        }
                //        if (null != diff) {
                //            // Start from 0 off the diff
                //            dMRN.setCurrentValueFor(new int[diff.size()]);
                //            do {
                //                for (RandomVariable rv : diff.keySet()) {
                //                    RVInfo drvInfo = diff.get(rv);
                //                    RVInfo qrvInfo = quotient.randomVarInfo.get(rv);
                //                    qRVs[qrvInfo.getRadixIdx()] = dMRN
                //                            .getCurrentNumeralValue(drvInfo
                //                                    .getRadixIdx());
                //                }
                //                updateQuotient(probability);
                //            } while (dMRN.increment());
                //        } else {
                //            updateQuotient(probability);
                //        }
                //    }

                //    //
                //
                //private void updateQuotient(double probability) {
                //    int offset = (int) qMRN.getCurrentValueFor(qRVs);
                //    if (0 == probability) {
                //        quotient.getValues()[offset] = 0;
                //    } else {
                //        quotient.getValues()[offset] += getValues()[offset]
                //                / probability;
                //    }
                //}
                ////// 	};

                //	divisor.iterateOverTable(divisorIterator);
                // TODO
            }

            return quotient;
        }

        public ProbabilityTable pointwiseProduct(ProbabilityTable multiplier)
        {
            List<RandomVariable> prodVars = SetOps.union(new List<RandomVariable>(randomVarInfo.Keys),
                                                        new List<RandomVariable>(multiplier.randomVarInfo.Keys));
            return pointwiseProductPOS(multiplier, prodVars
                                                       .ToArray());
        }

        public ProbabilityTable pointwiseProductPOS(
            ProbabilityTable multiplier, params RandomVariable[] prodVarOrder)
        {
            ProbabilityTable product = new ProbabilityTable(prodVarOrder);
            if (!product.randomVarInfo.keySet().Equals(
                SetOps.union(new List<RandomVariable>(randomVarInfo.keySet()),
                                                      new List<RandomVariable>(multiplier.randomVarInfo
                                                                                   .keySet()))))
            {
                if (1 == product.getValues().Length)
                {
                    product.getValues()[0] = getValues()[0]*multiplier.getValues()[0];
                }
                else
                {
                    // Otherwise need to iterate through the product
                    // to calculate its values based on the terms.
                    Object[] term1Values = new Object[randomVarInfo.size()];
                    Object[] term2Values = new Object[multiplier.randomVarInfo
                        .size()];
                    //ProbabilityTable.Iterator di = new ProbabilityTable.Iterator() {
                    //    private int idx = 0;

                    //    public void iterate(Map<RandomVariable, Object> possibleWorld,
                    //            double probability) {
                    //        int term1Idx = termIdx(term1Values, ProbabilityTable.this,
                    //                possibleWorld);
                    //        int term2Idx = termIdx(term2Values, multiplier,
                    //                possibleWorld);

                    //        product.getValues()[idx] = getValues()[term1Idx]
                    //                * multiplier.getValues()[term2Idx];

                    //        idx++;
                    //    }

                    //    private int termIdx(Object[] termValues, ProbabilityTable d,
                    //            Map<RandomVariable, Object> possibleWorld) {
                    //        if (0 == termValues.Length) {
                    //            // The term has no variables so always position 0.
                    //            return 0;
                    //        }

                    //        int i = 0;
                    //        for (RandomVariable rv : d.randomVarInfo.keySet()) {
                    //            termValues[i] = possibleWorld.get(rv);
                    //            i++;
                    //        }

                    //        return d.getIndex(termValues);
                    //    }
                    //};
                    //product.iterateOverTable(di);
                    // TODO		
                }
            }
            return product;
        }


        public override String ToString()
        {
            if (null == _toString)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<");
                for (int i = 0; i < values.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(values[i]);
                }
                sb.Append(">");

                _toString = sb.ToString();
            }
            return _toString;
        }

        //
        // PRIVATE METHODS
        //
        private void reinitLazyValues()
        {
            sum = -1;
            _toString = null;
        }

        private int[] createRadixs(Map<RandomVariable, RVInfo> mapRtoInfo)
        {
            int[] r = new int[mapRtoInfo.size()];
            // Read in reverse order so that the enumeration
            // through the distributions is of the following
            // order using a MixedRadixNumber, e.g. for two Booleans:
            // X Y
            // true true
            // true false
            // false true
            // false false
            // which corresponds with how displayed in book.
            int x = mapRtoInfo.size() - 1;
            foreach (RVInfo rvInfo in
            mapRtoInfo.values())
            {
                r[x] = rvInfo.getDomainSize();
                rvInfo.setRadixIdx(x);
                x--;
            }
            return r;
        }

        private class RVInfo
        {
            private RandomVariable variable;
            private FiniteDomain varDomain;
            private int radixIdx = 0;

            public RVInfo(RandomVariable rv)
            {
                variable = rv;
                varDomain = (FiniteDomain) variable.getDomain();
            }

            public RandomVariable getVariable()
            {
                return variable;
            }

            public int getDomainSize()
            {
                return varDomain.size();
            }

            public int getIdxForDomain(Object value)
            {
                return varDomain.getOffset(value);
            }

            public Object getDomainValueAt(int idx)
            {
                return varDomain.getValueAt(idx);
            }

            public void setRadixIdx(int idx)
            {
                radixIdx = idx;
            }

            public int getRadixIdx()
            {
                return radixIdx;
            }
        }

        private class CategoricalDistributionIteratorAdapter : Iterator
        {
            private CategoricalDistribution.Iterator cdi = null;

            public CategoricalDistributionIteratorAdapter(
                CategoricalDistribution.Iterator cdi)
            {
                this.cdi = cdi;
            }

            public override void iterate(Map<RandomVariable, Object> possibleAssignment,
                                         double probability)
            {
                cdi.iterate(possibleAssignment, probability);
            }
        }

        private class FactorIteratorAdapter : Iterator
        {
            private Factor.Iterator fi = null;

            public FactorIteratorAdapter(Factor.Iterator fi)
            {
                this.fi = fi;
            }

            public override void iterate(Map<RandomVariable, Object> possibleAssignment,
                                double probability)
            {
                fi.iterate(possibleAssignment, probability);
            }
        }
    }
}