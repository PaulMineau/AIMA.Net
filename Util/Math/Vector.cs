namespace CosmicFlow.AIMA.Core.Util.Math
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class Vector : Matrix
    {
        private const long serialVersionUID = 1L;

        // Vector is modelled as a matrix with a single column;
        public Vector(int size)
            : base(size, 1)
        {

        }

        public Vector(List<Double> lst)
            : base(lst.Count, 1)
        {

            for (int i = 0; i < lst.Count; i++)
            {
                setValue(i, lst[i]);
            }
        }

        public double getValue(int i)
        {
            return base.get(i, 0);
        }

        public void setValue(int index, double value)
        {
            base.set(index, 0, value);
        }

        public Vector copyVector()
        {
            Vector result = new Vector(getRowDimension());
            for (int i = 0; i < getRowDimension(); i++)
            {
                result.setValue(i, getValue(i));
            }
            return result;
        }

        public int size()
        {
            return getRowDimension();
        }

        public Vector minus(Vector v)
        {
            Vector result = new Vector(size());
            for (int i = 0; i < size(); i++)
            {
                result.setValue(i, getValue(i) - v.getValue(i));
            }
            return result;
        }

        public Vector plus(Vector v)
        {
            Vector result = new Vector(size());
            for (int i = 0; i < size(); i++)
            {
                result.setValue(i, getValue(i) + v.getValue(i));
            }
            return result;
        }

        public int indexHavingMaxValue()
        {
            if (size() <= 0)
            {
                throw new InvalidOperationException("can't perform this op on empty vector");
            }
            int res = 0;
            for (int i = 0; i < size(); i++)
            {
                if (getValue(i) > getValue(res))
                {
                    res = i;
                }
            }
            return res;
        }
    }
}
