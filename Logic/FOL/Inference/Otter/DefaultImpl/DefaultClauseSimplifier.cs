namespace AIMA.Core.Logic.FOL.Inference.Otter.DefaultImpl
{

    using AIMA.Core.Logic.FOL.Inference;
    using AIMA.Core.Logic.FOL.Inference.Otter;
    using AIMA.Core.Logic.FOL.KB.Data;
    using AIMA.Core.Logic.FOL.Parsing.AST;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class DefaultClauseSimplifier : ClauseSimplifier
    {

        private Demodulation demodulation = new Demodulation();
        private List<TermEquality> rewrites = new List<TermEquality>();

        public DefaultClauseSimplifier()
        {

        }

        public DefaultClauseSimplifier(List<TermEquality> rewrites)
        {
            this.rewrites.AddRange(rewrites);
        }

        //
        // START-ClauseSimplifier
        public Clause simplify(Clause c) {
		Clause simplified = c;

		// Apply each of the rewrite rules to
		// the clause
		foreach (TermEquality te in rewrites) {
			Clause dc = simplified;
			// Keep applying the rewrite as many times as it
			// can be applied before moving on to the next one.
			while (null != (dc = demodulation.apply(te, dc))) {
				simplified = dc;
			}
		}

		return simplified;
	}

        // END-ClauseSimplifier
        //
    }
}