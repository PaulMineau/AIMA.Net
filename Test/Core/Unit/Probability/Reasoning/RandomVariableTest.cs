using AIMA.Probability;

namespace AIMA.Test.Core.Unit.Probability.Reasoning
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System;
    using AIMA.Core.Probability;
    using AIMA.Core.Probability.Reasoning;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class RandomVariableTest
    {

        private RandomVariable aDistribution;

        [TestInitialize]
        public void setUp()
        {
            List<String> states = new List<String>(){
				HmmConstants.DOOR_OPEN, HmmConstants.DOOR_CLOSED };
            aDistribution = new RandomVariable("HiddenState", states);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void testSettingValuesOnInvalidStateThrowsException()
        {
            aDistribution.setProbabilityOf("invalid", 0.5);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void testGettingValuesOnInvalidStateThrowsException()
        {
            aDistribution.getProbabilityOf("invalid");
        }
    }
}