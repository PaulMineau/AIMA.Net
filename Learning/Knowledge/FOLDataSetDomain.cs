namespace CosmicFlow.AIMA.Core.Learning.Knowledge
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CosmicFlow.AIMA.Core.Learning.Framework;
    using CosmicFlow.AIMA.Core.Logic.FOL.Domain;
    using System.Text.RegularExpressions;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class FOLDataSetDomain : FOLDomain
    {
        //
        private static Regex allowableCharactersRegEx = new Regex("[^a-zA-Z_$0-9]");
        //
        private DataSetSpecification dataSetSpecification;
        private String trueGoalValue = null;
        // Default example prefix, see pg679 of AIMA
        private String examplePrefix = "X";
        private List<String> descriptionPredicateNames = new List<String>();
        private List<String> descriptionDataSetNames = new List<String>();
        private Dictionary<String, String> dsToFOLNameDictionary = new Dictionary<String, String>();

        //
        // PUBLIC METHODS
        //
        public FOLDataSetDomain(DataSetSpecification dataSetSpecification,
                String trueGoalValue)
        {
            this.dataSetSpecification = dataSetSpecification;
            this.trueGoalValue = trueGoalValue;
            constructFOLDomain();
        }

        public String getDataSetTargetName()
        {
            return dataSetSpecification.getTarget();
        }

        public String getGoalPredicateName()
        {
            return getFOLName(dataSetSpecification.getTarget());
        }

        public String getTrueGoalValue()
        {
            return trueGoalValue;
        }

        public List<String> getDescriptionPredicateNames()
        {
            return descriptionPredicateNames;
        }

        public List<String> getDescriptionDataSetNames()
        {
            return descriptionDataSetNames;
        }

        public bool isMultivalued(String descriptiveDataSetName)
        {
            List<String> possibleValues = dataSetSpecification
                    .getPossibleAttributeValues(descriptiveDataSetName);
            // If more than two possible values
            // then is multivalued
            if (possibleValues.Count > 2)
            {
                return true;
            }
            // If one of the possible values for the attribute
            // matches the true goal value then consider
            // it not being multivalued.
            foreach (String pv in possibleValues)
            {
                if (trueGoalValue.Equals(pv))
                {
                    return false;
                }
            }

            return true;
        }

        public String getExampleConstant(int egNo)
        {
            String egConstant = examplePrefix + egNo;
            addConstant(egConstant);
            return egConstant;
        }

        private bool isJavaIdentifierStart(char c)
        {
            return char.IsLetter(c) || c == '_' || c == '$';
        }

        public String getFOLName(String dsName)
        {
            String folName = String.Empty;
            if (!dsToFOLNameDictionary.ContainsKey(dsName))
            {
                folName = dsName;
                if (!isJavaIdentifierStart(dsName[0]))
                {
                    folName = "_" + dsName;
                }
                folName = allowableCharactersRegEx.Match(folName).Value.Replace("_", "");
                dsToFOLNameDictionary.Add(dsName, folName);
            }
            else
            {
                folName = dsToFOLNameDictionary[dsName];
            }

            return folName;
        }

        //
        // PRIVATE METHODS
        //
        private void constructFOLDomain()
        {
            // Ensure the target predicate is included
            addPredicate(getFOLName(dataSetSpecification.getTarget()));
            // Create the descriptive predicates
            foreach (String saName in dataSetSpecification.getNamesOfStringAttributes())
            {
                if (dataSetSpecification.getTarget().Equals(saName))
                {
                    // Don't add the target to the descriptive predicates
                    continue;
                }
                String folSAName = getFOLName(saName);
                // Add a predicate for the attribute
                addPredicate(folSAName);

                descriptionPredicateNames.Add(folSAName);
                descriptionDataSetNames.Add(saName);

                List<String> attributeValues = dataSetSpecification
                        .getPossibleAttributeValues(saName);
                // If a multivalued attribute need to setup
                // Constants for the different possible values
                if (isMultivalued(saName))
                {
                    foreach (String av in attributeValues)
                    {
                        addConstant(getFOLName(av));
                    }
                }
            }
        }
    }
}