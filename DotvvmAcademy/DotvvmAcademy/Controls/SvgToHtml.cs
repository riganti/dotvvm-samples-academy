//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Hosting;
//using DotVVM.Framework.Binding;
//using DotVVM.Framework.Controls;
//using DotVVM.Framework.Hosting;

//namespace UpdateConferencePrague.Controls
//{
//    public class SvgToHtml : HtmlGenericControl
//    {
//        [MarkupOptions(Required = true, AllowBinding = false)]
//        public string Source
//        {
//            get { return (string)GetValue(SourceProperty); }
//            set { SetValue(SourceProperty, value); }
//        }
//        public static readonly DotvvmProperty SourceProperty
//            = DotvvmProperty.Register<string, SvgToHtml>(c => c.Source, null);

//        public SvgToHtml() : base("svg")
//        {

//        }

//        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
//        {
//        }

//        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
//        {
//            if (RenderOnServer)
//            {
//                var path = HostingEnvironment.MapPath(Source);
//                if (path != null)
//                {
//                    //var lines = File.ReadAllLines(path).Skip(1).ToArray();
//                    var svgXml = File.ReadAllText(path);
//                    writer.WriteUnencodedText(string.Join(string.Empty, svgXml));
//                }
//            }
//            else
//            {
//                var path = VirtualPathUtility.ToAbsolute(Source);
//                writer.AddKnockoutDataBind("svg", $"'{path}'");
//                writer.RenderSelfClosingTag("svg");
//            }
//        }

//        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
//        {
//        }
//    }
//}
