using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using CommonMark;

namespace DotvvmAcademy.Controls
{
    public class MarkdownLiteral : HtmlGenericControl
    {

        public MarkdownLiteral() : base("div")
        {
        }

        [MarkupOptions(AllowHardCodedValue = false)]
        public string Markdown
        {
            get { return (string)GetValue(MarkdownProperty); }
            set { SetValue(MarkdownProperty, value); }
        }
        public static readonly DotvvmProperty MarkdownProperty
            = DotvvmProperty.Register<string, MarkdownLiteral>(c => c.Markdown, null);


        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var html = CommonMarkConverter.Convert(Markdown);
            writer.WriteUnencodedText(html);
        }

    }
}
