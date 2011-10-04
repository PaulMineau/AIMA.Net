namespace CosmicFlow.AIMA.Core.Logic.FOL
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Logic.FOL.KB.Data;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing;
    using CosmicFlow.AIMA.Core.Logic.FOL.Parsing.AST;

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public class SubstVisitor : AbstractFOLVisitor
    {

        public SubstVisitor()
        {
        }

        /**
         * Note: Refer to Artificial Intelligence A Modern Approach (3rd Edition):
         * page 323.
         * 
         * @param theta
         *            a substitution.
         * @param aSentence
         *            the substitution has been applied to.
         * @return a new Sentence representing the result of applying the
         *         substitution theta to aSentence.
         * 
         */
        public Sentence subst(Dictionary<Variable, Term> theta, Sentence aSentence)
        {
            return (Sentence)aSentence.accept(this, theta);
        }

        public Term subst(Dictionary<Variable, Term> theta, Term aTerm)
        {
            return (Term)aTerm.accept(this, theta);
        }

        public Function subst(Dictionary<Variable, Term> theta, Function aFunction)
        {
            return (Function)aFunction.accept(this, theta);
        }

        public Literal subst(Dictionary<Variable, Term> theta, Literal aLiteral)
        {
            return aLiteral.newInstance((AtomicSentence)aLiteral
                    .getAtomicSentence().accept(this, theta));
        }

        public override Object visitVariable(Variable variable, Object arg)
        {
            Dictionary<Variable, Term> substitution = (Dictionary<Variable, Term>)arg;
            if (substitution.ContainsKey(variable))
            {
                return substitution[variable].copy();
            }
            return variable.copy();
        }

        public override Object visitQuantifiedSentence(QuantifiedSentence sentence,
                Object arg) {

                    Dictionary<Variable, Term> substitution = (Dictionary<Variable, Term>)arg;

		Sentence quantified = sentence.getQuantified();
		Sentence quantifiedAfterSubs = (Sentence) quantified.accept(this, arg);

		List<Variable> variables = new List<Variable>();
		foreach (Variable v in sentence.getVariables()) {

			if (substitution.ContainsKey(v)) {
                Term st = substitution[v];
				if (st is Variable) {
					// Only if it is a variable to I replace it, otherwise
					// I drop it.
					variables.Add((Variable) st.copy());
				}
			} else {
				// No substitution for the quantified variable, so
				// keep it.
				variables.Add((Variable)v.copy());
			}
		}

		// If not variables remaining on the quantifier, then drop it
		if (variables.Count == 0) {
			return quantifiedAfterSubs;
		}

		return new QuantifiedSentence(sentence.getQuantifier(), variables,
				quantifiedAfterSubs);
	}
    }
}