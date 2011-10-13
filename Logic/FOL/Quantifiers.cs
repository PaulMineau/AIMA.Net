namespace AIMA.Core.Logic.FOL
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class Quantifiers
    {
        public const String FORALL = "FORALL";
        public const String EXISTS = "EXISTS";

        public static bool isFORALL(String quantifier)
        {
            return FORALL.Equals(quantifier);
        }

        public static bool isEXISTS(String quantifier)
        {
            return EXISTS.Equals(quantifier);
        }
    }
}