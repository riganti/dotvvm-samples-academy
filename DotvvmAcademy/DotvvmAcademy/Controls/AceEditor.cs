using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.BL.DTO;

namespace DotvvmAcademy.Controls
{
    public class AceEditor : HtmlGenericControl
    {
        public static readonly DotvvmProperty CodeProperty
            = DotvvmProperty.Register<string, AceEditor>(c => c.Code, null);

        public static readonly DotvvmProperty LanguageProperty
            = DotvvmProperty.Register<SampleCodeLanguage, AceEditor>(c => c.Language, SampleCodeLanguage.Html);

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
        public SampleCodeLanguage Language
        {
            get { return (SampleCodeLanguage)GetValue(LanguageProperty); }
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