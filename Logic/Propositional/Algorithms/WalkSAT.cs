namespace AIMA.Core.Logic.Propositional.Algorithms
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Propositional.Parsing;
    using AIMA.Core.Logic.Propositional.Parsing.Ast;
    using AIMA.Core.Logic.Propositional.Visitors;
    using AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public class WalkSAT
    {
        private Model myModel;

        private Random random = new Random();

        public Model findModelFor(String logicalSentence, int numberOfFlips,
                double probabilityOfRandomWalk)
        {
            myModel = new Model();
            Sentence s = (Sentence)new PEParser().parse(logicalSentence);
            CNFTransformer transformer = new CNFTransformer();
            CNFClauseGatherer clauseGatherer = new CNFClauseGatherer();
            SymbolCollector sc = new SymbolCollector();

            List<Symbol> symbols = sc.getSymbolsIn(s);
            Random r = new Random();
            for (int i = 0; i < symbols.Count; i++)
            {
                Symbol sym = (Symbol)symbols[i];
                myModel = myModel.extend(sym, Util.randombool());
            }
            List<Sentence> clauses = clauseGatherer.getClausesFrom(transformer
                            .transform(s));

            for (int i = 0; i < numberOfFlips; i++)
            {
                if (getNumberOfClausesSatisfiedIn(clauses, myModel) == clauses.Count)
                {
                    return myModel;
                }
                Sentence clause = clauses[random.Next(clauses.Count)];

                List<Symbol> symbolsInClause = sc
                        .getSymbolsIn(clause);
                if (random.NextDouble() >= probabilityOfRandomWalk)
                {
                    Symbol randomSymbol = symbolsInClause[random
                            .Next(symbolsInClause.Count)];
                    myModel = myModel.flip(randomSymbol);
                }
                else
                {
                    Symbol symbolToFlip = getSymbolWhoseFlipMaximisesSatisfiedClauses(
                            clauses,
                            symbolsInClause, myModel);
                    myModel = myModel.flip(symbolToFlip);
                }

            }
            return null;
        }

        private Symbol getSymbolWhoseFlipMaximisesSatisfiedClauses(
                List<Sentence> clauses, List<Symbol> symbols, Model model)
        {
            if (symbols.Count > 0)
            {
                Symbol retVal = symbols[0];
                int maxClausesSatisfied = 0;
                for (int i = 0; i < symbols.Count; i++)
                {
                    Symbol sym = symbols[i];
                    if (getNumberOfClausesSatisfiedIn(clauses, model.flip(sym)) > maxClausesSatisfied)
                    {
                        retVal = sym;
                        maxClausesSatisfied = getNumberOfClausesSatisfiedIn(
                                clauses, model.flip(sym));
                    }
                }
                return retVal;
            }
            else
            {
                return null;
            }

        }

        private int getNumberOfClausesSatisfiedIn(List<Sentence> clauses, Model model)
        {
            int retVal = 0;
            foreach(Sentence s in clauses)
            {
                if (model.isTrue(s))
                {
                    retVal += 1;
                }
            }
            return retVal;
        }
    }
}
