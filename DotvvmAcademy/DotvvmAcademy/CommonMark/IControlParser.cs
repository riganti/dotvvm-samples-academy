using DotVVM.Framework.Controls;
using System.Xml.Linq;

namespace DotvvmAcademy.CommonMark
{
    public interface IControlParser<out TControl> where TControl : DotvvmControl
    {
        bool CanParse(XElement element);

        TControl Parse(XElement element);
    }
}