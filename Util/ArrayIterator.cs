namespace AIMA.Core.Util
{
    using System;
    using System.Collections.Generic;
    /**
     * Iterates efficiently through an array.
     * 
     * @author Ruediger Lunde
     */
    public class ArrayIterator<T> : Iterator<T>
    {

        T[] values;
        int counter;

        public ArrayIterator(T[] values)
        {
            this.values = values;
            counter = 0;
        }

        public bool hasNext()
        {
            return counter < values.length;
        }

        public T next()
        {
            return values[counter++];
        }

        public void remove()
        {
            throw new UnsupportedOperationException();
        }
    }
}