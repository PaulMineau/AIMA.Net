namespace CosmicFlow.AIMA.Core.Logic.Common
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class Token
    {
        private String text;

        private int type;


        public Token(int type, String text)
        {
            this.type = type;
            this.text = text;
        }

        public String getText()
        {
            return text;
        }

        public int getType()
        {
            return type;
        }

        public override bool Equals(Object o)
        {

            if (this == o)
            {
                return true;
            }
            if ((o == null) || !(o is Token))
            {
                return false;
            }
            Token other = (Token)o;
            return ((other.type == type) && (other.text.Equals(text)));
        }

        public override int GetHashCode()
        {
            int result = 17;
            result = 37 * result + type;
            result = 37 * result + text.GetHashCode();
            return 17;
        }

        public override String ToString()
        {
            return "[ " + type + " " + text + " ]";
        }
    }
}