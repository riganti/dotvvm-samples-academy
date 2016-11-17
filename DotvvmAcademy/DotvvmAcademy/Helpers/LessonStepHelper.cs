using System.IO;
using System.Xml.Linq;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps;
using DotvvmAcademy.Steps.StepsBases;
using DotvvmAcademy.Steps.Validation;
using DotvvmAcademy.Steps.Validation.Validators;

namespace DotvvmAcademy.Helpers
{
    public static class LessonStepHelper
    {
        private static InfoStep CreateInfoStep(this XElement step, LessonBase currentLessonBase, int iterator)
        {
            var result = new InfoStep(currentLessonBase);
            result.FillStepBasicData(step, iterator);
            return result;
        }

        private static void FillStepBasicData(this StepBase stepBase, XElement step, int iterator)
        {
            stepBase.StepIndex = iterator;
            stepBase.Description = step.GetElementValueString("Description");
            stepBase.Title = step.GetElementValueString("Title");
        }

        private static void FillStepCodeData(this CodeStepBase<ICSharpCodeStepValidationObject> stepCodeBase, XElement step,
            int iterator)
        {
            stepCodeBase.FillStepBasicData(step, iterator);
            stepCodeBase.StartupCode = step.GetElementValueString("StartupCode");
            stepCodeBase.FinalCode = step.GetElementValueString("FinalCode");
            stepCodeBase.ShadowBoxDescription = step.GetElementValueString("ShadowBoxDescription");
        }

        private static void FillStepCodeData(this CodeStepBase<IDotHtmlCodeStepValidationObject> stepCodeBase, XElement step,
            int iterator)
        {
            stepCodeBase.FillStepBasicData(step, iterator);
            stepCodeBase.StartupCode = step.GetElementValueString("StartupCode");
            stepCodeBase.FinalCode = step.GetElementValueString("FinalCode");
            stepCodeBase.ShadowBoxDescription = step.GetElementValueString("ShadowBoxDescription");
        }


        private static CodeStepCsharp CreateCodeCsharpStep(this XElement step, LessonBase currentLessonBase, int iterator)
        {
            var result = new CodeStepCsharp(currentLessonBase);
            result.FillStepCodeData(step, iterator);
            result.CreateValidator(step);
            return result;
        }


        private static CodeStepDotHtml CreateCodeDothtmlStep(this XElement step, LessonBase currentLessonBase, int iterator)
        {
            var result = new CodeStepDotHtml(currentLessonBase);

            result.FillStepCodeData(step, iterator);
            result.CreateValidator(step);
            return result;
        }

        private static void CreateValidator(this CodeStepBase<ICSharpCodeStepValidationObject> result, XElement step)
        {
            var vp = new ValidatorProvider<ICSharpCodeStepValidationObject>();
            result.Validator = vp.CreateValidator(step.GetValidationKey());
        }

        private static void CreateValidator(this CodeStepBase<IDotHtmlCodeStepValidationObject> result, XElement step)
        {
            var vp = new ValidatorProvider<IDotHtmlCodeStepValidationObject>();
            result.Validator = vp.CreateValidator(step.GetValidationKey());
        }


        public static StepBase CreateStep(this XElement stepElement, LessonBase currentLessonBase, int iterator)
        {
            if (stepElement.IsStepType("Code"))
            {
                return stepElement.CreateCodeCsharpStep(currentLessonBase, iterator);
            }
            if (stepElement.IsStepType("Dothtml"))
            {
                return stepElement.CreateCodeDothtmlStep(currentLessonBase, iterator);
            }

            if (stepElement.IsStepType("Info"))
            {
                return stepElement.CreateInfoStep(currentLessonBase, iterator);
            }
            throw new InvalidDataException($"Step type {stepElement.Name} ins`t supported");
        }
    }
}