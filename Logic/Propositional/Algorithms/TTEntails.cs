namespace CosmicFlow.AIMA.Core.Logic.Propositional.Algorithms
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Visitors;
    using CosmicFlow.AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public class TTEntails
    {
        public bool ttEntails(KnowledgeBase kb, String alpha)
        {
            Sentence kbSentence = kb.asSentence();
            Sentence querySentence = (Sentence)new PEParser().parse(alpha);
            SymbolCollector collector = new SymbolCollector();
            List<Symbol> kbSymbols = collector.getSymbolsIn(kbSentence);
            List<Symbol> querySymbols = collector.getSymbolsIn(querySentence);
            List<Symbol> symbols = SetOps.union(kbSymbols, querySymbols);
            List<Symbol> symbolList = symbols;
            return ttCheckAll(kbSentence, querySentence, symbolList, new Model());
        }

        public bool ttCheckAll(Sentence kbSentence, Sentence querySentence,
                List<Symbol> symbols, Model model)
        {
            if (symbols.Count==0)
            {
                if (model.isTrue(kbSentence))
                {
                    // System.Console.WriteLine("#");
                    return model.isTrue(querySentence);
                }
                else
                {
                    // System.Console.WriteLine("0");
                    return true;
                }
            }
            else
            {
                Symbol symbol = (Symbol)Util.first(symbols);
                List<Symbol> rest = Util.rest(symbols);

                Model trueModel = model.extend(new Symbol(symbol.getValue()), true);
                Model falseModel = model.extend(new Symbol(symbol.getValue()),
                        false);
                return (ttCheckAll(kbSentence, querySentence, rest, trueModel) && (ttCheckAll(
                        kbSentence, querySentence, rest, falseModel)));
            }
        }
    }
}