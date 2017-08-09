using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Controls
{
    public abstract class SourceComponentRendererBase : DotvvmControl
    {
        public virtual void SetBindings(StepRenderer renderer)
        {
        }

        public abstract void SetComponent(object component);
    }
}