namespace CosmicFlow.AIMA.Core.Logic.FOL.Domain
{
    using System;
    using System.Collections.Generic;
/**
 * @author Ciaran O'Reilly
 * 
 */
public class FOLDomainSkolemConstantAddedEvent : FOLDomainEvent {

	private const long serialVersionUID = 1L;

	private String skolemConstantName;

	public FOLDomainSkolemConstantAddedEvent(Object source,
			String skolemConstantName) : base(source) {

		this.skolemConstantName = skolemConstantName;
	}

	public String getSkolemConstantName() {
		return skolemConstantName;
	}

	public override void notifyListener(FOLDomainListener listener) {
		listener.skolemConstantAdded(this);
	}
}
}