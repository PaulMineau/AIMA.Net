namespace AIMA.Core.Logic.FOL.Domain
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class FOLDomainSkolemFunctionAddedEvent : FOLDomainEvent
    {

        private const long serialVersionUID = 1L;

        private String skolemFunctionName;

        public FOLDomainSkolemFunctionAddedEvent(Object source,
                String skolemFunctionName) : base(source)
        {
            this.skolemFunctionName = skolemFunctionName;
        }

        public String getSkolemConstantName()
        {
            return skolemFunctionName;
        }

        public override void notifyListener(FOLDomainListener listener)
        {
            listener.skolemFunctionAdded(this);
        }
    }
}