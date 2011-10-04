namespace CosmicFlow.AIMA.Core.Logic.Propositional.Algorithms
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Visitors;
    using CosmicFlow.AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */
    public class PLFCEntails
    {

        private Dictionary<HornClause, int> count;

        private Dictionary<Symbol, bool> _inferred;

        private Stack<Symbol> agenda;

        public PLFCEntails()
        {
            count = new Dictionary<HornClause, int>();
            _inferred = new Dictionary<Symbol, bool>();
            agenda = new Stack<Symbol>();
        }

        public bool plfcEntails(KnowledgeBase kb, String s)
        {
            return plfcEntails(kb, new Symbol(s));
        }

        public bool plfcEntails(KnowledgeBase kb, Symbol q)
        {
            List<HornClause> hornClauses = asHornClauses(kb.getSentences());
            while (agenda.Count != 0)
            {
                Symbol p = agenda.Pop();
                while (!inferred(p))
                {
                    _inferred.Add(p, true);

                    for (int i = 0; i < hornClauses.Count; i++)
                    {
                        HornClause hornClause = hornClauses[i];
                        if (hornClause.premisesContainsSymbol(p))
                        {
                            decrementCount(hornClause);
                            if (countisZero(hornClause))
                            {
                                if (hornClause.head().Equals(q))
                                {
                                    return true;
                                }
                                else
                                {
                                    agenda.Push(hornClause.head());
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private List<HornClause> asHornClauses(List<Sentence> sentences)
        {
            List<HornClause> hornClauses = new List<HornClause>();
            for (int i = 0; i < sentences.Count; i++)
            {
                Sentence sentence = (Sentence)sentences[i];
                HornClause clause = new HornClause(sentence,this);
                hornClauses.Add(clause);
            }
            return hornClauses;
        }

        private bool countisZero(HornClause hornClause)
        {

            return count[hornClause] == 0;
        }

        private void decrementCount(HornClause hornClause)
        {
            count[hornClause]--;
        }

        private bool inferred(Symbol p)
        {
            Object value = _inferred[p];
            return ((value == null) || value.Equals(true));
        }

        public class HornClause
        {
            List<Symbol> premiseSymbols;

            Symbol _head;

            PLFCEntails plfcEntails;

            public HornClause(Sentence sentence, PLFCEntails plfcEntails)
            {
                this.plfcEntails = plfcEntails;
                if (sentence is Symbol)
                {
                    _head = (Symbol)sentence;
                    plfcEntails.agenda.Push(_head);
                    premiseSymbols = new List<Symbol>();
                    plfcEntails.count.Add(this, 0);
                    plfcEntails._inferred.Add(_head, false);
                }
                else if (!isImpliedSentence(sentence))
                {
                    throw new ApplicationException("Sentence " + sentence
                            + " is not a horn clause");

                }
                else
                {
                    BinarySentence bs = (BinarySentence)sentence;
                    _head = (Symbol)bs.getSecond();
                    if (plfcEntails._inferred.ContainsKey(_head))
                    {
                        plfcEntails._inferred[_head] = false;
                    }
                    else
                    {
                        plfcEntails._inferred.Add(_head, false);
                    }
                    List<Symbol> symbolsInPremise = new SymbolCollector()
                            .getSymbolsIn(bs.getFirst());
                    foreach(Symbol s in symbolsInPremise)
                    {
                        plfcEntails._inferred.Add(s, false);
                    }
                    premiseSymbols = symbolsInPremise;
                    plfcEntails.count.Add(this, premiseSymbols.Count);
                }

            }

            private bool isImpliedSentence(Sentence sentence)
            {
                return ((sentence is BinarySentence) && ((BinarySentence)sentence)
                        .getOperator().Equals("=>"));
            }

            public Symbol head()
            {

                return _head;
            }

            public bool premisesContainsSymbol(Symbol q)
            {
                return premiseSymbols.Contains(q);
            }

            public List<Symbol> getPremiseSymbols()
            {
                return premiseSymbols;
            }

            public override bool Equals(Object o)
            {

                if (this == o)
                {
                    return true;
                }
                if ((o == null) || !(o is PLFCEntails))
                {
                    return false;
                }
                HornClause ohc = (HornClause)o;
                if (premiseSymbols.Count != ohc.premiseSymbols.Count)
                {
                    return false;
                }

                foreach (Symbol s in premiseSymbols)
                {
                    if (!ohc.premiseSymbols.Contains(s))
                    {
                        return false;
                    }
                }

                return true;

            }


            public override int GetHashCode()
            {
                int result = 17;
                foreach (Symbol s in premiseSymbols)
                {
                    result = 37 * result + s.GetHashCode();
                }
                return result;
            }

            public override String ToString()
            {
                return premiseSymbols.ToString() + " => " + _head;
            }
        }
    }
}