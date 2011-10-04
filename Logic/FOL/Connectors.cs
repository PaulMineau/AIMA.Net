namespace CosmicFlow.AIMA.Core.Logic.FOL
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class Connectors
    {
        public const String AND = "AND";

        public const String OR = "OR";

        public const String NOT = "NOT";

        public const String IMPLIES = "=>";

        public const String BICOND = "<=>";

        public static bool isAND(String connector)
        {
            return AND.Equals(connector);
        }

        public static bool isOR(String connector)
        {
            return OR.Equals(connector);
        }

        public static bool isNOT(String connector)
        {
            return NOT.Equals(connector);
        }

        public static bool isIMPLIES(String connector)
        {
            return IMPLIES.Equals(connector);
        }

        public static bool isBICOND(String connector)
        {
            return BICOND.Equals(connector);
        }
    }
}