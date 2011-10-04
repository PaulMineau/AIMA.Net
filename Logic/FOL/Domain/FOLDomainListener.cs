namespace CosmicFlow.AIMA.Core.Logic.FOL.Domain
{
    using System;
    using System.Collections.Generic;
/**
 * @author Ciaran O'Reilly
 * 
 */
public interface FOLDomainListener {
	void skolemConstantAdded(FOLDomainSkolemConstantAddedEvent evt);

	void skolemFunctionAdded(FOLDomainSkolemFunctionAddedEvent evt);

    void answerLiteralNameAdded(FOLDomainAnswerLiteralAddedEvent evt);
}
}