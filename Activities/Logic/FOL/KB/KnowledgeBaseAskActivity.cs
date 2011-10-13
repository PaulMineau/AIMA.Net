using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using AIMA.Core.Logic.FOL.KB;
using AIMA.Core.Logic.FOL.Inference;

namespace AIMA.Logic.FOL.KB
{
    [Designer(typeof(KnowledgeBaseAskDesigner))]
    public sealed class KnowledgeBaseAskActivity : CodeActivity<InferenceResult>
    {
        public InArgument<FOLKnowledgeBase> KB { get; set; }

        [DefaultValue(null)]
        public string AskText { get; set; }

        protected override InferenceResult Execute(CodeActivityContext context)
        {
            FOLKnowledgeBase kb = context.GetValue<FOLKnowledgeBase>(KB);
            return kb.ask(AskText);
        }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

            if (string.IsNullOrEmpty(AskText))
                metadata.AddValidationError("AskText must not be empty");
        }
    }
}
