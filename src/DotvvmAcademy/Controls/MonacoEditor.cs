using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using System.Collections.Generic;

namespace DotvvmAcademy.Controls
{
    public class MonacoEditor : DotvvmControl
    {
        public static readonly DotvvmProperty CodeProperty
            = DotvvmProperty.Register<string, MonacoEditor>(c => c.Code, null);

        public static readonly DotvvmProperty LanguageProperty
            = DotvvmProperty.Register<MonacoLanguage, MonacoEditor>(c => c.Language, MonacoLanguage.Html);

        public static readonly DotvvmProperty MarkersProperty
            = DotvvmProperty.Register<List<MonacoMarker>, MonacoEditor>(c => c.Markers, null);

        public static readonly DotvvmProperty ThemeProperty
                    = DotvvmProperty.Register<MonacoTheme, MonacoEditor>(c => c.Theme, MonacoTheme.Dark);

        public static string CssClass = "monaco-editor-wrapper";

        [MarkupOptions(AllowHardCodedValue = false, MappingMode = MappingMode.Attribute, Required = true)]
        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        public MonacoLanguage Language
        {
            get { return (MonacoLanguage)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }

        public List<MonacoMarker> Markers
        {
            get { return (List<MonacoMarker>)GetValue(MarkersProperty); }
            set { SetValue(MarkersProperty, value); }
        }

        public MonacoTheme Theme
        {
            get { return (MonacoTheme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public override void Render(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var theme = MonacoValueAttribute.GetValue(Theme);
            var language = MonacoValueAttribute.GetValue(Language);
            var group = new KnockoutBindingGroup();
            group.Add("code", this, CodeProperty);
            group.Add("language", language, true);
            group.Add("theme", theme, true);
            group.Add("markers", this, MarkersProperty);
            writer.AddAttribute("class", CssClass, true);
            writer.AddKnockoutDataBind("dotvvm-monaco", group);
            writer.RenderBeginTag("div");
            writer.RenderEndTag();
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm-monaco");
            base.OnPreRender(context);
        }
    }
}