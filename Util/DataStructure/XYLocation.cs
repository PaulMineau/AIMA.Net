namespace AIMA.Core.Util.DataStructure
{
    using System;
    using System.Collections.Generic;
    /**
     * Note: If looking at a rectangle - the coordinate (x=0, y=0) will be the top left hand corner.
     * This corresponds with Java's AWT coordinate system.
     */

    /**
     * @author Ravi Mohan
     * 
     */
    public class XYLocation
    {
        public enum Direction
        {
            North, South, East, West
        };

        int xCoOrdinate, yCoOrdinate;

        public XYLocation(int x, int y)
        {
            xCoOrdinate = x;
            yCoOrdinate = y;
        }

        public int getXCoOrdinate()
        {
            return xCoOrdinate;
        }

        public int getYCoOrdinate()
        {
            return yCoOrdinate;
        }

        public XYLocation west()
        {
            return new XYLocation(xCoOrdinate - 1, yCoOrdinate);
        }

        public XYLocation east()
        {
            return new XYLocation(xCoOrdinate + 1, yCoOrdinate);
        }

        public XYLocation north()
        {
            return new XYLocation(xCoOrdinate, yCoOrdinate - 1);
        }

        public XYLocation south()
        {
            return new XYLocation(xCoOrdinate, yCoOrdinate + 1);
        }

        public XYLocation right()
        {
            return east();
        }

        public XYLocation left()
        {
            return west();
        }

        public XYLocation up()
        {
            return north();
        }

        public XYLocation down()
        {
            return south();
        }

        public XYLocation locationAt(Direction direction)
        {
            if (direction.Equals(Direction.North))
            {
                return north();
            }
            if (direction.Equals(Direction.South))
            {
                return south();
            }
            if (direction.Equals(Direction.East))
            {
                return east();
            }
            if (direction.Equals(Direction.West))
            {
                return west();
            }
            else
            {
                throw new ApplicationException("Unknown direction " + direction);
            }
        }

        public override bool Equals(Object o)
        {
            if (null == o || !(o is XYLocation))
            {
                return base.Equals(o);
            }
            XYLocation anotherLoc = (XYLocation)o;
            return ((anotherLoc.getXCoOrdinate() == xCoOrdinate) && (anotherLoc
                    .getYCoOrdinate() == yCoOrdinate));
        }

        public override String ToString()
        {
            return " ( " + xCoOrdinate + " , " + yCoOrdinate + " ) ";
        }

        public override int GetHashCode()
        {
            int result = 17;
            result = 37 * result + xCoOrdinate;
            result = result + yCoOrdinate;
            return result;
        }
    }
}