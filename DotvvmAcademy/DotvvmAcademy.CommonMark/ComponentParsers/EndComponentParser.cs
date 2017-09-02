using DotvvmAcademy.CommonMark.Components;

namespace DotvvmAcademy.CommonMark.ComponentParsers
{
    internal class EndComponentParser : ComponentParser
    {
        public EndComponentParser() : base(null)
        {
        }

        public override IComponent Parse(string placeholder)
        {
            return null;
        }
    }
}