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
    [Designer(typeof(DomainAddConstantDesigner))]
    public sealed class DomainAddConstantActivity : CodeActivity
    {
        public InArgument<FOLDomain> Domain { get; set; }

        [DefaultValue(null)]
        public string ConstantText { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            FOLDomain domain = context.GetValue(Domain);
            domain.addConstant(ConstantText);
        }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

            if (string.IsNullOrEmpty(ConstantText))
                metadata.AddValidationError("ConstantText must not be empty");
        }
    }
}
