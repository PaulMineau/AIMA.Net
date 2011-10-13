namespace AIMA.Core.Util
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class FrequencyCounter<T>
    {
        private Dictionary<T, int> counter;

        public FrequencyCounter()
        {
            counter = new Dictionary<T, int>();
        }

        public int getCount(T key)
        {
            int value = counter[key];
            if (value == null)
            {
                return 0;
            }
            return value;
        }

        public void incrementFor(T key)
        {
            if (!counter.ContainsKey(key))
            {
                counter.Add(key, 1);
            }
            else
            {
                counter[key]++;
            }
        }

        public Double probabilityOf(T key)
        {
            int value = getCount(key);
            if (value == 0)
            {
                return 0.0;
            }
            else
            {
                Double total = 0.0;
                foreach (T k in counter.Keys)
                {
                    total += getCount(k);
                }
                return value / total;
            }
        }

        public override String ToString()
        {
            return counter.ToString();
        }

        public HashSet<T> getStates()
        {
            return new HashSet<T>(counter.Keys);
        }
    }
}