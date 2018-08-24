using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DotvvmAcademy.Web.Pages
{
    // Come to think of it, controls should never do IO, since they cannot be async. This control does just that and therefore blocks.
    public class SvgToHtml : DotvvmControl
    {
        public static readonly DotvvmProperty SourceProperty
            = DotvvmProperty.Register<string, SvgToHtml>(c => c.Source, null);

        public SvgToHtml() : base()
        {
        }

        [MarkupOptions(Required = true, AllowBinding = true, AllowHardCodedValue = true)]
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (RenderOnServer 
                || HasBinding<ResourceBindingExpression>(SourceProperty)
                || !HasBinding(SourceProperty))
            {
                var hostingEnvironment = context.Services.GetRequiredService<IHostingEnvironment>();
                var file = hostingEnvironment.WebRootFileProvider.GetFileInfo(Source);
                if (file.Exists)
                {
                    var svgContent = File.ReadAllText(file.PhysicalPath) ?? string.Empty;
                    writer.WriteUnencodedText(svgContent);
                }
            }
            else
            {
                writer.AddKnockoutDataBind("dotvvm-svg", this, SourceProperty);
                writer.RenderSelfClosingTag("svg");
            }
        }
    }
}