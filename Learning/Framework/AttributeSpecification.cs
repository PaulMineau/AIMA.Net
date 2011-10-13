namespace AIMA.Core.Learning.Framework
{
    using System;
    using System.Collections.Generic;
/**
 * @author Ravi Mohan
 * 
 */
public interface AttributeSpecification {

	bool isValid(String s);

	String getAttributeName();

	LearningAttribute createAttribute(String rawValue);
}
}