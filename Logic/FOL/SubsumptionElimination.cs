namespace CosmicFlow.AIMA.Core.Logic.FOL
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.FOL.KB.Data;

    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 356.
     * 
     * The subsumption method eliminates all sentences that are subsumed by (that
     * is, more specific than) an existing sentence in the KB. For example, P(x) is in the KB, then
     * there is no sense in adding P(A) and even less sense in adding P(A) V Q(B). Subsumption
     * helps keep the KB small and thus helps keep the search space small.
     * 
     * Note: From slide 17.  
     * http://logic.stanford.edu/classes/cs157/2008/lectures/lecture12.pdf
     * 
     * Relational Subsumption
     * 
     * A relational clause Phi subsumes Psi is and only if there
     * is a substitution delta that, when applied to Phi, produces a
     * clause Phidelta that is a subset of Psi.
     */

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class SubsumptionElimination
    {
        public static List<Clause> findSubsumedClauses(List<Clause> clauses)
        {
            List<Clause> subsumed = new List<Clause>();

            // Group the clauses by their # of literals.
            // Keep track of the min and max # of literals.
            int min = int.MaxValue;
            int max = 0;
            Dictionary<int, List<Clause>> clausesGroupedBySize = new Dictionary<int, List<Clause>>();
            foreach (Clause c in clauses)
            {
                int size = c.getNumberLiterals();
                if (size < min)
                {
                    min = size;
                }
                if (size > max)
                {
                    max = size;
                }
                List<Clause> cforsize = null;
                if (clausesGroupedBySize.ContainsKey(size))
                {
                    cforsize = clausesGroupedBySize[size];
                }
                if (null == cforsize)
                {
                    cforsize = new List<Clause>();
                    clausesGroupedBySize.Add(size, cforsize);
                }
                cforsize.Add(c);
            }
            // Check if each smaller clause
            // subsumes any of the larger clauses.
            for (int i = min; i < max; i++)
            {
                List<Clause> scs = clausesGroupedBySize[i];
                // Ensure there are clauses with this # of literals
                if (null != scs)
                {
                    for (int j = i + 1; j <= max; j++)
                    {
                        
                        // Ensure there are clauses with this # of literals
                        if (clausesGroupedBySize.ContainsKey(j))
                        {
                            List<Clause> lcs = clausesGroupedBySize[j];
                            foreach (Clause sc in scs)
                            {
                                // Don't bother checking clauses
                                // that are already subsumed.
                                if (!subsumed.Contains(sc))
                                {
                                    foreach (Clause lc in lcs)
                                    {
                                        if (!subsumed.Contains(lc))
                                        {
                                            if (sc.subsumes(lc))
                                            {
                                                subsumed.Add(lc);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return subsumed;
        }
    }
}