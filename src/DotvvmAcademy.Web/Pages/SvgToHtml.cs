using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DotvvmAcademy.Web.Pages
{
    public class SvgToHtml : HtmlGenericControl
    {
        [MarkupOptions(Required = true, AllowBinding = true)]
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DotvvmProperty SourceProperty
            = DotvvmProperty.Register<string, SvgToHtml>(c => c.Source, null);

        public SvgToHtml() : base("svg")
        {
        }

        public override void Render(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (RenderOnServer)
            {
                var hostingEnvironment = context.Services.GetRequiredService<IHostingEnvironment>();
                var path = Path.Combine(hostingEnvironment.WebRootPath, Source);
                if (path != null)
                {
                    var svgXml = File.ReadAllText(path);
                    writer.WriteUnencodedText(string.Join(string.Empty, svgXml));
                }
            }
            else
            {
                writer.AddKnockoutDataBind("svg", $"'{Source}'");
                writer.RenderSelfClosingTag("svg");
            }
        }

        private string VirtualPathToAbsolute(string path)
        {
            return path.Replace('/', '\\');
        }
    }
}