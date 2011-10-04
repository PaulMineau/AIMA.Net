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
    public class PositiveSymbolCollector : BasicTraverser
    {

        public override Object visitSymbol(Symbol symbol, Object arg)
        {
            List<Symbol> s = (List<Symbol>)arg;
            s.Add(symbol);// add ALL symbols not discarded by the visitNotSentence
            // mathod
            return arg;
        }

        public override Object visitNotSentence(UnarySentence ns, Object arg)
        {
            List<Symbol> s = (List<Symbol>)arg;
            if (ns.getNegated() is Symbol)
            {
                // do nothing .do NOT add a negated Symbol
            }
            else
            {
                s = SetOps
                        .union(s, (List<Symbol>)ns.getNegated().accept(this, arg));
            }
            return s;
        }

        public List<Symbol> getPositiveSymbolsIn(Sentence sentence)
        {
            return (List<Symbol>)sentence.accept(this, new List<Symbol>());
        }
    }
}