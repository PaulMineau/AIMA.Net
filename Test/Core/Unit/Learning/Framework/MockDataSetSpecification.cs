namespace CosmicFlow.AIMA.Test.Core.Unit.Learning.Framework
{


    using CosmicFlow.AIMA.Core.Learning.Framework;
    using System;
    using System.Collections.Generic;

    /**
     * @author Ravi Mohan
     * 
     */
    public class MockDataSetSpecification : DataSetSpecification
    {

        public MockDataSetSpecification(String targetAttributeName)
        {
            setTarget(targetAttributeName);
        }


        public override List<String> getAttributeNames()
        {
            return new List<String>();
        }
    }
}