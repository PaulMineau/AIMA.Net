namespace CosmicFlow.AIMA.Core.Learning.Framework
{
    using System;
    using System.Collections.Generic;
using CosmicFlow.AIMA.Core.Util;
    using System.IO;
    using System.Reflection;
    using RTools.Util;

/**
 * @author Ravi Mohan
 * 
 */
public class DataSetFactory {

	public DataSet fromFile(String filename, DataSetSpecification spec,
			Char separator) {
		// assumed file in data directory and ends in .csv
		DataSet ds = new DataSet(spec);
        using (StreamReader reader = new StreamReader(typeof(DataSetFactory).Assembly.GetManifestResourceStream("AIMA.Resource." + filename)))
        {
            String line;
            while ((line = reader.ReadLine()) != null)
            {
                ds.add(exampleFromString(line, spec, separator));
            }
        }
		return ds;

	}

	public static Example exampleFromString(String data,
			DataSetSpecification dataSetSpec, Char separator) {
		Dictionary<String, LearningAttribute> attributes = new Dictionary<String, LearningAttribute>();
        List<String> attributeValues = new List<String>();

        string [] vals = data.Split(new char[] { ' ', '\t', ',' });
        foreach (string v in vals)
        {
            if (!String.IsNullOrWhiteSpace(v))
            {
                attributeValues.Add(v);
            }
        }
		if (dataSetSpec.isValid(attributeValues)) {
			List<String> names = dataSetSpec.getAttributeNames();
			List<String>.Enumerator nameiter = names.GetEnumerator();
            List<String>.Enumerator valueiter = attributeValues.GetEnumerator();
			while (nameiter.MoveNext() && valueiter.MoveNext()) {
                String name = nameiter.Current;
				AttributeSpecification attributeSpec = dataSetSpec
						.getAttributeSpecFor(name);
				LearningAttribute attribute = attributeSpec.createAttribute(valueiter.Current);
				attributes.Add(name, attribute);
			}
			String targetAttributeName = dataSetSpec.getTarget();
			return new Example(attributes, attributes[targetAttributeName]);
		} else {
			throw new ApplicationException("Unable to construct Example from "
					+ data);
		}
	}

	public static DataSet getRestaurantDataSet() {
		DataSetSpecification spec = createRestaurantDataSetSpec();
		return new DataSetFactory().fromFile("restaurant.csv", spec, '\t');
	}

	public static DataSetSpecification createRestaurantDataSetSpec() {
		DataSetSpecification dss = new DataSetSpecification();
		dss.defineStringAttribute("alternate", Util.yesno());
		dss.defineStringAttribute("bar", Util.yesno());
		dss.defineStringAttribute("fri/sat", Util.yesno());
		dss.defineStringAttribute("hungry", Util.yesno());
		dss.defineStringAttribute("patrons", new String[] { "None", "Some",
				"Full" });
		dss.defineStringAttribute("price", new String[] { "$", "$$", "$$$" });
		dss.defineStringAttribute("raining", Util.yesno());
		dss.defineStringAttribute("reservation", Util.yesno());
		dss.defineStringAttribute("type", new String[] { "French", "Italian",
				"Thai", "Burger" });
		dss.defineStringAttribute("wait_estimate", new String[] { "0-10",
				"10-30", "30-60", ">60" });
		dss.defineStringAttribute("will_wait", Util.yesno());
		// last attribute is the target attribute unless the target is
		// explicitly reset with dss.setTarget(name)

		return dss;
	}

	public static DataSet getIrisDataSet() {
		DataSetSpecification spec = createIrisDataSetSpec();
		return new DataSetFactory().fromFile("iris.csv", spec, ',');
	}

	public static DataSetSpecification createIrisDataSetSpec() {
		DataSetSpecification dss = new DataSetSpecification();
		dss.defineNumericAttribute("sepal_length");
		dss.defineNumericAttribute("sepal_width");
		dss.defineNumericAttribute("petal_length");
		dss.defineNumericAttribute("petal_width");
		dss.defineStringAttribute("plant_category", new String[] { "setosa",
				"versicolor", "virginica" });
		return dss;
	}
}
}