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
    public class PLResolution
    {

        public bool plResolution(KnowledgeBase kb, String alpha)
        {
            return plResolution(kb, (Sentence)new PEParser().parse(alpha));
        }

        public bool plResolution(KnowledgeBase kb, Sentence alpha)
        {
            Sentence kBAndNotAlpha = new BinarySentence("AND", kb.asSentence(),
                    new UnarySentence(alpha));
            List<Sentence> clauses = new CNFClauseGatherer()
                    .getClausesFrom(new CNFTransformer().transform(kBAndNotAlpha));
            clauses = filterOutClausesWithTwoComplementaryLiterals(clauses);
            List<Sentence> newClauses = new List<Sentence>();
            while (true)
            {
                List<List<Sentence>> pairs = getCombinationPairs(clauses);

                for (int i = 0; i < pairs.Count; i++)
                {
                    List<Sentence> pair = pairs[i];
                    // System.Console.WriteLine("pair number" + i+" of "+pairs.Count);
                    List<Sentence> resolvents = plResolve(pair[0], pair[1]);
                    resolvents = filterOutClausesWithTwoComplementaryLiterals(resolvents);

                    if (resolvents.Contains(new Symbol("EMPTY_CLAUSE")))
                    {
                        return true;
                    }
                    newClauses = SetOps.union(newClauses, resolvents);
                    // System.Console.WriteLine("clauseslist size = " +clauses.Count);

                }
                if (SetOps.intersection(newClauses, clauses).Count == newClauses
                        .Count)
                {// subset test
                    return false;
                }
                clauses = SetOps.union(newClauses, clauses);
                clauses = filterOutClausesWithTwoComplementaryLiterals(clauses);
            }

        }

        public List<Sentence> plResolve(Sentence clause1, Sentence clause2)
        {
            List<Sentence> resolvents = new List<Sentence>();
            ClauseSymbols cs = new ClauseSymbols(clause1, clause2);
            List<Symbol> complementedSymbols = cs.getComplementedSymbols();
            foreach(Symbol symbol in complementedSymbols)
            {
                resolvents.Add(createResolventClause(cs, symbol));
            }

            return resolvents;
        }

        public bool plResolution(String kbs, String alphaString)
        {
            KnowledgeBase kb = new KnowledgeBase();
            kb.tell(kbs);
            Sentence alpha = (Sentence)new PEParser().parse(alphaString);
            return plResolution(kb, alpha);
        }

        //
        // PRIVATE METHODS
        //

        private List<Sentence> filterOutClausesWithTwoComplementaryLiterals(
                List<Sentence> clauses)
        {
            List<Sentence> filtered = new List<Sentence>();
            SymbolClassifier classifier = new SymbolClassifier();
            foreach(Sentence clause in clauses)
            {

                List<Symbol> positiveSymbols = classifier
                        .getPositiveSymbolsIn(clause);
                List<Symbol> negativeSymbols = classifier
                        .getNegativeSymbolsIn(clause);
                if ((SetOps.intersection(positiveSymbols, negativeSymbols).Count == 0))
                {
                    filtered.Add(clause);
                }
            }
            return filtered;
        }

        private Sentence createResolventClause(ClauseSymbols cs, Symbol toRemove)
        {
            List<Symbol> positiveSymbols = SetOps
                    .union(cs.clause1PositiveSymbols, cs.clause2PositiveSymbols);
            List<Symbol> negativeSymbols = SetOps
                    .union(cs.clause1NegativeSymbols, cs.clause2NegativeSymbols);
            if (positiveSymbols.Contains(toRemove))
            {
                positiveSymbols.Remove(toRemove);
            }
            if (negativeSymbols.Contains(toRemove))
            {
                negativeSymbols.Remove(toRemove);
            }

            positiveSymbols.Sort(new SymbolComparator());
           negativeSymbols.Sort( new SymbolComparator());

            List<Sentence> sentences = new List<Sentence>();
            for (int i = 0; i < positiveSymbols.Count; i++)
            {
                sentences.Add(positiveSymbols[i]);
            }
            for (int i = 0; i < negativeSymbols.Count; i++)
            {
                sentences.Add(new UnarySentence(negativeSymbols[i]));
            }
            if (sentences.Count == 0)
            {
                return new Symbol("EMPTY_CLAUSE"); // == empty clause
            }
            else
            {
                return LogicUtils.chainWith("OR", sentences);
            }

        }

        private List<List<Sentence>> getCombinationPairs(List<Sentence> clausesList)
        {
            int odd = clausesList.Count % 2;
            int midpoint = 0;
            if (odd == 1)
            {
                midpoint = (clausesList.Count / 2) + 1;
            }
            else
            {
                midpoint = (clausesList.Count / 2);
            }

            List<List<Sentence>> pairs = new List<List<Sentence>>();
            for (int i = 0; i < clausesList.Count; i++)
            {
                for (int j = i; j < clausesList.Count; j++)
                {
                    List<Sentence> pair = new List<Sentence>();
                    Sentence first = clausesList[i];
                    Sentence second = clausesList[j];

                    if (!(first.Equals(second)))
                    {
                        pair.Add(first);
                        pair.Add(second);
                        pairs.Add(pair);
                    }
                }
            }
            return pairs;
        }

        class ClauseSymbols
        {
            public List<Symbol> clause1Symbols, clause1PositiveSymbols,
                    clause1NegativeSymbols;

            public List<Symbol> clause2Symbols, clause2PositiveSymbols,
                    clause2NegativeSymbols;

            public List<Symbol> positiveInClause1NegativeInClause2,
                    negativeInClause1PositiveInClause2;

            public ClauseSymbols(Sentence clause1, Sentence clause2)
            {

                SymbolClassifier classifier = new SymbolClassifier();

                clause1Symbols = classifier.getSymbolsIn(clause1);
                clause1PositiveSymbols = classifier.getPositiveSymbolsIn(clause1);
                clause1NegativeSymbols = classifier.getNegativeSymbolsIn(clause1);

                clause2Symbols = classifier.getSymbolsIn(clause2);
                clause2PositiveSymbols = classifier.getPositiveSymbolsIn(clause2);
                clause2NegativeSymbols = classifier.getNegativeSymbolsIn(clause2);

                positiveInClause1NegativeInClause2 = SetOps.intersection(
                        clause1PositiveSymbols, clause2NegativeSymbols);
                negativeInClause1PositiveInClause2 = SetOps.intersection(
                        clause1NegativeSymbols, clause2PositiveSymbols);

            }

            public List<Symbol> getComplementedSymbols()
            {
                return SetOps.union(positiveInClause1NegativeInClause2,
                        negativeInClause1PositiveInClause2);
            }

        }
    }
}