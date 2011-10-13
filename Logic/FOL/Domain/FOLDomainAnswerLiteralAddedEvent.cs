namespace AIMA.Core.Logic.FOL.Domain
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class FOLDomainAnswerLiteralAddedEvent : FOLDomainEvent
    {

        private const long serialVersionUID = 1L;

        private String answerLiteralName;

        public FOLDomainAnswerLiteralAddedEvent(Object source,
                String answerLiteralName) : base(source)
        {
            this.answerLiteralName = answerLiteralName;
        }

        public String getAnswerLiteralNameName()
        {
            return answerLiteralName;
        }

        public override void notifyListener(FOLDomainListener listener)
        {
            listener.answerLiteralNameAdded(this);
        }
    }
}
