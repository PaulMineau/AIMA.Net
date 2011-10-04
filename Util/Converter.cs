namespace AIMA.Core.Util
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class Converter<T>
    {

        public List<T> setToList(Set<T> set)
        {
            List<T> retVal = new List<T>(set);
            return retVal;
        }

        public HashSet<T> listToSet(List<T> l)
        {
            HashSet<T> retVal = new HashSet<T>(l);
            return retVal;
        }
    }
}