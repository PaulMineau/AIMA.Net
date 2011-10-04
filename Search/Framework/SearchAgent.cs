namespace AIMA.Core.Search.Framework
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Agent.Impl;

    /**
     * @author Ravi Mohan
     * 
     */
    public class SearchAgent : AbstractAgent
    {
        protected List<Action> actionList;

        private Iterator<Action> actionIterator;

        private Metrics searchMetrics;

        public SearchAgent(Problem p, Search search)
        {
            actionList = search.search(p);
            actionIterator = actionList.iterator();
            searchMetrics = search.getMetrics();
        }

        public override Action execute(Percept p)
        {
            if (actionIterator.hasNext())
            {
                return actionIterator.next();
            }
            else
            {
                return NoOpAction.NO_OP;
            }
        }

        public bool isDone()
        {
            return !actionIterator.hasNext();
        }

        public List<Action> getActions()
        {
            return actionList;
        }

        public Properties getInstrumentation()
        {
            Properties retVal = new Properties();
            Iterator<String> iter = searchMetrics.keySet().iterator();
            while (iter.hasNext())
            {
                String key = iter.next();
                String value = searchMetrics.get(key);
                retVal.setProperty(key, value);
            }
            return retVal;
        }
    }
}