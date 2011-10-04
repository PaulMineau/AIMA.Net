namespace CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;

    /**
     * @author Ravi Mohan
     * 
     */
    public class MultiSentence : ComplexSentence
    {
        private String op;

        private List<Sentence> sentences;

        public MultiSentence(String op, List<Sentence> sentences)
        {
            this.op = op;
            this.sentences = sentences;
        }

        public String getOperator()
        {
            return op;
        }

        public List<Sentence> getSentences()
        {
            return sentences;
        }

        public override bool Equals(Object o)
        {

            if (this == o)
            {
                return true;
            }
            if ((o == null) || !(o is MultiSentence))
            {
                return false;
            }
            MultiSentence sen = (MultiSentence)o;
            return ((sen.getOperator().Equals(getOperator())) && (sen
                    .getSentences().Equals(getSentences())));

        }

        public override int GetHashCode()
        {
            int result = 17;
            foreach (Sentence s in sentences)
            {
                result = 37 * result + s.GetHashCode();
            }
            return result;
        }

        public override String ToString()
        {
            String part1 = "( " + getOperator() + " ";
            for (int i = 0; i < getSentences().Count; i++)
            {
                part1 = part1 + sentences[i].ToString() + " ";
            }
            return part1 + " ) ";
        }

        public override Object accept(PLVisitor plv, Object arg)
        {
            return plv.visitMultiSentence(this, arg);
        }
    }
}