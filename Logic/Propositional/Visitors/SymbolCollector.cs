namespace CosmicFlow.AIMA.Core.Logic.Propositional.Visitors
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;

    /**
     * @author Ravi Mohan
     * 
     */
    public class SymbolCollector : BasicTraverser
    {

        public override Object visitSymbol(Symbol s, Object arg)
        {
            List<Symbol> symbolsCollectedSoFar = (List<Symbol>)arg;
            symbolsCollectedSoFar.Add(new Symbol(s.getValue()));
            return symbolsCollectedSoFar;
        }

        public List<Symbol> getSymbolsIn(Sentence s)
        {
            if (s == null)
            {// empty knowledge bases == null fix this later
                return new List<Symbol>();
            }
            return (List<Symbol>)s.accept(this, new List<Symbol>());
        }
    }
}