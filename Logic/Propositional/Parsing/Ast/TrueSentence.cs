namespace CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;

    /**
     * @author Ravi Mohan
     * 
     */
    public class TrueSentence : AtomicSentence
    {

        public override String ToString()
        {
            return "TRUE";
        }

        public override Object accept(PLVisitor plv, Object arg)
        {
            return plv.visitTrueSentence(this, arg);
        }
    }
}