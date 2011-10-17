namespace AIMA.Core.Agent.Impl
{
    using System;
    using System.Collections.Generic;
    public class NoOpAction : DynamicAction
    {

        public static readonly NoOpAction NO_OP = new NoOpAction();

        //
        // START-Action
        public bool isNoOp()
        {
            return true;
        }

        // END-Action
        //

        private NoOpAction() : base("NoOp")
        {

        }
    }
}