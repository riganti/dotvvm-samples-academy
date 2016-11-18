using DotvvmAcademy.Lessons;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Steps.StepsBases
{
    public abstract class CodeStepBase<TValidationObject> : StepBase, ICodeStepBase
    {

        public string Code { get; set; } = "";


        [Bind(Direction.None)]
        public string StartupCode { get; set; }

        [Bind(Direction.None)]
        public string FinalCode { get; set; }

        public string ShadowBoxDescription { get; internal set; }
        public abstract TValidationObject Validator { get; set; }


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