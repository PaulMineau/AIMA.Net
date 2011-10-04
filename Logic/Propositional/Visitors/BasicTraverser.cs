namespace CosmicFlow.AIMA.Core.Logic.Propositional.Visitors
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;
    using CosmicFlow.AIMA.Core.Util;

    /**
     * @author Ravi Mohan
     * 
     */

    /*
     * Super class of Visitors that are "read only" and gather information from an
     * existing parse tree .
     */
    public class BasicTraverser : PLVisitor
    {

        public virtual Object visitSymbol(Symbol s, Object arg)
        {
            return arg;
        }

        public virtual Object visitTrueSentence(TrueSentence ts, Object arg)
        {
            return arg;
        }

        public virtual Object visitFalseSentence(FalseSentence fs, Object arg)
        {
            return arg;
        }

        public virtual Object visitNotSentence(UnarySentence ns, Object arg)
        {
            List<Sentence> s = (List<Sentence>)arg;
            return SetOps.union(s, (List<Sentence>)ns.getNegated().accept(this, arg));
        }

        public virtual Object visitBinarySentence(BinarySentence bs, Object arg)
        {
            List<Sentence> s = new List<Sentence>();
            if (arg is List<Symbol>)
            {
                List<Symbol> symbols = ((List<Symbol>)arg);
                foreach (Symbol symbol in symbols)
                {
                    s.Add((Sentence)symbol);
                }
            }
            else
            {
                throw new ArgumentException("Could not cast arg to List<Sentence>");
            }
            List<Sentence> termunion = SetOps.union((List<Sentence>)bs.getFirst().accept(this, arg),
                    (List<Sentence>)bs.getSecond().accept(this, arg));
            return SetOps.union(s, termunion);
        }

        public virtual Object visitMultiSentence(MultiSentence fs, Object arg)
        {
            throw new ApplicationException("Can't handle MultiSentence");
        }
    }
}
