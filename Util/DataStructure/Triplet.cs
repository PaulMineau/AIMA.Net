namespace CosmicFlow.AIMA.Core.Util.DataStructure
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * @author Paul Mineau - I added a default constructor so this will serialize, and use automatic properties
     */
    
    public class Triplet<X, Y, Z>
    {
        public X First
        {
            get;
            set;
        }

        public Y Second
        {
            get;
            set;
        }

        public Z Third
        {
            get;
            set;
        }

        public Triplet()
        {
        }

        public Triplet(X x, Y y, Z z)
        {
            First = x;
            Second = y;
            Third = z;
        }

        public X getFirst()
        {
            return First;
        }

        public Y getSecond()
        {
            return Second;
        }

        public Z getThird()
        {
            return Third;
        }

        public override bool Equals(Object o)
        {
            if (o is Triplet<X, Y, Z>)
            {
                Triplet<X, Y, Z> other = (Triplet<X, Y, Z>)o;
                return (First.Equals(other.First)) && (Second.Equals(other.Second))
                        && (Third.Equals(other.Third));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return First.GetHashCode() + 31 * Second.GetHashCode() + 31 * Third.GetHashCode();
        }

        public override String ToString()
        {
            return "< " + First.ToString() + " , " + Second.ToString() + " , "
                    + Third.ToString() + " >";
        }
    }
}