namespace AIMA.Core.Logic.FOL.Domain
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public class FOLDomain
    {
        private List<String> constants, functions, predicates;
        private int skolemConstantIndexical = 0;
        private int skolemFunctionIndexical = 0;
        private int answerLiteralIndexical = 0;
        private List<FOLDomainListener> listeners = new List<FOLDomainListener>();

        public FOLDomain()
        {
            this.constants = new List<String>();
            this.functions = new List<String>();
            this.predicates = new List<String>();
        }

        public FOLDomain(FOLDomain toCopy) : this(toCopy.getConstants(), toCopy.getFunctions(), toCopy
                    .getPredicates())
        {
            
        }

        public FOLDomain(List<String> constants, List<String> functions,
                List<String> predicates)
        {
            this.constants = new List<String>(constants);
            this.functions = new List<String>(functions);
            this.predicates = new List<String>(predicates);
        }

        public List<String> getConstants()
        {
            return constants;
        }

        public List<String> getFunctions()
        {
            return functions;
        }

        public List<String> getPredicates()
        {
            return predicates;
        }

        public void addConstant(String constant)
        {
            constants.Add(constant);
        }

        public String addSkolemConstant()
        {

            String sc = null;
            do
            {
                sc = "SC" + (skolemConstantIndexical++);
            } while (constants.Contains(sc) || functions.Contains(sc)
                    || predicates.Contains(sc));

            addConstant(sc);
            notifyFOLDomainListeners(new FOLDomainSkolemConstantAddedEvent(this, sc));

            return sc;
        }

        public void addFunction(String function)
        {
            functions.Add(function);
        }

        public String addSkolemFunction()
        {
            String sf = null;
            do
            {
                sf = "SF" + (skolemFunctionIndexical++);
            } while (constants.Contains(sf) || functions.Contains(sf)
                    || predicates.Contains(sf));

            addFunction(sf);
            notifyFOLDomainListeners(new FOLDomainSkolemFunctionAddedEvent(this, sf));

            return sf;
        }

        public void addPredicate(String predicate)
        {
            predicates.Add(predicate);
        }

        public String addAnswerLiteral()
        {
            String al = null;
            do
            {
                al = "Answer" + (answerLiteralIndexical++);
            } while (constants.Contains(al) || functions.Contains(al)
                    || predicates.Contains(al));

            addPredicate(al);
            notifyFOLDomainListeners(new FOLDomainAnswerLiteralAddedEvent(this, al));

            return al;
        }

        public void addFOLDomainListener(FOLDomainListener listener)
        {
            lock (listeners)
            {
                if (!listeners.Contains(listener))
                {
                    listeners.Add(listener);
                }
            }
        }

        public void removeFOLDomainListener(FOLDomainListener listener)
        {
            lock (listeners)
            {
                listeners.Remove(listener);
            }
        }

        //
        // PRIVATE METHODS
        //
        private void notifyFOLDomainListeners(FOLDomainEvent evt)
        {
            lock (listeners)
            {
                foreach (FOLDomainListener l in listeners)
                {
                    evt.notifyListener(l);
                }
            }
        }
    }
}