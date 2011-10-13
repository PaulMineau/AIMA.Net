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
    public interface PLVisitor : Visitor
    {
        Object visitSymbol(Symbol s, Object arg);

        Object visitTrueSentence(TrueSentence ts, Object arg);

        Object visitFalseSentence(FalseSentence fs, Object arg);

        Object visitNotSentence(UnarySentence fs, Object arg);

        Object visitBinarySentence(BinarySentence fs, Object arg);

        Object visitMultiSentence(MultiSentence fs, Object arg);
    }
}