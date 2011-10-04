namespace AIMA.Core.Agent.Impl
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;

    /**
     * @author Ciaran O'Reilly
     */
    public class DynamicState : ObjectWithDynamicAttributes, State
    {
        public DynamicState()
        {

        }


        public override String describeType()
        {
            return typeof(State).Name;
        }
    }
}