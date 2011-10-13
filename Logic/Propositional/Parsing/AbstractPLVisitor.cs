namespace AIMA.Core.Logic.Propositional.Parsing
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Propositional.Parsing.Ast;

    /**
     * @author Ravi Mohan
     * 
     */
    public class AbstractPLVisitor : PLVisitor
    {
        private PEParser parser = new PEParser();

        public Object visitSymbol(Symbol s, Object arg)
        {
            return new Symbol(s.getValue());
        }

        public Object visitTrueSentence(TrueSentence ts, Object arg)
        {
            return new TrueSentence();
        }

        public Object visitFalseSentence(FalseSentence fs, Object arg)
        {
            return new FalseSentence();
        }

        public virtual Object visitNotSentence(UnarySentence fs, Object arg)
        {
            return new UnarySentence((Sentence)fs.getNegated().accept(this, arg));
        }

        public virtual Object visitBinarySentence(BinarySentence fs, Object arg)
        {
            return new BinarySentence(fs.getOperator(), (Sentence)fs.getFirst()
                    .accept(this, arg), (Sentence)fs.getSecond().accept(this, arg));
        }

        public Object visitMultiSentence(MultiSentence fs, Object arg)
        {
            List<Sentence> terms = fs.getSentences();
            List<Sentence> newTerms = new List<Sentence>();
            for (int i = 0; i < terms.Count; i++)
            {
                Sentence s = (Sentence)terms[i];
                Sentence subsTerm = (Sentence)s.accept(this, arg);
                newTerms.Add(subsTerm);
            }
            return new MultiSentence(fs.getOperator(), newTerms);
        }

        protected Sentence recreate(Object ast)
        {
            return (Sentence)parser.parse(((Sentence)ast).ToString());
        }
    }
}