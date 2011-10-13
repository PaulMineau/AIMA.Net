namespace AIMA.Core.Logic.Propositional.Visitors
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Propositional.Parsing.Ast;
    using AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public class SymbolClassifier
    {

        public List<Symbol> getPositiveSymbolsIn(Sentence sentence)
        {
            return new PositiveSymbolCollector().getPositiveSymbolsIn(sentence);
        }

        public List<Symbol> getNegativeSymbolsIn(Sentence sentence)
        {
            return new NegativeSymbolCollector().getNegativeSymbolsIn(sentence);
        }

        public List<Symbol> getPureNegativeSymbolsIn(Sentence sentence)
        {
            List<Symbol> allNegatives = getNegativeSymbolsIn(sentence);
            List<Symbol> allPositives = getPositiveSymbolsIn(sentence);
            return SetOps.difference(allNegatives, allPositives);
        }

        public List<Symbol> getPurePositiveSymbolsIn(Sentence sentence)
        {
            List<Symbol> allNegatives = getNegativeSymbolsIn(sentence);
            List<Symbol> allPositives = getPositiveSymbolsIn(sentence);
            return SetOps.difference(allPositives, allNegatives);
        }

        public List<Symbol> getPureSymbolsIn(Sentence sentence)
        {
            List<Symbol> allPureNegatives = getPureNegativeSymbolsIn(sentence);
            List<Symbol> allPurePositives = getPurePositiveSymbolsIn(sentence);
            return SetOps.union(allPurePositives, allPureNegatives);
        }

        public List<Symbol> getImpureSymbolsIn(Sentence sentence)
        {
            List<Symbol> allNegatives = getNegativeSymbolsIn(sentence);
            List<Symbol> allPositives = getPositiveSymbolsIn(sentence);
            return SetOps.intersection(allPositives, allNegatives);
        }

        public List<Symbol> getSymbolsIn(Sentence sentence)
        {
            return new SymbolCollector().getSymbolsIn(sentence);
        }
    }
}