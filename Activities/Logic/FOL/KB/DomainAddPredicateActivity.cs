using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using CosmicFlow.AIMA.Core.Logic.FOL.KB;
using CosmicFlow.AIMA.Core.Logic.FOL.Inference;
using CosmicFlow.AIMA.Core.Logic.FOL.Domain;

namespace CosmicFlow.AIMA.Logic.FOL.KB
{
    [Designer(typeof(DomainAddPredicateDesigner))]
    public sealed class DomainAddPredicateActivity : CodeActivity
    {
        public InArgument<FOLDomain> Domain { get; set; }

        [DefaultValue(null)]
        public string PredicateText { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            FOLDomain domain = context.GetValue<FOLDomain>(Domain);
            domain.addPredicate(PredicateText);
        }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

            if (string.IsNullOrEmpty(PredicateText))
                metadata.AddValidationError("PredicateText must not be empty");
        }
    }
}
