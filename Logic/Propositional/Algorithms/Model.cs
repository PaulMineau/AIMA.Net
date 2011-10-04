namespace CosmicFlow.AIMA.Core.Logic.Propositional.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing;
    using CosmicFlow.AIMA.Core.Logic.Propositional.Parsing.Ast;
    using System.Collections.ObjectModel;

/**
 * @author Ravi Mohan
 * 
 */
public class Model : PLVisitor {

	private Dictionary<Symbol, bool> h = new Dictionary<Symbol, bool>();
	
	public Model() {
		
	}

	public bool getStatus(Symbol symbol) {
		return h[symbol];
	}

	public bool isTrue(Symbol symbol) {
		return true.Equals(h[symbol]);
	}

	public bool isFalse(Symbol symbol) {
		return false.Equals(h[symbol]);
	}

	public Model extend(Symbol symbol, bool b) {
		Model m = new Model();
		m.h = this.h;
		m.h.Add(symbol, b);
		return m;
	}

	public bool isTrue(Sentence clause) {
		return clause.accept(this, null).Equals(true);
	}

	public bool isFalse(Sentence clause) {
		return clause.accept(this, null).Equals(false);
	}

	public bool isUnknown(Sentence clause) {
		return null == clause.accept(this, null);
	}

	public Model flip(Symbol s) {
		if (isTrue(s)) {
			return extend(s, false);
		}
		if (isFalse(s)) {
			return extend(s, true);
		}
		return this;
	}

    public ReadOnlyCollection<Symbol> getAssignedSymbols()
    {
		return new ReadOnlyCollection<Symbol>(h.Keys.ToList<Symbol>());
	}

	public void print() {
		foreach (Symbol key in h.Keys) {
			Console.Write(key + " = " + h[key] + " ");
		}
		Console.WriteLine();
	}

	public override String ToString() {
		return h.ToString();
	}

	//
	// START-PLVisitor
	public Object visitSymbol(Symbol s, Object arg) {
		return getStatus(s);
	}

	public Object visitTrueSentence(TrueSentence ts, Object arg) {
		return true;
	}

	public Object visitFalseSentence(FalseSentence fs, Object arg) {
		return false;
	}

	public Object visitNotSentence(UnarySentence fs, Object arg) {
		Object negatedValue = fs.getNegated().accept(this, null);
		if (negatedValue != null) {
			return !((bool) negatedValue);
		} else {
			return null;
		}
	}

	public Object visitBinarySentence(BinarySentence bs, Object arg) {
		bool? firstValue = (bool?) bs.getFirst().accept(this, null);
		bool? secondValue = (bool?) bs.getSecond().accept(this, null);
		if (!firstValue.HasValue  || !secondValue.HasValue) {
			// strictly not true for or/and
			// -FIX later
			return null;
		} else {
			String op = bs.getOperator();
			if (op.Equals("AND")) {
                return firstValue.Value && secondValue.Value;
			} else if (op.Equals("OR")) {
                return firstValue.Value || secondValue.Value;
			} else if (op.Equals("=>")) {
                return !(firstValue.Value && !secondValue.Value);
            }
            else if (op.Equals("<=>"))
            {
				return firstValue.Equals(secondValue);
			}
			return null;
		}
	}

	public Object visitMultiSentence(MultiSentence fs, Object argd) {
		// TODO remove this?
		return null;
	}
	// END-PLVisitor
	//
}
}