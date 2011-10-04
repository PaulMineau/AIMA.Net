namespace AIMA.Core.Util
{
    using System;
    using System.Collections.Generic;
    /**
     * : a thread with an additional flag indicating cancellation.
     * 
     * @author R. Lunde
     * 
     */
    public class CancelableThread : Thread
    {

        public static bool currIsCanceled()
        {
            if (Thread.currentThread() is CancelableThread)
                return ((CancelableThread)Thread.currentThread()).isCanceled;
            return false;
        }

        private bool isCanceled;

        public bool isCanceled()
        {
            return isCanceled;
        }

        public void cancel()
        {
            isCanceled = true;
        }
    }
}