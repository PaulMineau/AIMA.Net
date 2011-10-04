namespace AIMA.Core.Search.Framework
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;

    /**
     * @author Ravi Mohan
     * 
     */
    public interface Search
    {
        List<Action> search(Problem p);

        Metrics getMetrics();
    }
}