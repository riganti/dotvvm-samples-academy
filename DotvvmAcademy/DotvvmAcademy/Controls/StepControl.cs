using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.CommonMark;

namespace DotvvmAcademy.Controls
{
    public class StepControl : DotvvmControl
    {
        public static readonly DotvvmProperty SourceProperty
            = DotvvmProperty.Register<string, StepControl>(c => c.Source, null);

        private StepConverter converter;

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("class", "step");
        }

        protected override void OnLoad(IDotvvmRequestContext context)
        {
            converter = new StepConverter(Source);
            converter.ControlCreatedCallback = c => Children.Add(c);
            converter.RegisterParser(new SampleControlParser());
        }

        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.RenderBeginTag("div");
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.WriteUnencodedText(converter.Render(context));
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.RenderEndTag();
        }
    }
}