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
        

    }

    public class LinkedHashSet<T> : HashSet<T>
    {

    }

    public class LinkedHashMap<T1, T2> : Map<T1, T2>
    {

    }

    public class Map<T1, T2> : Dictionary<T1, T2>
    {
    }
}
