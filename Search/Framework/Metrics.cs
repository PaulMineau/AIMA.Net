namespace AIMA.Core.Search.Framework
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class Metrics
    {
        private Dictionary<String, String> hash;

        public Metrics()
        {
            this.hash = new Dictionary<String, String>();
        }

        public void set(String name, int i)
        {
            hash.put(name, int.ToString(i));
        }

        public void set(String name, double d)
        {
            hash.put(name, Double.ToString(d));
        }

        public int getInt(String name)
        {
            return new int(hash.get(name)).intValue();
        }

        public double getDouble(String name)
        {
            return new Double(hash.get(name)).doubleValue();
        }

        public String get(String name)
        {
            return hash.get(name);
        }

        public HashSet<String> keySet()
        {
            return hash.keySet();
        }
    }
}