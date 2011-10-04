namespace CosmicFlow.AIMA.Core.Logic.FOL.Domain
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ciaran O'Reilly
     * 
     */
    public abstract class FOLDomainEvent 
    {

        private const long serialVersionUID = 1L;

        public FOLDomainEvent(Object source) 
        {
            
        }

        public abstract void notifyListener(FOLDomainListener listener);
    }
}