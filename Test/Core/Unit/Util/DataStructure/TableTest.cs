namespace AIMA.Test.Core.Unit
{

using AIMA.Core.Util.DataStructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

/**
 * @author Ravi Mohan
 * 
 */
[TestClass]
public class TableTest {
    private Table<String, String, int> table;

	[TestInitialize]
	public void setUp() {
        List<String> rowHeaders = new List<String>();
        List<String> columnHeaders = new List<String>();

		rowHeaders.Add("row1");
        rowHeaders.Add("ravi");
        rowHeaders.Add("peter");

        columnHeaders.Add("col1");
        columnHeaders.Add("iq");
        columnHeaders.Add("age");
		table = new Table<String, String,int>(rowHeaders, columnHeaders);

	}

	[TestMethod]
	public void testTableInitialization() {
		Assert.IsFalse(table.get("ravi", "iq")<0);
		table.set("ravi", "iq", 50);
		int? i = table.get("ravi", "iq");
		Assert.AreEqual(50, i.Value);
	}

	[TestMethod]
	public void testNullAccess() {
		// No value yet assigned
        Assert.IsFalse(table.get("row1", "col2")==Int32.MinValue);
		table.set("row1", "col1", 1);
        Assert.AreEqual(1, table.get("row1", "col1") == Int32.MinValue);
		// Check null returned if column does not exist
        Assert.IsFalse(table.get("row1", "col2") == Int32.MinValue);
		// Check null returned if row does not exist
        Assert.IsFalse(table.get("row2", "col1") == Int32.MinValue);
	}

}
}