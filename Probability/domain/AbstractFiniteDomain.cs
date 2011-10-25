using System;

namespace AIMA.Probability.Domain
{

    public abstract class AbstractFiniteDomain : FiniteDomain
    {

        private String toString = null;
        private Map<Object, int> valueToIdx = new Map<Object, int>();
        private Map<int, Object> idxToValue = new Map<int, Object>();

        public AbstractFiniteDomain()
        {

        }

        //
        // START-Domain

        public bool isFinite()
        {
            return true;
        }



        public bool isInfinite()
        {
            return false;
        }

        public abstract int size();

        public abstract bool isOrdered();

        // END-Domain
        //

        //
        // START-FiniteDomain
        public abstract Set<Object> getPossibleValues();

       

        public int getOffset(Object value)
        {
            int idx = valueToIdx[value];
            if (null == idx)
            {
                throw new ArgumentException("Value [" + value
                                                   + "] is not a possible value of this domain.");
            }
            return idx;
        }



        public Object getValueAt(int offset)
        {
            object result = null;
            idxToValue.TryGetValue(offset, out result);
            return result;
        }

        // END-FiniteDomain
        //



        public override String ToString()
        {
            if (null == toString)
            {
                toString = getPossibleValues().ToString();
            }
            return toString;
        }

        //
        // PROTECTED METHODS
        //
        protected void indexPossibleValues<T>(Set<T 
    >
        possibleValues 
    )
    {
        int idx = 0;
        foreach (T value in
        possibleValues)
        {
            valueToIdx.Add(value, idx);
            idxToValue.Add(idx, value);
            idx++;
        }
    }
    }
}