using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using System.Reflection;

namespace DotvvmAcademy.Controls
{
    public class MonacoEditor : DotvvmControl
    {
        public static readonly DotvvmProperty CodeProperty
            = DotvvmProperty.Register<string, MonacoEditor>(c => c.Code, null);

        public static readonly DotvvmProperty LanguageProperty
            = DotvvmProperty.Register<MonacoLanguages, MonacoEditor>(c => c.Language, MonacoLanguages.Html);

        public static string CssClass = "monaco-editor-wrapper";

        private static int editorCount;

        private static string loaderScript =
@"require.config({{ paths: {{ ""vs"": ""monaco-editor/min/vs"" }}}});
    require([""vs/editor/editor.main""], function() {{
        var editor = monaco.editor.create(document.getElementById(""{0}""), {{
            value: [
                ""function x() {{"",
                ""\tconsole.log(\""Hello world!\"");"",
                ""}}""
            ].join(""\n""),
            language: ""{1}""
        }});
    }});";

        private int editorId;

        public MonacoEditor()
        {
            editorCount++;
            editorId = editorCount;
        }

        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        public MonacoLanguages Language
        {
            get { return (MonacoLanguages)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }

        public override void Render(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var id = $"monaco{editorId}";
            var language = typeof(MonacoLanguages)
                .GetField(Language.ToString())
                .GetCustomAttribute<MonacoLanguageAttribute>()
                .Value;
            writer.AddAttribute("id", id);
            writer.AddAttribute("class", CssClass, true);
            writer.RenderBeginTag("div");
            writer.RenderEndTag();
            context.ResourceManager.AddStartupScript("monaco-init", string.Format(loaderScript, id, language), "monaco-loader");
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("monaco-loader");
            base.OnPreRender(context);
        }
    }
}