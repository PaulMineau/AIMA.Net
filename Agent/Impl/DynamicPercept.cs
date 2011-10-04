namespace AIMA.Core.Agent.Impl
{

    using System;
    using System.Collections.Generic;
    using AIMA.Core.Agent;

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public class DynamicPercept : ObjectWithDynamicAttributes,
            Percept
    {
        public DynamicPercept()
        {

        }

        public override String describeType()
        {
            return typeof(Percept).Name;
        }

        public DynamicPercept(Object key1, Object value1)
        {
            setAttribute(key1, value1);
        }

        public DynamicPercept(Object key1, Object value1, Object key2, Object value2)
        {
            setAttribute(key1, value1);
            setAttribute(key2, value2);
        }

        public DynamicPercept(Object[] keys, Object[] values)
        {
            assert(keys.length == values.length);

            for (int i = 0; i < keys.length; i++)
            {
                setAttribute(keys[i], values[i]);
            }
        }
    }
}