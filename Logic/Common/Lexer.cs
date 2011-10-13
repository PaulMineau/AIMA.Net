namespace AIMA.Core.Logic.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    /**
     * @author Ravi Mohan
     * 
     */
    public abstract class Lexer
    {
        public abstract Token nextToken();

        protected StringReader input;

        protected int _lookAhead = 1;

        protected int[] lookAheadBuffer;

        public void setInput(String inputString)
        {
            lookAheadBuffer = new int[_lookAhead];
            this.input = new StringReader(inputString);
            fillLookAheadBuffer();
        }

        public void clear()
        {
            this.input = null;
            lookAheadBuffer = null;
        }

        protected void fillLookAheadBuffer()
        {

                lookAheadBuffer[0] = (char)input.Read();
        }

        protected char lookAhead(int position)
        {
            return (char)lookAheadBuffer[position - 1];
        }

        protected bool isEndOfFile(int i)
        {
            return (-1 == i);
        }

        protected void loadNextCharacterFromInput()
        {

            bool eofEncountered = false;
            for (int i = 0; i < _lookAhead - 1; i++)
            {

                lookAheadBuffer[i] = lookAheadBuffer[i + 1];
                if (isEndOfFile(lookAheadBuffer[i]))
                {
                    eofEncountered = true;
                    break;
                }
            }
            if (!eofEncountered)
            {
                    lookAheadBuffer[_lookAhead - 1] = input.Read();
                
            }

        }

        protected void consume()
        {
            loadNextCharacterFromInput();
        }
    }
}