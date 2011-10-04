namespace CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class SymbolComparator : IComparer<Symbol>
    {


        public int Compare(Symbol one, Symbol two)
        {
            return one.getValue().CompareTo(two.getValue());
        }
    }
}