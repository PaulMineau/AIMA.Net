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
            hash[name] = i.ToString();
        }

        public void set(String name, double d)
        {
            hash[name] = d.ToString();
        }

        public int getInt(String name)
        {
            return int.Parse(hash[name]);
        }

        public double getDouble(String name)
        {
            return double.Parse(hash[name]);
        }

        public String get(String name)
        {
            return hash[name];
        }

        public HashSet<String> keySet()
        {
            return new HashSet<string>(hash.Keys);
        }
    }
}