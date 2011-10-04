namespace AIMA.Core.Environment.Map
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent.Impl;

    public class MoveToAction : DynamicAction
    {
        public const String ATTRIBUTE_MOVE_TO_LOCATION = "location";

        public MoveToAction(String location)
        {
            super("moveTo");
            setAttribute(ATTRIBUTE_MOVE_TO_LOCATION, location);
        }

        public String getToLocation()
        {
            return (String)getAttribute(ATTRIBUTE_MOVE_TO_LOCATION);
        }
    }
}
