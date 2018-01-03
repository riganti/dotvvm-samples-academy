using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Binding.Properties;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Controls
{
    public class StepRenderer : DotvvmControl
    {
        public static readonly DotvvmProperty ExercisesProperty
            = DotvvmProperty.Register<List<ExerciseViewModel>, StepRenderer>(c => c.Exercises, null);

        public static readonly DotvvmProperty SourceProperty
            = DotvvmProperty.Register<IStepPartDto[], StepRenderer>(c => c.Source, null);

        public List<ExerciseViewModel> Exercises
        {
            get { return (List<ExerciseViewModel>)GetValue(ExercisesProperty); }
            set { SetValue(ExercisesProperty, value); }
        }

        public IStepPartDto[] Source
        {
            get { return (IStepPartDto[])GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("class", "step");
        }

        protected override void OnLoad(IDotvvmRequestContext context)
        {
            foreach (var part in Source)
            {
                switch (part)
                {
                    case HtmlStepPartDto htmlPart:
                        Children.Add(new HtmlLiteral { Html = htmlPart.Source });
                        break;

                    case ExerciseStepPartDto exercisePart:
                        var exerciseControl = GetExerciseControl(context);
                        var index = Source.OfType<ExerciseStepPartDto>().ToList().IndexOf(exercisePart);
                        exerciseControl.SetBinding(DataContextProperty, GetExerciseBinding(index));
                        Children.Add(exerciseControl);
                        break;

                    default:
                        throw new NotSupportedException($"{nameof(StepRenderer)} does not support {part.GetType().Name}.");
                }
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

        private IValueBinding GetExerciseBinding(int index)
        {
            var exercisesBinding = GetValueBinding(ExercisesProperty);
            var expression = exercisesBinding.GetProperty<OriginalStringBindingProperty>();
            expression = new OriginalStringBindingProperty($"{expression.Code}[{index}]");
            return exercisesBinding.DeriveBinding(new object[] { expression });
        }

        private DotvvmControl GetExerciseControl(IDotvvmRequestContext context) => context.GetDotControl("Controls/ExerciseControl.dotcontrol");
    }
}