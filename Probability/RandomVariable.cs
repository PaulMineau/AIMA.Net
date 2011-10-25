using System;
using AIMA.Probability.Domain;

namespace AIMA.Probability
{
    public interface RandomVariable
    {
        /**
         * 
         * @return the name used to uniquely identify this variable.
         */
        String getName();

        /**
         * 
         * @return the Set of possible values the Random Variable can take on.
         */
        IDomain getDomain();
    }

}