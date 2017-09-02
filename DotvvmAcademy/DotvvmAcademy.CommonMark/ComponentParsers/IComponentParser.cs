using DotvvmAcademy.CommonMark.Components;

namespace DotvvmAcademy.CommonMark.ComponentParsers
{
    public abstract class ComponentParser
    {
        public ComponentParser(ComponentParser next)
        {
            Next = next;
        }

        protected ComponentParser Next { get; }

        public abstract IComponent Parse(string placeholder);
    }
}