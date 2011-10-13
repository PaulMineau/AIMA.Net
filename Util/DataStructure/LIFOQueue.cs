namespace AIMA.Core.Util.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): pg 80.<br>
     * 
     * Last-in, first-out or LIFO queue (also known as a stack), which pops the newest element of the queue;
     */

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public class LIFOQueue<E> : Stack<E>
    {
        private const long serialVersionUID = 1;

        public LIFOQueue()
            : base()
        {

        }

        public LIFOQueue(IEnumerable<E> c)
            : base(c)
        {

        }

        //
        // START-Queue
        public bool isEmpty()
        {
            return 0 == Count;
        }

        public E pop()
        {
            return Pop();
        }

        public void push(E element)
        {
            Push(element);
        }

        //public Queue<E> insert(E element)
        //{
        //    if (offer(element))
        //    {
        //        return this;
        //    }
        //    return null;
        //}

        // END-Queue
        //

        //
        // START-Override LinkedList methods in order for it to behave in LIFO
        // order.
        public bool add(E e)
        {
            Push(e);
            return true;
        }

        public bool addAll(IEnumerable<E> c)
        {
            foreach(E e in c)
            {
                Push(e);
            }
            return true;
        }

        public bool offer(E e)
        {
            Push(e);
            return true;
        }
        // End-Override LinkedList methods in order for it to behave like a LIFO.
        //
    }
}