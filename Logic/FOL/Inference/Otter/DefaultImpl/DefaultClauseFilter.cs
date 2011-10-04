namespace CosmicFlow.AIMA.Core.Logic.FOL.Inference.Otter.DefaultImpl
{

    using CosmicFlow.AIMA.Core.Logic.FOL.Inference.Otter;
    using CosmicFlow.AIMA.Core.Logic.FOL.KB.Data;
    using System;
    using System.Collections.Generic;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class DefaultClauseFilter : ClauseFilter
    {
        public DefaultClauseFilter()
        {

        }

        //
        // START-ClauseFilter
        public List<Clause> filter(List<Clause> clauses)
        {
            return clauses;
        }

        // END-ClauseFilter
        //
    }
}
