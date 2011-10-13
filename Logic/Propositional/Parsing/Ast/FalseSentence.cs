namespace AIMA.Core.Logic.Propositional.Parsing.Ast
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Propositional.Parsing;

    /**
     * @author Ravi Mohan
     * 
     */
    public class FalseSentence : AtomicSentence
    {

        public override String ToString()
        {
            return "FALSE";
        }

        public override Object accept(PLVisitor plv, Object arg)
        {
            return plv.visitFalseSentence(this, arg);
        }
    }
}