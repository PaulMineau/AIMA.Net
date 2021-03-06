namespace AIMA.Core.Logic.FOL.Inference.Otter
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.FOL.KB.Data;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public interface ClauseSimplifier
    {
        Clause simplify(Clause c);
    }
}