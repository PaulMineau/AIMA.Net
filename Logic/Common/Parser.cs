namespace AIMA.Core.Logic.Common
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public abstract class Parser
    {

        protected Lexer lexer;

        protected Token[] lookAheadBuffer;

        protected int _lookAhead = 3;

        public abstract ParseTreeNode parse(String input);

        protected void fillLookAheadBuffer()
        {
            for (int i = 0; i < _lookAhead; i++)
            {
                lookAheadBuffer[i] = lexer.nextToken();
            }
        }

        protected Token lookAhead(int i)
        {
            return lookAheadBuffer[i - 1];
        }

        protected void consume()
        {
            loadNextTokenFromInput();
        }

        protected void loadNextTokenFromInput()
        {

            bool eoiEncountered = false;
            for (int i = 0; i < _lookAhead - 1; i++)
            {

                lookAheadBuffer[i] = lookAheadBuffer[i + 1];
                if (isEndOfInput(lookAheadBuffer[i]))
                {
                    eoiEncountered = true;
                    break;
                }
            }
            if (!eoiEncountered)
            {
                try
                {
                    lookAheadBuffer[_lookAhead - 1] = lexer.nextToken();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

        }

        protected bool isEndOfInput(Token t)
        {
            return (t.getType() == (int)LogicTokenTypes.EOI);
        }

        protected void match(String terminalSymbol)
        {
            if (lookAhead(1).getText().Equals(terminalSymbol))
            {
                consume();
            }
            else
            {
                throw new ApplicationException(
                        "Syntax error detected at match. Expected "
                                + terminalSymbol + " but got "
                                + lookAhead(1).getText());
            }

        }
    }
}