namespace AIMA.Core.Logic.Propositional.Parsing.Ast
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Propositional.Parsing;

    /**
     * @author Ravi Mohan
     * 
     */
    public class Symbol : AtomicSentence
    {
        private String value;

        public Symbol(String value)
        {
            this.value = value;
        }

        public String getValue()
        {
            return value;
        }

        public override bool Equals(Object o)
        {

            if (this == o)
            {
                return true;
            }
            if ((o == null) || !(o is Symbol))
            {
                return false;
            }
            Symbol sym = (Symbol)o;
            return (sym.getValue().Equals(getValue()));

        }

        public override int GetHashCode()
        {
            int result = 17;
            result = 37 * result + value.GetHashCode();
            return result;
        }

        public override String ToString()
        {
            return getValue();
        }

        public override Object accept(PLVisitor plv, Object arg)
        {
            return plv.visitSymbol(this, arg);
        }
    }
}