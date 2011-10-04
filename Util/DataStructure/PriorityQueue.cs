namespace AIMA.Core.Util.DataStructure
{
    using System;
    using System.Collections.Generic;
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): pg 80.<br>
     * 
     * The priority queue, which pops the element of the queue with the highest
     * priority according to some ordering function.
     */

    /**
     * @author Ciaran O'Reilly
     */
    public class PriorityQueue<E> : java.util.PriorityQueue<E>,
            Queue<E>
    {
        private const long serialVersionUID = 1;

        public PriorityQueue()
            : base()
        {

        }

        public PriorityQueue(Collection<E> c)
            : base(c)
        {

        }

        public PriorityQueue(int initialCapacity)
        {
            super(initialCapacity);
        }

        public PriorityQueue(int initialCapacity, Comparator<E> comparator)
            : base(initialCapacity, comparator)
        {

        }

        public PriorityQueue(PriorityQueue<E> c)
            : base(c)
        {
        }

        public PriorityQueue(SortedSet<E> c)
            : base(c)
        {

        }

        //
        // START-Queue
        public bool isEmpty()
        {
            return 0 == size();
        }

        public E pop()
        {
            return poll();
        }

        public Queue<E> insert(E element)
        {
            if (offer(element))
            {
                return this;
            }
            return null;
        }
        // END-Queue
        //
    }
}