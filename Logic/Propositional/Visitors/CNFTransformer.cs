namespace AIMA.Core.Logic.Propositional.Visitors
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Logic.Propositional.Parsing;
    using AIMA.Core.Logic.Propositional.Parsing.Ast;

    /**
     * @author Ravi Mohan
     * 
     */
    public class CNFTransformer : AbstractPLVisitor
    {

        public override Object visitBinarySentence(BinarySentence bs, Object arg)
        {
            if (bs.isBiconditional())
            {
                return transformBiConditionalSentence(bs);
            }
            else if (bs.isImplication())
            {
                return transformImpliedSentence(bs);
            }
            else if (bs.isOrSentence()
                  && (bs.firstTermIsAndSentence() || bs.secondTermIsAndSentence()))
            {
                return distributeOrOverAnd(bs);
            }
            else
            {
                return base.visitBinarySentence(bs, arg);
            }
        }

        public override Object visitNotSentence(UnarySentence us, Object arg)
        {
            return transformNotSentence(us);
        }

        public Sentence transform(Sentence s)
        {
            Sentence toTransform = s;
            while (!(toTransform.Equals(step(toTransform))))
            {
                toTransform = step(toTransform);
            }

            return toTransform;
        }

        private Sentence step(Sentence s)
        {
            return (Sentence)s.accept(this, null);
        }

        private Sentence transformBiConditionalSentence(BinarySentence bs)
        {
            Sentence first = new BinarySentence("=>", (Sentence)bs.getFirst()
                    .accept(this, null), (Sentence)bs.getSecond().accept(this,
                    null));
            Sentence second = new BinarySentence("=>", (Sentence)bs.getSecond()
                    .accept(this, null), (Sentence)bs.getFirst()
                    .accept(this, null));
            return new BinarySentence("AND", first, second);
        }

        private Sentence transformImpliedSentence(BinarySentence bs)
        {
            Sentence first = new UnarySentence((Sentence)bs.getFirst().accept(
                    this, null));
            return new BinarySentence("OR", first, (Sentence)bs.getSecond()
                    .accept(this, null));
        }

        private Sentence transformNotSentence(UnarySentence us)
        {
            if (us.getNegated() is UnarySentence)
            {
                return (Sentence)((UnarySentence)us.getNegated()).getNegated()
                        .accept(this, null);
            }
            else if (us.getNegated() is BinarySentence)
            {
                BinarySentence bs = (BinarySentence)us.getNegated();
                if (bs.isAndSentence())
                {
                    Sentence first = new UnarySentence((Sentence)bs.getFirst()
                            .accept(this, null));
                    Sentence second = new UnarySentence((Sentence)bs.getSecond()
                            .accept(this, null));
                    return new BinarySentence("OR", first, second);
                }
                else if (bs.isOrSentence())
                {
                    Sentence first = new UnarySentence((Sentence)bs.getFirst()
                            .accept(this, null));
                    Sentence second = new UnarySentence((Sentence)bs.getSecond()
                            .accept(this, null));
                    return new BinarySentence("AND", first, second);
                }
                else
                {
                    return (Sentence)base.visitNotSentence(us, null);
                }
            }
            else
            {
                return (Sentence)base.visitNotSentence(us, null);
            }
        }

        private Sentence distributeOrOverAnd(BinarySentence bs)
        {
            BinarySentence andTerm = bs.firstTermIsAndSentence() ? (BinarySentence)bs
                    .getFirst()
                    : (BinarySentence)bs.getSecond();
            Sentence otherterm = bs.firstTermIsAndSentence() ? bs.getSecond() : bs
                    .getFirst();
            // (alpha or (beta and gamma) = ((alpha or beta) and (alpha or gamma))
            Sentence alpha = (Sentence)otherterm.accept(this, null);
            Sentence beta = (Sentence)andTerm.getFirst().accept(this, null);
            Sentence gamma = (Sentence)andTerm.getSecond().accept(this, null);
            Sentence distributed = new BinarySentence("AND", new BinarySentence(
                    "OR", alpha, beta), new BinarySentence("OR", alpha, gamma));
            return distributed;
        }
    }
}