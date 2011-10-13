namespace AIMA.Core.Logic.Propositional.Parsing.Ast
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Propositional.Parsing;

    /**
     * @author Ravi Mohan
     * 
     */
    public class UnarySentence : ComplexSentence
    {
        private Sentence negated;

        public Sentence getNegated()
        {
            return negated;
        }

        public UnarySentence(Sentence negated)
        {
            this.negated = negated;
        }

        public override bool Equals(Object o)
        {

            if (this == o)
            {
                return true;
            }
            if ((o == null) || !(o is UnarySentence))
            {
                return false;
            }
            UnarySentence ns = (UnarySentence)o;
            return (ns.negated.Equals(negated));

        }

        public override int GetHashCode()
        {
            int result = 17;
            result = 37 * result + negated.GetHashCode();
            return result;
        }

        public override String ToString()
        {
            return " ( NOT " + negated.ToString() + " ) ";
        }

        public override Object accept(PLVisitor plv, Object arg)
        {
            return plv.visitNotSentence(this, arg);
        }
    }
}