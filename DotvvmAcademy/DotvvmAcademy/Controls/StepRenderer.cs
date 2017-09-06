using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.ViewModels;
using System.Collections.Generic;

namespace DotvvmAcademy.Controls
{
    public class StepRenderer : DotvvmControl
    {
        public List<ExerciseViewModel> Exercises
        {
            get { return (List<ExerciseViewModel>)GetValue(ExercisesProperty); }
            set { SetValue(ExercisesProperty, value); }
        }

        public static readonly DotvvmProperty ExercisesProperty
            = DotvvmProperty.Register<List<ExerciseViewModel>, StepRenderer>(c => c.Exercises, null);

        public IStepPartDto[] Source
        {
            get { return (IStepPartDto[])GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DotvvmProperty SourceProperty
            = DotvvmProperty.Register<IStepPartDto[], StepRenderer>(c => c.Source, null);

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("class", "step");
        }

        protected override void OnLoad(IDotvvmRequestContext context)
        {
            var provider = context.Configuration.ServiceLocator.GetServiceProvider();
            foreach (var part in Source)
            {
                var rendererType = typeof(StepPartRenderer<>).MakeGenericType(part.GetType());
                var renderer = (IStepPartRenderer)provider.GetService(rendererType);
                renderer.SetPart(part);
                Children.Add((DotvvmControl)renderer);
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