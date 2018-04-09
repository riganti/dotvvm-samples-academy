using System.IO;
using System.Linq;
using System.Web;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Controls
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

        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (RenderOnServer)
            {
                var hostingEnvironment = context.Services.GetService(typeof(IHostingEnvironment)) as IHostingEnvironment;
                //TODO This is not good idea at all, you have to use hostingEnvironment.WebRootPath / hostingEnvironment.ContentRootPath
                var path = Path.Combine(hostingEnvironment.WebRootPath, VirtualPathToAbsolute(Source));
                if (path != null)
                {
                    //var lines = File.ReadAllLines(path).Skip(1).ToArray();
                    var svgXml = File.ReadAllText(path);
                    writer.WriteUnencodedText(string.Join(string.Empty, svgXml));
                }
            }
            else
            {
                var path = Source;
                writer.AddKnockoutDataBind("svg", $"'{path}'");
                writer.RenderSelfClosingTag("svg");
            }
        }

        private string VirtualPathToAbsolute(string path)
        {
            return path.Replace('/', '\\');
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
        }
    }
}
