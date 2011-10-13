namespace AIMA.Core.Logic.Propositional.Parsing.Ast
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Common;
    using AIMA.Core.Logic.Propositional.Parsing;

    /**
     * @author Ravi Mohan
     * 
     */
    public abstract class Sentence : ParseTreeNode
    {

        public abstract Object accept(PLVisitor plv, Object arg);
    }
}