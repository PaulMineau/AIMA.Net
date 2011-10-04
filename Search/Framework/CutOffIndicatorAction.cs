namespace AIMA.Core.Search.Framework
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent.Impl;

    /**
     * A NoOp action that indicates a CutOff has occurred in a search. Used
     * primarily by DepthLimited and IterativeDeepening search routines. 
     */

    /**
     * @author Ciaran O'Reilly
     */
    public class CutOffIndicatorAction : DynamicAction
    {
        public const CutOffIndicatorAction CUT_OFF = new CutOffIndicatorAction();

        //
        // START-Action
        public bool isNoOp()
        {
            return true;
        }

        // END-Action
        //

        private CutOffIndicatorAction()
        {
            super("CutOff");
        }
    }
}