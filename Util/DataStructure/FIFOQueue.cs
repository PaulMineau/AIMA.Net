namespace AIMA.Core.Util.DataStructure
{
    using System;
    using System.Collections.Generic;
/**
 * Artificial Intelligence A Modern Approach (3rd Edition): pg 80.<br>
 * 
 * First-in, first-out or FIFO queue, which pops the oldest element of the queue;
 */

/**
 * @author Ravi Mohan
 * @author Ciaran O'Reilly
 */
public class FIFOQueue<E> : Queue<E> {
	private const long serialVersionUID = 1;

	public FIFOQueue() : base() {
	}

	public FIFOQueue(IEnumerable<E> c) : base(c) {

	}

	//
	// START-Queue
	public bool isEmpty() {
		return 0 == Count;
	}

	public E pop() {
		return this.Dequeue();
	}

	public void push(E element) {
		this.Enqueue(element);
	}

    //public Queue<E> insert(E element) {
    //    if (offer(element)) {
    //        return this;
    //    }
    //    return null;
    //}
	// END-Queue
	//
}
}