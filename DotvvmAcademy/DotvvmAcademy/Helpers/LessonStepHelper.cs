using System.IO;
using System.Xml.Linq;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps;
using DotvvmAcademy.Steps.StepsBases;
using DotvvmAcademy.Steps.Validation;
using DotvvmAcademy.Steps.Validation.Interfaces;

namespace DotvvmAcademy.Helpers
{
    public static class LessonStepHelper
    {
        private static InfoStep CreateInfoStep(this XElement step, int iterator)
        {
            var result = new InfoStep();
            result.FillStepBasicData(step, iterator);
            return result;
        }

        private static void FillStepBasicData(this StepBase stepBase, XElement step, int iterator)
        {
            //todo to resources
            stepBase.StepIndex = iterator;
            stepBase.Description = step.GetElementValueString("Description");
            stepBase.Title = step.GetElementValueString("Title");
        }

        private static void FillStepCodeData(this CodeStepBase<ICSharpCodeStepValidationObject> stepCodeBase,
            XElement step,
            int iterator)
        {
            //todo to resources
            stepCodeBase.FillStepBasicData(step, iterator);
            stepCodeBase.StartupCode = step.GetElementValueString("StartupCode");
            stepCodeBase.FinalCode = step.GetElementValueString("FinalCode");
            stepCodeBase.ShadowBoxDescription = step.GetElementValueString("ShadowBoxDescription");
        }

        private static void FillStepCodeData(this CodeStepBase<IDotHtmlCodeStepValidationObject> stepCodeBase,
            XElement step,
            int iterator)
        {
            //todo to resources
            stepCodeBase.FillStepBasicData(step, iterator);

            stepCodeBase.StartupCode = step.GetElementValueString("StartupCode");
            stepCodeBase.FinalCode = step.GetElementValueString("FinalCode");
            stepCodeBase.ShadowBoxDescription = step.GetElementValueString("ShadowBoxDescription");
        }


        private static CodeStepCsharp CreateCodeCsharpStep(this XElement step, int iterator)
        {
            var result = new CodeStepCsharp();
            result.FillStepCodeData(step, iterator);
            result.CreateValidator(step);
            return result;
        }


        private static CodeStepDotHtml CreateCodeDothtmlStep(this XElement step, int iterator)
        {
            var result = new CodeStepDotHtml();

            result.FillStepCodeData(step, iterator);
            result.CreateValidator(step);
            return result;
        }

        private static void CreateValidator(this CodeStepBase<ICSharpCodeStepValidationObject> result, XElement step)
        {
            var vp = new ValidatorProvider<ICSharpCodeStepValidationObject>();
            result.Validator = vp.CreateValidator(step.GetValidationKey(), step.GetValidatorFolder());
        }

        private static void CreateValidator(this CodeStepBase<IDotHtmlCodeStepValidationObject> result, XElement step)
        {
            var vp = new ValidatorProvider<IDotHtmlCodeStepValidationObject>();
            result.Validator = vp.CreateValidator(step.GetValidationKey(), step.GetValidatorFolder());
        }

        public static string GetValidationKey(this XElement parentElement)
        {
            //todo to resources
            return parentElement.GetElementValueString("ValidationKey");
        }

        private static string GetValidatorFolder(this XElement stepElement)
        {
            //todo to resources
            return stepElement.Parent.Parent.GetElementValueString("ValidatorFolder");
        }


        public static StepBase CreateStep(this XElement stepElement, int iterator)
        {
            //todo to resources
            if (stepElement.IsStepType("CsharpCode"))
            {
                return stepElement.CreateCodeCsharpStep(iterator);
            }
            if (stepElement.IsStepType("DothtmlCode"))
            {
                return stepElement.CreateCodeDothtmlStep(iterator);
            }

            if (stepElement.IsStepType("Info"))
            {
                return stepElement.CreateInfoStep(iterator);
            }
            throw new InvalidDataException($"Step type {stepElement.Name} ins`t supported");
        }
    }
}