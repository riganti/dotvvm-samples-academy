using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.BL.DTO.Components;

namespace DotvvmAcademy.Controls
{
    public class HtmlLiteralComponentRenderer : SourceComponentRenderer<HtmlLiteralComponent>
    {
        protected override void RenderControl(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.WriteUnencodedText(Component.Source);
        }
    }
}