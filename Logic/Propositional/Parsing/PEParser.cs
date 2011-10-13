namespace AIMA.Core.Logic.Propositional.Parsing
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Common;
    using AIMA.Core.Logic.Propositional.Parsing.Ast;

    /**
     * @author Ravi Mohan
     * 
     */
    public class PEParser : Parser
    {

        public PEParser()
        {
            lookAheadBuffer = new Token[_lookAhead];
        }

        public override ParseTreeNode parse(String inputString)
        {
            lexer = new PELexer(inputString);
            fillLookAheadBuffer();
            return parseSentence();
        }

        private TrueSentence parseTrue()
        {
            consume();
            return new TrueSentence();
        }

        private FalseSentence parseFalse()
        {
            consume();
            return new FalseSentence();
        }

        private Symbol parseSymbol()
        {
            String sym = lookAhead(1).getText();
            consume();
            return new Symbol(sym);
        }

        private AtomicSentence parseAtomicSentence()
        {
            Token t = lookAhead(1);
            if (t.getType() == (int)LogicTokenTypes.TRUE)
            {
                return parseTrue();
            }
            else if (t.getType() == (int)LogicTokenTypes.FALSE)
            {
                return parseFalse();
            }
            else if (t.getType() == (int)LogicTokenTypes.SYMBOL)
            {
                return parseSymbol();
            }
            else
            {
                throw new ApplicationException(
                        "Error in parseAtomicSentence with Token " + lookAhead(1));
            }
        }

        private UnarySentence parseNotSentence()
        {
            match("NOT");
            Sentence sen = parseSentence();
            return new UnarySentence(sen);
        }

        private MultiSentence parseMultiSentence()
        {
            consume();
            String connector = lookAhead(1).getText();
            consume();
            List<Sentence> sentences = new List<Sentence>();
            while (lookAhead(1).getType() != (int)LogicTokenTypes.RPAREN)
            {
                Sentence sen = parseSentence();
                // consume();
                sentences.Add(sen);
            }
            match(")");
            return new MultiSentence(connector, sentences);
        }

        private Sentence parseSentence()
        {
            if (detectAtomicSentence())
            {
                return parseAtomicSentence();
            }
            else if (detectBracket())
            {
                return parseBracketedSentence();
            }
            else if (detectNOT())
            {
                return parseNotSentence();
            }
            else
            {

                throw new ApplicationException("Parser Error Token = " + lookAhead(1));
            }
        }

        private bool detectNOT()
        {
            return (lookAhead(1).getType() == (int)LogicTokenTypes.CONNECTOR)
                    && (lookAhead(1).getText().Equals("NOT"));
        }

        private Sentence parseBracketedSentence()
        {

            if (detectMultiOperator())
            {
                return parseMultiSentence();
            }
            else
            {
                match("(");
                Sentence one = parseSentence();
                if (lookAhead(1).getType() == (int)LogicTokenTypes.RPAREN)
                {
                    match(")");
                    return one;
                }
                else if ((lookAhead(1).getType() == (int)LogicTokenTypes.CONNECTOR)
                      && (!(lookAhead(1).getText().Equals("Not"))))
                {
                    String connector = lookAhead(1).getText();
                    consume(); // connector
                    Sentence two = parseSentence();
                    match(")");
                    return new BinarySentence(connector, one, two);
                }

            }
            throw new ApplicationException(
                    " Runtime Exception at Bracketed Expression with token "
                            + lookAhead(1));
        }

        private bool detectMultiOperator()
        {
            return (lookAhead(1).getType() == (int)LogicTokenTypes.LPAREN)
                    && ((lookAhead(2).getText().Equals("AND")) || (lookAhead(2)
                            .getText().Equals("OR")));
        }

        private bool detectBracket()
        {
            return lookAhead(1).getType() == (int)LogicTokenTypes.LPAREN;
        }

        private bool detectAtomicSentence()
        {
            int type = lookAhead(1).getType();
            return (type == (int)LogicTokenTypes.TRUE)
                    || (type == (int)LogicTokenTypes.FALSE)
                    || (type == (int)LogicTokenTypes.SYMBOL);
        }
    }
}