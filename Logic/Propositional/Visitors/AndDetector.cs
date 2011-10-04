namespace CosmicFlow.AIMA.Core.Logic.Propositional.Visitors
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;

    /**
     * @author Ravi Mohan
     * 
     */
    public class AndDetector : PLVisitor
    {

        public Object visitSymbol(Symbol s, Object arg)
        {

            return false;
        }

        public Object visitTrueSentence(TrueSentence ts, Object arg)
        {
            return false;
        }

        public Object visitFalseSentence(FalseSentence fs, Object arg)
        {
            return false;
        }

        public Object visitNotSentence(UnarySentence fs, Object arg)
        {
            return fs.getNegated().accept(this, null);
        }

        public Object visitBinarySentence(BinarySentence fs, Object arg)
        {
            if (fs.isAndSentence())
            {
                return true;
            }
            else
            {
                bool first = ((bool)fs.getFirst().accept(this, null));
                bool second = ((bool)fs.getSecond().accept(this, null));
                return (first || second);
            }
        }

        public Object visitMultiSentence(MultiSentence fs, Object arg)
        {
            throw new ApplicationException("can't handle multisentences");
        }

        public bool containsEmbeddedAnd(Sentence s)
        {
            return (bool)s.accept(this, null);
        }
    }
}