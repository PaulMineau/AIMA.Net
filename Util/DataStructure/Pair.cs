namespace AIMA.Core.Util.DataStructure
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class Pair<X, Y>
    {
        private X a;

        private Y b;

        public Pair(X a, Y b)
        {
            this.a = a;
            this.b = b;
        }

        public X getFirst()
        {
            return a;
        }

        public Y getSecond()
        {
            return b;
        }


        public override bool Equals(Object o)
        {
            if (o is Pair<X, Y>)
            {
                Pair<X, Y> p = (Pair<X, Y>)o;
                return a.Equals(p.a) && b.Equals(p.b);
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (a == null || b == null) return base.GetHashCode();
            return a.GetHashCode() + 31 * b.GetHashCode();
        }

        public override String ToString()
        {
            return "< " + getFirst().ToString() + " , " + getSecond().ToString()
                    + " > ";
        }
    }
}