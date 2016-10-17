using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;

namespace DotvvmAcademy.Controls
{
    public class AceEditor : HtmlGenericControl
    {

        [MarkupOptions(AllowHardCodedValue = false)]
        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }
        public static readonly DotvvmProperty CodeProperty
            = DotvvmProperty.Register<string, AceEditor>(c => c.Code, null);

        [MarkupOptions(AllowBinding = false)]
        public AceEditorLanguage Language
        {
            get { return (AceEditorLanguage)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }
        public static readonly DotvvmProperty LanguageProperty
            = DotvvmProperty.Register<AceEditorLanguage, AceEditor>(c => c.Language, AceEditorLanguage.Html);


        public AceEditor() : base("div")
        {
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm-ace");
            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("class", "code-editor");
            writer.AddKnockoutDataBind("aceEditor", this, CodeProperty);
            writer.AddKnockoutDataBind("aceEditor-language", KnockoutHelper.MakeStringLiteral(Language.ToString().ToLower()));
            base.AddAttributesToRender(writer, context);
        }

    }
}
