using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.BL.Dtos;

namespace DotvvmAcademy.Controls
{
    public class HtmlStepPartRenderer : StepPartRenderer<HtmlStepPartDto>
    {
        protected override void RenderControl(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.WriteUnencodedText(Part.Source);
        }
    }
}