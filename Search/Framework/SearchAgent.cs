namespace AIMA.Core.Search.Framework
{
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

        private List<Action>.Enumerator actionIterator;

        private Metrics searchMetrics;

        public SearchAgent(Problem p, Search search)
        {
            actionList = search.search(p);
            actionIterator = actionList.GetEnumerator();
            searchMetrics = search.getMetrics();
        }

        public override Action execute(Percept p)
        {
            
            if (actionIterator.MoveNext())
            {
                return actionIterator.Current;
            }
            else
            {
                return NoOpAction.NO_OP;
            }
        }

        public bool isDone()
        {
            return null != actionIterator.Current;
        }

        public List<Action> getActions()
        {
            return actionList;
        }

        public Dictionary<string,string> getInstrumentation()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
           foreach(string key in  searchMetrics.keySet())
           {
                System.String value = searchMetrics.get(key);
                retVal.Add(key, value);
            }
            return retVal;
        }
    }
}