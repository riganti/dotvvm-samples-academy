using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.Javascript;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.BL.DTO.Components;
using DotvvmAcademy.Models;
using System.Linq;

namespace DotvvmAcademy.Controls
{
    public class SampleComponentRenderer : SourceComponentRenderer<SampleComponent>
    {
        private AceEditor ace;
        private DotvvmControl buttons;
        private DotvvmControl errorList;
        private int uniqueIdIndex = 0;

        public Sample Sample
        {
            get { return (Sample)GetValue(SampleProperty); }
            set { SetValue(SampleProperty, value); }
        }

        public static readonly DotvvmProperty SampleProperty
            = DotvvmProperty.Register<Sample, SampleComponentRenderer>(c => c.Sample, null);

        public override void SetBindings(StepRenderer renderer)
        {
            var sampleIndex = renderer.Source.OfType<SampleComponent>().ToList().IndexOf(Component);
            SetBinding(SampleProperty, GetSampleBinding(renderer, sampleIndex));
            ace.SetBinding(AceEditor.CodeProperty, GetCodeBinding());
            ace.Language = Sample.DTO.CodeLanguage;
            buttons.SetBinding(DataContextProperty, GetBinding(SampleProperty));
            errorList.SetBinding(DataContextProperty, GetBinding(SampleProperty));
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
                Sample.ResetCode();
            }
        }

        private IValueBinding GetCodeBinding()
        {
            return new ValueBindingExpression(new CompiledBindingExpression
            {
                Delegate = (h, c) => Sample.Code,
                Javascript = $"{GetValueBinding(SampleProperty).GetKnockoutBindingExpression()}().{nameof(Sample.Code)}"
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

        private IValueBinding GetSampleBinding(StepRenderer renderer, int index)
        {
            return new ValueBindingExpression(new CompiledBindingExpression
            {
                Delegate = (h, c) => renderer.Samples[index],
                Javascript = JavascriptCompilationHelper
                .AddIndexerToViewModel(renderer.GetValueBinding(StepRenderer.SamplesProperty).GetKnockoutBindingExpression(), index)
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