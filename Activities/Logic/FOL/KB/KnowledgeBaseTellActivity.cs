using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using AIMA.Core.Logic.FOL.KB;

namespace AIMA.Logic.FOL.KB
{
    [Designer(typeof(KnowledgeBaseTellDesigner))]
    public sealed class KnowledgeBaseTellActivity : CodeActivity
    {
        public InArgument<FOLKnowledgeBase> KB { get; set; }

        [DefaultValue(null)]
        public string TellText { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            FOLKnowledgeBase kb = context.GetValue<FOLKnowledgeBase>(KB);
            kb.tell(TellText);
        }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

            if (string.IsNullOrEmpty(TellText))
                metadata.AddValidationError("TellText must not be empty");
           
        }
    }
}
