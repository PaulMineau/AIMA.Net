namespace AIMA.Core.Environment.NQueens
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent.Impl;
    using AIMA.Core.Util.DataStructure;

    /**
     * Queens can be placed, removed, and moved. For movements, a vertical direction
     * is assumed. Therefore, only the end point needs to be specified.
     * 
     * @author Ravi Mohan
     * @author R. Lunde
     */
    public class QueenAction : DynamicAction
    {
        public const String PLACE_QUEEN = "placeQueenAt";
        public const String REMOVE_QUEEN = "removeQueenAt";
        public const String MOVE_QUEEN = "moveQueenTo";

        public const String ATTRIBUTE_QUEEN_LOC = "location";

        /**
         * Creates a queen action. Supported values of type are {@link #PLACE_QUEEN}
         * , {@link #REMOVE_QUEEN}, or {@link #MOVE_QUEEN}.
         */
        public QueenAction(String type, XYLocation loc)
        {
            super(type);
            setAttribute(ATTRIBUTE_QUEEN_LOC, loc);
        }

        public XYLocation getLocation()
        {
            return (XYLocation)getAttribute(ATTRIBUTE_QUEEN_LOC);
        }

        public int getX()
        {
            return getLocation().getXCoOrdinate();
        }

        public int getY()
        {
            return getLocation().getYCoOrdinate();
        }
    }
}
