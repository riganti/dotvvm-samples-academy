using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System;
using DotVVM.Framework.Hosting;

namespace DotvvmAcademy.Controls
{
    public class AceEditor : HtmlGenericControl
    {
        public static readonly DotvvmProperty CodeProperty
            = DotvvmProperty.Register<string, AceEditor>(c => c.Code, null);

        public static readonly DotvvmProperty LanguageProperty
            = DotvvmProperty.Register<AceEditorLanguage, AceEditor>(c => c.Language, AceEditorLanguage.Html);

        public AceEditor() : base("div")
        {
        }

        [MarkupOptions(AllowHardCodedValue = false)]
        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        [MarkupOptions(AllowBinding = false)]
        public AceEditorLanguage Language
        {
            get { return (AceEditorLanguage)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("class", "code-editor");
            writer.AddKnockoutDataBind("aceEditor", this, CodeProperty);
            writer.AddKnockoutDataBind("aceEditor-language", KnockoutHelper.MakeStringLiteral(Language.ToString().ToLower()));
            base.AddAttributesToRender(writer, context);
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm-ace");
            base.OnPreRender(context);
        }
    }
}