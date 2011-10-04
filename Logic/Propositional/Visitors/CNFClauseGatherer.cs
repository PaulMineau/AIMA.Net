namespace CosmicFlow.AIMA.Core.Logic.Propositional.Visitors
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;

    /**
     * @author Ravi Mohan
     * 
     */
    public class CNFClauseGatherer : BasicTraverser
    {
        AndDetector detector;

        public CNFClauseGatherer()
        {
            detector = new AndDetector();
        }

        public override Object visitBinarySentence(BinarySentence bs, Object args)
        {

            List<Sentence> soFar = (List<Sentence>)args;

            Sentence first = bs.getFirst();
            Sentence second = bs.getSecond();
            processSubTerm(second, processSubTerm(first, soFar));

            return soFar;

        }

        public List<Sentence> getClausesFrom(Sentence sentence)
        {
            List<Sentence> set = new List<Sentence>();
            if (sentence is Symbol)
            {
                set.Add(sentence);
            }
            else if (sentence is UnarySentence)
            {
                set.Add(sentence);
            }
            else
            {
                set = (List<Sentence>)sentence.accept(this, set);
            }
            return set;
        }

        //
        // PRIVATE METHODS
        //
        private List<Sentence> processSubTerm(Sentence s, List<Sentence> soFar)
        {
            if (detector.containsEmbeddedAnd(s))
            {
                return (List<Sentence>)s.accept(this, soFar);
            }
            else
            {
                soFar.Add(s);
                return soFar;
            }
        }
    }
}