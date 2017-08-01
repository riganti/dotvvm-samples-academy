using CommonMark;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System;
using DotVVM.Framework.Hosting;

namespace DotvvmAcademy.Controls
{
    public class MarkdownLiteral : HtmlGenericControl
    {
        public static readonly DotvvmProperty MarkdownProperty
            = DotvvmProperty.Register<string, MarkdownLiteral>(c => c.Markdown, null);

        public MarkdownLiteral() : base("div")
        {
        }

        [MarkupOptions(AllowHardCodedValue = false)]
        public string Markdown
        {
            get { return (string)GetValue(MarkdownProperty); }
            set { SetValue(MarkdownProperty, value); }
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var html = CommonMarkConverter.Convert(Markdown);
            writer.WriteUnencodedText(html);
        }
    }
}