namespace AIMA.Core.Logic.FOL.Inference.Otter
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.FOL.KB.Data;

    /**
     * @author Ciaran O'Reilly
     * 
     */

    // Heuristic for selecting lightest clause from SOS.
    // To avoid recalculating the lightest clause
    // on every call, the interface supports defining
    // the initial sos and updates to that set so
    // that it can maintain its own internal data
    // structures to allow for incremental re-calculation
    // of the lightest clause.
    public interface LightestClauseHeuristic
    {

        // Get the lightest clause from the SOS
        Clause getLightestClause();

        // SOS life-cycle methods allowing implementations
        // of this interface to incrementally update
        // the calculation of the lightest clause as opposed
        // to having to recalculate each time.
        void initialSOS(List<Clause> clauses);

        void addedClauseToSOS(Clause clause);

        void removedClauseFromSOS(Clause clause);
    }
}