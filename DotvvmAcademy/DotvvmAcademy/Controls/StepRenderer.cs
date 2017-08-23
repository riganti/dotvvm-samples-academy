using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.DTO.Components;
using DotvvmAcademy.BL.Facades;
using DotvvmAcademy.ViewModels;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Controls
{
    public class StepRenderer : DotvvmControl
    {
        public List<SampleViewModel> Samples
        {
            get { return (List<SampleViewModel>)GetValue(SamplesProperty); }
            set { SetValue(SamplesProperty, value); }
        }

        public static readonly DotvvmProperty SamplesProperty
            = DotvvmProperty.Register<List<SampleViewModel>, StepRenderer>(c => c.Samples, null);


        public List<SourceComponent> Source
        {
            get { return (List<SourceComponent>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DotvvmProperty SourceProperty
            = DotvvmProperty.Register<List<SourceComponent>, StepRenderer>(c => c.Source, null);

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("class", "step");
        }

        protected override void OnLoad(IDotvvmRequestContext context)
        {
            var provider = context.Configuration.ServiceLocator.GetServiceProvider();
            foreach (var component in Source)
            {
                var rendererType = typeof(SourceComponentRenderer<>).MakeGenericType(component.GetType());
                var renderer = (SourceComponentRendererBase)provider.GetService(rendererType);
                renderer.SetComponent(component);
                Children.Add(renderer);
                renderer.SetBindings(this);
            }
        }

        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.RenderBeginTag("div");
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.RenderEndTag();
        }
    }
}