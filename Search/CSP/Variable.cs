namespace AIMA.Core.Search.CSP
{
    using System;
    using System.Collections.Generic;
    /**
     * A variable is a distinguishable object with a name.
     * 
     * @author Ruediger Lunde
     */
    public class Variable
    {
        private String name;

        public Variable(String name)
        {
            this.name = name;
        }

        public String getName()
        {
            return name;
        }

        public override String ToString()
        {
            return name;
        }

        public override bool Equals(Object obj)
        {
            if (obj is Variable)
            {
                return this.name.Equals(((Variable)obj).name);
            }
            return super.Equals(obj);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }
}