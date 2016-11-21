using System;
using System.IO;
using System.Xml.Linq;
using DotvvmAcademy.LessonXmlParser;
using DotvvmAcademy.Steps.StepsBases.Interfaces;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators;

namespace DotvvmAcademy.Steps.StepBuilder
{
    public class StepBuilder
    {
        public IStep CreateStep(XElement stepElement, int index)
        {
            var stepType = stepElement.GetAttribute(LessonXmlElements.StepTypeAttribute).Value;

            if (stepType == LessonXmlElements.StepTypeCsharpCode)
            {
                return CreateCodeCsharpStep(stepElement, index);
            }
            if (stepType == LessonXmlElements.StepTypeDothtmlCode)
            {
                return CreateCodeDothtmlStep(stepElement, index);
            }

            if (stepType == LessonXmlElements.StepTypeInfo)
            {
                return CreateInfoStep(stepElement, index);
            }
            throw new InvalidDataException(string.Format(ValidationErrorMessages.StepTypeExpected, stepElement.Name));
        }


        private CodeStepCsharp CreateCodeCsharpStep(XElement stepElement, int index)
        {
            var csharpStep = new CodeStepCsharp();
            FillStepCodeData(csharpStep, stepElement, index);
            CreateValidator(csharpStep, stepElement);
            FillAdditionalCsharpCodeData(csharpStep, stepElement);
            return csharpStep;
        }


        private CodeStepDotHtml CreateCodeDothtmlStep(XElement stepElement, int index)
        {
            var dothtmlStep = new CodeStepDotHtml();
            FillStepCodeData(dothtmlStep, stepElement, index);
            CreateValidator(dothtmlStep, stepElement);
            return dothtmlStep;
        }

        private InfoStep CreateInfoStep(XElement step, int index)
        {
            var infoStep = new InfoStep();
            FillStepBasicData(infoStep, step, index);
            return infoStep;
        }


        private void FillStepBasicData(IStep stepBase, XElement stepElement, int index)
        {
            stepBase.StepIndex = index;
            stepBase.Description = stepElement.GetChildElementStringValue(LessonXmlElements.DescriptionElement);
            stepBase.Title = stepElement.GetChildElementStringValue(LessonXmlElements.TitleElement);
        }

        private void FillStepCodeData(ICodeStepData stepCodeBase, XElement stepElement, int index)
        {
            FillStepBasicData(stepCodeBase, stepElement, index);
            stepCodeBase.StartupCode = stepElement.GetChildElementStringValue(LessonXmlElements.StartupCodeElement);
            stepCodeBase.FinalCode = stepElement.GetChildElementStringValue(LessonXmlElements.FinalCodeElement);
            if (stepElement.HaveChildElement(LessonXmlElements.ShadowBoxDescriptionElement))
            {
                stepCodeBase.ShadowBoxDescription =
                    stepElement.GetChildElementStringValue(LessonXmlElements.ShadowBoxDescriptionElement);
            }
        }

        private void FillAdditionalCsharpCodeData(CodeStepCsharp csharpCode, XElement stepElement)
        {
            Action<CodeStepCsharp, string> addAction;

            if (stepElement.HaveChildElement(LessonXmlElements.CodeDependenciesElement))
            {
                addAction = (code, at) => code.OtherCodeDependencies.Add(at);
                FillElementChildrenCollection(csharpCode, stepElement, LessonXmlElements.CodeDependenciesElement,
                    LessonXmlElements.CodeDependencyElement, addAction);
            }

            if (stepElement.HaveChildElement(LessonXmlElements.AllowedTypesConstructedElement))
            {
                addAction = (code, at) => code.AllowedTypesConstructed.Add(at);
                FillElementChildrenCollection(csharpCode, stepElement, LessonXmlElements.AllowedTypesConstructedElement,
                    LessonXmlElements.AllowedTypeElement, addAction);
            }

            if (stepElement.HaveChildElement(LessonXmlElements.AllowedMethodsCalledElement))
            {
                addAction = (code, at) => code.AllowedMethodsCalled.Add(at);
                FillElementChildrenCollection(csharpCode, stepElement, LessonXmlElements.AllowedMethodsCalledElement,
                    LessonXmlElements.AllowedMethodElement, addAction);
            }
        }


        private static void FillElementChildrenCollection(CodeStepCsharp csharpCode, XElement parentStepElement,
            string elementWithCollectionName, string itemElementName, Action<CodeStepCsharp, string> addAction)
        {
            var otherCodeDependencies = parentStepElement.GetChildCollection(elementWithCollectionName, itemElementName);

            foreach (var dependency in otherCodeDependencies)
            {
                addAction.Invoke(csharpCode, dependency.GetElementStringValue());
            }
        }


        private void CreateValidator<T>(ICodeStep<T> result, XElement stepElement)
            where T : ILessonValidationObject
        {
            var provider = new ValidatorProvider<T>();
            var validationKey = stepElement.GetChildElementStringValue(LessonXmlElements.ValidationKeyElement);
            var validationNamespace =
                stepElement.Parent.Parent.GetChildElementStringValue(LessonXmlElements.ValidatorsNamespaceElement);
            result.Validator = provider.CreateValidator(validationKey, validationNamespace);
        }
    }
}