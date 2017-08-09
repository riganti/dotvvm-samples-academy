using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotvvmAcademy.BL.DTO.Components;

namespace DotvvmAcademy.Controls
{
    public abstract class SourceComponentRenderer<TComponent> : SourceComponentRendererBase
        where TComponent : SourceComponent
    {
        public TComponent Component
        {
            get { return (TComponent)GetValue(ComponentProperty); }
            set { SetValue(ComponentProperty, value); }
        }

        public static readonly DotvvmProperty ComponentProperty
            = DotvvmProperty.Register<TComponent, SourceComponentRenderer<TComponent>>(c => c.Component, null);

        public override void SetComponent(object component)
        {
            Component = (TComponent)component;
        }
    }
}