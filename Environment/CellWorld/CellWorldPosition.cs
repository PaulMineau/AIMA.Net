namespace AIMA.Core.Environment.CellWorld
{
    using System;
    using System.Collections.Generic;
/**
 * @author Ravi Mohan
 * 
 */
public class CellWorldPosition  {
	private int x, y;

	public CellWorldPosition(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public int getX() {
		return x;
	}

	public int getY() {
		return y;
	}

	public override bool Equals(Object o) {
		if (o == this) {
			return true;
		}
		if (!(o is CellWorldPosition)) {
			return false;
		}
		CellWorldPosition cwp = (CellWorldPosition) o;
		return ((this.x == cwp.x) && (this.y == cwp.y));
	}

	public override int GetHashCode() {
		return x + 31 * y;
	}

	public override String ToString() {
		return "< " + x + " , " + y + " > ";
	}
}
}