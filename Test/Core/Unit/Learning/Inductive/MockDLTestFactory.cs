namespace AIMA.Test.Core.Unit.Learning.Inductive
{

    using AIMA.Core.Learning.Framework;
    using AIMA.Core.Learning.Inductive;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    [TestClass]
    public class MockDLTestFactory : DLTestFactory
    {

        private List<DLTest> tests;

        public MockDLTestFactory(List<DLTest> tests)
        {
            this.tests = tests;
        }

        [TestInitialize]
        public override List<DLTest> createDLTestsWithAttributeCount(DataSet ds, int i)
        {
            return tests;
        }
    }
}
