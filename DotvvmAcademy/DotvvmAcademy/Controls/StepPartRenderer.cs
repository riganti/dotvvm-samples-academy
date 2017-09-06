using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotvvmAcademy.BL.Dtos;

namespace DotvvmAcademy.Controls
{
    public abstract class StepPartRenderer<TStepPart> : DotvvmControl, IStepPartRenderer
        where TStepPart : IStepPartDto
    {
        public static readonly DotvvmProperty PartProperty
            = DotvvmProperty.Register<TStepPart, StepPartRenderer<TStepPart>>(c => c.Part);

        public TStepPart Part
        {
            get { return (TStepPart)GetValue(PartProperty); }
            set { SetValue(PartProperty, value); }
        }

        public virtual void SetBindings(StepRenderer renderer)
        {
        }

        public void SetPart(IStepPartDto part)
        {
            Part = (TStepPart)part;
        }
    }
}