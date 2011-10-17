namespace AIMA.Core.Search.Framework
{
    using System.Collections.Generic;
    using AIMA.Core.Agent;

    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 67.
     * 
     * Given a particular state s, ACTIONS(s) returns the set of actions that can be
     * executed in s. We say that each of these actions is <b>applicable</b> in s.
     */

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public interface ActionsFunction
    {
        /**
         * Given a particular state s, returns the set of actions that can be
         * executed in s.
         * 
         * @param s
         *            a particular state.
         * @return the set of actions that can be executed in s.
         */
        HashSet<Action> actions(object s);
    }
}