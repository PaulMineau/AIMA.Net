using System;
using AIMA.Probability.Proposition;

namespace AIMA.Probability
{

    public abstract class AbstractTermProposition : AbstractProposition
                                                    , TermProposition
    {

        private RandomVariable termVariable = null;

        public AbstractTermProposition(RandomVariable var)
        {
            if (null == var)
            {
                throw new ArgumentException(
                    "The Random Variable for the Term must be specified.");
            }
            this.termVariable = var;
            addScope(this.termVariable);
        }

        public RandomVariable getTermVariable()
        {
            return termVariable;
        }
    }
}