namespace AIMA.Core.Logic.Propositional.Algorithms
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Propositional.Parsing;
    using AIMA.Core.Logic.Propositional.Visitors;
    using AIMA.Core.Logic.Propositional.Parsing.Ast;

    /**
     * @author Ravi Mohan
     * 
     */
    public class KnowledgeBase
    {
        private List<Sentence> sentences;

        private PEParser parser;

        public KnowledgeBase()
        {
            sentences = new List<Sentence>();
            parser = new PEParser();
        }

        public void tell(String aSentence)
        {
            Sentence sentence = (Sentence)parser.parse(aSentence);
            if (!(sentences.Contains(sentence)))
            {
                sentences.Add(sentence);
            }
        }

        public void tellAll(String[] percepts)
        {
            for (int i = 0; i < percepts.Length; i++)
            {
                tell(percepts[i]);
            }

        }

        public int size()
        {
            return sentences.Count;
        }

        public Sentence asSentence()
        {
            return LogicUtils.chainWith("AND", sentences);
        }

        public bool askWithDpll(String queryString)
        {
            Sentence query = null, cnfForm = null;
            try
            {
                // just a check to see that the query is well formed
                query = (Sentence)parser.parse(queryString);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("error parsing query" + e);
            }

            Sentence kbSentence = asSentence();
            Sentence kbPlusQuery = null;
            if (kbSentence != null)
            {
                kbPlusQuery = (Sentence)parser.parse(" ( " + kbSentence.ToString()
                        + " AND (NOT " + queryString + " ))");
            }
            else
            {
                kbPlusQuery = query;
            }
            try
            {
                cnfForm = new CNFTransformer().transform(kbPlusQuery);
                // System.Console.WriteLine(cnfForm.ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("error converting kb +  query to CNF "
                        + e);

            }
            return !new DPLL().dpllSatisfiable(cnfForm);
        }

        public bool askWithTTEntails(String queryString)
        {

            return new TTEntails().ttEntails(this, queryString);
        }

        public override String ToString()
        {
            if (sentences.Count == 0)
            {
                return "";
            }
            else
                return asSentence().ToString();
        }

        public List<Sentence> getSentences()
        {
            return sentences;
        }
    }
}