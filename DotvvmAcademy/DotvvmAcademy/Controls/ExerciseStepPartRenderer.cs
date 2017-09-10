using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.Javascript;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.ViewModels;
using System.Linq;

namespace DotvvmAcademy.Controls
{
    public abstract class ExerciseStepPartRenderer<TExerciseStepPartDto> : StepPartRenderer<TExerciseStepPartDto>
        where TExerciseStepPartDto : class, IExerciseStepPartDto
    {
        private AceEditor ace;
        private DotvvmControl buttons;
        private DotvvmControl errorList;
        private int uniqueIdIndex = 0;

        public ExerciseViewModel Exercise
        {
            get { return (ExerciseViewModel)GetValue(ExerciseProperty); }
            set { SetValue(ExerciseProperty, value); }
        }

        public static readonly DotvvmProperty ExerciseProperty
            = DotvvmProperty.Register<ExerciseViewModel, ExerciseStepPartRenderer<TExerciseStepPartDto>>(c => c.Exercise, null);

        public override void SetBindings(StepRenderer renderer)
        {
            var exercise = renderer.Exercises.Single(e => e.Dto == Part);
            var sampleIndex = renderer.Exercises.IndexOf(exercise);
            SetBinding(ExerciseProperty, GetExerciseBinding(renderer, sampleIndex));
            ace.SetBinding(AceEditor.CodeProperty, GetCodeBinding());
            ace.Language = Exercise.Dto.CodeLanguage;
            buttons.SetBinding(DataContextProperty, GetBinding(ExerciseProperty));
            errorList.SetBinding(DataContextProperty, GetBinding(ExerciseProperty));
        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            var editorWrapper = new HtmlGenericControl("div");
            editorWrapper.Attributes.Add("class", "editor-wrapper");
            ace = new AceEditor();
            editorWrapper.Children.Add(ace);
            Children.Add(editorWrapper);
            errorList = GetErrorList(context);
            Children.Add(errorList);
            buttons = GetCodeEditorButtons(context);
            Children.Add(buttons);

            base.OnInit(context);
        }

        protected override void OnLoad(IDotvvmRequestContext context)
        {
            base.OnLoad(context);
            if (!context.IsPostBack)
            {
                Exercise.ResetCode().Wait();
            }
        }

        private IValueBinding GetCodeBinding()
        {
            return new ValueBindingExpression(new CompiledBindingExpression
            {
                Delegate = (h, c) => Exercise.Code,
                Javascript = $"{GetValueBinding(ExerciseProperty).GetKnockoutBindingExpression()}().{nameof(Exercise.Code)}"
            });
        }

        private DotvvmControl GetCodeEditorButtons(IDotvvmRequestContext context) => GetDotControl(context, "Controls/CodeEditorButtons.dotcontrol");

        private DotvvmControl GetDotControl(IDotvvmRequestContext context, string path)
        {
            var controlBuilderFactory = context.Configuration.ServiceLocator.GetService<IControlBuilderFactory>();
            var builder = controlBuilderFactory.GetControlBuilder(path);
            var control = builder.BuildControl(controlBuilderFactory);
            control.SetValue(Internal.UniqueIDProperty, GetUniqueID());
            return control;
        }

        private DotvvmControl GetErrorList(IDotvvmRequestContext context) => GetDotControl(context, "Controls/CodeEditorErrorList.dotcontrol");

        private IValueBinding GetExerciseBinding(StepRenderer renderer, int index)
        {
            return new ValueBindingExpression(new CompiledBindingExpression
            {
                Delegate = (h, c) => renderer.Exercises[index],
                Javascript = JavascriptCompilationHelper
                .AddIndexerToViewModel(renderer.GetValueBinding(StepRenderer.ExercisesProperty).GetKnockoutBindingExpression(), index)
            });
        }

        private string GetUniqueID()
        {
            var id = "d" + uniqueIdIndex;
            uniqueIdIndex++;
            return id;
        }
    }
}