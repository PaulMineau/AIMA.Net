namespace AIMA.Core.Search.Framework
{
    using System;
    using System.Collections.Generic;
    /**
     * An interface describing a problem that can be tackled from both directions 
     * at once (i.e InitialState<->Goal).
     */

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public interface BidirectionalProblem
    {
        Problem getOriginalProblem();

        Problem getReverseProblem();
    }
}