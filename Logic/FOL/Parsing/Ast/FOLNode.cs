namespace CosmicFlow.AIMA.Core.Logic.FOL.Parsing.AST
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.Common;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing;

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public interface FOLNode : ParseTreeNode
    {
        String getSymbolicName();

        bool isCompound();

        List<FOLNode> getArgs();

        Object accept(FOLVisitor v, Object arg);

        FOLNode copy();
    }
}