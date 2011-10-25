using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIMA
{
    public class Set<T> : HashSet<T>
    {
        public Set()
        {
        }

        public Set(IEnumerable<T> initList)
            : base(initList)
        {

        }
        
        public void add(T obj)
        {

            Add(obj);
        }
        public void addAll (List<T> objs )
        {
                foreach(T obj in objs)
                {
                    add(obj);
                }

        }

        internal void remove(T obj)
        {
            Remove(obj);
        }

        internal bool containsAll(Set<Probability.RandomVariable> set)
        {
            // TODO
            return false;
        }

        internal int size()
        {
            return Count;
        }

        internal void removeAll(Set<T> set)
        {
           foreach(T t in set)
           {
               Remove(t);
           }
        }
    }

    public class LinkedHashSet<T> : HashSet<T>
    {

    }

    public class LinkedHashMap<T1, T2> : Map<T1, T2>
    {

    }

    public class Map<T1, T2> : Dictionary<T1, T2>
    {
        public T2 get(T1 key)
        {
            T2 result = default(T2);
            TryGetValue(key, out result);
            return result;
        }

        public void put(T1 key, T2 obj)
        {
            Add(key,obj);
        }

        internal Set<T1> keySet()
        {
            T1 [] keys = new T1[Count];
             Keys.CopyTo(keys, 0);
            return new Set<T1>(keys);
        }

        internal int size()
        {
            return Count;
        }

        internal IEnumerable<T2> values()
        {
            return Values;
        }

        internal bool containsKey(T1 key)
        {
            return ContainsKey(key);
        }
    }

    

    public class IllegalArgumentException : ArgumentException
    {
        
        public IllegalArgumentException(string message) : base(message)
        {
            
        }
    }
}
