using System.Collections.Generic;
using DotvvmAcademy.Lessons;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Steps
{
    public abstract class CodeBaseStep : StepBase, ICodeEditorStep
    {
        public string Code { get; set; } = "";


        [Bind(Direction.None)]
        public string StartupCode { get; set; }

        [Bind(Direction.None)]
        public string FinalCode { get; set; }

        public string ShadowBoxDescription { get; internal set; }

        //todo ICodeEditorStep
        public CodeBaseStep(LessonBase currentLesson) : base(currentLesson)
        {
        }

        public void ResetCode()
        {
            Code = StartupCode;
        }

        public void ShowCorrectCode()
        {
            Code = FinalCode;
        }
    }
}