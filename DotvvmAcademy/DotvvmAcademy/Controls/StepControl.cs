using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.Facades;
using System.Collections.Generic;

namespace DotvvmAcademy.Controls
{
    public class StepControl : DotvvmControl
    {

        public int LessonIndex
        {
            get { return (int)GetValue(LessonIndexProperty); }
            set { SetValue(LessonIndexProperty, value); }
        }
        public static readonly DotvvmProperty LessonIndexProperty
            = DotvvmProperty.Register<int, StepControl>(c => c.LessonIndex, -1);

        public string LessonLanguage
        {
            get { return (string)GetValue(LessonLanguageProperty); }
            set { SetValue(LessonLanguageProperty, value); }
        }
        public static readonly DotvvmProperty LessonLanguageProperty
            = DotvvmProperty.Register<string, StepControl>(c => c.LessonLanguage, null);


        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DotvvmProperty SourceProperty
            = DotvvmProperty.Register<string, StepControl>(c => c.Source, null);

        public int StepIndex
        {
            get { return (int)GetValue(StepIndexProperty); }
            set { SetValue(StepIndexProperty, value); }
        }
        public static readonly DotvvmProperty StepIndexProperty
            = DotvvmProperty.Register<int, StepControl>(c => c.StepIndex, -1);

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("class", "step");
        }

        protected override void OnLoad(IDotvvmRequestContext context)
        {
            //var sampleFacade = context.Configuration.ServiceLocator.GetService<SampleFacade>();
            //converter = new StepConverter(Source);
            //converter.ControlCreatedCallback = c => Children.Add(c);
            //converter.RegisterParser(new SampleControlParser((correct, incorrect) =>
            //{
            //    return sampleFacade.GetSample(LessonIndex, LessonLanguage, StepIndex, correct, incorrect);
            //}));
        }

        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.RenderBeginTag("div");
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            //writer.WriteUnencodedText(converter.Render(context));
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.RenderEndTag();
        }
    }
}