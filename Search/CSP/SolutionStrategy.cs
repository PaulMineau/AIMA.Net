namespace AIMA.Core.Search.CSP
{
    using System;
    using System.Collections.Generic;
    /**
     * Base class for CSP solver implementations. Solving a CSP means finding an
     * assignment, which is consistent and complete with respect to a CSP. This
     * abstract class provides the central interface method and additionally an
     * implementation of an observer mechanism.
     * 
     * @author Ruediger Lunde
     */
    public abstract class SolutionStrategy
    {
        List<CSPStateListener> listeners = new List<CSPStateListener>();

        public void addCSPStateListener(CSPStateListener listener)
        {
            listeners.Add(listener);
        }

        public void removeCSPStateListener(CSPStateListener listener)
        {
            listeners.remove(listener);
        }

        protected void fireStateChanged(CSP csp)
        {
            foreach (CSPStateListener listener in listeners)
                listener.stateChanged(csp.copyDomains());
        }

        protected void fireStateChanged(Assignment assignment, CSP csp)
        {
            foreach (CSPStateListener listener in listeners)
                listener.stateChanged(assignment.copy(), csp.copyDomains());
        }

        public abstract Assignment solve(CSP csp);
    }
}