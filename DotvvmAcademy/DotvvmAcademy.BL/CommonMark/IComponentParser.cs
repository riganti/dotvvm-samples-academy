using DotvvmAcademy.BL.DTO.Components;
using System.Xml.Linq;

namespace DotvvmAcademy.BL.CommonMark
{
    public interface IComponentParser<out TComponent> where TComponent : SourceComponent
    {
        bool CanParse(XElement element);

        TComponent Parse(XElement element, int lessonIndex, string language, int stepIndex);
    }
}