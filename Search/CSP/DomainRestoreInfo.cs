namespace AIMA.Core.Search.CSP
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Util.DataStructure;

    /**
     * Provides informations which might be useful for a caller of a
     * constraint propagation algorithm. It maintains old domains for
     * variables and provides means to restore the initial state of the
     * CSP (before domain reduction started). Additionally, a flag indicates
     * whether an empty domain has been found during propagation. 
     * @author Ruediger Lunde
     *
     */
    public class DomainRestoreInfo
    {
        private List<Pair<Variable, Domain>> savedDomains;
        private HashSet<Variable> affectedVariables;
        private bool emptyDomainObserved;

        public DomainRestoreInfo()
        {
            savedDomains = new List<Pair<Variable, Domain>>();
            affectedVariables = new HashSet<Variable>();
        }

        public void clear()
        {
            savedDomains.clear();
            affectedVariables.clear();
        }

        public bool isEmpty()
        {
            return savedDomains.isEmpty();
        }

        /**
         * Stores the specified domain for the specified variable if a domain has
         * not yet been stored for the variable.
         */
        public void storeDomainFor(Variable var, Domain domain)
        {
            if (!affectedVariables.contains(var))
            {
                savedDomains.Add(new Pair<Variable, Domain>(var, domain));
                affectedVariables.Add(var);
            }
        }

        public void setEmptyDomainFound(bool b)
        {
            emptyDomainObserved = b;
        }

        /**
         * Can be called after all domain information has been collected to reduce
         * storage consumption.
         * 
         * @return this object, after removing one hashtable.
         */
        public DomainRestoreInfo compactify()
        {
            affectedVariables = null;
            return this;
        }

        public bool isEmptyDomainFound()
        {
            return emptyDomainObserved;
        }

        public List<Pair<Variable, Domain>> getSavedDomains()
        {
            return savedDomains;
        }

        public void restoreDomains(CSP csp)
        {
            foreach (Pair<Variable, Domain> pair in getSavedDomains())
                csp.setDomain(pair.getFirst(), pair.getSecond());
        }

        public override String ToString()
        {
            StringBuffer result = new StringBuffer();
            foreach (Pair<Variable, Domain> pair in savedDomains)
                result.append(pair.getFirst() + "=" + pair.getSecond() + " ");
            if (emptyDomainObserved)
                result.append("!");
            return result.ToString();
        }
    }
}