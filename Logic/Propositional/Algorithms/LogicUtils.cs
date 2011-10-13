namespace AIMA.Core.Logic.Propositional.Algorithms
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Propositional.Parsing.Ast;

    /**
     * @author Ravi Mohan
     * 
     */
    public class LogicUtils
    {

        public static Sentence chainWith(String connector, List<Sentence> sentences)
        {
            if (sentences.Count == 0)
            {
                return null;
            }
            else if (sentences.Count == 1)
            {
                return (Sentence)sentences[0];
            }
            else
            {
                Sentence soFar = (Sentence)sentences[0];
                for (int i = 1; i < sentences.Count; i++)
                {
                    Sentence next = (Sentence)sentences[i];
                    soFar = new BinarySentence(connector, soFar, next);
                }
                return soFar;
            }
        }
    }
}