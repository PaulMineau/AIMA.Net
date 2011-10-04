namespace CosmicFlow.AIMA.Test.Core.Unit
{

using CosmicFlow.AIMA.Core.Util.DataStructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/**
 * @author Ravi Mohan
 * 
 */
[TestClass]
public class XYLocationTest {

	[TestMethod]
	public void testXYLocationAtributeSettingOnConstruction() {
		XYLocation loc = new XYLocation(3, 4);
		Assert.AreEqual(3, loc.getXCoOrdinate());
        Assert.AreEqual(4, loc.getYCoOrdinate());
	}

	[TestMethod]
	public void testEquality() {
		XYLocation loc1 = new XYLocation(3, 4);
		XYLocation loc2 = new XYLocation(3, 4);
        Assert.AreEqual(loc1, loc2);
	}
}
}