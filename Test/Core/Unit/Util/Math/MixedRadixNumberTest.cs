namespace AIMA.Test.Core.Unit
{


    using AIMA.Core.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using AIMA.Core.Util.Math;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    [TestClass]
    public class MixedRadixNumberTest
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void testInvalidRadixs()
        {
            new MixedRadixNumber(100, new int[] { 1, 0, -1 });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void testInvalidMaxValue()
        {
            new MixedRadixNumber(100, new int[] { 3, 3, 3 });
        }

        [TestMethod]
        public void testAllowedMaxValue()
        {
            Assert.AreEqual(15, (new MixedRadixNumber(0,
                    new int[] { 2, 2, 2, 2 }).getMaxAllowedValue()));
            Assert.AreEqual(80, (new MixedRadixNumber(0,
                    new int[] { 3, 3, 3, 3 }).getMaxAllowedValue()));
            Assert.AreEqual(5, (new MixedRadixNumber(0, new int[] { 3, 2 })
                    .getMaxAllowedValue()));
            Assert.AreEqual(35, (new MixedRadixNumber(0,
                    new int[] { 3, 3, 2, 2 }).getMaxAllowedValue()));
            Assert.AreEqual(359, (new MixedRadixNumber(0, new int[] { 3, 4, 5,
				6 }).getMaxAllowedValue()));
            Assert.AreEqual(359, (new MixedRadixNumber(0, new int[] { 6, 5, 4,
				3 }).getMaxAllowedValue()));
        }

        [TestMethod]
        public void testIncrement()
        {
            MixedRadixNumber mrn = new MixedRadixNumber(0, new int[] { 3, 2 });
            int i = 0;
            while (mrn.increment())
            {
                i++;
            }
            Assert.AreEqual(i, mrn.getMaxAllowedValue());
        }

        [TestMethod]
        public void testDecrement()
        {
            MixedRadixNumber mrn = new MixedRadixNumber(5, new int[] { 3, 2 });
            int i = 0;
            while (mrn.decrement())
            {
                i++;
            }
            Assert.AreEqual(i, mrn.getMaxAllowedValue());
        }

        [TestMethod]
        public void testCurrentNumberalValue()
        {
            MixedRadixNumber mrn;
            //
            mrn = new MixedRadixNumber(0, new int[] { 3, 3, 2, 2 });
            Assert.AreEqual(0, mrn.getCurrentNumeralValue(0));
            Assert.AreEqual(0, mrn.getCurrentNumeralValue(1));
            Assert.AreEqual(0, mrn.getCurrentNumeralValue(2));
            Assert.AreEqual(0, mrn.getCurrentNumeralValue(3));
            //
            mrn = new MixedRadixNumber(35, new int[] { 3, 3, 2, 2 });
            Assert.AreEqual(2, mrn.getCurrentNumeralValue(0));
            Assert.AreEqual(2, mrn.getCurrentNumeralValue(1));
            Assert.AreEqual(1, mrn.getCurrentNumeralValue(2));
            Assert.AreEqual(1, mrn.getCurrentNumeralValue(3));
            //
            mrn = new MixedRadixNumber(25, new int[] { 3, 3, 2, 2 });
            Assert.AreEqual(1, mrn.getCurrentNumeralValue(0));
            Assert.AreEqual(2, mrn.getCurrentNumeralValue(1));
            Assert.AreEqual(0, mrn.getCurrentNumeralValue(2));
            Assert.AreEqual(1, mrn.getCurrentNumeralValue(3));
            //
            mrn = new MixedRadixNumber(17, new int[] { 3, 3, 2, 2 });
            Assert.AreEqual(2, mrn.getCurrentNumeralValue(0));
            Assert.AreEqual(2, mrn.getCurrentNumeralValue(1));
            Assert.AreEqual(1, mrn.getCurrentNumeralValue(2));
            Assert.AreEqual(0, mrn.getCurrentNumeralValue(3));
            //
            mrn = new MixedRadixNumber(8, new int[] { 3, 3, 2, 2 });
            Assert.AreEqual(2, mrn.getCurrentNumeralValue(0));
            Assert.AreEqual(2, mrn.getCurrentNumeralValue(1));
            Assert.AreEqual(0, mrn.getCurrentNumeralValue(2));
            Assert.AreEqual(0, mrn.getCurrentNumeralValue(3));
            //
            mrn = new MixedRadixNumber(359, new int[] { 3, 4, 5, 6 });
            Assert.AreEqual(2, mrn.getCurrentNumeralValue(0));
            Assert.AreEqual(3, mrn.getCurrentNumeralValue(1));
            Assert.AreEqual(4, mrn.getCurrentNumeralValue(2));
            Assert.AreEqual(5, mrn.getCurrentNumeralValue(3));
            //
            mrn = new MixedRadixNumber(359, new int[] { 6, 5, 4, 3 });
            Assert.AreEqual(5, mrn.getCurrentNumeralValue(0));
            Assert.AreEqual(4, mrn.getCurrentNumeralValue(1));
            Assert.AreEqual(3, mrn.getCurrentNumeralValue(2));
            Assert.AreEqual(2, mrn.getCurrentNumeralValue(3));
        }
    }
}
