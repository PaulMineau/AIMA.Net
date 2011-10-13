namespace AIMA.Core.Logic.FOL.Parsing
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Common;
    using AIMA.Core.Logic.FOL;
    using System.Text;
using System.Text.RegularExpressions;
    using AIMA.Core.Logic.FOL.Domain;

    /**
     * @author Ravi Mohan
     * 
     */
    public class FOLLexer : Lexer
    {
        private FOLDomain domain;
        private List<String> connectors, quantifiers;

        public FOLLexer(FOLDomain domain)
        {
            this.domain = domain;

            connectors = new List<String>();
            connectors.Add(Connectors.NOT);
            connectors.Add(Connectors.AND);
            connectors.Add(Connectors.OR);
            connectors.Add(Connectors.IMPLIES);
            connectors.Add(Connectors.BICOND);

            quantifiers = new List<String>();
            quantifiers.Add(Quantifiers.FORALL);
            quantifiers.Add(Quantifiers.EXISTS);
        }

        public FOLDomain getFOLDomain()
        {
            return domain;
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
            else if (lookAhead(1) == ',')
            {
                consume();
                return new Token((int)LogicTokenTypes.COMMA, ",");

            }
            else if (identifierDetected())
            {
                // System.Console.WriteLine("identifier detected");
                return identifier();
            }
            else if (char.IsWhiteSpace(lookAhead(1)))
            {
                consume();
                return nextToken();
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
            return char.IsLetter(c) || c == '_' || c == '$' || char.IsNumber(c);
        }

        private Token identifier()
        {
           

            StringBuilder sbuf = new StringBuilder();
            while ((isJavaIdentifierStart(lookAhead(1)))
                    || partOfConnector())
            {
                sbuf.Append(lookAhead(1));
                consume();
            }
            String readString = sbuf.ToString();
            // System.Console.WriteLine(readString);
            if (connectors.Contains(readString))
            {
                return new Token((int)LogicTokenTypes.CONNECTOR, readString);
            }
            else if (quantifiers.Contains(readString))
            {
                return new Token((int)LogicTokenTypes.QUANTIFIER, readString);
            }
            else if (domain.getPredicates().Contains(readString))
            {
                return new Token((int)LogicTokenTypes.PREDICATE, readString);
            }
            else if (domain.getFunctions().Contains(readString))
            {
                return new Token((int)LogicTokenTypes.FUNCTION, readString);
            }
            else if (domain.getConstants().Contains(readString))
            {
                return new Token((int)LogicTokenTypes.CONSTANT, readString);
            }
            else if (isVariable(readString))
            {
                return new Token((int)LogicTokenTypes.VARIABLE, readString);
            }
            else if (readString.Equals("="))
            {
                return new Token((int)LogicTokenTypes.EQUALS, readString);
            }
            else
            {
                throw new ApplicationException("Lexing error on character "
                        + lookAhead(1));
            }

        }

        private bool isVariable(String s)
        {
            return (char.IsLower(s[0]));
        }

        private bool identifierDetected()
        {
            return (isJavaIdentifierStart(((char)lookAheadBuffer[0]))
                    || partOfConnector());
        }

        private bool partOfConnector()
        {
            return (lookAhead(1) == '=') || (lookAhead(1) == '<')
                    || (lookAhead(1) == '>');
        }
    }
}