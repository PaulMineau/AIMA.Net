namespace AIMA.Core.Logic.Propositional.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AIMA.Core.Logic.Propositional.Parsing;
    using AIMA.Core.Logic.Propositional.Parsing.Ast;
    using AIMA.Core.Logic.Propositional.Visitors;
    using AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public class DPLL
    {

//        private const Converter<Symbol> SYMBOL_CONVERTER = new Converter<Symbol>();

        public bool dpllSatisfiable(Sentence s)
        {

            return dpllSatisfiable(s, new Model());
        }

        public bool dpllSatisfiable(String s)
        {
            Sentence sen = (Sentence)new PEParser().parse(s);
            return dpllSatisfiable(sen, new Model());
        }

        public bool dpllSatisfiable(Sentence s, Model m)
        {
            List<Sentence> clauses = new CNFClauseGatherer()
                    .getClausesFrom(new CNFTransformer().transform(s));
            List<Symbol> symbols = new SymbolCollector()
                    .getSymbolsIn(s);
            // System.Console.WriteLine(" numberOfSymbols = " + symbols.Count);
            return dpll(clauses, symbols, m);
        }

        public List<Sentence> clausesWithNonTrueValues(List<Sentence> clauseList,
                Model model)
        {
            List<Sentence> clausesWithNonTrueValues = new List<Sentence>();
            for (int i = 0; i < clauseList.Count; i++)
            {
                Sentence clause = clauseList[i];
                if (!(isClauseTrueInModel(clause, model)))
                {
                    if (!(clausesWithNonTrueValues.Contains(clause)))
                    {// defensive
                        // programming not really necessary
                        clausesWithNonTrueValues.Add(clause);
                    }
                }

            }
            return clausesWithNonTrueValues;
        }

        public SymbolValuePair findPureSymbolValuePair(List<Sentence> clauseList,
                Model model, List<Symbol> symbols)
        {
            List<Sentence> _clausesWithNonTrueValues = clausesWithNonTrueValues(clauseList,
                    model);
            Sentence nonTrueClauses = LogicUtils.chainWith("AND",
                    _clausesWithNonTrueValues);
            // System.Console.WriteLine("Unsatisfied clauses = "
            // + clausesWithNonTrueValues.Count);
            List<Symbol> symbolsAlreadyAssigned = new List<Symbol>( model.getAssignedSymbols());

            // debug
            // List symList = asList(symbolsAlreadyAssigned);
            //
            // System.Console.WriteLine(" assignedSymbols = " + symList.Count);
            // if (symList.Count == 52) {
            // System.Console.WriteLine("untrue clauses = " + clausesWithNonTrueValues);
            // System.Console.WriteLine("model= " + model);
            // }

            // debug
            List<Symbol> purePositiveSymbols = SetOps
                    .difference(new SymbolClassifier()
                            .getPurePositiveSymbolsIn(nonTrueClauses),
                            symbolsAlreadyAssigned);

            List<Symbol> pureNegativeSymbols = SetOps
                    .difference(new SymbolClassifier()
                            .getPureNegativeSymbolsIn(nonTrueClauses),
                            symbolsAlreadyAssigned);
            // if none found return "not found
            if ((purePositiveSymbols.Count == 0)
                    && (pureNegativeSymbols.Count == 0))
            {
                return new SymbolValuePair();// automatically set to null values
            }
            else
            {
                if (purePositiveSymbols.Count > 0)
                {
                    Symbol symbol =purePositiveSymbols[0];
                    if (pureNegativeSymbols.Contains(symbol))
                    {
                        throw new ApplicationException("Symbol " + symbol.getValue()
                                + "misclassified");
                    }
                    return new SymbolValuePair(symbol, true);
                }
                else
                {
                    Symbol symbol = new Symbol((pureNegativeSymbols[0])
                            .getValue());
                    if (purePositiveSymbols.Contains(symbol))
                    {
                        throw new ApplicationException("Symbol " + symbol.getValue()
                                + "misclassified");
                    }
                    return new SymbolValuePair(symbol, false);
                }
            }
        }

        //
        // PRIVATE METHODS
        //

        private bool dpll(List<Sentence> clauses, List<Symbol> symbols, Model model)
        {
            // List<Sentence> clauseList = asList(clauses);
            List<Sentence> clauseList = clauses;
            // System.Console.WriteLine("clauses are " + clauses.ToString());
            // if all clauses are true return true;
            if (areAllClausesTrue(model, clauseList))
            {
                // System.Console.WriteLine(model.ToString());
                return true;
            }
            // if even one clause is false return false
            if (isEvenOneClauseFalse(model, clauseList))
            {
                // System.Console.WriteLine(model.ToString());
                return false;
            }
            // System.Console.WriteLine("At least one clause is unknown");
            // try to find a unit clause
            SymbolValuePair svp = findPureSymbolValuePair(clauseList, model,
                    symbols);
            if (svp.notNull())
            {
                Symbol[] copy = new Symbol[symbols.Count];
                symbols.CopyTo(copy);
                List<Symbol> newSymbols = new List<Symbol>(copy);
                newSymbols.Remove(new Symbol(svp.symbol.getValue()));
                Model newModel = model.extend(new Symbol(svp.symbol.getValue()),
                        svp.value);
                return dpll(clauses, newSymbols, newModel);
            }

            SymbolValuePair svp2 = findUnitClause(clauseList, model, symbols);
            if (svp2.notNull())
            {
                Symbol[] copy = new Symbol[symbols.Count];
                symbols.CopyTo(copy);
                List<Symbol> newSymbols = new List<Symbol>(copy);
                newSymbols.Remove(new Symbol(svp2.symbol.getValue()));
                Model newModel = model.extend(new Symbol(svp2.symbol.getValue()),
                        svp2.value);
                return dpll(clauses, newSymbols, newModel);
            }

            Symbol symbol = (Symbol)symbols[0];
            // System.Console.WriteLine("default behaviour selecting " + symbol);
            Symbol[] symbolsArr = new Symbol[symbols.Count];
            symbols.CopyTo(symbolsArr);
            List<Symbol> newSymbols2 = symbolsArr.ToList<Symbol>();
            newSymbols2.RemoveAt(0);
            return (dpll(clauses, newSymbols2, model.extend(symbol, true)) || dpll(
                    clauses, newSymbols2, model.extend(symbol, false)));
        }

        private bool isEvenOneClauseFalse(Model model, List<Sentence> clauseList)
        {
            for (int i = 0; i < clauseList.Count; i++)
            {
                Sentence clause = (Sentence)clauseList[i];
                if (model.isFalse(clause))
                {
                    // System.Console.WriteLine(clause.ToString() + " is false");
                    return true;
                }

            }

            return false;
        }

        private bool areAllClausesTrue(Model model, List<Sentence> clauseList)
        {

            for (int i = 0; i < clauseList.Count; i++)
            {
                Sentence clause = (Sentence)clauseList[i];
                // System.Console.WriteLine("evaluating " + clause.ToString());
                if (!isClauseTrueInModel(clause, model))
                { // ie if false or
                    // UNKNOWN
                    // System.Console.WriteLine(clause.ToString()+ " is not true");
                    return false;
                }

            }
            return true;
        }

        private bool isClauseTrueInModel(Sentence clause, Model model)
        {
            List<Symbol> positiveSymbols = new SymbolClassifier().getPositiveSymbolsIn(clause);
            List<Symbol> negativeSymbols = new SymbolClassifier().getNegativeSymbolsIn(clause);

            foreach (Symbol symbol in positiveSymbols)
            {
                if ((model.isTrue(symbol)))
                {
                    return true;
                }
            }
            foreach (Symbol symbol in negativeSymbols)
            {
                if ((model.isFalse(symbol)))
                {
                    return true;
                }
            }
            return false;

        }

        private SymbolValuePair findUnitClause(List<Sentence> clauseList, Model model,
                List<Symbol> symbols)
        {
            for (int i = 0; i < clauseList.Count; i++)
            {
                Sentence clause = (Sentence)clauseList[i];
                if ((clause is Symbol)
                        && (!(model.getAssignedSymbols().Contains(clause))))
                {
                    // System.Console.WriteLine("found unit clause - assigning");
                    return new SymbolValuePair(new Symbol(((Symbol)clause)
                            .getValue()), true);
                }

                if (clause is UnarySentence)
                {
                    UnarySentence sentence = (UnarySentence)clause;
                    Sentence negated = sentence.getNegated();
                    if ((negated is Symbol)
                            && (!(model.getAssignedSymbols().Contains(negated))))
                    {
                        // System.Console.WriteLine("found unit clause type 2 -
                        // assigning");
                        return new SymbolValuePair(new Symbol(((Symbol)negated)
                                .getValue()), false);
                    }
                }

            }

            return new SymbolValuePair();// failed to find any unit clause;

        }

        public class SymbolValuePair
        {
            public Symbol symbol;// public to avoid unnecessary get and set

            // accessors

            public bool value;

            public SymbolValuePair()
            {
                // represents "No Symbol found with a bool value that makes all
                // its literals true
                symbol = null;
            }

            public SymbolValuePair(Symbol symbol, bool b)
            {
                // represents "Symbol found with a bool value that makes all
                // its literals true
                this.symbol = symbol;
                value = b;
            }

            public bool notNull()
            {
                return (symbol != null) && (value != null);
            }

            public override String ToString()
            {
                String symbolString, valueString;
                if (symbol == null)
                {
                    symbolString = "NULL";
                }
                else
                {
                    symbolString = symbol.ToString();
                }
                if (value == null)
                {
                    valueString = "NULL";
                }
                else
                {
                    valueString = value.ToString();
                }
                return symbolString + " -> " + valueString;
            }
        }
    }
}