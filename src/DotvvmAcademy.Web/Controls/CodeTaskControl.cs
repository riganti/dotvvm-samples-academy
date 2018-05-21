using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotvvmAcademy.Web.Controls
{
    public class CodeTaskControl : HtmlGenericControl
    {
        public string Language
        {
            get { return (string)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }

        public static readonly DotvvmProperty LanguageProperty
            = DotvvmProperty.Register<string, CodeTaskControl>(c => c.Language, null);

        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        public static readonly DotvvmProperty CodeProperty
            = DotvvmProperty.Register<string, CodeTaskControl>(c => c.Code, null);

        public CodeTaskControl() : base("div")
        {
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            base.AddAttributesToRender(writer, context);
            var group = new KnockoutBindingGroup
            {
                { "code", this, CodeProperty },
                { "language", this, LanguageProperty }
            };
            writer.AddKnockoutDataBind("dotvvm-monaco", group);
        }
    }
}
