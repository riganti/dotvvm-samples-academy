using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.DTO.Components;
using System.Linq;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.Javascript;

namespace DotvvmAcademy.Controls
{
    public class SampleComponentRenderer : SourceComponentRenderer<SampleComponent>
    {
        private AceEditor ace;

        public SampleDTO Sample
        {
            get { return (SampleDTO)GetValue(SampleProperty); }
            set { SetValue(SampleProperty, value); }
        }
        public static readonly DotvvmProperty SampleProperty
            = DotvvmProperty.Register<SampleDTO, SampleComponentRenderer>(c => c.Sample, null);

        protected override void OnInit(IDotvvmRequestContext context)
        {
            var editorWrapper = new HtmlGenericControl("div");
            editorWrapper.Attributes.Add("class", "editor-wrapper");
            ace = new AceEditor();
            editorWrapper.Children.Add(ace);
            Children.Add(editorWrapper);

            base.OnInit(context);
        }

        public override void SetBindings(StepRenderer renderer)
        {
            var sampleIndex = renderer.Source.OfType<SampleComponent>().ToList().IndexOf(Component);
            SetBinding(SampleProperty, GetSampleBinding(renderer, sampleIndex));
            ace.SetBinding(AceEditor.CodeProperty, GetCodeBinding());
            ace.Language = Sample.CodeLanguage;

            Sample.Code = Sample.IncorrectCode;
        }

        private IValueBinding GetSampleBinding(StepRenderer renderer, int index)
        {
            return new ValueBindingExpression(new CompiledBindingExpression
            {
                Delegate = (h, c) => renderer.Samples[index],
                Javascript = JavascriptCompilationHelper.AddIndexerToViewModel(renderer.GetValueBinding(StepRenderer.SamplesProperty).GetKnockoutBindingExpression(), index)

            });
        }

        private IValueBinding GetCodeBinding()
        {
            return new ValueBindingExpression(new CompiledBindingExpression
            {
                Delegate = (h, c) => Sample.Code,
                Javascript = $"{GetValueBinding(SampleProperty).GetKnockoutBindingExpression()}().{nameof(SampleDTO.Code)}()"
            });
        }
    }
}