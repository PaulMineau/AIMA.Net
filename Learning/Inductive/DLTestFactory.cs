namespace CosmicFlow.AIMA.Core.Learning.Inductive
{
    using System;
    using System.Collections.Generic;
    using CosmicFlow.AIMA.Core.Learning.Framework;

    /**
     * @author Ravi Mohan
     * 
     */
    public class DLTestFactory
    {

        public virtual List<DLTest> createDLTestsWithAttributeCount(DataSet ds, int i)
        {
            if (i != 1)
            {
                throw new ApplicationException(
                        "For now DLTests with only 1 attribute can be craeted , not"
                                + i);
            }
            List<String> nonTargetAttributes = ds.getNonTargetAttributes();
            List<DLTest> tests = new List<DLTest>();
            foreach (String ntAttribute in nonTargetAttributes)
            {
                List<String> ntaValues = ds.getPossibleAttributeValues(ntAttribute);
                foreach (String ntaValue in ntaValues)
                {

                    DLTest test = new DLTest();
                    test.add(ntAttribute, ntaValue);
                    tests.Add(test);

                }
            }
            return tests;
        }
    }
}
