using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.BL.DTO;

namespace DotvvmAcademy.Controls
{
    public class SampleControl : DotvvmControl
    {
        public SampleDTO Sample
        {
            get { return (SampleDTO)GetValue(SampleProperty); }
            set { SetValue(SampleProperty, value); }
        }
        public static readonly DotvvmProperty SampleProperty
            = DotvvmProperty.Register<SampleDTO, SampleControl>(c => c.Sample, null);


        public string ValidatorName
        {
            get { return (string)GetValue(ValidatorNameProperty); }
            set { SetValue(ValidatorNameProperty, value); }
        }

        public static readonly DotvvmProperty ValidatorNameProperty
            = DotvvmProperty.Register<string, SampleControl>(c => c.ValidatorName, null);

        protected override void OnLoad(IDotvvmRequestContext context)
        {
            var editorWrapper = new HtmlGenericControl("div");
            editorWrapper.Attributes.Add("class", "editor-wrapper");
            var ace = new AceEditor();
            ace.Language = Sample.CodeLanguage;
            var code = GetValueBinding(SampleProperty);
            editorWrapper.Children.Add(ace);
            Children.Add(editorWrapper);
            base.OnLoad(context);
        }
    }
}