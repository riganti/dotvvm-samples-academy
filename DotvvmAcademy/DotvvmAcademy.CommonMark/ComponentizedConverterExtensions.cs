using DotvvmAcademy.CommonMark.ComponentParsers;
using DotvvmAcademy.CommonMark.Components;
using DotvvmAcademy.CommonMark.Components.Mvvm;

namespace DotvvmAcademy.CommonMark
{
    public static class ComponentizedConverterExtensions
    {
        public static void UseDefaultXmlParsers(this ComponentizedConverter converter)
        {
            converter.Use<XmlComponentParser<CSharpSampleComponent>>();
            converter.Use<XmlComponentParser<DothtmlSampleComponent>>();
            converter.Use<XmlComponentParser<MvvmSampleComponent>>();
        }
    }
}