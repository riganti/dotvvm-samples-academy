using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.CourseFormat;
using System.Collections.Generic;

namespace DotvvmAcademy.Web.Controls
{
    public class CodeTaskControl : HtmlGenericControl
    {
        public static readonly DotvvmProperty CodeProperty
            = DotvvmProperty.Register<string, CodeTaskControl>(c => c.Code, null);

        public static readonly DotvvmProperty DiagnosticsProperty
            = DotvvmProperty.Register<List<ICodeTaskDiagnostic>, CodeTaskControl>(c => c.Diagnostics, null);

        public static readonly DotvvmProperty LanguageProperty
                    = DotvvmProperty.Register<string, CodeTaskControl>(c => c.Language, null);

        public CodeTaskControl() : base("div")
        {
        }

        [MarkupOptions(Required = true, AllowBinding = true, AllowHardCodedValue = false)]
        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        [MarkupOptions(Required = true, AllowBinding = true, AllowHardCodedValue = false)]
        public List<ICodeTaskDiagnostic> Diagnostics
        {
            get { return (List<ICodeTaskDiagnostic>)GetValue(DiagnosticsProperty); }
            set { SetValue(DiagnosticsProperty, value); }
        }

        public string Language
        {
            get { return (string)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            base.AddAttributesToRender(writer, context);
            var group = new KnockoutBindingGroup
            {
                { "code", this, CodeProperty },
                { "language", this, LanguageProperty },
                { "diagnostics", this, DiagnosticsProperty }
            };
            writer.AddKnockoutDataBind("dotvvm-monaco", group);
        }
    }
}