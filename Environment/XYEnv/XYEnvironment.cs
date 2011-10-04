namespace AIMA.Core.Environment.XYEnv
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;
    using AIMA.Core.Agent.Impl;
    using AIMA.Core.Util.DataStructure;

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public class XYEnvironment : AbstractEnvironment
    {
        private XYEnvironmentState envState = null;

        //
        // PUBLIC METHODS
        //
        public XYEnvironment(int width, int height)
        {
            assert(width > 0);
            assert(height > 0);

            envState = new XYEnvironmentState(width, height);
        }

        public override EnvironmentState getCurrentState()
        {
            return envState;
        }

        public override EnvironmentState executeAction(Agent a, Action action)
        {
            return envState;
        }

        public override Percept getPerceptSeenBy(Agent anAgent)
        {
            return new DynamicPercept();
        }

        public void addObjectToLocation(EnvironmentObject eo, XYLocation loc)
        {
            moveObjectToAbsoluteLocation(eo, loc);
        }

        public void moveObjectToAbsoluteLocation(EnvironmentObject eo,
                XYLocation loc)
        {
            // Ensure the object is not already at a location
            envState.moveObjectToAbsoluteLocation(eo, loc);

            // Ensure is added to the environment
            addEnvironmentObject(eo);
        }

        public void moveObject(EnvironmentObject eo, XYLocation.Direction direction)
        {
            XYLocation presentLocation = envState.getCurrentLocationFor(eo);

            if (null != presentLocation)
            {
                XYLocation locationToMoveTo = presentLocation.locationAt(direction);
                if (!(isBlocked(locationToMoveTo)))
                {
                    moveObjectToAbsoluteLocation(eo, locationToMoveTo);
                }
            }
        }

        public XYLocation getCurrentLocationFor(EnvironmentObject eo)
        {
            return envState.getCurrentLocationFor(eo);
        }

        public HashSet<EnvironmentObject> getObjectsAt(XYLocation loc)
        {
            return envState.getObjectsAt(loc);
        }

        public HashSet<EnvironmentObject> getObjectsNear(Agent agent, int radius)
        {
            return envState.getObjectsNear(agent, radius);
        }

        public bool isBlocked(XYLocation loc)
        {
            foreach (EnvironmentObject eo in envState.getObjectsAt(loc))
            {
                if (eo is Wall)
                {
                    return true;
                }
            }
            return false;
        }

        public void makePerimeter()
        {
            for (int i = 0; i < envState.width; i++)
            {
                XYLocation loc = new XYLocation(i, 0);
                XYLocation loc2 = new XYLocation(i, envState.height - 1);
                envState.moveObjectToAbsoluteLocation(new Wall(), loc);
                envState.moveObjectToAbsoluteLocation(new Wall(), loc2);
            }

            for (int i = 0; i < envState.height; i++)
            {
                XYLocation loc = new XYLocation(0, i);
                XYLocation loc2 = new XYLocation(envState.width - 1, i);
                envState.moveObjectToAbsoluteLocation(new Wall(), loc);
                envState.moveObjectToAbsoluteLocation(new Wall(), loc2);
            }
        }
    }

    class XYEnvironmentState : EnvironmentState
    {
        int width;
        int height;

        private Map<XYLocation, HashSet<EnvironmentObject>> objsAtLocation = new LinkedHashMap<XYLocation, HashSet<EnvironmentObject>>();

        public XYEnvironmentState(int width, int height)
        {
            this.width = width;
            this.height = height;
            for (int h = 1; h <= height; h++)
            {
                for (int w = 1; w <= width; w++)
                {
                    objsAtLocation.put(new XYLocation(h, w),
                            new LinkedHashSet<EnvironmentObject>());
                }
            }
        }

        public void moveObjectToAbsoluteLocation(EnvironmentObject eo,
                XYLocation loc)
        {
            // Ensure is not already at another location
            foreach (Set<EnvironmentObject> eos in objsAtLocation.values())
            {
                if (eos.remove(eo))
                {
                    break; // Should only every be at 1 location
                }
            }
            // Add it to the location specified
            getObjectsAt(loc).Add(eo);
        }

        public HashSet<EnvironmentObject> getObjectsAt(XYLocation loc)
        {
            HashSet<EnvironmentObject> objectsAt = objsAtLocation.get(loc);
            if (null == objectsAt)
            {
                // Always ensure an empty Set is returned
                objectsAt = new LinkedHashSet<EnvironmentObject>();
                objsAtLocation.put(loc, objectsAt);
            }
            return objectsAt;
        }

        public XYLocation getCurrentLocationFor(EnvironmentObject eo)
        {
            foreach (XYLocation loc in objsAtLocation.keySet())
            {
                if (objsAtLocation.get(loc).contains(eo))
                {
                    return loc;
                }
            }
            return null;
        }

        public HashSet<EnvironmentObject> getObjectsNear(Agent agent, int radius)
        {
            HashSet<EnvironmentObject> objsNear = new LinkedHashSet<EnvironmentObject>();

            XYLocation agentLocation = getCurrentLocationFor(agent);
            foreach (XYLocation loc in objsAtLocation.keySet())
            {
                if (withinRadius(radius, agentLocation, loc))
                {
                    objsNear.AddRange(objsAtLocation.get(loc));
                }
            }
            // Ensure the 'agent' is not included in the Set of
            // objects near
            objsNear.remove(agent);

            return objsNear;
        }

        public override String ToString()
        {
            return "XYEnvironmentState:" + objsAtLocation.ToString();
        }

        //
        // PRIVATE METHODS
        //
        private bool withinRadius(int radius, XYLocation agentLocation,
                XYLocation objectLocation)
        {
            int xdifference = agentLocation.getXCoOrdinate()
                    - objectLocation.getXCoOrdinate();
            int ydifference = agentLocation.getYCoOrdinate()
                    - objectLocation.getYCoOrdinate();
            return Math.sqrt((xdifference * xdifference)
                    + (ydifference * ydifference)) <= radius;
        }
    }
}