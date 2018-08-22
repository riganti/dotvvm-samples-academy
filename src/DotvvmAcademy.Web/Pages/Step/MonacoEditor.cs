using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using System.Collections.Generic;

namespace DotvvmAcademy.Web.Pages.Step
{
    public class MonacoEditor : HtmlGenericControl
    {
        public static readonly DotvvmProperty CodeProperty
            = DotvvmProperty.Register<string, MonacoEditor>(c => c.Code, null);

        public static readonly DotvvmProperty MarkersProperty
            = DotvvmProperty.Register<IEnumerable<MonacoMarker>, MonacoEditor>(c => c.Markers, null);

        public static readonly DotvvmProperty LanguageProperty
            = DotvvmProperty.Register<string, MonacoEditor>(c => c.Language, null);

        public MonacoEditor() : base("div")
        {
        }

        [MarkupOptions(Required = true, AllowBinding = true, AllowHardCodedValue = false)]
        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        [MarkupOptions(Required = true, AllowBinding = true, AllowHardCodedValue = false)]
        public IEnumerable<MonacoMarker> Markers
        {
            get { return (IEnumerable<MonacoMarker>)GetValue(MarkersProperty); }
            set { SetValue(MarkersProperty, value); }
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
                { "markers", this, MarkersProperty }
            };
            writer.AddKnockoutDataBind("dotvvm-monaco", group);
        }
    }
}