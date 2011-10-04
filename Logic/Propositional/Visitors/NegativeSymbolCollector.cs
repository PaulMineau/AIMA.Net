namespace CosmicFlow.AIMA.Core.Logic.Propositional.Visitors
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;
    using CosmicFlow.AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public class NegativeSymbolCollector : BasicTraverser
    {

        public override Object visitNotSentence(UnarySentence ns, Object arg)
        {
            List<Symbol> s = (List<Symbol>)arg;
            if (ns.getNegated() is Symbol)
            {
                s.Add((Symbol)ns.getNegated());
            }
            else
            {
                s = SetOps
                        .union(s, (List<Symbol>)ns.getNegated().accept(this, arg));
            }
            return s;
        }

        public List<Symbol> getNegativeSymbolsIn(Sentence s)
        {
            return (List<Symbol>)s.accept(this, new List<Symbol>());
        }
    }
}