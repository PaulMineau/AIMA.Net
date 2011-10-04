namespace CosmicFlow.AIMA.Core.Logic.Propositional.Parsing
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.Common;
    using System.Text;

    /**
     * @author Ravi Mohan
     * 
     */
    public class PELexer : Lexer
    {

        List<String> connectors;

        public PELexer()
        {
            connectors = new List<String>();
            connectors.Add("NOT");
            connectors.Add("AND");
            connectors.Add("OR");
            connectors.Add("=>");
            connectors.Add("<=>");
        }

        public PELexer(String inputString) : this()
        {
            setInput(inputString);
        }

        public override Token nextToken()
        {
            if (lookAhead(1) == '(')
            {
                consume();
                return new Token((int)LogicTokenTypes.LPAREN, "(");

            }
            else if (lookAhead(1) == ')')
            {
                consume();
                return new Token((int)LogicTokenTypes.RPAREN, ")");
            }
            else if (identifierDetected())
            {
                return symbol();

            }
            else if (char.IsWhiteSpace(lookAhead(1)))
            {
                consume();
                return nextToken();
                // return whiteSpace();
            }
            else if ((int)lookAhead(1) == 65535)
            {
                return new Token((int)LogicTokenTypes.EOI, "EOI");
            }
            else
            {
                throw new ApplicationException("Lexing error on character "
                        + lookAhead(1));
            }

        }

        private bool isJavaIdentifierStart(char c)
        {
            return char.IsLetter(c) || c == '_' || c == '$';
        }

        private bool identifierDetected()
        {
            return (isJavaIdentifierStart((char)lookAheadBuffer[0]))
                    || partOfConnector();
        }

        private bool partOfConnector()
        {
            return (lookAhead(1) == '=') || (lookAhead(1) == '<')
                    || (lookAhead(1) == '>');
        }

        private Token symbol()
        {
            StringBuilder sbuf = new StringBuilder();
            while ((char.IsLetterOrDigit(lookAhead(1)))
                    || (lookAhead(1) == '=') || (lookAhead(1) == '<')
                    || (lookAhead(1) == '>'))
            {
                sbuf.Append(lookAhead(1));
                consume();
            }
            String symbol = sbuf.ToString();
            if (isConnector(symbol))
            {
                return new Token((int)LogicTokenTypes.CONNECTOR, sbuf.ToString());
            }
            else if (symbol.ToLower().Equals("true"))
            {
                return new Token((int)LogicTokenTypes.TRUE, "TRUE");
            }
            else if (symbol.ToLower().Equals("false"))
            {
                return new Token((int)LogicTokenTypes.FALSE, "FALSE");
            }
            else
            {
                return new Token((int)LogicTokenTypes.SYMBOL, sbuf.ToString());
            }

        }

        private Token connector()
        {
            StringBuilder sbuf = new StringBuilder();
            while (char.IsLetterOrDigit(lookAhead(1)))
            {
                sbuf.Append(lookAhead(1));
                consume();
            }
            return new Token((int)LogicTokenTypes.CONNECTOR, sbuf.ToString());
        }

        private Token whiteSpace()
        {
            StringBuilder sbuf = new StringBuilder();
            while (Char.IsWhiteSpace(lookAhead(1)))
            {
                sbuf.Append(lookAhead(1));
                consume();
            }
            return new Token((int)LogicTokenTypes.WHITESPACE, sbuf.ToString());

        }

        private bool isConnector(String aSymbol)
        {
            return (connectors.Contains(aSymbol));
        }
    }
}